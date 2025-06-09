using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace DgView;

/// <summary>
/// 프로그램의 전역 설정을 관리하는 정적 클래스입니다.
/// 윈도우 위치, 뷰 모드, 최근 파일, 보안 등 다양한 설정을 저장하고 불러옵니다.
/// </summary>
internal static class Configs
{
	public record MoveLocation(int No, string Alias, string Path);

	// 설정 
	private static string s_app_path = string.Empty;
	private static string s_config_data_source = string.Empty;
	private static readonly DateTime s_launched = DateTime.Now;

	// 캐시 할 데이터
	private static long s_run_count;
	private static long s_run_duration;

	private static int s_window_width = 600;
	private static int s_window_height = 400;

	private static bool s_general_run_once = true;
	private static bool s_general_esc_exit = true;
	private static bool s_general_confirm_when_delete_file = true;
	private static int s_general_max_page_cache = 230; // 1048576곱해야함

	private static string s_general_external_run = string.Empty;
	private static bool s_general_reload_after_external = true;

	private static bool s_mouse_double_click_fullscreen;
	private static bool s_mouse_click_paging;

	private static bool s_view_zoom = true;
	private static ViewMode s_view_mode = ViewMode.Fit;
	private static ViewQuality s_view_quality = ViewQuality.Default;

	private static bool s_sec_use_pass;
	private static bool s_sec_unlock_pass;
	private static string s_sec_pass_code = string.Empty;
	private static string s_sec_pass_usages = string.Empty;

	private static readonly Dictionary<string, string> s_caches = [];
	private static readonly List<MoveLocation> s_move_locations = [];

	// 최근 파일 목록
	private static readonly List<string> s_near_files = [];
	private static string s_near_path = string.Empty;

	/// <summary>
	/// 설정을 초기화합니다.
	/// </summary>
	/// <returns>초기화에 성공하면 참, 아니면 거짓을 반환합니다.</returns>
	public static bool Initialize()
	{
		s_app_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ksh");
		if (!Directory.Exists(s_app_path))
			Directory.CreateDirectory(s_app_path);
		s_config_data_source = $"Data Source={Path.Combine(s_app_path, "DgView.conf")}";

		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();

		try
		{
			if (!conn.ExecuteStatement("CREATE TABLE IF NOT EXISTS configs (key TEXT PRIMARY KEY, value TEXT);") ||
				!conn.ExecuteStatement("CREATE TABLE IF NOT EXISTS moves (no INTEGER PRIMARY KEY, alias TEXT, folder TEXT);") ||
				!conn.ExecuteStatement("CREATE TABLE IF NOT EXISTS recently (page INTEGER, filename TEXT PRIMARY KEY);"))
			{
				Doumi.MessageDialog(null, "데이터 베이스를 만들 수 없어요!");
				return false;
			}
		}
		catch (Exception e)
		{
			Doumi.MessageDialog(null, $"설정 파일에 접근할 수 없어요!{Environment.NewLine}{Environment.NewLine}{e.Message}");
			return false;
		}

		s_run_count = conn.SelectConfigsAsLong("RunCount") + 1;
		s_run_duration = conn.SelectConfigsAsLong("RunDuration");
		s_general_run_once = conn.SelectConfigsAsBool("GeneralRunOnce", s_general_run_once);

		return true;
	}

	/// <summary>
	/// 리소스를 해제하고 애플리케이션 실행 통계를 설정 데이터베이스에 저장합니다.
	/// </summary>
	/// <remarks>
	/// 이 메서드는 애플리케이션의 실행 시간을 계산하여 전체 실행 횟수와 실행 누적 시간을 설정 데이터베이스에 갱신합니다.
	/// 애플리케이션이 종료될 때 호출하여 실행 통계가 저장되도록 해야 합니다.
	/// </remarks>
	public static void Dispose()
	{
		var now = DateTime.Now;
		var delta = now - s_launched;
		s_run_duration += (long)delta.TotalSeconds;

		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();
		using var transaction = conn.BeginTransaction();
		conn.IntoConfigs("WindowWidth", s_window_width);
		conn.IntoConfigs("WindowHeight", s_window_height);
		conn.IntoConfigs("RunCount", s_run_count);
		conn.IntoConfigs("RunDuration", s_run_duration);
		transaction.Commit();
	}

	/// <summary>
	/// 캐시할 데이터를 읽어옵니다.
	/// </summary>
	public static void LoadCaches()
	{
		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();

		// 설정
		s_window_width = conn.SelectConfigsAsInt("WindowWidth", s_window_width);
		s_window_height = conn.SelectConfigsAsInt("WindowHeight", s_window_height);

		s_general_esc_exit = conn.SelectConfigsAsBool("GeneralEscExit", s_general_esc_exit);
		s_general_confirm_when_delete_file = conn.SelectConfigsAsBool("GeneralConfirmDelete", s_general_confirm_when_delete_file);
		s_general_max_page_cache = conn.SelectConfigsAsInt("GeneralMaxPageCache", s_general_max_page_cache);
		s_general_external_run = conn.SelectConfigs("GeneralExternalRun") ?? s_general_external_run;
		s_general_reload_after_external = conn.SelectConfigsAsBool("GeneralReloadAfterExternalExit", s_general_reload_after_external);

		s_mouse_double_click_fullscreen = conn.SelectConfigsAsBool("MouseDoubleClickFullScreen", s_mouse_double_click_fullscreen);
		s_mouse_click_paging = conn.SelectConfigsAsBool("MouseUseClickPaging", s_mouse_click_paging);

		s_view_zoom = conn.SelectConfigsAsBool("ViewZoom", s_view_zoom);
		s_view_mode = (ViewMode)conn.SelectConfigsAsInt("ViewMode", (int)s_view_mode);
		s_view_quality = (ViewQuality)conn.SelectConfigsAsInt("ViewQuality", (int)s_view_quality);

		s_sec_use_pass = conn.SelectConfigsAsBool("SecurityUsePass", s_sec_use_pass);
		s_sec_unlock_pass = conn.SelectConfigsAsBool("SecurityUnlockPass", s_sec_unlock_pass);
		s_sec_pass_code = conn.SelectConfigs("SecurityPassCode") ?? s_sec_pass_code;
		s_sec_pass_usages = conn.SelectConfigs("SecurityPassUsage") ?? s_sec_pass_usages;

		// 이동 디렉토리
		var mCmd = conn.CreateCommand();
		mCmd.CommandText = "SELECT no, alias, folder FROM moves ORDER BY no;";
		using var mRdr = mCmd.ExecuteReader();
		s_move_locations.Clear();
		while (mRdr.Read())
		{
			var no = mRdr.GetInt32(0);
			var alias = mRdr.GetString(1);
			var path = mRdr.GetString(2);
			s_move_locations.Add(new MoveLocation(no, alias, path));
		}
	}

	#region SQL 도우미
	private static bool ExecuteStatement(this SqliteConnection conn, string query)
	{
		var cmd = conn.CreateCommand();
		cmd.CommandText = query;
		try
		{
			cmd.ExecuteNonQuery();
			return true;
		}
		catch
		{
			return false;
		}
	}

	private static string? SelectConfigs(this SqliteConnection conn, string key, string? defaultValue = null)
	{
		var cmd = conn.CreateCommand();
		cmd.CommandText = "SELECT value FROM configs WHERE key = @key LIMIT 1;";
		cmd.Parameters.AddWithValue("@key", key);
		using var rdr = cmd.ExecuteReader();
		return rdr.Read() ? rdr.GetString(0) : defaultValue;
	}

	private static int SelectConfigsAsInt(this SqliteConnection conn, string key, int defaultValue = 0) =>
		Alter.ToInt(conn.SelectConfigs(key), defaultValue);

	private static long SelectConfigsAsLong(this SqliteConnection conn, string key, long defaultValue = 0) =>
		Alter.ToLong(conn.SelectConfigs(key), defaultValue);

	private static bool SelectConfigsAsBool(this SqliteConnection conn, string key, bool defaultValue = false) =>
		Alter.ToBool(conn.SelectConfigs(key), defaultValue);

	private static void IntoConfigs(this SqliteConnection conn, string key, string value)
	{
		var cmd = conn.CreateCommand();
		cmd.CommandText = "INSERT OR REPLACE INTO configs (key, value) VALUES (@key, @value);";
		cmd.Parameters.AddWithValue("@key", key);
		cmd.Parameters.AddWithValue("@value", value);
		try
		{
			cmd.ExecuteNonQuery();
		}
		catch (Exception e)
		{
#if DEBUG
			System.Diagnostics.Debug.WriteLine($"Failed to set config: {key} = {value}");
			System.Diagnostics.Debug.WriteLine($" -> {e.Message}");
#endif
		}
	}

	private static void IntoConfigs(this SqliteConnection conn, string key, int value) =>
		conn.IntoConfigs(key, value.ToString());

	private static void IntoConfigs(this SqliteConnection conn, string key, long value) =>
		conn.IntoConfigs(key, value.ToString());

	private static string? SqlGetConfig(string key, string? defaultValue = null)
	{
		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();
		return conn.SelectConfigs(key, defaultValue);
	}

	private static void SqlSetConfig(string key, string value)
	{
		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();
		conn.IntoConfigs(key, value);
	}

	private static void SqlSetConfig(string key, int value) =>
		SqlSetConfig(key, value.ToString());

	private static void SqlSetConfig(string key, bool value) =>
		SqlSetConfig(key, value ? "true" : "false");
	#endregion

	#region 속성: 윈도우
	// 윈도우의 너비. 프로그램 종료 전에 저장하므로 캐시 입출력
	public static int WindowWidth
	{
		get => s_window_width;
		set => s_window_width = value;
	}

	// 윈도우의 높이. 프로그램 종료 전에 저장하므로 캐시 입출력
	public static int WindowHeight
	{
		get => s_window_height;
		set => s_window_height = value;
	}
	#endregion

	#region 속성: 파일
	/// <summary>
	/// 마지막으로 열었던 폴더 경로를 가져오거나 설정합니다.
	/// </summary>
	public static string LastFolder
	{
		get
		{
			var s = SqlGetConfig("FileLastFolder") ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			return (s_caches["FileLastFolder"] = s);
		}
		set
		{
			var s = s_caches.GetValueOrDefault("FileLastFolder");
			if (s != null && value.Equals(s))
				return;
			SqlSetConfig("FileLastFolder", s_caches["FileLastFolder"] = value);
		}
	}

	/// <summary>
	/// 마지막으로 열었던 파일 이름을 가져오거나 설정합니다.
	/// </summary>
	public static string LastFileName
	{
		get
		{
			var s = SqlGetConfig("FileLastFileName") ?? string.Empty;
			return (s_caches["FileLastFileName"] = s);
		}
		set
		{
			var s = s_caches.GetValueOrDefault("FileLastFileName");
			if (s != null && value.Equals(s))
				return;
			SqlSetConfig("FileLastFileName", s_caches["FileLastFileName"] = value);
		}
	}

	/// <summary>
	/// 기억해둘 파일 이름을 가져오거나 설정합니다.
	/// </summary>
	public static string RememberFileName
	{
		get
		{
			var s = SqlGetConfig("FileRemember") ?? string.Empty;
			return (s_caches["FileRemember"] = s);
		}
		set
		{
			var s = s_caches.GetValueOrDefault("FileRemember");
			if (s != null && value.Equals(s))
				return;
			SqlSetConfig("FileRemember", s_caches["FileRemember"] = value);
		}
	}
	#endregion

	#region 속성: 기본

	public static bool GeneralRunOnce
	{
		get => s_general_run_once;
		set
		{
			if (value == s_general_run_once)
				return;
			SqlSetConfig("GeneralRunOnce", s_general_run_once = value);
		}
	}

	/// <summary>
	/// ESC 키로 프로그램을 종료할지 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool GeneralEscExit
	{
		get => s_general_esc_exit;
		set
		{
			if (value == s_general_esc_exit)
				return;
			SqlSetConfig("GeneralEscExit", s_general_esc_exit = value);
		}
	}

	/// <summary>
	/// 파일 삭제 시 확인 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool GeneralConfirmDelete
	{
		get => s_general_confirm_when_delete_file;
		set
		{
			if (value == s_general_confirm_when_delete_file)
				return;
			SqlSetConfig("GeneralConfirmDelete", s_general_confirm_when_delete_file = value);
		}
	}

	/// <summary>
	/// 최대 페이지 캐시 크기(바이트)를 반환합니다.
	/// </summary>
	public static long GeneralMaxActualPageCache => s_general_max_page_cache * 1048576L;

	/// <summary>
	/// 최대 페이지 캐시 크기(MB)를 가져오거나 설정합니다.
	/// </summary>
	public static int GeneralMaxPageCache
	{
		get => s_general_max_page_cache;
		set
		{
			if (value == s_general_max_page_cache)
				return;
			SqlSetConfig("GeneralMaxPageCache", s_general_max_page_cache = value);
		}
	}

	/// <summary>
	/// 외부 실행 파일 경로를 가져오거나 설정합니다.
	/// </summary>
	public static string GeneralExternalRun
	{
		get => s_general_external_run;
		set
		{
			if (value.Equals(s_general_external_run))
				return;
			SqlSetConfig("GeneralExternalRun", s_general_external_run = value);
		}
	}

	/// <summary>
	/// 외부 실행 후 책을 다시 열지 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool GeneralReloadAfterExternalExit
	{
		get => s_general_reload_after_external;
		set
		{
			if (value == s_general_reload_after_external)
				return;
			SqlSetConfig("GeneralReloadAfterExternalExit", s_general_reload_after_external = value);
		}
	}

	/// 실행 횟수를 가져옵니다.
	public static long RunCount => s_run_count;

	// 실행 누적 시간을 가져옵니다.
	public static long RunDuration => s_run_duration;
	#endregion

	#region 속성: 마우스
	/// <summary>
	/// 두 번 클릭 상태 사용 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool MouseDoubleClickFullScreen
	{
		get => s_mouse_double_click_fullscreen;
		set
		{
			if (value == s_mouse_double_click_fullscreen)
				return;
			SqlSetConfig("MouseDoubleClickFullScreen", s_mouse_double_click_fullscreen = value);
		}
	}

	/// <summary>
	/// 마우스 버튼으로 페이지 이동 사용 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool MouseUseClickPaging
	{
		get => s_mouse_click_paging;
		set
		{
			if (value == s_mouse_click_paging)
				return;
			SqlSetConfig("MouseUseClickPaging", s_mouse_click_paging = value);
		}
	}
	#endregion

	#region 속성: 보기
	/// <summary>
	/// 확대 사용 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool ViewZoom
	{
		get => s_view_zoom;
		set
		{
			if (value == s_view_zoom)
				return;
			SqlSetConfig("ViewZoom", s_view_zoom = value);
		}
	}

	/// <summary>
	/// 현재 뷰 모드를 가져오거나 설정합니다.
	/// </summary>
	public static ViewMode ViewMode
	{
		get => s_view_mode;
		set
		{
			if (value == s_view_mode)
				return;
			SqlSetConfig("ViewMode", (int)(s_view_mode = value));
		}
	}

	/// <summary>
	/// 현재 뷰 품질을 가져오거나 설정합니다.
	/// </summary>
	public static ViewQuality ViewQuality
	{
		get => s_view_quality;
		set
		{
			if (value == s_view_quality)
				return;
			SqlSetConfig("ViewQuality", (int)(s_view_quality = value));
		}
	}
	#endregion

	#region 속성 + 기능: 보안
	/// <summary>
	/// 비밀번호 사용 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool SecurityUsePass
	{
		get => s_sec_use_pass;
		set
		{
			if (value == s_sec_use_pass)
				return;
			SqlSetConfig("SecurityUsePass", s_sec_use_pass = value);
		}
	}

	/// <summary>
	/// 비밀번호가 해제되었는지 여부를 가져오거나 설정합니다.
	/// </summary>
	public static bool SecurityUnlockPass
	{
		get => s_sec_unlock_pass;
		set
		{
			if (value == s_sec_unlock_pass)
				return;
			SqlSetConfig("SecurityUnlockPass", s_sec_unlock_pass = value);
		}
	}

	/// <summary>
	/// 비밀번호를 가져오거나 설정합니다.
	/// </summary>
	public static string SecurityPassCode
	{
		get => Alter.DecompressString(s_sec_pass_code) ?? string.Empty;
		set
		{
			var pw = value.Length > 0 ? Alter.CompressString(value) ?? string.Empty : string.Empty;
			if (s_sec_pass_code.Equals(pw))
				return;
			SqlSetConfig("SecurityPassCode", s_sec_pass_code = pw);
		}
	}

	/// <summary>
	/// 비밀번호 사용처 배열을 반환합니다.
	/// </summary>
	public static PassCodeUsage[] PassUsageGetArray()
	{
		var ss = s_sec_pass_usages.Split(',');
		if (ss.Length == 0)
			return [];

		List<PassCodeUsage> l = [];
		foreach (var s in ss)
		{
			if (Enum.TryParse<PassCodeUsage>(s, out var u) && !l.Contains(u))
				l.Add(u);
		}

		return l.ToArray();
	}

	/// <summary>
	/// 특정 사용처에 대해 비밀번호가 필요한지 검사합니다.
	/// </summary>
	/// <param name="usage">사용처</param>
	public static bool PassUsageTest(PassCodeUsage usage) =>
		s_sec_pass_usages.Contains(usage.ToString());

	/// <summary>
	/// 특정 사용처 문자열에 대해 비밀번호가 필요한지 검사합니다.
	/// </summary>
	/// <param name="usage">사용처 문자열</param>
	public static bool PassUsageTest(string usage) =>
		s_sec_pass_usages.Contains(usage);

	/// <summary>
	/// 입력한 비밀번호가 맞는지 확인하고 해제합니다.
	/// </summary>
	/// <param name="value">입력한 비밀번호</param>
	/// <returns>일치하면 true, 아니면 false</returns>
	public static bool PassUnlock(string value)
	{
		var pw = value.Length > 0 ? Alter.CompressString(value) ?? string.Empty : string.Empty;
		if (!pw.Equals(s_sec_pass_code))
			return false;
		s_sec_unlock_pass = true;
		return true;
	}

	/// <summary>
	/// 비밀번호 사용처를 저장합니다.
	/// </summary>
	/// <param name="usages">사용처 배열</param>
	public static void PassUsageCommit(IEnumerable<PassCodeUsage> usages)
	{
		var s = string.Join(',', usages);
		if (s.Equals(s_sec_pass_usages))
			return;
		SqlSetConfig("SecurityPassUsage", s_sec_pass_usages = s);
	}
	#endregion

	#region 최근 페이지
	/// <summary>
	/// 파일 이름에 해당하는 최근 페이지 번호를 반환합니다.
	/// </summary>
	/// <param name="onlyFilename">파일 이름(경로 제외)</param>
	/// <returns>최근 페이지 번호</returns>
	public static int GetRecentlyPage(string onlyFilename)
	{
		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();
		var cmd = conn.CreateCommand();
		cmd.CommandText = "SELECT page FROM recently WHERE filename = @filename LIMIT 1;";
		cmd.Parameters.AddWithValue("@filename", onlyFilename);
		using var rdr = cmd.ExecuteReader();
		return !rdr.Read() ? 0 : // 최근 페이지가 없으면 0 반환
			rdr.GetInt32(0);
	}

	/// <summary>
	/// 파일 이름에 해당하는 최근 페이지 번호를 저장합니다.
	/// </summary>
	/// <param name="onlyFilename">파일 이름(경로 제외)</param>
	/// <param name="page">페이지 번호</param>
	public static void SetRecentlyPage(string onlyFilename, int page)
	{
		using var conn = new SqliteConnection(s_config_data_source);
		conn.Open();
		var cmd = conn.CreateCommand();
		if (page <= 0)
		{
			// 페이지가 0 이면 삭제
			cmd.CommandText = "DELETE FROM recently WHERE filename = @filename;";
			cmd.Parameters.AddWithValue("@filename", onlyFilename);
		}
		else
		{
			cmd.CommandText = "INSERT OR REPLACE INTO recently (filename, page) VALUES (@filename, @page);";
			cmd.Parameters.AddWithValue("@filename", onlyFilename);
			cmd.Parameters.AddWithValue("@page", page);
		}
		try
		{
			cmd.ExecuteNonQuery();
		}
		catch (Exception e)
		{
#if DEBUG
			System.Diagnostics.Debug.WriteLine($"Failed to set recently page: {onlyFilename} = {page}");
			System.Diagnostics.Debug.WriteLine($" -> {e.Message}");
#endif
		}
	}

	/// <summary>
	/// 책 객체의 최근 페이지를 저장합니다.
	/// </summary>
	/// <param name="book">책 객체</param>
	public static void SetRecentlyPage(BookBase book)
	{
		var page = book.CurrentPage - 1 == book.TotalPage ? 0 : book.CurrentPage;
		SetRecentlyPage(book.OnlyFileName, page);
	}
	#endregion

	#region 책 이동 위치
	// 마지막 책 이동 디렉토리
	public static string MoveLocationLast { get; set; } = string.Empty;

	// 책 이동 위치를 추가합니다.
	public static int MoveLocationAdd(string path, string alias)
	{
		var last = s_move_locations.Count;
		if (last >= 100) // 최대 100개까지만 저장
			return 1; // 실패
		if (s_move_locations.Any(item => item.Path.Equals(path, StringComparison.OrdinalIgnoreCase)))
			return 2; // 이미 존재함
		s_move_locations.Add(new MoveLocation(last, alias, path));
		return 0;
	}

	/// <summary>
	/// 책 이동 위치를 수정합니다.
	/// </summary>
	/// <param name="index">인덱스</param>
	/// <param name="path">경로</param>
	/// <param name="alias">별명</param>
	public static void MoveLocationSet(int index, string path, string alias)
	{
		if (index < 0 || index >= s_move_locations.Count)
			return;
		s_move_locations[index] = new MoveLocation(index, alias, path);
	}

	// 책 이동 위치 인덱스 재설정
	private static void MoveLocationResetIndex()
	{
		for (var i = 0; i < s_move_locations.Count; i++)
		{
			var m = s_move_locations[i];
			s_move_locations[i] = m with { No = i };
		}
	}

	/// <summary>
	/// 책 이동 위치를 삭제합니다.
	/// </summary>
	/// <param name="index">인덱스</param>
	public static void MoveLocationDelete(int index)
	{
		if (index < 0 || index >= s_move_locations.Count)
			return;
		s_move_locations.RemoveAt(index);
		MoveLocationResetIndex();
	}

	/// <summary>
	/// 책 이동 위치의 순서를 변경합니다.
	/// </summary>
	/// <param name="from">이동할 인덱스</param>
	/// <param name="to">이동될 인덱스</param>
	/// <returns>성공 여부</returns>
	public static bool MoveLocationIndexing(int from, int to)
	{
		if (from < 0 || from >= s_move_locations.Count ||
			to < 0 || to >= s_move_locations.Count)
			return false;

		var m = s_move_locations[from];
		s_move_locations.RemoveAt(from);

		if (from < to)
			to--;
		s_move_locations.Insert(to, m);

		MoveLocationResetIndex();
		return true;
	}

	/// <summary>
	/// 모든 책 이동 위치를 배열로 반환합니다.
	/// </summary>
	public static MoveLocation[] MoveLocationGetAll() =>
		s_move_locations.ToArray();

	/// <summary>
	/// 책 이동 위치를 설정 파일에 저장합니다.
	/// </summary>
	public static void MoveLocationCommit()
	{
		var conn = new SqliteConnection(s_config_data_source);
		conn.Open();

		// 먼저 기존 데이터를 삭제합니다.
		var cmd = conn.CreateCommand();
		cmd.CommandText = "DELETE FROM moves;";
		cmd.ExecuteNonQuery();

		// 그리고 새로 저장합니다.
		using var transaction = conn.BeginTransaction();
		cmd.Transaction = transaction;
		foreach (var m in s_move_locations)
		{
			cmd.CommandText = "INSERT INTO moves (no, alias, folder) VALUES (@no, @alias, @folder);";
			cmd.Parameters.Clear();
			cmd.Parameters.AddWithValue("@no", m.No);
			cmd.Parameters.AddWithValue("@alias", m.Alias);
			cmd.Parameters.AddWithValue("@folder", m.Path);
			cmd.ExecuteNonQuery();
		}
		transaction.Commit();
	}
	#endregion

	#region 부근 파일
	// 다음 파일을 가져옵니다.
	public static string? NearGetFile(string currentFile, BookDirection direction)
	{
		if (s_near_files.Count == 0)
			return null;
		var index = s_near_files.IndexOf(currentFile);
		var want = direction == BookDirection.Previous ? index - 1 : index + 1;
		return want < 0 || want >= s_near_files.Count ? null : s_near_files[want];
	}

	// 랜덤 파일을 가져옵니다.
	public static string? NearGetRandomFile(string currentFile)
	{
		if (s_near_files.Count == 0)
			return null;

		var random = new Random();
		while (true)
		{
			var index = random.Next(s_near_files.Count);
			var select = s_near_files[index];
			if (select == currentFile)
				continue;
			return select;
		}
	}

	// 부근 파일 디렉토리가 같은가 확인합니다.
	public static bool NearIsPathSame(string path)
	{
		if (string.IsNullOrEmpty(path) || !Path.Exists(path))
			return false;
		return s_near_path.Equals(path, StringComparison.OrdinalIgnoreCase);
	}

	// 부근 파일을 설정합니다.
	public static void NearSetFiles(string path, IEnumerable<string> files)
	{
		s_near_files.Clear();
		//s_near_files.AddRange(files.Where(f => !string.IsNullOrEmpty(f) && File.Exists(f)));
		s_near_files.AddRange(files);
		s_near_path = path;
	}
	#endregion
}

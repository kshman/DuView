using System.Text;

namespace DuView;

internal static class Settings
{
	private static SettingsLineDb? s_lines;

	private static string s_language = string.Empty;

	// 값
	private static Rectangle s_bound = new(0, 0, 800, 480);
	private static int s_magnetic_dock_size = 10;

	// -- 최근
	private static string s_last_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
	private static string s_last_filename = string.Empty;
	private static string s_remember_filename = string.Empty;

	private static ResizableLineDb? s_recently;
	private static int s_max_recently = 1000;

	// -- 이동
	private static readonly List<KeyValuePair<string, string>> s_move = new();

	// -- 뷰
	private static bool s_view_zoom = true;
	private static Types.ViewMode s_view_mode = Types.ViewMode.FitWidth;
	private static Types.ViewQuality s_view_quality = Types.ViewQuality.Default;

	// -- 이름바꾸기
	private static bool s_rename_open_next = true;

	// -- 캐시
	private static int s_max_page_cache = 230; // 1048576곱해야함

	// -- 일반
	private static bool s_run_only_once_instance = true;
	private static bool s_esc_to_exit = true;
	private static bool s_use_magnetic_window;
	private static bool s_confirm_when_delete_file = true;
	private static bool s_always_on_top;
	private static bool s_use_update_notification = true;
	private static string s_external_run = string.Empty;
	private static bool s_reload_after_external = true;
	private static bool s_keep_book_direction;
	private static string s_firefox_run = string.Empty;
	private static bool s_extened_renamer = true;

	// -- 마우스
	private static bool s_use_doubleclick_state;
	private static bool s_use_click_page;

	// -- 보안
	private static bool s_use_pass;
	private static bool s_unlock_pass;
	private static string s_pass_code = string.Empty;
	private static string s_pass_usages = string.Empty;

	// -- 기타
	private static bool s_use_anim_thread = false;
	private static bool s_use_gdip_gif = true;

	//
	public static string StartupPath => Application.StartupPath;

	//
	public static string SettingsPath => Path.Combine(StartupPath, "DuView.config");

	//
	public static string RecentlyPath => Path.Combine(StartupPath, "DuView.recently");

	//
	private static SettingsLineDb ReadSettings()
	{
		s_lines ??= SettingsLineDb.FromFile(SettingsPath);
		return s_lines;
	}

	// 시작할 때 앞
	public static void WhenBeforeStart()
	{
		var lines = ReadSettings();

		s_run_only_once_instance = lines.GetBool("GeneralRunOnce", s_run_only_once_instance);
	}

	// 시작할 때 뒤
	public static void WhenAfterStart()
	{
		var lines = ReadSettings();

		var v = lines.GetString("Window");
		if (v.TestHave())
		{
			var ss = v.Split(',');
			if (ss.Length == 4)
			{
				s_bound = new Rectangle(
					int.Parse(ss[0]),
					int.Parse(ss[1]),
					int.Parse(ss[2]),
					int.Parse(ss[3]));
			}
		}

		//
		s_language = lines.GetString("Language", s_language) ?? string.Empty;

		s_magnetic_dock_size = lines.GetInt("MagneticDockSize", s_magnetic_dock_size);

		//
		v = lines.GetDecodedString("LastFolder");
		if (!string.IsNullOrEmpty(v) && Directory.Exists(v))
			s_last_folder = v;

		v = lines.GetDecodedString("LastFileName");
		if (!string.IsNullOrEmpty(v) && File.Exists(v))
			s_last_filename = v;

		v = lines.GetDecodedString("RememberFileName");
		if (!string.IsNullOrEmpty(v) && File.Exists(v))
			s_remember_filename = v;

		s_max_recently = lines.GetInt("MaxRecently", s_max_recently);

		//
		s_view_zoom = lines.GetInt("ViewZoom", s_view_zoom ? 1 : 0) != 0;
		s_view_mode = (Types.ViewMode)lines.GetInt("ViewMode", (int)s_view_mode);
		s_view_quality = (Types.ViewQuality)lines.GetInt("ViewQuality", (int)s_view_quality);

		//
		s_rename_open_next = lines.GetBool("RenameOpenNext", s_rename_open_next);

		//
		s_max_page_cache = lines.GetInt("MaxPageCache", s_max_page_cache);

		//
		s_esc_to_exit = lines.GetBool("GeneralEscExit", s_esc_to_exit);
		s_use_magnetic_window = lines.GetBool("GeneralUseMagnetic", s_use_magnetic_window);
		s_confirm_when_delete_file = lines.GetBool("GeneralConfirmDelete", s_confirm_when_delete_file);
		s_always_on_top = lines.GetBool("GeneralAlwaysTop", s_always_on_top);
		s_use_update_notification = lines.GetBool("GeneralUpdateNotify", s_use_update_notification);
		s_external_run = lines.GetString("ExternalRun") ?? s_external_run;
		s_reload_after_external = lines.GetBool("ReloadAfterExternalExit", s_reload_after_external);
		s_keep_book_direction = lines.GetBool("KeepBookDirection", s_keep_book_direction);
		s_firefox_run = lines.GetString("FirefoxRun") ?? s_firefox_run;
		s_extened_renamer = lines.GetBool("ExtendedRenamer", s_extened_renamer);

		//
		s_use_doubleclick_state = lines.GetBool("MouseUseDoubleClick", s_use_doubleclick_state);
		s_use_click_page = lines.GetBool("MouseUseClickPage", s_use_click_page);

		//
		s_use_pass = lines.GetBool("UsePassCode", s_use_pass);
		s_pass_code = lines.GetString("PassCode") ?? s_pass_code;
		s_pass_usages = lines.GetString("PassUsage") ?? s_pass_usages;

		//
		for (var i = 0; ; i++)
		{
			var s = lines.GetString($"MoveKeep{i}");
			if (string.IsNullOrEmpty(s))
				break;
			var d = Converter.DecodingString(s);
			if (!string.IsNullOrEmpty(d))
				s = d;
			var n = s.LastIndexOf("@:", StringComparison.Ordinal);
			if (n == -1)
			{
				var di = new DirectoryInfo(s);
				s_move.Add(new KeyValuePair<string, string>(s, di.Name));
			}
			else
			{
				var sk = s[..n];
				var sv = s[(n + 2)..];
				s_move.Add(new KeyValuePair<string, string>(sk, sv));
			}
		}

		var rfn = RecentlyPath;
		s_recently = File.Exists(rfn) ? ResizableLineDb.FromFile(rfn) : ResizableLineDb.New();
	}

	private static bool s_init_locale;

	//
	public static void InitLocale()
	{
		if (s_init_locale)
			return;

		Locale.AddLocale("en", Properties.Resources.locale_english);
		Locale.AddLocale("ko", Properties.Resources.locale_korean);
		Locale.SetDefaultLocale();

		SetLocale(s_language);

		s_init_locale = true;
	}

	//
	public static void SetLocale(string locale)
	{
		if (!Locale.HasLocale(locale))
		{
			var culture = Thread.CurrentThread.CurrentUICulture;
			locale = culture.GetKnownCultureLocale();
		}

		if (locale != Locale.CurrentLocale)
			Locale.SetLocale(locale);
	}

	//
	public static void WhenMainLoad(Form form)
	{
		form.Location = s_bound.Location;
		form.Size = s_bound.Size;
	}

	//
	public static void KeepLocationSize(FormWindowState state, Point location, Size size)
	{
		if (state != FormWindowState.Normal)
			return;

		s_bound = new Rectangle(location, size);

		var lines = ReadSettings();
		lines.SetString("Window", $"{s_bound.X},{s_bound.Y},{s_bound.Width},{s_bound.Height}");
	}

	//
	public static string Language
	{
		get => s_language;
		set
		{
			if (value == s_language)
				return;

			s_language = value;

			if (!Locale.HasLocale(value))
			{
				var culture = Thread.CurrentThread.CurrentUICulture;
				s_language = culture.GetKnownCultureLocale();

				if (s_language != Locale.CurrentLocale)
					Locale.SetLocale(s_language);

				var lines = ReadSettings();
				lines.Remove("Language");
			}
			else
			{
				if (s_language != Locale.CurrentLocale)
					Locale.SetLocale(s_language);

				var lines = ReadSettings();
				lines.SetString("Language", value);
			}
		}
	}

	//
	public static int MagneticDockSize
	{
		get => s_magnetic_dock_size;
		set
		{
			if (value == s_magnetic_dock_size)
				return;

			s_magnetic_dock_size = value;

			var lines = ReadSettings();
			lines.SetInt("MagneticDockSize", value);
		}
	}

	//
	public static string LastFolder
	{
		get => s_last_folder;
		set
		{
			if (value.Equals(s_last_folder))
				return;

			s_last_folder = value;

			var lines = ReadSettings();
			lines.SetEncodedString("LastFolder", value);
		}
	}

	//
	public static string LastFileName
	{
		get => s_last_filename;
		set
		{
			if (value.Equals(s_last_filename))
				return;

			s_last_filename = value;

			var lines = ReadSettings();
			lines.SetEncodedString("LastFileName", value);
		}
	}

	//
	public static string RememberFileName
	{
		get => s_remember_filename;
		set
		{
			if (value.Equals(s_remember_filename))
				return;

			s_remember_filename = value;

			var lines = ReadSettings();
			lines.SetEncodedString("RememberFileName", value);
		}
	}

	//
	public static bool ViewZoom
	{
		get => s_view_zoom;
		set
		{
			if (value == s_view_zoom)
				return;

			s_view_zoom = value;

			var lines = ReadSettings();
			lines.SetInt("ViewZoom", value ? 1 : 0);
		}
	}

	//
	public static Types.ViewMode ViewMode
	{
		get => s_view_mode;
		set
		{
			if (value == s_view_mode)
				return;

			s_view_mode = value;

			var lines = ReadSettings();
			lines.SetInt("ViewMode", (int)value);
		}
	}

	//
	public static Types.ViewQuality ViewQuality
	{
		get => s_view_quality;
		set
		{
			if (value == s_view_quality)
				return;

			s_view_quality = value;

			var lines = ReadSettings();
			lines.SetInt("ViewQuality", (int)value);
		}
	}

	//
	public static long MaxActualPageCache => s_max_page_cache * 1048576L;

	//
	public static int MaxPageCache
	{
		get => s_max_page_cache;
		set
		{
			if (value == s_max_page_cache)
				return;

			var lines = ReadSettings();
			lines.SetInt("MaxPageCache", s_max_page_cache = value);
		}
	}

	//
	public static int MaxRecently
	{
		get => s_max_recently;
		set
		{
			if (value == s_max_recently)
				return;

			var lines = ReadSettings();
			lines.SetInt("MaxRecently", s_max_recently = value);
		}
	}

	//
	public static int GetRecentlyPage(string onlyfilename)
	{
		if (string.IsNullOrEmpty(onlyfilename) || s_recently == null)
			return 0;

		var s = Converter.EncodingString(onlyfilename);
		return s != null ? s_recently.Get(s) : 0;
	}

	//
	public static void SetRecentlyPage(string onlyfilename, int page)
	{
		if (string.IsNullOrEmpty(onlyfilename) || s_recently == null)
			return;

		var s = Converter.EncodingString(onlyfilename);
		if (s == null)
			return;

		if (page > 0)
		{
			s_recently.Set(s, page);
			s_recently.ResizeCutFrontSlowly(s_max_recently);
		}
		else
		{
			// 페이지가 0 이면 저장할 필요가 없쟎음
			s_recently.Remove(s);
		}
	}

	//
	public static void SetRecentlyPage(BookBase book)
	{
		var page = book.CurrentPage - 1 == book.TotalPage ? 0 : book.CurrentPage;
		SetRecentlyPage(book.OnlyFileName, page);
	}

	//
	public static (string path, string desc) GetMoveLocation(int index)
	{
		if (index < 0 || index >= s_move.Count)
			return (string.Empty, string.Empty);

		var move = s_move[index];
		return (move.Key, move.Value);
	}

	//
	public static void AddMoveLocation(string path, string desc)
	{
		s_move.Add(new KeyValuePair<string, string>(path, desc));
	}

	//
	public static void SetMoveLocation(int index, string path, string desc)
	{
		if (index < 0 || index >= s_move.Count)
			return;

		s_move[index] = new KeyValuePair<string, string>(path, desc);
	}

	//
	public static void DeleteMoveLocation(int index)
	{
		if (index < s_move.Count)
			s_move.RemoveAt(index);
	}

	//
	public static bool ReIndexMoveLocation(int from, int to)
	{
		if (from < 0 || from >= s_move.Count ||
			to < 0 || to >= s_move.Count)
			return false;

		var m = s_move[from];
		s_move.RemoveAt(from);

		if (from < to)
			to--;
		s_move.Insert(to, m);

		return true;
	}

	//
	public static KeyValuePair<string, string>[] GetMoveLocations()
	{
		return s_move.ToArray();
	}

	//
	public static void KeepMoveLocation()
	{
		var lines = ReadSettings();

		for (var i = 0; i < s_move.Count; i++)
		{
			var m = s_move[i];
			if (string.IsNullOrEmpty(m.Key))
				break;
			// lines.SetEncodedString
			lines.SetString($"MoveKeep{i}", 
				string.IsNullOrWhiteSpace(m.Value) ? m.Key : $"{m.Key}@:{m.Value}");
		}

		lines.SetString($"MoveKeep{s_move.Count}", string.Empty);
	}

	//
	public static void SaveSettings()
	{
		var lines = ReadSettings();

		var rfn = SettingsPath;
		lines.Save(rfn, Encoding.UTF8, new []
		{
			"DuView settings",
			$"Created: {DateTime.Now}"
		});
	}

	//
	public static void SaveFileInfos()
	{
		if (s_recently == null)
			return;

		var rfn = RecentlyPath;
		s_recently.Save(rfn, Encoding.UTF8, new[]
		{
			"DuView recently files list",
			$"Created: {DateTime.Now}"
		});
	}

	//
	public static bool RenameOpenNext
	{
		get => s_rename_open_next;
		set
		{
			if (value == s_rename_open_next)
				return;

			var lines = ReadSettings();
			lines.SetBool("RenameOpenNext", s_rename_open_next = value);
		}
	}

	#region 기본
	//
	public static bool GeneralRunOnce
	{
		get => s_run_only_once_instance;
		set
		{
			if (value == s_run_only_once_instance)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralRunOnce", s_run_only_once_instance = value);
		}
	}

	//
	public static bool GeneralEscExit
	{
		get => s_esc_to_exit;
		set
		{
			if (value == s_esc_to_exit)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralEscExit", s_esc_to_exit = value);
		}
	}

	//
	public static bool GeneralUseMagnetic
	{
		get => s_use_magnetic_window;
		set
		{
			if (value == s_use_magnetic_window)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralUseMagnetic", s_use_magnetic_window = value);
		}
	}

	//
	public static bool GeneralConfirmDelete
	{
		get => s_confirm_when_delete_file;
		set
		{
			if (value == s_confirm_when_delete_file)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralConfirmDelete", s_confirm_when_delete_file = value);
		}
	}

	//
	public static bool GeneralAlwaysTop
	{
		get => s_always_on_top;
		set
		{
			if (value == s_always_on_top)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralAlwaysTop", s_always_on_top = value);
		}
	}

	//
	public static bool GeneralUpdateNotify
	{
		get => s_use_update_notification;
		set
		{
			if (value == s_use_update_notification)
				return;

			var lines = ReadSettings();
			lines.SetBool("GeneralUpdateNotify", s_use_update_notification = value);
		}
	}

	//
	public static string ExternalRun
	{
		get => s_external_run;
		set
		{
			if (value.Equals(s_external_run))
				return;

			var lines = ReadSettings();
			lines.SetString("ExternalRun", s_external_run = value);
		}
	}

	//
	public static bool ReloadAfterExternal
	{
		get => s_reload_after_external;
		set
		{
			if (value == s_reload_after_external)
				return;

			var lines = ReadSettings();
			lines.SetBool("ReloadAfterExternalExit", s_reload_after_external = value);
		}
	}

	//
	public static bool KeepBookDirection
	{
		get => s_keep_book_direction;
		set
		{
			if (value == s_keep_book_direction)
				return;

			var lines = ReadSettings();
			lines.SetBool("KeepBookDirection", s_keep_book_direction = value);
		}
	}

	//
	public static string FirefoxRun
	{
		get => s_firefox_run;
		set
		{
			if (value.Equals(s_firefox_run))
				return;

			var lines = ReadSettings();
			lines.SetString("FirefoxRun", s_firefox_run = value);
		}
	}

	//
	public static bool ExtendedRenamer
	{
		get => s_extened_renamer;
		set
		{
			if (value == s_extened_renamer)
				return;

			var lines = ReadSettings();
			lines.SetBool("ExtendedRenamer", s_extened_renamer = value);
		}
	}
	#endregion // 기본

	#region 마우스
	//
	public static bool MouseUseDoubleClickState
	{
		get => s_use_doubleclick_state;
		set
		{
			if (value == s_use_doubleclick_state)
				return;

			var lines = ReadSettings();
			lines.SetBool("MouseUseDoubleClick", s_use_doubleclick_state = value);
		}
	}

	//
	public static bool MouseUseClickPage
	{
		get => s_use_click_page;
		set
		{
			if (value == s_use_click_page)
				return;

			var lines = ReadSettings();
			lines.SetBool("MouseUseClickPage", s_use_click_page = value);
		}
	}
	#endregion // 마우스

	#region 보안
	//
	public static bool UsePassCode
	{
		get => s_use_pass;
		set
		{
			if (value == s_use_pass)
				return;

			var lines = ReadSettings();
			lines.SetBool("UsePassCode", s_use_pass = value);
		}
	}

	//
	public static bool UnlockedPassCode
	{
		get => s_unlock_pass;
		set => s_unlock_pass = value;
	}

	//
	public static string PassCode
	{
		get => Converter.DecompressString(s_pass_code) ?? string.Empty;
		set
		{
			var pw = value.Length > 0 ? Converter.CompressString(value) ?? string.Empty : string.Empty;
			if (s_pass_code.Equals(pw))
				return;

			var lines = ReadSettings();
			lines.SetString("PassCode", s_pass_code = pw);
		}
	}

	//
	public static string PassUsage => s_pass_usages;

	//
	public static void CommitPassUsage(IEnumerable<Types.PassCodeUsage> usages)
	{
		var s = string.Join(',', usages);
		if (s.Equals(s_pass_usages))
			return;

		var lines = ReadSettings();
		lines.SetString("PassUsage", s);
	}

	//
	public static Types.PassCodeUsage[] GetPassUsageArray()
	{
		var ss = s_pass_usages.Split(',');
		if (ss.Length == 0)
			return Array.Empty<Types.PassCodeUsage>();

		List<Types.PassCodeUsage> l = new();
		foreach (var s in ss)
		{
			if (Enum.TryParse<Types.PassCodeUsage>(s, out var u) && !l.Contains(u))
				l.Add(u);
		}
		return l.ToArray();
	}

	//
	public static bool TestPassUsage(Types.PassCodeUsage usage)
	{
		return s_pass_usages.Contains(usage.ToString());
	}

	//
	public static bool TestPassUsage(string usage)
	{
		return s_pass_usages.Contains(usage);
	}

	//
	public static bool UnlockPass(string value)
	{
		var pw = value.Length > 0 ? Converter.CompressString(value) ?? string.Empty : string.Empty;
		if (!pw.Equals(s_pass_code))
			return false;
		s_unlock_pass = true;
		return true;
	}
	#endregion

	#region 기타
	//
	public static bool UseAnimationThread
	{
		get => s_use_anim_thread;
		set
		{
			if (value == s_use_anim_thread)
				return;

			var lines = ReadSettings();
			lines.SetBool("UseAnimationThread", s_use_anim_thread = value);
		}
	}
	
	//
	public static bool UseGdipGif
	{
		get => s_use_gdip_gif;
		set
		{
			if (value == s_use_gdip_gif)
				return;

			var lines = ReadSettings();
			lines.SetBool("UseGdipGif", s_use_gdip_gif = value);
		}
	}
	#endregion // 기타
}

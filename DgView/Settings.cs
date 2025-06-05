namespace DgView;

internal static class Settings
{
    private static SettingsHash? s_lines;

    private static Cairo.ImageSurface? s_noimg;

    // 책
    internal static BookBase? Book { get; set; }

    // -- 윈도우 정보
    private static BoundRect s_bound = new(0, 0, 800, 480);
    private static int s_magnetic_dock_size = 10;

    // -- 뷰
    private static bool s_view_zoom = true;
    private static ViewMode s_view_mode = ViewMode.Fit;
    private static ViewQuality s_view_quality = ViewQuality.Default;

    // -- 파일
    private static string s_last_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    private static string s_last_filename = string.Empty;
    private static string s_remember_filename = string.Empty;

    private static ResizableHash? s_recently;
    private static int s_max_recently = 1000;

    private static readonly List<KeyValuePair<string, string>> s_moves = new();
    private static int s_max_page_cache = 230; // 1048576곱해야함

    // -- 일반
    private static bool s_run_only_once_instance = true;
    private static bool s_esc_to_exit = true;
    private static bool s_use_magnetic_window;
    private static bool s_confirm_when_delete_file = true;
    private static bool s_always_on_top;

    // -- 마우스
    private static bool s_use_double_click_state;
    private static bool s_use_click_page;

    // -- 보안
    private static bool s_use_pass;
    private static bool s_unlock_pass;
    private static string s_pass_code = string.Empty;
    private static string s_pass_usages = string.Empty;

    //
    public static string AppPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ksh");

    //
    public static string SettingsPath => Path.Combine(AppPath, "DgView.config");

    //
    public static string RecentlyPath => Path.Combine(AppPath, "DgView.recently");

    //
    private static SettingsHash ReadSettings()
    {
        s_lines ??= SettingsHash.FromFile(SettingsPath);
        return s_lines;
    }

    // 시작할 때 앞
    public static void OnMainBefore()
    {
        var lines = ReadSettings();

        s_run_only_once_instance = lines.GetBool("GeneralRunOnce", s_run_only_once_instance);
    }

    // 시작할 때 뒤
    public static void OnMainAfter()
    {
        var lines = ReadSettings();

        var v = lines.GetString("Window");
        if (v.TestHave())
        {
            var ss = v.Split(',');
            try
            {
                s_bound = new BoundRect(ss);
            }
            catch
            {
                // 예외가 발생하면 기본값으로 설정
                s_bound = new BoundRect(0, 0, 800, 480);
            }
        }

        //
        s_magnetic_dock_size = lines.GetInt("MagneticDockSize", s_magnetic_dock_size);

        //
        v = lines.GetDecodedString("LastFolder");
        if (!v.EmptyString() && Directory.Exists(v))
            s_last_folder = v;

        v = lines.GetDecodedString("LastFileName");
        if (!v.EmptyString() && File.Exists(v))
            s_last_filename = v;

        v = lines.GetDecodedString("RememberFileName");
        if (!v.EmptyString() && File.Exists(v))
            s_remember_filename = v;

        s_max_recently = lines.GetInt("MaxRecently", s_max_recently);

        //
        s_view_zoom = lines.GetInt("ViewZoom", s_view_zoom ? 1 : 0) != 0;
        s_view_mode = (ViewMode)lines.GetInt("ViewMode", (int)s_view_mode);
        s_view_quality = (ViewQuality)lines.GetInt("ViewQuality", (int)s_view_quality);

        //
        s_max_page_cache = lines.GetInt("MaxPageCache", s_max_page_cache);

        //
        s_esc_to_exit = lines.GetBool("GeneralEscExit", s_esc_to_exit);
        s_use_magnetic_window = lines.GetBool("GeneralUseMagnetic", s_use_magnetic_window);
        s_confirm_when_delete_file = lines.GetBool("GeneralConfirmDelete", s_confirm_when_delete_file);
        s_always_on_top = lines.GetBool("GeneralAlwaysTop", s_always_on_top);

        //
        s_use_double_click_state = lines.GetBool("MouseUseDoubleClick", s_use_double_click_state);
        s_use_click_page = lines.GetBool("MouseUseClickPage", s_use_click_page);

        //
        s_use_pass = lines.GetBool("UsePassCode", s_use_pass);
        s_pass_code = lines.GetString("PassCode") ?? s_pass_code;
        s_pass_usages = lines.GetString("PassUsage") ?? s_pass_usages;

        //
        for (var i = 0;; i++)
        {
            var s = lines.GetString($"MoveKeep{i}");
            if (s.EmptyString())
                break;
            var n = s.LastIndexOf("@:", StringComparison.Ordinal);
            if (n == -1)
            {
                var di = new DirectoryInfo(s);
                s_moves.Add(new KeyValuePair<string, string>(s, di.Name));
            }
            else
            {
                var sk = s[..n];
                var sv = s[(n + 2)..];
                s_moves.Add(new KeyValuePair<string, string>(sk, sv));
            }
        }

        var rfn = RecentlyPath;
        s_recently = File.Exists(rfn) ? ResizableHash.FromFile(rfn) : ResizableHash.New();
    }

    // 메인 로드하고
    public static void OnWindowInit(Window window)
    {
        window.SetDefaultSize(s_bound.Width, s_bound.Height);
        window.SetPosition(WindowPosition.Center);
        window.Move(s_bound.X, s_bound.Y);
    }

    // 
    public static Cairo.ImageSurface OopsNoImage()
    {
        if (s_noimg == null)
        {
            s_noimg = new Cairo.ImageSurface(Cairo.Format.Argb32, 300, 300);
            using var cr = new Cairo.Context(s_noimg);
            cr.SetSourceRGB(0, 0, 0);
            cr.Paint();
                
            // 빨간색 X 모양 그리기
            cr.SetSourceRGB(1, 0, 0); // 빨간색 (RGB 값)
            cr.LineWidth = 10; // 선 두께
            
            // 첫 번째 대각선 (왼쪽 위 -> 오른쪽 아래)
            cr.MoveTo(300 * 0.1, 300 * 0.1); // 시작점
            cr.LineTo(300 * 0.9, 300 * 0.9); // 끝점
            
            // 두 번째 대각선 (오른쪽 위 -> 왼쪽 아래)
            cr.MoveTo(300 * 0.9, 300 * 0.1); // 시작점
            cr.LineTo(300 * 0.1, 300 * 0.9); // 끝점
            
            cr.Stroke(); // 선 그리기 실행
        }
        return s_noimg;
    }

    //
    public static void KeepLocationSize(int x, int y, int width, int height)
    {
        s_bound = new BoundRect(x, y, width, height);

        var lines = ReadSettings();
        lines.SetString("Window", $"{s_bound.X},{s_bound.Y},{s_bound.Width},{s_bound.Height}");
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
    public static ViewMode ViewMode
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
    public static ViewQuality ViewQuality
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
    public static int GetRecentlyPage(string onlyFilename)
    {
        if (onlyFilename.EmptyString() || s_recently == null)
            return 0;

        var s = Alter.EncodingString(onlyFilename);
        return s != null ? s_recently.Get(s) : 0;
    }

    //
    public static void SetRecentlyPage(string onlyFilename, int page)
    {
        if (onlyFilename.EmptyString() || s_recently == null)
            return;

        var s = Alter.EncodingString(onlyFilename);
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
        if (index < 0 || index >= s_moves.Count)
            return (string.Empty, string.Empty);

        var move = s_moves[index];
        return (move.Key, move.Value);
    }

    //
    public static void AddMoveLocation(string path, string desc) =>
        s_moves.Add(new KeyValuePair<string, string>(path, desc));

    //
    public static void SetMoveLocation(int index, string path, string desc)
    {
        if (index < 0 || index >= s_moves.Count)
            return;

        s_moves[index] = new KeyValuePair<string, string>(path, desc);
    }

    //
    public static void DeleteMoveLocation(int index)
    {
        if (index < s_moves.Count)
            s_moves.RemoveAt(index);
    }

    //
    public static bool IndexingMoveLocation(int from, int to)
    {
        if (from < 0 || from >= s_moves.Count ||
            to < 0 || to >= s_moves.Count)
            return false;

        var m = s_moves[from];
        s_moves.RemoveAt(from);

        if (from < to)
            to--;
        s_moves.Insert(to, m);

        return true;
    }

    //
    public static KeyValuePair<string, string>[] GetMoveLocations()
    {
        return s_moves.ToArray();
    }

    //
    public static void KeepMoveLocation()
    {
        var lines = ReadSettings();

        for (var i = 0; i < s_moves.Count; i++)
        {
            var m = s_moves[i];
            if (m.Key.EmptyString())
                break;
            // lines.SetEncodedString
            lines.SetString($"MoveKeep{i}",
                m.Value.WhiteString() ? m.Key : $"{m.Key}@:{m.Value}");
        }

        lines.SetString($"MoveKeep{s_moves.Count}", string.Empty);
    }

    //
    public static void SaveSettings()
    {
        var lines = ReadSettings();

        var rfn = SettingsPath;
        lines.Save(rfn, [
            "DgView settings",
            $"Created: {DateTime.Now}"
        ]);
    }

    //
    public static void SaveFileInfos()
    {
        if (s_recently == null)
            return;

        var rfn = RecentlyPath;
        s_recently.Save(rfn, [
            "DgView recently files list",
            $"Created: {DateTime.Now}"
        ]);
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

    #endregion // 기본

    #region 마우스

    //
    public static bool MouseUseDoubleClickState
    {
        get => s_use_double_click_state;
        set
        {
            if (value == s_use_double_click_state)
                return;

            var lines = ReadSettings();
            lines.SetBool("MouseUseDoubleClick", s_use_double_click_state = value);
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
        get => Alter.DecompressString(s_pass_code) ?? string.Empty;
        set
        {
            var pw = value.Length > 0 ? Alter.CompressString(value) ?? string.Empty : string.Empty;
            if (s_pass_code.Equals(pw))
                return;

            var lines = ReadSettings();
            lines.SetString("PassCode", s_pass_code = pw);
        }
    }

    //
    public static string PassUsage => s_pass_usages;

    //
    public static void CommitPassUsage(IEnumerable<PassCodeUsage> usages)
    {
        var s = string.Join(',', usages);
        if (s.Equals(s_pass_usages))
            return;

        var lines = ReadSettings();
        lines.SetString("PassUsage", s);
    }

    //
    public static PassCodeUsage[] GetPassUsageArray()
    {
        var ss = s_pass_usages.Split(',');
        if (ss.Length == 0)
            return [];

        List<PassCodeUsage> l = new();
        foreach (var s in ss)
        {
            if (Enum.TryParse<PassCodeUsage>(s, out var u) && !l.Contains(u))
                l.Add(u);
        }

        return l.ToArray();
    }

    //
    public static bool TestPassUsage(PassCodeUsage usage)
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
        var pw = value.Length > 0 ? Alter.CompressString(value) ?? string.Empty : string.Empty;
        if (!pw.Equals(s_pass_code))
            return false;
        s_unlock_pass = true;
        return true;
    }

    #endregion
}
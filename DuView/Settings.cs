using System.Text;

namespace DuView;

internal static class Settings
{
	private const string c_keyname = @"PuruLive\DuView";

	private static string s_locale = string.Empty;

	private static int s_magnetic_dock_size = 10;

	private static string s_last_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
	private static string s_last_filename = string.Empty;

	private static bool s_view_zoom = true;
	private static Types.ViewMode s_view_mode = Types.ViewMode.FitWidth;
	private static Types.ViewQuality s_view_quality = Types.ViewQuality.Default;

	private static long s_max_cache_size = 230 * 1048576; // 쉽게해서 230메가
	private static int s_max_recently = 1000;

	private static ResizableLineDb? s_recently;

	public static void WhenLoad(Form form)
	{
		using (var rk = new RegKey(c_keyname))
		{
			if (rk.IsOpen)
			{
				var v = rk.GetString("Window");
				if (!string.IsNullOrEmpty(v))
				{
					var ss = v.Split(',');
					if (ss.Length == 4)
					{
						var rt = new Rectangle(
							int.Parse(ss[0]),
							int.Parse(ss[1]),
							int.Parse(ss[2]),
							int.Parse(ss[3]));

						form.Location = rt.Location;
						form.Size = rt.Size;
					}
				}

				s_locale = rk.GetString("Locale", s_locale) ?? string.Empty;

				s_magnetic_dock_size = rk.GetInt("MagneticDockSize", s_magnetic_dock_size);

				v = rk.GetDecodingString("LastFolder");
				if (!string.IsNullOrEmpty(v) && Directory.Exists(v))
					s_last_folder = v;

				v = rk.GetDecodingString("LastFileName");
				if (!string.IsNullOrEmpty(v) && File.Exists(v))
					s_last_filename = v;

				s_view_zoom = rk.GetInt("ViewZoom", s_view_zoom ? 1 : 0) != 0;
				s_view_mode = (Types.ViewMode)rk.GetInt("ViewMode", (int)s_view_mode);
				s_view_quality = (Types.ViewQuality)rk.GetInt("ViewQuality", (int)s_view_quality);

				s_max_cache_size = rk.GetLong("MaxCacheSize", s_max_cache_size);
				s_max_recently = rk.GetInt("MaxRecently", s_max_recently);
			}
		}

		var rfn = RecentlyPath;
		s_recently = File.Exists(rfn) ? ResizableLineDb.FromFile(rfn) : ResizableLineDb.New();
	}

	//
	public static void KeepLocationSize(FormWindowState state, Point location, Size size)
	{
		if (state == FormWindowState.Normal)
		{
			var rt = new Rectangle(location, size);

			using var rk = new RegKey(c_keyname, true);
			rk.SetString("Window", $"{rt.X},{rt.Y},{rt.Width},{rt.Height}");
		}
	}

	//
	public static string StartupPath => Application.StartupPath;

	//
	public static string RecentlyPath => Path.Combine(StartupPath, "DuView.recently");

	//
	public static string Locale
	{
		get => s_locale;
		set
		{
			if (value!= s_locale)
			{
				s_locale = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetString("Locale", value);
			}
		}
	}

	//
	public static int MagneticDockSize
	{
		get => s_magnetic_dock_size;
		set
		{
			if (value != s_magnetic_dock_size)
			{
				s_magnetic_dock_size = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetInt("MagneticDockSize", value);
			}
		}
	}

	//
	public static string LastFolder
	{
		get => s_last_folder;
		set
		{
			if (!value.Equals(s_last_folder))
			{
				s_last_folder = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetEncodingString("LastFolder", value);
			}
		}
	}

	//
	public static string LastFileName
	{
		get => s_last_filename;
		set
		{
			if (!value.Equals(s_last_filename))
			{
				s_last_filename = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetEncodingString("LastFileName", value);
			}
		}
	}

	//
	public static bool ViewZoom
	{
		get => s_view_zoom;
		set
		{
			if (value != s_view_zoom)
			{
				s_view_zoom = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetInt("ViewZoom", value ? 1 : 0);
			}
		}
	}

	//
	public static Types.ViewMode ViewMode
	{
		get => s_view_mode;
		set
		{
			if (value != s_view_mode)
			{
				s_view_mode = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetInt("ViewMode", (int)value);
			}
		}
	}

	//
	public static Types.ViewQuality ViewQuality
	{
		get => s_view_quality;
		set
		{
			if (value != s_view_quality)
			{
				s_view_quality = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetInt("ViewQuality", (int)value);
			}
		}
	}

	//
	public static long MaxCacheSize
	{
		get => s_max_cache_size;
		set
		{
			if (value != s_max_cache_size)
			{
				s_max_cache_size = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetLong("MaxCacheSize", value);
			}
		}
	}

	//
	public static int MaxRecently
	{
		get => s_max_recently;
		set
		{
			if (value != s_max_recently)
			{
				s_max_recently = value;

				using var rk = new RegKey(c_keyname, true);
				rk.SetInt("MaxRecently", value);
			}
		}
	}

	//
	public static int GetRecentlyPage(string onlyfilename)
	{
		if (string.IsNullOrEmpty(onlyfilename) || s_recently == null)
			return 0;

		var s = Converter.EncodingString(onlyfilename);
		return s_recently.Get(s);
	}

	//
	public static void SetRecentlyPage(string onlyfilename, int page)
	{
		if (!string.IsNullOrEmpty(onlyfilename) && s_recently != null)
		{
			var s = Converter.EncodingString(onlyfilename);

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
	}

	//
	public static void SetRecentlyPage(BookBase book)
	{
		var page = book.CurrentPage - 1 == book.TotalPage ? 0 : book.CurrentPage;
		SetRecentlyPage(book.OnlyFileName, page);
	}

	//
	public static void SaveFileInfos()
	{
		if (s_recently != null)
		{
			var rfn = RecentlyPath;
			s_recently.Save(rfn, Encoding.UTF8, new string[]
			{
				"DuView recently files list",
				$"Created: {DateTime.Now}"
			});
		}
	}
}


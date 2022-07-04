using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Du;
using Du.Globalization;
using Du.Platform;

namespace DuView
{
	internal static class Settings
	{
		private const string c_keyname = @"PuruLive\DuView";

		private static string s_language = string.Empty;

		// 값
		private static Rectangle s_bound = new Rectangle(0, 0, 800, 480);
		private static int s_magnetic_dock_size = 10;

		// -- 최근
		private static string s_last_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		private static string s_last_filename = string.Empty;

		private static ResizableLineDb s_recently;
		private static int s_max_recently = 1000;

		// -- 뷰
		private static bool s_view_zoom = true;
		private static Types.ViewMode s_view_mode = Types.ViewMode.FitWidth;
		private static Types.ViewQuality s_view_quality = Types.ViewQuality.Default;

		// -- 이름바꾸기
		private static bool s_rename_open_next = true;

		// -- 캐시
		private static int s_max_page_cache = 230; // 1048576곱해야함

		// -- 기본
		private static bool s_run_only_once_instance = true;
		private static bool s_esc_to_exit = true;
		private static bool s_use_windows_notification = true;
		private static bool s_use_magnetic_window = false;
		private static bool s_confirm_when_delete_file = true;
		private static bool s_always_on_top = false;
		private static bool s_use_update_notification = true;
		private static string s_external_run = string.Empty;
		private static bool s_reload_after_external = true;
		private static bool s_keep_book_direction = false;
		private static string s_firefox_run = string.Empty;

		// -- 마우스
		private static bool s_use_doubleclick_state = false;
		private static bool s_use_click_page = false;

		// -- 기타

		// 시작할 때 앞
		public static void WhenBeforeStart()
		{
			using (var rk = new RegKey(c_keyname))
			{
				if (rk.IsOpen)
				{
					s_run_only_once_instance = rk.GetBool("GeneralRunOnce", s_run_only_once_instance);
				}
			}
		}

		// 시작할 때 뒤
		public static void WhenAfterStart()
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
							s_bound = new Rectangle(
								int.Parse(ss[0]),
								int.Parse(ss[1]),
								int.Parse(ss[2]),
								int.Parse(ss[3]));
						}
					}

					//
					s_language = rk.GetString("Language", s_language) ?? string.Empty;

					s_magnetic_dock_size = rk.GetInt("MagneticDockSize", s_magnetic_dock_size);

					//
					v = rk.GetDecodingString("LastFolder");
					if (!string.IsNullOrEmpty(v) && Directory.Exists(v))
						s_last_folder = v;

					v = rk.GetDecodingString("LastFileName");
					if (!string.IsNullOrEmpty(v) && File.Exists(v))
						s_last_filename = v;

					s_max_recently = rk.GetInt("MaxRecently", s_max_recently);

					//
					s_view_zoom = rk.GetInt("ViewZoom", s_view_zoom ? 1 : 0) != 0;
					s_view_mode = (Types.ViewMode)rk.GetInt("ViewMode", (int)s_view_mode);
					s_view_quality = (Types.ViewQuality)rk.GetInt("ViewQuality", (int)s_view_quality);

					//
					s_rename_open_next = rk.GetBool("RenameOpenNext", s_rename_open_next);

					//
					s_max_page_cache = rk.GetInt("MaxPageCache", s_max_page_cache);

					//
					s_esc_to_exit = rk.GetBool("GeneralEscExit", s_esc_to_exit);
					s_use_magnetic_window = rk.GetBool("GeneralUseMagnetic", s_use_magnetic_window);
					s_use_windows_notification = rk.GetBool("GeneralUseWinNotify", s_use_windows_notification);
					s_confirm_when_delete_file = rk.GetBool("GeneralConfirmDelete", s_confirm_when_delete_file);
					s_always_on_top = rk.GetBool("GeneralAlwaysTop", s_always_on_top);
					s_use_update_notification = rk.GetBool("GeneralUpdateNotify", s_use_update_notification);
					s_external_run = rk.GetString("ExternalRun") ?? s_external_run;
					s_reload_after_external = rk.GetBool("ReloadAfterExternalExit", s_reload_after_external);
					s_keep_book_direction = rk.GetBool("KeepBookDirection", s_keep_book_direction);
					s_firefox_run = rk.GetString("FirefoxRun") ?? s_firefox_run;

					//
					s_use_doubleclick_state = rk.GetBool("MouseUseDoubleClick", s_use_doubleclick_state);
					s_use_click_page = rk.GetBool("MouseUseClickPage", s_use_click_page);
				}
			}

			var rfn = RecentlyPath;
			s_recently = File.Exists(rfn) ? ResizableLineDb.FromFile(rfn) : ResizableLineDb.New();
		}

		private static bool s_init_locale;

		//
		public static void InitLocale()
		{
			if (!s_init_locale)
			{
				Locale.AddLocale("en", Properties.Resources.locale_english);
				Locale.AddLocale("ko", Properties.Resources.locale_korean);
				Locale.SetDefaultLocale();

				SetLocale(s_language);

				s_init_locale = true;
			}
		}

		//
		private static void SetLocale(string locale)
		{
			if (!Locale.HasLocale(locale))
			{
				var culture = Thread.CurrentThread.CurrentUICulture;
				locale = ToolBox.GetKnownCultureLocale(culture);
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
			if (state == FormWindowState.Normal)
			{
				s_bound = new Rectangle(location, size);

				using (var rk = new RegKey(c_keyname, true))
					rk.SetString("Window", $"{s_bound.X},{s_bound.Y},{s_bound.Width},{s_bound.Height}");
			}
		}

		//
		public static string StartupPath => Application.StartupPath;

		//
		public static string RecentlyPath => Path.Combine(StartupPath, "DuView.recently");

		//
		public static string Language
		{
			get => s_language;
			set
			{
				if (value != s_language)
				{
					s_language = value;

					if (!Locale.HasLocale(value))
					{
						var culture = Thread.CurrentThread.CurrentUICulture;
						s_language = ToolBox.GetKnownCultureLocale(culture);

						if (s_language != Locale.CurrentLocale)
							Locale.SetLocale(s_language);

						using (var rk = new RegKey(c_keyname, true))
							rk.DeleteValue("Language");
					}
					else
					{
						if (s_language != Locale.CurrentLocale)
							Locale.SetLocale(s_language);

						using (var rk = new RegKey(c_keyname, true))
							rk.SetString("Language", value);
					}
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

					using (var rk = new RegKey(c_keyname, true))
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

					using (var rk = new RegKey(c_keyname, true))
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

					using (var rk = new RegKey(c_keyname, true))
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

					using (var rk = new RegKey(c_keyname, true))
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

					using (var rk = new RegKey(c_keyname, true))
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

					using (var rk = new RegKey(c_keyname, true))
						rk.SetInt("ViewQuality", (int)value);
				}
			}
		}

		//
		public static long MaxActualPageCache
		{
			get => s_max_page_cache * 1048576L;
		}

		//
		public static int MaxPageCache
		{
			get => s_max_page_cache;
			set
			{
				if (value != s_max_page_cache)
				{
					s_max_page_cache = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetInt("MaxPageCache", value);
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

					using (var rk = new RegKey(c_keyname, true))
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
				s_recently.Save(rfn, Encoding.UTF8, new[]
				{
					"DuView recently files list",
					$"Created: {DateTime.Now}"
				});
			}
		}

		//
		public static bool RenameOpenNext
		{
			get => s_rename_open_next;
			set
			{
				if (value != s_rename_open_next)
				{
					s_rename_open_next = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("RenameOpenNext", s_rename_open_next);
				}
			}
		}

		#region 기본
		//
		public static bool GeneralRunOnce
		{
			get => s_run_only_once_instance;
			set
			{
				if (value != s_run_only_once_instance)
				{
					s_run_only_once_instance = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralRunOnce", s_run_only_once_instance);
				}
			}
		}

		//
		public static bool GeneralEscExit
		{
			get => s_esc_to_exit;
			set
			{
				if (value != s_esc_to_exit)
				{
					s_esc_to_exit = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralEscExit", s_esc_to_exit);
				}
			}
		}

		//
		public static bool GeneralUseMagnetic
		{
			get => s_use_magnetic_window;
			set
			{
				if (value != s_use_magnetic_window)
				{
					s_use_magnetic_window = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralUseMagnetic", s_use_magnetic_window);
				}
			}
		}

		//
		public static bool GeneralUseWinNotify
		{
			get => s_use_windows_notification;
			set
			{
				if (value != s_use_windows_notification)
				{
					s_use_windows_notification = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralUseWinNotify", s_use_windows_notification);
				}
			}
		}

		//
		public static bool GeneralConfirmDelete
		{
			get => s_confirm_when_delete_file;
			set
			{
				if (value != s_confirm_when_delete_file)
				{
					s_confirm_when_delete_file = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralConfirmDelete", s_confirm_when_delete_file);
				}
			}
		}

		//
		public static bool GeneralAlwaysTop
		{
			get => s_always_on_top;
			set
			{
				if (value != s_always_on_top)
				{
					s_always_on_top = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralAlwaysTop", s_always_on_top);
				}
			}
		}

		//
		public static bool GeneralUpdateNotify
		{
			get => s_use_update_notification;
			set
			{
				if (value != s_use_update_notification)
				{
					s_use_update_notification = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("GeneralUpdateNotify", s_use_update_notification);
				}
			}
		}

		//
		public static string ExternalRun
		{
			get => s_external_run;
			set
			{
				if (!value.Equals(s_external_run))
				{
					s_external_run = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetString("ExternalRun", s_external_run);
				}
			}
		}

		//
		public static bool ReloadAfterExternal
		{
			get => s_reload_after_external;
			set
			{
				if (value != s_reload_after_external)
				{
					s_reload_after_external = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("ReloadAfterExternalExit", s_reload_after_external);
				}
			}
		}

		//
		public static bool KeepBookDirection
		{
			get => s_keep_book_direction;
			set
			{
				if (value != s_keep_book_direction)
				{
					s_keep_book_direction = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("KeepBookDirection", s_keep_book_direction);
				}
			}
		}

		//
		public static string FirefoxRun
		{
			get => s_firefox_run;
			set
			{
				if (!value.Equals(s_firefox_run))
				{
					s_firefox_run = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetString("FirefoxRun", s_firefox_run);
				}
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
				if (value != s_use_doubleclick_state)
				{
					s_use_doubleclick_state = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("MouseUseDoubleClick", s_use_doubleclick_state);
				}
			}
		}

		//
		public static bool MouseUseClickPage
		{
			get => s_use_click_page;
			set
			{
				if (value != s_use_click_page)
				{
					s_use_click_page = value;

					using (var rk = new RegKey(c_keyname, true))
						rk.SetBool("MouseUseClickPage", s_use_click_page);
				}
			}
		}
		#endregion // 마우스

		#region 기타
		#endregion // 기타
	}
}

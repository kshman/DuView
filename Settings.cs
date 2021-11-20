using Du;
using Du.Platform;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuView
{
	internal static class Settings
	{
		private static readonly string _keyname = @"PuruLive\DuView";

		private static int _magnetic_dock_size = 10;

		private static string _last_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		private static string _last_filename;

		public static bool _view_zoom = true;
		public static Types.ViewMode _view_mode = Types.ViewMode.FitWidth;
		public static Types.ViewQuality _view_quality = Types.ViewQuality.Default;

		public static long _max_cache_size = 230 * 1048576; // 쉽게해서 230메가
		public static int _max_recently = 1000;

		public static ResizableLineDb _recently;

		public static void WhenLoad(Form form)
		{
			using (var rk = new RegKey(_keyname))
			{
				if (rk.IsOpen)
				{
					string v;

					v = rk.GetString("Window");
					if (!string.IsNullOrEmpty(v))
					{
						string[] ss = v.Split(',');
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

					_magnetic_dock_size = rk.GetInt("MagneticDockSize", _magnetic_dock_size);

					v = rk.GetDecodingString("LastFolder");
					if (!string.IsNullOrEmpty(v) && Directory.Exists(v))
						_last_folder = v;

					v = rk.GetDecodingString("LastFileName");
					if (!string.IsNullOrEmpty(v) && File.Exists(v))
						_last_filename = v;

					_view_zoom = rk.GetInt("ViewZoom", _view_zoom ? 1 : 0) != 0;
					_view_mode = (Types.ViewMode)rk.GetInt("ViewMode", (int)_view_mode);
					_view_quality = (Types.ViewQuality)rk.GetInt("ViewQuality", (int)_view_quality);

					_max_cache_size = rk.GetLong("MaxCacheSize", _max_cache_size);
				}
			}

			var rfn = RecentlyPath;
			_recently = File.Exists(rfn) ? ResizableLineDb.FromFile(rfn) : ResizableLineDb.New();
		}

		//
		public static void KeepLocationSize(FormWindowState state, Point location, Size size)
		{
			if (state == FormWindowState.Normal)
			{
				var rt = new Rectangle(location, size);

				using (var rk = new RegKey(_keyname, true))
					rk.SetString("Window", $"{rt.X},{rt.Y},{rt.Width},{rt.Height}");
			}
		}

		//
		public static string StartupPath
		{
			get { return Application.StartupPath; }
		}

		//
		public static string RecentlyPath
		{
			get { return Path.Combine(Application.StartupPath, "DuView.recently"); }
		}

		//
		public static int MagneticDockSize
		{
			get => _magnetic_dock_size;
			set
			{
				if (value != _magnetic_dock_size)
				{
					_magnetic_dock_size = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetInt("MagneticDockSize", value);
				}
			}
		}

		//
		public static string LastFolder
		{
			get => _last_folder;
			set
			{
				if (!value.Equals(_last_folder))
				{
					_last_folder = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetEncodingString("LastFolder", value);
				}
			}
		}

		//
		public static string LastFileName
		{
			get => _last_filename;
			set
			{
				if (!value.Equals(_last_filename))
				{
					_last_filename = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetEncodingString("LastFileName", _last_filename);
				}
			}
		}

		//
		public static bool ViewZoom
		{
			get => _view_zoom;
			set
			{
				if (value != _view_zoom)
				{
					_view_zoom = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetInt("ViewZoom", ViewZoom ? 1 : 0);
				}
			}
		}

		//
		public static Types.ViewMode ViewMode
		{
			get => _view_mode;
			set
			{
				if (value != _view_mode)
				{
					_view_mode = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetInt("ViewMode", (int)ViewMode);
				}
			}
		}

		//
		public static Types.ViewQuality ViewQuality
		{
			get => _view_quality;
			set
			{
				if (value != _view_quality)
				{
					_view_quality = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetInt("ViewQuality", (int)ViewQuality);
				}
			}
		}

		//
		public static long MaxCacheSize
		{
			get => _max_cache_size;
			set
			{
				if (value != _max_cache_size)
				{
					_max_cache_size = value;
					using (var rk = new RegKey(_keyname, true))
						rk.SetLong("MaxCacheSize", MaxCacheSize);
				}
			}
		}

		//
		public static int GetRecentlyPage(string onlyfilename)
		{
			if (string.IsNullOrEmpty(onlyfilename))
				return 0;

			var s = Converter.EncodingString(onlyfilename);
			var v = _recently.Get(s);

			return string.IsNullOrEmpty(v) ? 0 : Convert.ToInt32(v);
		}

		//
		public static void SetRecentlyPage(string onlyfilename, int page)
		{
			if (!string.IsNullOrEmpty(onlyfilename))
			{
				var s = Converter.EncodingString(onlyfilename);

				if (page > 0)
				{
					var v = page.ToString();

					_recently.Set(s, v);
					_recently.ResizeCutBeginSlowly(_max_recently);
				}
				else
				{
					// 페이지가 0 이면 저장할 필요가 없쟎음
					_recently.Remove(s);
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
			var rfn = RecentlyPath;
			_recently.SaveToFile(rfn, System.Text.Encoding.UTF8, "# DuView recently files");
		}
	}
}

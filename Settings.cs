using DuLib;
using DuLib.Platform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuView
{
	internal static class Settings
	{
		private static readonly string _keyname = @"PuruLive\DuView";

		public static Rectangle Window { get; set; } = new Rectangle();
		public static string LastFolder { get; set; } = string.Empty;

		public static string LastFileName { get; set; } = string.Empty;
		public static int LastFilePage { get; set; } = 0;

		public static bool ViewZoom { get; set; } = true;
		public static Types.ViewMode ViewMode { get; set; } = Types.ViewMode.FitWidth;

		public static void WhenLoad(Form form)
		{
			Window = new Rectangle(form.Location, form.Size);
			LastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

			using (RegKey rk = new RegKey(_keyname))
			{
				if (rk.IsOpen)
				{
					string v;
					int n;

					v = rk.GetString("Window");
					if (!string.IsNullOrEmpty(v))
					{
						string[] ss = v.Split(',');
						if (ss.Length == 4)
						{
							Window = new Rectangle(
								int.Parse(ss[0]),
								int.Parse(ss[1]),
								int.Parse(ss[2]),
								int.Parse(ss[3]));
						}
					}

					v = rk.GetDecodingString("LastFolder");
					if (!string.IsNullOrEmpty(v) && Directory.Exists(v))
						LastFolder = v;

					v = rk.GetDecodingString("LastFile");
					if (!string.IsNullOrEmpty(v))
						(LastFileName, LastFilePage) = ConvertFileString(v);

					ViewZoom = rk.GetInt("ViewZoom", ViewZoom ? 1 : 0) != 0;

					n = rk.GetInt("ViewMode", (int)ViewMode);
					ViewMode = (Types.ViewMode)n;
				}
			}
		}

		public static void WhenClose(Form form)
		{
			Window = new Rectangle(form.Location, form.Size);

			using (RegKey rk = new RegKey(_keyname, true))
			{
				rk.SetString("Window", $"{Window.X},{Window.Y},{Window.Width},{Window.Height}");
				rk.SetEncodingString("LastFolder", LastFolder);
				rk.SetEncodingString("LastFile", ConvertRegString(LastFileName, LastFilePage));
				rk.SetInt("ViewZoom", ViewZoom ? 1 : 0);
				rk.SetInt("ViewMode", (int)ViewMode);
			}
		}

		public static int GetRecentlyPage(string onlyfilename)
		{
			// 해야함
			return 0;
		}

		private static (string filename, int line) ConvertFileString(string s)
		{
			var n = s.IndexOf('|');
			if (n < 0)
				return (string.Empty, 0);

			var line = Converter.ToInt(s.Substring(0, n));
			var filename = s.Substring(n + 1);

			return (filename, line);
		}

		private static string ConvertRegString(string filename, int line)
		{
			return string.IsNullOrEmpty(filename) ? string.Empty : $"{line}|{filename}";
		}
	}
}

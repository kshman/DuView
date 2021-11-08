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

		public static void WhenLoad(Form form)
		{
			Window = new Rectangle(form.Location, form.Size);
			LastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

			using (RegKey rk = new RegKey(_keyname))
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
			}
		}
	}
}

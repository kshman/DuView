using System;
using System.Windows.Forms;
using Du;
using Du.WinForms;

namespace DuView
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var args = Environment.GetCommandLineArgs();
			var filename = args.Length > 1 ? args[1] : string.Empty;

			Settings.WhenBeforeStart();

			if (Settings.GeneralRunOnce && HasProductProcess(filename))
				return;

			Settings.WhenAfterStart();
			Settings.InitLocale();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ReadForm(filename));
		}

		static bool HasProductProcess(string filename)
		{
			var prcs = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);

			if (prcs.Length < 2)
				return false;
			else
			{
				var p = prcs[0].Id == System.Diagnostics.Process.GetCurrentProcess().Id ? prcs[1] : prcs[0];
				var h = p.MainWindowHandle;

				FormDu.ShowIfIconic(h);
				FormDu.SetForeground(h);

				if (!string.IsNullOrEmpty(filename))
				{
					var enc = Converter.EncodingString(filename);
					FormDu.SendCopyDataString(h, enc);
				}
			}

			return true;
		}
	}
}

// 폼데이터 일괄 변환
// Text = "\{(\d{4})\}";
// Text = "\{(\d{1,4})\}";
// Text = Du.Globalization.Locale.Text($1);

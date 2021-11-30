global using Du;
global using Du.Data;
global using Du.Globalization;
global using Du.Platform;
global using Du.WinForms;
global using System;
global using System.Windows.Forms;

namespace DuView
{
	internal static class Program
	{
		/// <summary>
		/// 해당 애플리케이션의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var args = Environment.GetCommandLineArgs();
			var filename = args.Length > 1 ? args[1] : string.Empty;

			if (HasProductProcess(filename))
				return;

			Settings.WhenStart();
			Settings.InitLocale();

			ApplicationConfiguration.Initialize();
			Application.Run(new ReadForm(filename));
		}

		private static bool HasProductProcess(string filename)
		{
			var prcs = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);

			if (prcs.Length < 2)
				return false;
			else
			{
				var p = prcs[0].Id == Environment.ProcessId ? prcs[1] : prcs[0];
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

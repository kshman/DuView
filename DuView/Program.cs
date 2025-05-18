global using System;
global using System.Windows.Forms;
global using DuView.Properties;
global using DuView.Dowa;
global using DuView.Chaek;

namespace DuView
{
	internal static class Program
	{
		/// <summary>
		/// 해당 애플리케이션의 주 진입점입니다.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			var args = Environment.GetCommandLineArgs();
			var filename = args.Length > 1 ? args[1] : string.Empty;

			Settings.MainBefore();
			if (Settings.GeneralRunOnce && HasProductProcess(filename))
				return;
			Settings.MainAfter();

			ApplicationConfiguration.Initialize();
			Application.Run(new Forms.ReadForm(filename));
		}

		private static bool HasProductProcess(string filename)
		{
			var ps = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);

			if (ps.Length < 2)
				return false;
			
			var p = ps[0].Id == Environment.ProcessId ? ps[1] : ps[0];
			var h = p.MainWindowHandle;

			DuForm.ShowIfIconic(h);
			DuForm.SetForeground(h);

			if (string.IsNullOrEmpty(filename))
				return true;
				
			var enc = Alter.EncodingString(filename);
			if (enc != null)
				DuForm.SendCopyDataString(h, enc);

			return true;
		}
	}
}

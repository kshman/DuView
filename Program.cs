using Du.WinForms;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
			var filename = args.Length > 1 ? args[1] : null;

			if (HasProductProcess(filename))
				return;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(filename));
		}

		static bool HasProductProcess(string filename)
		{
			var prcs = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);

			if (prcs == null || prcs.Length == 1)
				return false;
			else if (prcs.Length == 2)
			{
				var p = prcs[0].Id == System.Diagnostics.Process.GetCurrentProcess().Id ? prcs[1] : prcs[0];
				IntPtr h = p.MainWindowHandle;
				
				ControlDu.ShowIfIconic(h);
				ControlDu.SetForeground(h);

				var enc = Du.Converter.EncodingString(filename);
				ControlDu.SendCopyDataString(h, enc);
			}

			return true;
		}
	}
}

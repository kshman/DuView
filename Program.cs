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
#if false
			bool ret;
			Mutex m = new Mutex(true, "PurLiveDuView", out var flag);
			return !ret;
#else
			var prcs = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
			if (prcs == null || prcs.Length == 1)
				return false;
			else if (prcs.Length == 2)
			{
				var p = prcs[0].Id == System.Diagnostics.Process.GetCurrentProcess().Id ? prcs[1] : prcs[0];
				IntPtr h = p.MainWindowHandle;
				if (NativeMethods.IsIconic(h))
					NativeMethods.ShowWindowAsync(h, NativeMethods.SW_RESTORE);
				NativeMethods.SetForegroundWindow(h);

				// https://iwoohaha.tistory.com/90
				var enc = DuLib.Converter.EncodingString(filename);
				NativeMethods.CopyDataStruct s = new NativeMethods.CopyDataStruct();
				try
				{
					s.cbData = (enc.Length + 1) * 2;
					s.lpData = NativeMethods.LocalAlloc(0x40, s.cbData);
					Marshal.Copy(enc.ToCharArray(), 0, s.lpData, enc.Length);
					s.dwData = (IntPtr)1;
					NativeMethods.SendMessage(h, NativeMethods.WM_COPYDATA, IntPtr.Zero, ref s);
				}
				finally
				{
					s.Dispose();
				}
			}

			return true;
#endif
		}
	}
}

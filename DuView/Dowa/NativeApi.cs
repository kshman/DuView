using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace DuView.Dowa;

internal static partial class NativeApi
{
    internal const int WM_COPYDATA = 0x004A;
    internal const int WM_WINDOWPOSCHANGING = 0x0046;
    internal const int WM_HSCROLL = 0x114;
    internal const int WM_VSCROLL = 0x115;
    internal const int WM_SIZE = 0x05;
    internal const int WM_NOTIFY = 0x4E;

    internal const int SW_RESTORE = 9;


    // 윈도우 위치 설정용
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPos
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int flags;
    }

    // CopyData 구조체 처리
    internal struct WmCopyData : IDisposable
    {
        private IntPtr LpData;

        public void Dispose()
        {
            if (LpData == IntPtr.Zero)
                return;

            _ = LocalFree(LpData);
            LpData = IntPtr.Zero;
        }

        public static string? Receive(IntPtr lparam)
        {
            var s = Marshal.PtrToStructure<WmCopyData>(lparam);
            var v = Marshal.PtrToStringUni(s.LpData);
            return v;
        }

        public void Send(IntPtr handle, string value)
        {
            Dispose();

            LpData = LocalAlloc(0x40, (value.Length + 1) * 2);
            Marshal.Copy(value.ToCharArray(), 0, LpData, value.Length);
            
            _ = SendMessage(handle, WM_COPYDATA, IntPtr.Zero, ref this);
        }
    }

    [LibraryImport("user32.dll", SetLastError = true)]
    internal static partial IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref WmCopyData lParam);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetForegroundWindow(IntPtr hWnd);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool IsIconic(IntPtr hWnd);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr LocalAlloc(int flag, int size);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr LocalFree(IntPtr p);
}

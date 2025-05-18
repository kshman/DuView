using System.Reflection;
using System.Runtime.InteropServices;

namespace DuView.Dowa;

internal static class FormerValues
{
    internal static readonly double[] EffectOpacityFaceIn = [0.1, 0.3, 0.7, 0.8, 0.9, 1.0];
    internal static readonly double[] EffectOpacityFaceOut = [0.9, 0.8, 0.7, 0.3, 0.1, 0.0];
}

/// <summary>
/// 컨트롤 도움
/// </summary>
public static class DuControl
{
    /// <summary>
    /// 컨트롤의 더블버퍼링 켬끔
    /// </summary>
    /// <param name="control"></param>
    /// <param name="enabled"></param>
    public static void DoubleBuffered(Control control, bool enabled)
    {
        var prop = control.GetType().GetProperty(
            "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        prop?.SetValue(control, enabled, null);
    }

    /// <summary>
    /// 컨트롤 페이드 인
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="interval"></param>
    public static void EffectFadeIn(Control ctrl, int interval = 120)
    {
        var count = 0;
        var timer = new System.Windows.Forms.Timer()
        {
            Interval = interval / FormerValues.EffectOpacityFaceIn.Length,
        };

        var fg = ctrl.ForeColor;
        var bg = ctrl.BackColor;

        ctrl.ForeColor = Color.Transparent;
        ctrl.BackColor = Color.Transparent;
        ctrl.Visible = true;

        timer.Tick += (_, _) =>
        {
            if ((count + 1) > FormerValues.EffectOpacityFaceIn.Length)
            {
                ctrl.ForeColor = fg;
                ctrl.BackColor = bg;

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            else
            {
                var d = FormerValues.EffectOpacityFaceIn[count++];
                var u = (int)(d * 255.0);

                ctrl.ForeColor = Color.FromArgb(u, fg);
                ctrl.BackColor = Color.FromArgb(u, bg);
            }
        };
        timer.Start();
    }

    /// <summary>
    /// 컨트롤 페이드 아웃
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="interval"></param>
    public static void EffectFadeOut(Control ctrl, int interval = 120)
    {
        var count = 0;
        var timer = new System.Windows.Forms.Timer()
        {
            Interval = interval / FormerValues.EffectOpacityFaceOut.Length,
        };

        var fg = ctrl.ForeColor;
        var bg = ctrl.BackColor;

        ctrl.Visible = true;

        timer.Tick += (_, _) =>
        {
            if ((count + 1) > FormerValues.EffectOpacityFaceOut.Length)
            {
                ctrl.Visible = false;
                ctrl.ForeColor = fg;
                ctrl.BackColor = bg;

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            else
            {
                var d = FormerValues.EffectOpacityFaceIn[count++];
                var u = (int)(d * 255.0);

                ctrl.ForeColor = Color.FromArgb(u, fg);
                ctrl.BackColor = Color.FromArgb(u, bg);
            }
        };
        timer.Start();
    }
}

/// <summary>
/// 폼 도움
/// </summary>
public static class DuForm
{
    /// <summary>
    /// 스르륵 나타나기 이펙트
    /// </summary>
    /// <param name="form"></param>
    public static void EffectAppear(Form form)
    {
        var count = 0;

        var timer = new System.Windows.Forms.Timer()
        {
            Interval = 20,
        };

        form.RightToLeftLayout = false;
        form.Opacity = 0d;

        timer.Tick += (_, _) =>
        {
            if ((count + 1 > FormerValues.EffectOpacityFaceIn.Length))
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            else
            {
                form.Opacity = FormerValues.EffectOpacityFaceIn[count++];
            }
        };
        timer.Start();
    }

    /// <summary>
    /// 최상단 윈도우로
    /// </summary>
    /// <param name="handle"></param>
    public static void SetForeground(IntPtr handle) =>
        NativeApi.SetForegroundWindow(handle);

    /// <summary>
    /// 최상단 윈도우로
    /// </summary>
    /// <param name="form"></param>
    public static void SetForeground(Form form) =>
        SetForeground(form.Handle);

    /// <summary>
    /// 아이콘이면 보이게한다
    /// </summary>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static bool ShowIfIconic(IntPtr handle)
    {
        if (!NativeApi.IsIconic(handle))
            return false;
        else
        {
            NativeApi.ShowWindowAsync(handle, NativeApi.SW_RESTORE);
            return true;
        }
    }

    /// <summary>
    /// 아이콘이면 보이게한다
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public static bool ShowIfIconic(Form form) =>
        ShowIfIconic(form.Handle);

    /// <summary>
    /// CopyData로 문자열을 보낸다
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="value"></param>
    public static void SendCopyDataString(IntPtr handle, string value)
    {
        var d = new NativeApi.WmCopyData();
        try
        {
            d.Send(handle, value);
        }
        finally
        {
            d.Dispose();
        }
    }

    /// <summary>
    /// CopyData로 문자열을 보낸다
    /// </summary>
    /// <param name="control"></param>
    /// <param name="value"></param>
    public static void SendCopyDataString(Control control, string value) =>
        SendCopyDataString(control.Handle, value);

    /// <summary>
    /// CopyData로 온 문자열을 받는다
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool ReceiveCopyDataString(ref Message msg, out string? value)
    {
        if (msg.Msg != NativeApi.WM_COPYDATA)
        {
            value = null;
            return false;
        }
        else
        {
            value = NativeApi.WmCopyData.Receive(msg.LParam);
            return true;
        }
    }

    /// <summary>
    /// 자석 도킹 기능을 수행한다
    /// </summary>
    /// <param name="msg">WndProc의 메시지</param>
    /// <param name="form">수행할 폼</param>
    /// <param name="margin">자석으로 붙일 감쇠거리</param>
    public static void MagneticDockForm(ref Message msg, Form form, int margin)
    {
        if (msg.Msg != NativeApi.WM_WINDOWPOSCHANGING)
            return;

        var desktop = (Screen.FromHandle(form.Handle)).WorkingArea;
        var pos = Marshal.PtrToStructure<NativeApi.WindowPos>(msg.LParam);

        // 왼쪽
        if (Math.Abs(pos.x - desktop.Left) < margin)
            pos.x = desktop.Left;

        // 오른쪽
        if (Math.Abs((pos.x + pos.cx) - (desktop.Left + desktop.Width)) < margin)
            pos.x = desktop.Right - pos.cx;

        // 위쪽
        if (Math.Abs(pos.y - desktop.Top) < margin)
            pos.y = desktop.Top;

        // 아래쪽
        if (Math.Abs((pos.y + pos.cy) - (desktop.Top + desktop.Height)) < margin)
            pos.y = desktop.Bottom - form.Bounds.Height;

        Marshal.StructureToPtr(pos, msg.LParam, false);
        msg.Result = IntPtr.Zero;
    }
}
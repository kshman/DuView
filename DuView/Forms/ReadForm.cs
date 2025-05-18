namespace DuView.Forms;

/// <summary>
/// 읽기 폼
/// </summary>
public partial class ReadForm : Form
{
    private Bitmap? _bmp;
    private readonly string _init_filename;
    private Rectangle[] _paging_bound = new Rectangle[2];

    private readonly System.Windows.Forms.Timer _notify_timer;

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="filename"></param>
    public ReadForm(string filename)
    {
        InitializeComponent();

        //
        Opacity = 0;
        BookCanvas.Dock = DockStyle.Fill;
        BookCanvas.MouseWheel += BookCanvas_MouseWheel;

        //
        _init_filename = filename;

        _notify_timer = new System.Windows.Forms.Timer { Interval = 5000 };
        _notify_timer.Tick += NotifyTimer_Tick;
    }

    #region 폼 이벤트

    private void ReadForm_Load(object sender, EventArgs e)
    {
        Text = Resources.DuView;
        Settings.MainLoaded(this);
        ApplyViewMenus();
        ResetFocus();

        PaintBook();
        if (!string.IsNullOrEmpty(_init_filename))
            OpenBook(_init_filename);
    }

    private void ReadForm_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void ReadForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        CleanBook();

        Settings.KeepMoveLocation();
        Settings.KeepLocationSize(WindowState, Location, Size);
        Settings.SaveSettings();
        Settings.SaveFileInfos();
    }

    // 창이 보일 때 -> 서서히 보이게
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        DuForm.EffectAppear(this);
    }

    // 윈도우 프로시저
    protected override void WndProc(ref Message m)
    {
        if (DuForm.ReceiveCopyDataString(ref m, out var s) && !string.IsNullOrEmpty(s))
        {
            // 파일명 받음
            var f = Alter.DecodingString(s);
            if (!string.IsNullOrEmpty(f))
                OpenBook(f);
            return;
        }

        if (Settings.GeneralUseMagnetic)
        {
            // 자석 윈도우
            DuForm.MagneticDockForm(ref m, this, Settings.MagneticDockSize);
        }

        base.WndProc(ref m);
    }

    private void TurnMaximize()
    {
        var s = WindowState == FormWindowState.Maximized
            ? FormWindowState.Normal
            : FormWindowState.Maximized;
        WindowState = s;
    }

    private void ResetFocus()
    {
        Focus();
        ActiveControl = BookCanvas;
    }

    private void ReadForm_Layout(object sender, LayoutEventArgs e)
    {
        ResetFocus();
    }

    private void ReadForm_ClientSizeChanged(object sender, EventArgs e)
    {
        var rt = BookCanvas.ClientRectangle;
        var w4 = rt.Width / 4;
        _paging_bound[0] = new Rectangle(rt.Left, rt.Top, w4, rt.Height);
        _paging_bound[1] = new Rectangle(rt.Right - w4, rt.Top, w4, rt.Height);

        DrawBook();
    }

    private void ReadForm_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)
            ? DragDropEffects.Link
            : DragDropEffects.None;
    }

    private void ReadForm_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data?.GetData(DataFormats.FileDrop) is string[] { Length: > 0 } files)
        {
            // 여러개가 있어도 하나만 쓴다
            OpenBook(files[0]);
        }
    }

    private void ReadForm_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            // 끝
            case Keys.Escape:
                if (Settings.GeneralEscExit)
                    Close();
                break;

            case Keys.W:
                if (e.Control)
                    CloseBook();
                break;

            // 페이지
            case Keys.Up:
            case Keys.Oemcomma:
                PageControl(BookControl.SeekMinusOne);
                break;

            case Keys.Down:
            case Keys.OemPeriod:
            case Keys.OemQuestion:
                PageControl(BookControl.SeekPlusOne);
                break;

            case Keys.Left:
                PageControl(e.Shift ? BookControl.SeekMinusOne : BookControl.Previous);
                break;

            case Keys.Right:
            case Keys.NumPad0:
            case Keys.Space:
                PageControl(e.Shift ? BookControl.SeekPlusOne : BookControl.Next);
                break;

            case Keys.Home:
                PageControl(BookControl.First);
                break;

            case Keys.End:
                PageControl(BookControl.Last);
                break;

            case Keys.PageUp:
                PageControl(BookControl.SeekPrevious10);
                break;

            case Keys.PageDown:
            case Keys.Back:
                PageControl(BookControl.SeekNext10);
                break;

            case Keys.Enter:
                if (!e.Control)
                    PageControl(BookControl.Select);
                else
                    TurnMaximize();

                break;

            // 보기
            case Keys.M:
                if (e.Control)
                    UpdateViewZoom(!Settings.ViewZoom);
                break;

            case Keys.Y:
                if (e.Control)
                    UpdateViewQuality(ViewQuality.Default);
                break;

            case Keys.Oemtilde:
                UpdateViewMode(ViewMode.Fit);
                break;

            case Keys.D1:
                UpdateViewMode(ViewMode.LeftToRight);
                break;

            case Keys.D2:
                UpdateViewMode(ViewMode.RightToLeft);
                break;

            case Keys.Tab:
                if (Settings.Book != null) // 혼란 방지: 책이 있을때만
                {
                    switch (Settings.ViewMode)
                    {
                        case ViewMode.LeftToRight:
                            UpdateViewMode(ViewMode.RightToLeft);
                            break;
                        case ViewMode.RightToLeft:
                            UpdateViewMode(ViewMode.LeftToRight);
                            break;
                        case ViewMode.Fit:
                        case ViewMode.Follow:
                        default:
                            break;
                    }
                }

                break;

            // 파일이나 디렉토리
            case Keys.BrowserBack:
            case Keys.OemOpenBrackets:
                OpenPrevBook();
                break;

            case Keys.BrowserForward:
            case Keys.OemCloseBrackets:
                OpenNextBook();
                break;

            case Keys.Add:
            case Keys.Oemplus:
            case Keys.Insert:
                MoveBook();
                break;

            case Keys.OemQuotes: // Oem7
                if (e.Control)
                    SaveRememberBook();
                break;
            case Keys.OemSemicolon: // Oem1
                if (e.Control)
                    OpenRememberBook();
                break;

            // 기능
            case Keys.F:
                if (!e.Alt) // ALT가 눌리면 ALT+F가 호출되야 하기 때문에
                    TurnMaximize();
                break;

#if DEBUG && true
            default:
                System.Diagnostics.Debug.WriteLine($"키코드: {e.KeyCode}");
                break;
#endif
        }
    }

    #endregion

    #region 캔바스 이벤트

    private void BookCanvas_MouseDown(object sender, MouseEventArgs e)
    {
        if (Settings.MouseUseClickPage && Settings.Book != null)
        {
            if (_paging_bound[0].Contains(e.Location))
            {
                PageControl(BookControl.Previous);
                return;
            }

            if (_paging_bound[1].Contains(e.Location))
            {
                PageControl(BookControl.Next);
                return;
            }
        }

        // 드래그 처리 여기서 해야하능데...

        if (e.Clicks != 2)
            return;

        if (Settings.Book != null)
        {
            // 책이 있으면 최대화
            if (Settings.MouseUseDoubleClickState)
                TurnMaximize();
            return;
        }

        // 책이 없으면 열기
        OpenDialogForBook();
    }

    private void BookCanvas_MouseUp(object sender, MouseEventArgs e)
    {
        // 드래그 처리 끝을 여기서...
    }

    private void BookCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (Settings.MouseUseClickPage && Settings.Book != null)
        {
            if (_paging_bound[0].Contains(e.Location))
                BookCanvas.Cursor = Cursors.PanWest;
            else if (_paging_bound[1].Contains(e.Location))
                BookCanvas.Cursor = Cursors.PanEast;
            else
                BookCanvas.Cursor = Cursors.Default;
        }

        // 드래그 처리 이동

        // 마우스 슬립 처리
    }

    private void BookCanvas_MouseWheel(object? sender, MouseEventArgs e)
    {
        switch (e.Delta)
        {
            // 위 + / 아래 -
            case > 0:
                PageGoPrev();
                break;
            case < 0:
                PageGoNext();
                break;
        }
        
        // 마우스 슬립 처리
    }

    #endregion

    #region 메뉴

    // 메뉴 설정
    private void ApplyViewMenus()
    {
        TopMost = Settings.GeneralAlwaysTop;
        UpdateViewZoom(Settings.ViewZoom);
        UpdateViewMode(CurrentViewMode);
        UpdateViewQuality(Settings.ViewQuality);
    }

    // 줌 상태
    private void UpdateViewZoom(bool zoom, bool redraw = true)
    {
        Settings.ViewZoom = zoom;
        cmiViewZoom.Checked = zoom;
        if (redraw)
            DrawBook();
    }

    private static readonly Bitmap[] s_view_mode_icon =
    [
        Resources.viewmode_pitwidth,
        Resources.viewmode_pitwidth,
        Resources.viewmode_l2r,
        Resources.viewmode_r2l
    ];

    // 뷰 모드
    private void UpdateViewMode(ViewMode mode, bool redraw = true)
    {
        Settings.ViewMode = mode;
        cmiViewByWidth.Checked = mode == ViewMode.Fit;
        cmiViewFromLeft.Checked = mode == ViewMode.LeftToRight;
        cmiViewFromRight.Checked = mode == ViewMode.RightToLeft;

        cmiView.Image = s_view_mode_icon[(int)mode];

        if (!redraw)
            return;
        Settings.Book?.PrepareImages();
        DrawBook();
    }

    // 퀄리티 상태
    private void UpdateViewQuality(ViewQuality quality, bool redraw = true)
    {
        Settings.ViewQuality = quality;
        cmiQtLow.Checked = quality == ViewQuality.Low;
        cmiQtNormal.Checked = quality == ViewQuality.Default;
        cmiQtBilinear.Checked = quality == ViewQuality.Bilinear;
        cmiQtBicubic.Checked = quality == ViewQuality.Bicubic;
        cmiQtHigh.Checked = quality == ViewQuality.High;
        cmiQtHighBilinear.Checked = quality == ViewQuality.HqBilinear;
        cmiQtHighBicubic.Checked = quality == ViewQuality.HqBicubic;

        if (redraw)
            DrawBook();
    }

    private void cmiFileOpen_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileLast_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFilePrev_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileNext_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileClose_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileExternal_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileRename_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileMove_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiFileDelete_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtLow_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtNormal_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtBilinear_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtBicubic_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtHigh_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtHighBilinear_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiQtHighBicubic_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewCopy_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewZoom_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewByWidth_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewByHeight_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewFromLeft_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiViewFromRight_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiPageGoto_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiPageHome_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiPageEnd_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiPagePrevTen_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiPageNextTen_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiSetting_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void cmiExit_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region 알림

    private void NotifyTimer_Tick(object? sender, EventArgs e)
    {
        _notify_timer.Stop();
        DuControl.EffectFadeOut(NotifyLabel);
    }

    #endregion

    #region 책 처리

    // 현재 뷰 모드
    private Chaek.ViewMode CurrentViewMode
    {
        get
        {
            if (Settings.Book == null || Settings.Book.ViewMode == ViewMode.Follow)
                return Settings.ViewMode;
            return Settings.Book.ViewMode;
        }
    }

    private void OpenDialogForBook()
    {
    }

    private void OpenBook(string filename)
    {
    }

    private void OpenPrevBook()
    {
    }

    private void OpenNextBook()
    {
    }

    private void OpenRememberBook()
    {
    }

    private void SaveRememberBook()
    {
    }

    private void CloseBook()
    {
    }

    private void CleanBook()
    {
    }

    private void MoveBook()
    {
    }

    private void PageControl(BookControl ctrl)
    {
    }

    private void PageGoPrev()
    {
        
    }
    
    private void PageGoNext()
    {
    }

    private void DrawBook()
    {
    }

    private void PaintBook()
    {
    }

    #endregion
}
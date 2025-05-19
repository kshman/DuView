// ReSharper disable MissingXmlDoc

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace DuView.Forms;

/// <summary>
/// 읽기 폼
/// </summary>
public partial class ReadForm : Form
{
    private readonly DuFormWorker _dfw;

    private Bitmap? _bmp;
    private readonly Rectangle[] _paging_bound = new Rectangle[2];

    private readonly System.Windows.Forms.Timer _notify_timer;
    private readonly string _init_filename;
    private readonly PageForm _page_form;

    public ReadForm(string filename)
    {
        InitializeComponent();

        //
        Opacity = 0;
        BookCanvas.Dock = DockStyle.Fill;
        BookCanvas.MouseWheel += BookCanvas_MouseWheel;

        //
        _dfw = new DuFormWorker(this)
        {
            BodyAsTitle = true,
            MoveTopToMaximize = false,
        };
        _init_filename = filename;

        _page_form = new PageForm();

        _notify_timer = new System.Windows.Forms.Timer { Interval = 5000 };
        _notify_timer.Tick += NotifyTimer_Tick;
    }

    #region 폼 이벤트

    private void ReadForm_Load(object sender, EventArgs e)
    {
        RefreshTitle();
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
        if (_dfw.WndProc(ref m))
            return;

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
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
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
                if (e.Control)
                    OpenPrevBook();
                else
                    PageControl(BookControl.SeekPrevious10);
                break;

            case Keys.PageDown:
            case Keys.Back:
                if (e is { Control: true, KeyCode: Keys.PageDown })
                    OpenNextBook();
                else
                    PageControl(BookControl.SeekNext10);
                break;

            case Keys.Enter:
                PageControl(BookControl.Select);
                break;

            // 보기
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
                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                    switch (Settings.ViewMode)
                    {
                        case ViewMode.LeftToRight:
                            UpdateViewMode(ViewMode.RightToLeft);
                            break;
                        case ViewMode.RightToLeft:
                            UpdateViewMode(ViewMode.LeftToRight);
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
                    _dfw.ToggleFullScreen();
                break;

            case Keys.F11:
#if DEBUG
                Notify("알림 메시지 테스트이와요~");
#endif
                break;

            default:
#if DEBUG && true
                System.Diagnostics.Debug.WriteLine($"키코드: {e.KeyCode}");
#endif
                break;
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

        if (_dfw.BodyAsTitle && _dfw.DragOnDown(e))
            return;

        if (e.Clicks != 2)
            return;

        if (Settings.Book == null)
            OpenDialogForBook();
        else if (Settings.MouseUseDoubleClickState)
            _dfw.Maximize();
    }

    private void BookCanvas_MouseUp(object sender, MouseEventArgs e)
    {
        if (_dfw.BodyAsTitle)
            _dfw.DragOnUp(e);
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

        if (_dfw.BodyAsTitle)
            _dfw.DragOnMove(e);

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
        ViewZoomMenuItem.Checked = zoom;
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
        ViewFitMenuItem.Checked = mode == ViewMode.Fit;
        ViewLeftMenuItem.Checked = mode == ViewMode.LeftToRight;
        ViewRightMenuItem.Checked = mode == ViewMode.RightToLeft;

        ViewMenuItem.Image = s_view_mode_icon[(int)mode];

        if (!redraw)
            return;

        Settings.Book?.PrepareImages();
        DrawBook();
    }

    // 퀄리티 상태
    private void UpdateViewQuality(ViewQuality quality, bool redraw = true)
    {
        Settings.ViewQuality = quality;
        QtLowMenuItem.Checked = quality == ViewQuality.Low;
        QtNormalMenuItem.Checked = quality == ViewQuality.Default;
        QtBilinearMenuItem.Checked = quality == ViewQuality.Bilinear;
        QtBicubicMenuItem.Checked = quality == ViewQuality.Bicubic;
        QtHighMenuItem.Checked = quality == ViewQuality.High;
        QtHighBilinearMenuItem.Checked = quality == ViewQuality.HqBilinear;
        QtHighBicubicMenuItem.Checked = quality == ViewQuality.HqBicubic;

        if (redraw)
            DrawBook();
    }

    private void FileOpenMenuItem_Click(object sender, EventArgs e)
    {
        OpenDialogForBook();
    }

    private void FileLastMenuItem_Click(object sender, EventArgs e)
    {
        var book = Settings.Book;
        if (book != null && book.FileName == Settings.LastFileName)
            return;
        // TODO: 원래 여기 패스코드 프롬프트
        OpenBook(Settings.LastFileName);
    }

    private void FilePrevMenuItem_Click(object sender, EventArgs e)
    {
        // TODO: OpenPrevBook();
    }

    private void FileNextMenuItem_Click(object sender, EventArgs e)
    {
        // TODO: OpenNextBook();
    }

    private void FileCloseMenuItem_Click(object sender, EventArgs e)
    {
        CloseBook();
    }

    private void FileExternalMenuItem_Click(object sender, EventArgs e)
    {
        OpenExternalRun();
    }

    private void FileRenameMenuItem_Click(object sender, EventArgs e)
    {
        RenameBook();
    }

    private void FileMoveMenuItem_Click(object sender, EventArgs e)
    {
        MoveBook();
    }

    private void FileDeleteMenuItem_Click(object sender, EventArgs e)
    {
        DeleteBookOrItem();
    }

    private void QualityMenuItems_Click(object sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem { Tag: not null } i)
            return;
        var q = (ViewQuality)i.Tag;
        UpdateViewQuality(q);
    }

    private void ViewCopyMenuItem_Click(object sender, EventArgs e)
    {
        var book = Settings.Book;
        try
        {
            if (book is not { PageLeft: not null })
                return;
            Clipboard.SetImage(book.PageLeft.Image);
            Notify("클립보드로 복사했어요");
        }
        catch
        {
            Notify("클립보드에 넣을 수 없어요!");
        }
    }

    private void ViewZoomMenuItem_Click(object sender, EventArgs e)
    {
        UpdateViewZoom(!ViewZoomMenuItem.Checked);
    }

    private void ViewModeMenuItems_Click(object sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem { Tag: not null } i)
            return;
        var m = (ViewMode)i.Tag;
        UpdateViewMode(m);
    }

    private void PageMenuItems_Click(object sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem { Tag: not null } i)
            return;
        var c = (BookControl)i.Tag;
        PageControl(c);
    }

    private void SettingMenuItem_Click(object sender, EventArgs e)
    {
        // TODO: 세팅창 띄우기 
        // TODO: 패스코드 적용
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region 알림

    private void NotifyTimer_Tick(object? sender, EventArgs e)
    {
        _notify_timer.Stop();
        DuControl.EffectFadeOut(NotifyLabel);
    }

    private void Notify(string message, int timeout = 2000)
    {
        NotifyLabel.Text = message;

        if (NotifyLabel.Visible)
            _notify_timer.Stop();
        else
        {
            NotifyLabel.Location = NotifyLabel.Location with
            {
                Y = (BookCanvas.Height - NotifyLabel.Height) / 2
            };
            DuControl.EffectFadeIn(NotifyLabel);
        }

        _notify_timer.Interval = timeout;
        _notify_timer.Start();
    }

    #endregion

    #region 파일 처리

    // 페이지 번호 표시
    private void RefreshTitle(int page = -1)
    {
        StringBuilder sb = new();
        sb.Append(Resources.DuView);

        var book = Settings.Book;
        if (book != null)
        {
            var cache = Doumi.SizeToString(book.CacheSize);
            sb.Append(" - ");
            sb.Append(book.OnlyFileName);
            sb.Append($@" ({page + 1}/{book.TotalPage})");
            sb.Append($" [{cache}]");
        }

        Text = sb.ToString();
    }

    // 책 닫기 공통
    private void CleanBook()
    {
        StopAnimation();

        var book = Settings.Book;
        if (book != null)
        {
            Settings.SetRecentlyPage(book);
            book.Dispose();
            Settings.Book = null;

            _page_form.ResetBook();
            GC.Collect();
        }

        ResetFocus();
    }

    // 책 닫기
    private void CloseBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        CleanBook();
        DrawBook();

        RefreshTitle();
    }

    // 책 고르기 다이얼로그
    private void OpenDialogForBook()
    {
        var dlg = new OpenFileDialog()
        {
            Title = Resources.OpenBook,
            Filter = Resources.SelectFilter,
            InitialDirectory = Settings.LastFolder,
        };
        if (dlg.ShowDialog(this) == DialogResult.OK)
            OpenBook(dlg.FileName);
    }

    // 책 열기
    private void OpenBook(string filename, int page = -1)
    {
        BookBase? book = null;

        if (File.Exists(filename))
        {
            // 단일 파일 또는 압축 파일
            var fi = new FileInfo(filename);
            var ext = fi.Extension;

            if (ext.IsArchiveExtension())
                book = OpenArchive(fi, ext);
            else if (ext.IsImageExtension())
            {
                var di = fi.Directory;
                if (di != null)
                {
                    book = OpenDirectory(di);
                    if (book is BookFolder folder)
                    {
                        page = folder.GetPageNumber(fi.FullName);
                        folder.ViewMode = ViewMode.Fit;
                    }
                }
            }
        }
        else if (Directory.Exists(filename))
        {
            // 디렉토리
            var di = new DirectoryInfo(filename);
            book = OpenDirectory(di);
        }

        if (book != null)
        {
            CleanBook();
            Settings.Book = book;

            if (page < 0)
                page = Settings.GetRecentlyPage(book.OnlyFileName);
            book.CurrentPage = page;
            book.PrepareImages();

            RefreshTitle(page);
            _page_form.SetBook(book);
            Settings.LastFileName = filename;

            DrawBook();
        }
        else
        {
            Notify(Resources.CannotOpenBook);
        }

        ResetFocus();
    }

    // 압축 파일 읽기
    private BookBase? OpenArchive(FileInfo fi, string ext)
    {
        Settings.LastFolder = fi.DirectoryName ?? string.Empty;

        BookBase? book = ext.ToLower() switch
        {
            ".zip" => BookZip.FromFile(fi.FullName),
            _ => null,
        };

        if (book == null)
            Notify(Resources.UnsupportArchive);
        return book;
    }

    // 디렉토리 읽기
    private BookFolder? OpenDirectory(DirectoryInfo di)
    {
        Settings.LastFolder = di.Parent?.FullName ?? string.Empty;

        var book = BookFolder.FromFolder(di);
        if (book == null)
            Notify(Resources.CannotOpenDirectory);
        return book;
    }

    // 앞쪽 책 열기
    private void OpenPrevBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        var filename = book.FindNextFile(BookDirection.Previous);
        if (string.IsNullOrEmpty(filename))
        {
            Notify(Resources.NoPreviousBook);
            return;
        }

        OpenBook(filename);
    }

    // 뒷쪽 책 열기
    private void OpenNextBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        var filename = book.FindNextFile(BookDirection.Next);
        if (string.IsNullOrEmpty(filename))
        {
            Notify(Resources.NoNextBook);
            return;
        }

        OpenBook(filename);
    }

    // 리멤버 책 열기
    private void OpenRememberBook()
    {
        var filename = Settings.RememberFileName;
        if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
        {
            Notify(Resources.CannotOpenRemember);
            return;
        }

        CloseBook();
        OpenBook(filename);
    }

    // 리멤버 책 저장
    private void SaveRememberBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        Settings.RememberFileName = book.FileName;
        Notify(Resources.RememberFilename);
    }

    // 책이나 파일 지우기
    private void DeleteBookOrItem()
    {
        var book = Settings.Book;
        if (book == null)
            return;
        if (!book.CanDeleteFile(out var reason))
            return;
        if (!string.IsNullOrEmpty(reason) && Settings.GeneralConfirmDelete &&
            MessageBox.Show(this, reason, Resources.DeleteBook, MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
            DialogResult.Yes)
            return;

        var next = book.FindNextFileAny(BookDirection.Next);

        if (!book.DeleteFile(out var closed))
        {
            Notify(Resources.CannotDeleteBook, 3000);
            return;
        }

        if (closed)
        {
            // 책을 지우라는 것
            book.CurrentPage = 0;
            CloseBook();
            if (!string.IsNullOrEmpty(next))
                OpenBook(next);
        }
        else
        {
            // 파일 단위로 처리했다는 것
            // 다음 파일을 위해 다시 그려
            book.PrepareImages();
            DrawBook();
        }
    }

    // 외부 프로그램 실행용 변수
    private string? _external_filename;
    private FormWindowState _external_state;

    // 외부 프로그램 실행
    private void OpenExternalRun()
    {
        if (string.IsNullOrEmpty(Settings.ExternalRun) || !File.Exists(Settings.ExternalRun))
        {
            Notify(Resources.NoExternalRunExist);
            return;
        }

        var book = Settings.Book;
        if (book == null)
            return;

        _external_filename = book.FileName;
        _external_state = WindowState;
        WindowState = FormWindowState.Minimized;
        CloseBook();

        var ps = new System.Diagnostics.Process();
        ps.StartInfo.FileName = Settings.ExternalRun;
        ps.StartInfo.Arguments = _external_filename;
        ps.StartInfo.UseShellExecute = false;
        ps.StartInfo.CreateNoWindow = true;
        ps.EnableRaisingEvents = true;
        ps.Exited += (_, _) => ExternalRun_Exited();
        ps.Start();
    }

    private void ExternalRun_Exited()
    {
        Invoke(method: () =>
        {
            if (Settings.ReloadAfterExternal && !string.IsNullOrEmpty(_external_filename))
                OpenBook(_external_filename);
            WindowState = _external_state;
        });
    }

    // 책 이름 바꾸기
    private void RenameBook(bool exRen = false)
    {
        var book = Settings.Book;
        if (book == null)
            return;

        // TODO: 패스 코드 적용

        // TODO: 다이얼로그 만들고 난 담에 하자
    }

    // 책 이동
    private void MoveBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        // TODO: 패스 코드 적용

        // TODO: 다이얼로그 만들고 난 담에 하자
    }
    #endregion

    #region 쪽 및 애니메이션

    // 현재 뷰 모드
    private static ViewMode CurrentViewMode
    {
        get
        {
            if (Settings.Book == null || Settings.Book.ViewMode == ViewMode.Follow)
                return Settings.ViewMode;
            return Settings.Book.ViewMode;
        }
    }

    // 쪽 컨트롤러
    private void PageControl(BookControl ctrl)
    {
        if (Settings.Book == null)
            return;

        switch (ctrl)
        {
            case BookControl.Previous:
                PageGoPrev();
                break;

            case BookControl.Next:
                PageGoNext();
                break;

            case BookControl.First:
                PageGoTo(0);
                break;

            case BookControl.Last:
                PageGoTo(int.MaxValue);
                break;

            case BookControl.SeekPrevious10:
                PageGoDelta(-10);
                break;

            case BookControl.SeekNext10:
                PageGoDelta(10);
                break;

            case BookControl.SeekMinusOne:
                PageGoDelta(-1);
                break;

            case BookControl.SeekPlusOne:
                PageGoDelta(1);
                break;

            case BookControl.ScanPrevious:
                OpenPrevBook();
                break;

            case BookControl.ScanNext:
                OpenNextBook();

                break;

            case BookControl.Select:
                PageSelect();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(ctrl), ctrl, null);
        }
    }

    // 쪽 이동
    private void PageGoTo(int page)
    {
        var book = Settings.Book;
        if (book == null || !book.MovePage(page))
            return;
        book.PrepareImages();
        DrawBook();
    }

    // 지정한 만큼 쪽 이동
    private void PageGoDelta(int delta)
    {
        var book = Settings.Book;
        if (book == null || !book.MovePage(book.CurrentPage + delta))
            return;
        book.PrepareImages();
        DrawBook();
    }

    // 앞으로 쪽 이동
    private void PageGoPrev()
    {
        var book = Settings.Book;
        if (book == null || !book.MovePrev())
            return;
        book.PrepareImages();
        DrawBook();
    }

    // 뒤로 쪽 이동
    private void PageGoNext()
    {
        var book = Settings.Book;
        if (book == null || !book.MoveNext())
            return;
        book.PrepareImages();
        DrawBook();
    }

    // 쪽 선택
    private void PageSelect()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        if (_page_form.ShowDialog(this, book.CurrentPage) != DialogResult.OK)
            return;

        book.CurrentPage = _page_form.SelectedPage;
        book.PrepareImages();
        DrawBook();
    }

    // 애니메이션 정지
    private void StopAnimation()
    {

    }

    // 애니메이션 업데이트
    private Image UpdateAnimation(PageImage page)
    {
        // 해야함. 일단 현재 이미지 반환
        return page.GetImage();
    }

    #endregion

    #region 그리기

    // 로고
    private static void DrawLogo(Graphics g, int w, int h)
    {
        var img = Resources.housebari_head_128;
        if (w > img.Width && h > img.Height)
            g.DrawImage(img, w - img.Width - 50, h - img.Height - 50);
        else
        {
            var rt = new Rectangle(0, 0, w, h);
            g.DrawImage(img, rt);
        }
    }

    // 가로로 채워 그리기
    private static void DrawBitmapFit(Graphics g, Image bmp, Image img,
        HorizontalAlignment align = HorizontalAlignment.Center)
    {
        var (nw, nh) = Doumi.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
        var rt = Doumi.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);
        g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
    }

    // 두장 그리기
    private static void DrawBitmapHalfHalf(Graphics g, Image bmp, Image l, Image r)
    {
        var f = bmp.Width / 2;

        // 왼쪽
        var (lw, lh) = Doumi.CalcDestSize(Settings.ViewZoom, f, bmp.Height, l.Width, l.Height);
        var lrt = Doumi.CalcDestRect(f, bmp.Height, lw, lh, HorizontalAlignment.Right);
        g.DrawImage(l, lrt, 0, 0, l.Width, l.Height, GraphicsUnit.Pixel);

        // 오른쪽
        var (rw, rh) = Doumi.CalcDestSize(Settings.ViewZoom, f, bmp.Height, r.Width, r.Height);
        var rrt = Doumi.CalcDestRect(f, bmp.Height, rw, rh, HorizontalAlignment.Left);
        rrt.X += f;
        g.DrawImage(r, rrt, 0, 0, r.Width, r.Height, GraphicsUnit.Pixel);
    }

    // 그리기
    private void DrawBook()
    {
        StopAnimation();

        var book = Settings.Book;
        if (book != null)
        {
            RefreshTitle(book.CurrentPage);
            // TODO: 엔트리 타이틀
        }

        //
        if (BookCanvas.Width == 0 || BookCanvas.Height == 0)
            return;

        PaintBook();
    }

    // 화면에 그리기
    private void PaintBook()
    {
        if (WindowState == FormWindowState.Minimized)
            return;

        var (w, h) = (BookCanvas.Width, BookCanvas.Height);
        if (w == 0 || h == 0)
            return;

        if (_bmp == null || _bmp.Width != w || _bmp.Height != h)
        {
            _bmp?.Dispose();
            _bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        }

        var book = Settings.Book;
        using (var g = Graphics.FromImage(_bmp))
        {
            g.Clear(Color.FromArgb(10, 10, 10));
            g.InterpolationMode = Doumi.QualityToInterpolationMode(Settings.ViewQuality);

            if (book == null)
                DrawLogo(g, w, h);
            else
            {
                if (CurrentViewMode == ViewMode.Fit)
                {
                    if (book.PageLeft != null)
                    {
                        var img = UpdateAnimation(book.PageLeft);
                        DrawBitmapFit(g, _bmp, img);
                    }
                }
                else
                {
                    if (book.PageLeft == null)
                    {
                        if (book.PageRight == null)
                        {
                            // 헐 페이지가 없어?
                            DrawLogo(g, w, h);
                        }
                        else
                        {
                            // 오른쪽 한장만
                            DrawBitmapFit(g, _bmp, book.PageRight.Image);
                        }
                    }
                    else if (book.PageRight == null)
                    {
                        // 왼쪽 한장만
                        DrawBitmapFit(g, _bmp, book.PageLeft.Image);
                    }
                    else
                    {
                        // 양쪽 다
                        DrawBitmapHalfHalf(g, _bmp, book.PageLeft.Image, book.PageRight.Image);
                    }
                }
            }

            // 그림 위에 그리려면 여기서 그려야 함
            g.InterpolationMode = InterpolationMode.Default;
        }

        BookCanvas.Image = _bmp;
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        PaintBook();
        base.OnPaint(e);
    }

    #endregion
}
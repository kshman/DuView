using System.Collections.Generic;
using Cairo;
using Gdk;
using System.Text;
using Window = Gtk.Window;
using WindowState = Gdk.WindowState;

namespace DgView.Forms;

internal class ReadWindow : Window
{
    private int _width;
    private int _height;
    private readonly BoundRect[] _paging_bound = new BoundRect[2];

    private readonly System.Timers.Timer _notify_timer;
    private string _notify_text = string.Empty;

    private WindowState _window_state = WindowState.Focused;
    private int _paint_count = 0;

    //
    private readonly DrawingArea _draw;
    private readonly Label _titleLabel;
    private readonly Label _infoLabel;
    private readonly Image _directionImage;
    private readonly MenuItem _menuOpenFile;
    private readonly MenuItem _menuCloseFile;
    private readonly MenuItem _subViewFit;
    private readonly MenuItem _subViewLeftToRight;
    private readonly MenuItem _subViewRightToLeft;
    private readonly CheckMenuItem _subViewZoom;
    private readonly MenuItem _menuSetting;
    private readonly MenuItem _menuExit;

    private readonly Dictionary<string, PixBitmap?> _images = new();

    public ReadWindow() : base("DgView")
    {
        Settings.OnWindowInit(this);

        #region 디자인

        Icon = Doumi.GetResourcePixmap("DgView.Resources.DuView.ico");

        // 타이틀바 + 메뉴
        var header = new HeaderBar();
        header.ShowCloseButton = true;

        _titleLabel = new Label("두그뷰");
        _titleLabel.Halign = Align.Start;
        _titleLabel.Valign = Align.Center;
        _titleLabel.MarginStart = 12;
        header.PackStart(_titleLabel);

        // 메뉴
        _images["test"] = Doumi.GetResourcePixmap("DgView.Resources.000.jpg");
        _images["logo"] = Doumi.GetResourcePixmap("DgView.Resources.housebari_head_128.jpg");
        _images["view_fit"] = Doumi.GetResourcePixmap("DgView.Resources.viewmode_pitwidth.png");
        _images["view_l2r"] = Doumi.GetResourcePixmap("DgView.Resources.viewmode_l2r.png");
        _images["view_r2l"] = Doumi.GetResourcePixmap("DgView.Resources.viewmode_r2l.png");

        var accel = new AccelGroup();
        AddAccelGroup(accel);

        var menu = new Menu();
        _menuOpenFile = new MenuItem("열기");
        _menuOpenFile.Activated += MenuOpenFile_OnActivated;
        _menuOpenFile.AddAccelerator("activate", accel, (uint)Gdk.Key.F3, ModifierType.None, AccelFlags.Visible);
        menu.Append(_menuOpenFile);
        _menuCloseFile = new MenuItem("닫기");
        _menuCloseFile.Activated += MenuClose_OnActivated;
        _menuCloseFile.AddAccelerator("activate", accel, (uint)Gdk.Key.F4, ModifierType.None, AccelFlags.Visible);
        menu.Append(_menuCloseFile);
        menu.Append(new SeparatorMenuItem());
        var menuViewMode = new MenuItem("보기");
        var msubView = new Menu();
        // 아래 세개는 RadioMenuItem으로 바꿔야 한다
        _subViewFit = Doumi.CreateImageMenuItem("화면에 맞춤", _images["view_fit"]);
        _subViewFit.Data["tag"] = ViewMode.Fit; // ViewMode.Fit으로 설정
        _subViewFit.Activated += SubMenuViewMode_OnActivated;
        _subViewFit.AddAccelerator("activate", accel, (uint)Gdk.Key.Key_0, ModifierType.None, AccelFlags.Visible);
        msubView.Append(_subViewFit);
        _subViewLeftToRight = Doumi.CreateImageMenuItem("왼쪽에서 오른쪽으로", _images["view_l2r"]);
        _subViewLeftToRight.Data["tag"] = ViewMode.LeftToRight; // ViewMode.LeftToRight으로 설정
        _subViewLeftToRight.Activated += SubMenuViewMode_OnActivated;
        _subViewLeftToRight.AddAccelerator("activate", accel, (uint)Gdk.Key.Key_1, ModifierType.None,
            AccelFlags.Visible);
        msubView.Append(_subViewLeftToRight);
        _subViewRightToLeft = Doumi.CreateImageMenuItem("오른쪽에서 왼쪽으로", _images["view_r2l"]);
        _subViewRightToLeft.Data["tag"] = ViewMode.RightToLeft; // ViewMode.RightToLeft으로 설정
        _subViewRightToLeft.Activated += SubMenuViewMode_OnActivated;
        _subViewRightToLeft.AddAccelerator("activate", accel, (uint)Gdk.Key.Key_2, ModifierType.None,
            AccelFlags.Visible);
        msubView.Append(_subViewRightToLeft);
        msubView.Append(new SeparatorMenuItem());
        _subViewZoom = new CheckMenuItem("늘려보기");
        _subViewZoom.Active = Settings.ViewZoom;
        _subViewZoom.Activated += SubMenuViewZoom_OnActivated;
        _subViewZoom.AddAccelerator("activate", accel, (uint)Gdk.Key.Key_0, ModifierType.ControlMask,
            AccelFlags.Visible);
        msubView.Append(_subViewZoom);
        msubView.ShowAll();
        menuViewMode.Submenu = msubView;
        menu.Append(menuViewMode);
        _menuSetting = new MenuItem("설정");
        _menuSetting.Activated += (_, _) =>
        {
            /* 설정 열기 동작 */
        };
        _menuSetting.AddAccelerator("activate", accel, (uint)Gdk.Key.F12, ModifierType.None, AccelFlags.Visible);
        menu.Append(_menuSetting);
        menu.Append(new SeparatorMenuItem());
        _menuExit = new MenuItem("종료");
        _menuExit.Activated += (_, _) => { Close(); };
        menu.Append(_menuExit);
        menu.ShowAll();

        var menuButton = new MenuButton();
        menuButton.Popup = menu;
        menuButton.CanFocus = false;
        menuButton.Label = "≡"; // 또는 아이콘 사용

        header.PackEnd(menuButton);

        // 방향 이미지
        _directionImage = new Image();
        _directionImage.Pixbuf = _images["view_fit"];
        _directionImage.MarginStart = 4;
        _directionImage.MarginEnd = 4;
        _directionImage.Halign = Align.Center;
        _directionImage.Valign = Align.Center;
        header.PackEnd(_directionImage);

        // 정보 라벨
        _infoLabel = new Label("----");
        _infoLabel.Halign = Align.Start;
        _infoLabel.Valign = Align.Center;
        _infoLabel.MarginStart = 12;
        _infoLabel.MarginEnd = 12;
        header.PackEnd(_infoLabel);

        Titlebar = header;

        // 그리기 영역
        _draw = new DrawingArea();
        _draw.SetSizeRequest(WidthRequest, HeightRequest);
        _draw.AddEvents((int)EventMask.ScrollMask); // 스크롤 이벤트 활성화
        _draw.ScrollEvent += OnScrollEvent;
        _draw.Drawn += DrawOnDrawn;

        Gtk.Drag.DestSet(_draw, DestDefaults.All, [
            new TargetEntry("text/uri-list", 0, 0)
        ], DragAction.Copy);
        _draw.DragDataReceived += OnDragDataReceived;

        var box = new Box(Orientation.Vertical, 0);
        box.PackStart(_draw, true, true, 0);
        Add(box);

        // 윈도우
        SetSizeRequest(300, 300);
        DeleteEvent += ReadWindow_OnDeleteEvent;
        KeyPressEvent += ReadWindow_OnKeyPressEvent;
        KeyReleaseEvent += ReadWindow_OnKeyReleaseEvent;
        ConfigureEvent += ReadWindow_OnConfigureEvent;
        WindowStateEvent += ReadWindow_OnWindowStateEvent;
        CanFocus = true;
        HasFocus = true;

        #endregion

        _notify_timer = new System.Timers.Timer() { Interval = 5000 };
        _notify_timer.Elapsed += (_, _) =>
        {
            _notify_timer.Stop();
            // 라벨 안보이게
            _notify_text = string.Empty;
            _draw.QueueDraw(); // 다시 그리기 요청
        };

        SetBookInfo();
        ApplyViewMenus();
        ResetFocus();
        ShowAll();
    }

    private void ResetFocus() =>
        GrabFocus();

    #region 이벤트 핸들러

    private void ReadWindow_OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        CleanBook();

        _notify_timer.Stop();
        _notify_timer.Dispose();

        Settings.KeepMoveLocation();
        GetSize(out int width, out var height);
        Window.GetRootOrigin(out var rx, out var ry);
        Settings.KeepLocationSize(rx, ry, width, height);
        Settings.SaveSettings();
        Settings.SaveFileInfos();

        Application.Quit();
    }

    private void ReadWindow_OnConfigureEvent(object o, ConfigureEventArgs args)
    {
        var w4 = args.Event.Width / 4;
        _paging_bound[0].Set(0, 0, w4, args.Event.Height); // 첫 번째 페이지 영역
        _paging_bound[1].Set(args.Event.Width - w4, 0, w4, args.Event.Height); // 두 번째 페이지 영역
    }

    private void ReadWindow_OnWindowStateEvent(object o, WindowStateEventArgs args)
    {
        if ((args.Event.ChangedMask & Gdk.WindowState.Maximized) != 0)
        {
            if ((args.Event.NewWindowState & Gdk.WindowState.Maximized) != 0)
            {
                // 최대화됨
                _window_state |= WindowState.Maximized;
            }
            else
            {
                // 최대화 해제됨
                _window_state &= ~WindowState.Maximized;
            }
        }

        if ((args.Event.ChangedMask & Gdk.WindowState.Iconified) != 0)
        {
            if ((args.Event.NewWindowState & Gdk.WindowState.Iconified) != 0)
            {
                // 최소화됨
                _window_state |= WindowState.Iconified;
            }
            else
            {
                // 최소화 해제됨
                _window_state &= ~WindowState.Iconified;
            }
        }

        _draw.QueueDraw(); // 다시 그리기 요청
    }

    private void OnDragDataReceived(object o, DragDataReceivedArgs args)
    {
        // 드롭된 데이터에서 파일 경로 추출
        var uris = args.SelectionData.Uris;
        if (uris is { Length: > 0 })
        {
            // 여러개가 있어도 하나만 쓴다
            var filename = new Uri(uris[0]).LocalPath;
            OpenBook(filename);
        }

        // 드롭 완료 알림
        Gtk.Drag.Finish(args.Context, true, false, args.Time);
    }

    private ModifierType _prev_state;
    private Gdk.Key _prev_key;

    [GLib.ConnectBefore]
    private void ReadWindow_OnKeyReleaseEvent(object o, KeyReleaseEventArgs args)
    {
        _prev_state = ModifierType.None;
        _prev_key = 0;
    }

    [GLib.ConnectBefore]
    private void ReadWindow_OnKeyPressEvent(object o, KeyPressEventArgs args)
    {
        if (args.Event.Key == _prev_key && args.Event.State == _prev_state)
        {
            // 중복 입력 방지
            return;
        }

        _prev_state = args.Event.State;
        _prev_key = args.Event.Key;

        if (args.Event.State == ModifierType.ControlMask)
        {
            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (args.Event.Key)
            {
                case Gdk.Key.w or Gdk.Key.W:
                    CloseBook();
                    break;

                case Gdk.Key.s or Gdk.Key.S:
                    SaveRememberBook();
                    break;

                case Gdk.Key.r or Gdk.Key.R:
                    OpenRememberBook();
                    break;

                case Gdk.Key.d or Gdk.Key.D:
                    DeleteBookOrItem();
                    break;

                // 페이지
                case Gdk.Key.Up:
                    PageControl(BookControl.SeekPrevious10);
                    break;

                case Gdk.Key.Down:
                    PageControl(BookControl.SeekNext10);
                    break;

                case Gdk.Key.Left:
                    PageControl(BookControl.SeekMinusOne);
                    break;

                case Gdk.Key.Right:
                    PageControl(BookControl.SeekPlusOne);
                    break;

                case Gdk.Key.Page_Up:
                    OpenBook(BookDirection.Previous);
                    break;

                case Gdk.Key.Page_Down:
                    OpenBook(BookDirection.Next);
                    break;
            }
        }
        else if (args.Event.State == ModifierType.ShiftMask)
        {
        }
        else
        {
            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (args.Event.Key)
            {
                // 끝
                case Gdk.Key.Escape:
                    if (Settings.GeneralEscExit)
                        Close();
                    break;

                // 페이지
                case Gdk.Key.Up or Gdk.Key.comma:
                    PageControl(BookControl.SeekMinusOne);
                    break;

                case Gdk.Key.Down or Gdk.Key.period or Gdk.Key.question:
                    PageControl(BookControl.SeekPlusOne);
                    break;

                case Gdk.Key.Left:
                    PageControl(BookControl.Previous);
                    break;

                case Gdk.Key.Right or Gdk.Key.space or Gdk.Key.KP_0 or Gdk.Key.KP_Space:
                    PageControl(BookControl.Next);
                    break;

                case Gdk.Key.Home:
                    PageControl(BookControl.First);
                    break;

                case Gdk.Key.End:
                    PageControl(BookControl.Last);
                    break;

                case Gdk.Key.Page_Up:
                    PageControl(BookControl.SeekPrevious10);
                    break;

                case Gdk.Key.Page_Down:
                    PageControl(BookControl.SeekNext10);
                    break;

                case Gdk.Key.Return:
                case Gdk.Key.KP_Enter:
                    PageControl(BookControl.Select);
                    break;

                case Gdk.Key.Tab:
                    if (Settings.Book != null) // 혼란 방지: 책이 있을 때만
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

                case Gdk.Key.bracketleft:
                    OpenBook(BookDirection.Previous);
                    break;

                case Gdk.Key.bracketright:
                    OpenBook(BookDirection.Next);
                    break;

                case Gdk.Key.plus or Gdk.Key.KP_Add or Gdk.Key.Insert:
                    MoveBook();
                    break;

                case Gdk.Key.Delete:
                    DeleteBookOrItem();
                    break;

                case Gdk.Key.F2:
                    RenameBook();
                    break;

                case Gdk.Key.f or Gdk.Key.F:
                    // 풀스크린
                    break;

#if DEBUG
                case Gdk.Key.F11:
                    Notify("알림 메시지 테스트이와요~");
                    break;
#endif

                default:
#if DEBUG && true
                    System.Diagnostics.Debug.WriteLine($"Key pressed: {args.Event.Key} ({args.Event.State})");
#endif
                    break;
            }
        }
    }

    private void OnScrollEvent(object o, ScrollEventArgs args)
    {
        switch (args.Event.Direction)
        {
            case ScrollDirection.Up: // 휠 위로
                PageGoPrev();
                break;
            case ScrollDirection.Down: // 휠 아래로
                PageGoNext();
                break;
            case ScrollDirection.Left: // 휠 왼쪽
            case ScrollDirection.Right: // 휠 오른쪽
            case ScrollDirection.Smooth: // 부드러운 스크롤?
            default:
                break;
        }
    }

    #endregion

    #region 메뉴 및 액셀 그룹

    private void ApplyViewMenus()
    {
        KeepAbove = Settings.GeneralAlwaysTop;
        UpdateViewZoom(Settings.ViewZoom);
        UpdateViewMode(CurrentViewMode);
    }

    private void UpdateViewZoom(bool zoom, bool redraw = true)
    {
        Settings.ViewZoom = zoom;
        _subViewZoom.Active = zoom;
        if (redraw)
            _draw.QueueDraw(); // 다시 그리기 요청
    }

    private void UpdateViewMode(ViewMode mode, bool redraw = true)
    {
        Settings.ViewMode = mode;
        var icon = mode switch
        {
            ViewMode.Fit => _images["view_fit"],
            ViewMode.LeftToRight => _images["view_l2r"],
            ViewMode.RightToLeft => _images["view_r2l"],
            _ => _images["view_fit"]
        };
        _directionImage.Pixbuf = icon;

        if (!redraw)
            return;

        Settings.Book?.PrepareImages();
        DrawBook();
    }

    private void MenuOpenFile_OnActivated(object? s, EventArgs e)
    {
        OpenBookDialog();
    }

    private void MenuClose_OnActivated(object? s, EventArgs e)
    {
        CloseBook();
    }

    private void SubMenuViewZoom_OnActivated(object? s, EventArgs e)
    {
        UpdateViewZoom(!_subViewZoom.Active);
    }

    private void SubMenuViewMode_OnActivated(object? s, EventArgs e)
    {
        if (s is MenuItem item && item.Data["tag"] is ViewMode mode)
            UpdateViewMode(mode);
    }

    #endregion

    #region 알림

    private void PaintNotify(Context cr)
    {
        if (_notify_text.EmptyString())
            return;

        // 폰트 설정
        cr.SelectFontFace("Serif", FontSlant.Normal, FontWeight.Normal);
        cr.SetFontSize(32);

        // 텍스트 크기 측정
        var te = cr.TextExtents(_notify_text);

        // DrawingArea 크기 구하기 (가운데 배치 예시)
        var areaWidth = _draw.Allocation.Width;
        var areaHeight = _draw.Allocation.Height;

        var x = (areaWidth - te.Width) / 2 - te.XBearing;
        var y = (areaHeight - te.Height) / 2 - te.YBearing;

        // 배경 사각형 좌표 및 크기 계산 (여백 포함)
        const double padding = 12.0;
        var rectX = x + te.XBearing - padding;
        var rectY = y + te.YBearing - padding;
        var rectW = te.Width + padding * 2;
        var rectH = te.Height + padding * 2;

        // 배경색 칠하기
        cr.SetSourceRGBA(0, 0.12, 0.12, 0.7);
        cr.Rectangle(rectX, rectY, rectW, rectH);
        cr.Fill();

        // 텍스트 그리기
        cr.SetSourceRGB(1, 1, 1); // 흰색
        cr.MoveTo(x, y);
        cr.ShowText(_notify_text);
    }

    private void Notify(string message, int timeout = 2000)
    {
        _notify_text = message;
        _notify_timer.Interval = timeout;
        _notify_timer.Start();
        _draw.QueueDraw(); // 다시 그리기 요청
    }

    #endregion

    #region 책 조작

    private void SetBookInfo(int page = -1)
    {
        var si = new StringBuilder("두그뷰");
        var book = Settings.Book;
        if (book == null)
            _infoLabel.Text = "----";
        else
        {
            si.Append($" - {book.OnlyFileName}");

            var cache = Doumi.SizeToString(book.CacheSize);
            _infoLabel.Text = $"{page + 1}/{book.TotalPage} [{cache}]";
        }

        _titleLabel.Text = si.ToString();
    }

    private void CleanBook()
    {
        // 애니메이션 스탑

        var book = Settings.Book;
        if (book != null)
        {
            Settings.SetRecentlyPage(book);
            book.Dispose();
            Settings.Book = null;

            // 페이지 폼 리셋
            GC.Collect();
        }

        ResetFocus();
        _paint_count = 0;
    }

    private void CloseBook()
    {
        var book = Settings.Book;
        if (book == null)
            return;

        CleanBook();
        DrawBook();
        SetBookInfo();
    }

    private void OpenBookDialog()
    {
        var dialog = new FileChooserDialog("파일 열기", this, FileChooserAction.Open);
        dialog.AddButton(Stock.Cancel, ResponseType.Cancel);
        dialog.AddButton(Stock.Open, ResponseType.Accept);
        if (dialog.Run() == (int)ResponseType.Accept)
        {
            var filename = dialog.Filename;
            OpenBook(filename);
        }

        dialog.Destroy();
    }

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

            SetBookInfo(page);
            // _page_form.SetBook(book);
            Settings.LastFileName = filename;

            DrawBook();
        }
        else
        {
            Notify("책을 열 수 없어요!");
        }

        ResetFocus();
    }

    // 압축 파일 읽기
    private BookBase? OpenArchive(FileInfo fi, string ext)
    {
        Settings.LastFolder = fi.DirectoryName ?? string.Empty;

        BookBase? book = ext.ToLower() switch
        {
            ".zip" or ".cbz" => BookZip.FromFile(fi.FullName),
            _ => null,
        };

        if (book == null)
            Notify("지원하지 않는 압축 파일이예요!");
        return book;
    }

    // 디렉토리 읽기
    private BookFolder? OpenDirectory(DirectoryInfo di)
    {
        Settings.LastFolder = di.Parent?.FullName ?? string.Empty;

        var book = BookFolder.FromFolder(di);
        if (book == null)
            Notify("디렉토리를 열 수 없어요!");
        return book;
    }

    private void OpenBook(BookDirection direction)
    {
        var book = Settings.Book;
        if (book == null)
            return;

        var filename = book.FindNextFile(direction);
        if (filename.EmptyString())
        {
            var wh = direction == BookDirection.Next ? "뒷쪽" : "앞쪽";
            Notify($"{wh} 책을 열 수 없어요!");
            return;
        }

        OpenBook(filename);
    }

    private void OpenRememberBook()
    {
        var filename = Settings.RememberFileName;
        if (filename.EmptyString() || !File.Exists(filename))
        {
            Notify("기억하고 있는 책을 열 수 없어요!");
            return;
        }

        var book = Settings.Book;
        if (book != null)
        {
            Notify("읽던 책을 먼저 닫으세요");
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

        using (var dialog = new MessageDialog(
                   this,
                   DialogFlags.Modal,
                   MessageType.Question,
                   ButtonsType.YesNo,
                   "현재 책을 기억하실건가요?"))
        {
            dialog.Title = "책 기억하기";
            var response = (ResponseType)dialog.Run();
            dialog.Destroy();

            if (response != ResponseType.Yes)
                return;
        }

        Settings.RememberFileName = book.FileName;
        Notify("읽고 있던 책을 기억했어요");
    }

    private void DeleteBookOrItem()
    {
        var book = Settings.Book;
        if (book == null)
            return;
        if (!book.CanDeleteFile(out var reason))
            return;
        if (!reason.EmptyString() && Settings.GeneralConfirmDelete)
        {
            using var dialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Question,
                ButtonsType.YesNo,
                "현재 책 파일을 지웁니다. 진짜예요?");
            dialog.Title = "책 지우기";
            var response = (ResponseType)dialog.Run();
            dialog.Destroy();

            if (response != ResponseType.Yes)
                return;
        }

        var next = book.FindNextFileAny(BookDirection.Next);

        if (!book.DeleteFile(out var closed))
        {
            Notify("책 파일을 지울 수 없어요!", 3000);
            return;
        }

        if (closed)
        {
            // 책을 지우라는 것
            book.CurrentPage = 0;
            CloseBook();
            if (!next.EmptyString())
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

    private void RenameBook(bool exRen = false)
    {
#if false
        var book = Settings.Book;
        if (book == null)
            return;

        // TODO: 패스 코드 적용

        var dlg = new RenexForm();
        if (dlg.ShowDialog(this, book.OnlyFileName) != DialogResult.OK)
            return;

        var filename = dlg.Filename;
        if (filename.EmptyString() || book.OnlyFileName.Equals(filename))
            return;

        var next = dlg.Reopen ? null : book.FindNextFileAny(BookDirection.Next);

        if (!book.RenameFile(filename, out var fullPath))
        {
            Notify(Resources.CannotRenameBook, 3000);
            return;
        }

        book.CurrentPage = 0;
        CloseBook();

        OpenBook(next ?? fullPath);
#endif
    }

    private void MoveBook()
    {
#if false
        var book = Settings.Book;
        if (book == null)
            return;

        // TODO: 패스 코드 적용

        var dlg = new MoveForm();
        if (dlg.ShowDialog(this, book.OnlyFileName) != DialogResult.OK)
            return;

        var filename = dlg.Filename;
        var next = book.FindNextFileAny(BookDirection.Next);

        if (!book.MoveFile(filename))
        {
            Notify(Resources.CannotMoveBook, 3000);
            return;
        }

        CloseBook();

        if (next.EmptyString())
            Notify(Resources.NoNextBook);
        else
            OpenBook(next);
#endif
    }

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
                OpenBook(BookDirection.Previous);
                break;

            case BookControl.ScanNext:
                OpenBook(BookDirection.Next);

                break;

            case BookControl.Select:
                PageSelect();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(ctrl), ctrl, null);
        }
    }

    private void PageGoTo(int page)
    {
        var book = Settings.Book;
        if (book == null || !book.MovePage(page))
            return;
        book.PrepareImages();
        DrawBook();
    }

    private void PageGoDelta(int delta)
    {
        var book = Settings.Book;
        if (book == null || !book.MovePage(book.CurrentPage + delta))
            return;
        book.PrepareImages();
        DrawBook();
    }

    private void PageGoPrev()
    {
        var book = Settings.Book;
        if (book == null || !book.MovePrev())
            return;
        book.PrepareImages();
        DrawBook();
    }

    private void PageGoNext()
    {
        var book = Settings.Book;
        if (book == null || !book.MoveNext())
            return;
        book.PrepareImages();
        DrawBook();
    }

    private void PageSelect()
    {
#if false
        var book = Settings.Book;
        if (book == null)
            return;

        if (_page_form.ShowDialog(this, book.CurrentPage) != DialogResult.OK)
            return;

        book.CurrentPage = _page_form.SelectedPage;
        book.PrepareImages();
        DrawBook();
#endif
    }

    private void StopAnimation()
    {
    }

    private ImageSurface UpdateAnimation(PageImage page)
    {
        // 해야함. 일단 현재 이미지 반환
        return page.GetImage();
    }

    private static ViewMode CurrentViewMode
    {
        get
        {
            if (Settings.Book == null || Settings.Book.ViewMode == ViewMode.Follow)
                return Settings.ViewMode;
            return Settings.Book.ViewMode;
        }
    }

    #endregion

    #region 책 그리기

    private void DrawOnDrawn(object o, DrawnArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"{DateTime.Now} 그리기!");

        _width = _draw.AllocatedWidth;
        _height = _draw.AllocatedHeight;

        var cr = args.Cr;
        cr.SetSourceRGB(0.1, 0.1, 0.1); // 더 어두운 배경
        cr.Paint();
        cr.SetSourceRGB(0.2, 0.4, 0.6);
        cr.Arc(200, 150, 100, 0, 2 * Math.PI);
        cr.Fill();

        PaintBook(cr);
        PaintNotify(cr);
    }

    private void DrawLogo(Context cr)
    {
        var img = _images["logo"];
        if (img == null)
            return;

        cr.Save();
        if (_width > img.Width && _height > img.Height)
            Gdk.CairoHelper.SetSourcePixbuf(cr, img, _width - img.Width - 50, _height - img.Height - 50);
        else
            Gdk.CairoHelper.SetSourcePixbuf(cr, img, 0, 0);
        cr.Paint();
        cr.Restore();
    }

    private void DrawBitmapFit(Context cr, ImageSurface img)
    {
        var (nw, nh) = Doumi.CalcDestSize(Settings.ViewZoom, _width, _height, img.Width, img.Height);
        var rt = Doumi.CalcDestRect(_width, _height, nw, nh);

        cr.Save();
        cr.Translate(rt.X, rt.Y);
        cr.Scale((double)nw / img.Width, (double)nh / img.Height);
        cr.SetSourceSurface(img, 0, 0);
        cr.Paint();
        cr.Restore();
    }

    private void DrawBitmapHalfHalf(Context cr, ImageSurface l, ImageSurface r)
    {
        var hw = _width / 2;

        // 왼쪽 이미지
        var (lw, lh) = Doumi.CalcDestSize(Settings.ViewZoom, hw, _height, l.Width, l.Height);
        var lb = Doumi.CalcDestRect(hw, _height, lw, lh, HorizAlign.Right);

        cr.Save();
        cr.Translate(lb.X, lb.Y);
        cr.Scale((double)lw / l.Width, (double)lh / l.Height);
        cr.SetSourceSurface(l, 0, 0);
        cr.Paint();
        cr.Restore();

        // 오른쪽 이미지
        var (rw, rh) = Doumi.CalcDestSize(Settings.ViewZoom, hw, _height, r.Width, r.Height);
        var rb = Doumi.CalcDestRect(hw, _height, rw, rh, HorizAlign.Left);
        rb.X += hw;

        cr.Save();
        cr.Translate(rb.X, rb.Y);
        cr.Scale((double)rw / r.Width, (double)rh / r.Height);
        cr.SetSourceSurface(r, 0, 0);
        cr.Paint();
        cr.Restore();
    }

    private void DrawBook()
    {
        StopAnimation();

        var book = Settings.Book;
        if (book != null)
        {
            SetBookInfo(book.CurrentPage);
            // TODO: 엔트리(페이지) 정보
        }

        if (_width == 0 || _height == 0)
        {
            // 크기가 없으면 그리지 않음
            return;
        }

        _draw.QueueDraw();
    }

    private void PaintBook(Context cr)
    {
        if ((_window_state & WindowState.Iconified) != 0)
        {
            // 최소화 상태면 그리지 않음
            return;
        }

        var (w, h) = (_width, _height);
        if (w <= 0 || h <= 0)
        {
            // 크기가 없으면 그리지 않음
            return;
        }

        var book = Settings.Book;
        if (book == null)
        {
            // 책이 없으면 로고만 그림
            DrawLogo(cr);
        }
        else
        {
            if (CurrentViewMode == ViewMode.Fit)
            {
                if (book.PageLeft != null)
                {
                    var img = UpdateAnimation(book.PageLeft);
                    DrawBitmapFit(cr, img);
                }
            }
            else if (CurrentViewMode is ViewMode.LeftToRight or ViewMode.RightToLeft)
            {
                if (book is { PageLeft: not null, PageRight: not null })
                {
                    var l = UpdateAnimation(book.PageLeft);
                    var r = UpdateAnimation(book.PageRight);
                    DrawBitmapHalfHalf(cr, l, r);
                }
                else if (book.PageLeft != null)
                {
                    var img = UpdateAnimation(book.PageLeft);
                    DrawBitmapFit(cr, img);
                }
                else if (book.PageRight != null)
                {
                    var img = UpdateAnimation(book.PageRight);
                    DrawBitmapFit(cr, img);
                }
                else
                {
                    // 헐?
                    DrawLogo(cr);
                }
            }
            else
            {
                // 알 수 없는 모드
                DrawLogo(cr);
            }
        }

        _paint_count++;
    }

    #endregion
}
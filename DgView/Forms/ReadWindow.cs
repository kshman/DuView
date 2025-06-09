using Cairo;
using Gdk;
using System.Text;
using Window = Gtk.Window;
using WindowState = Gdk.WindowState;

namespace DgView.Forms;

/// <summary>
/// 만화책, 이미지북 등 다양한 책 파일을 읽고 탐색할 수 있는 메인 뷰어 윈도우입니다.
/// 파일 열기, 닫기, 페이지 이동, 보기 모드/품질 변경, 전체화면, 파일 삭제/이름변경 등
/// 다양한 독서 및 파일 관리 기능을 제공합니다.
/// </summary>
internal class ReadWindow : Window
{
	private int _width;
	private int _height;
	private readonly BoundRect[] _paging_bound = new BoundRect[2];

	private uint? _notify_id;
	private string _notify_text = string.Empty;

	private WindowState _window_state = WindowState.Focused;
	private bool _random_book_ready;

	private readonly PageDialog _page_dialog;

	private readonly DrawingArea _draw;
	private readonly Label _titleLabel;
	private readonly Label _infoLabel;
	private readonly Image _directionImage;
	private readonly Menu _mainMenu;
	private readonly MenuItem _menuOpenFile;
	private readonly MenuItem _menuCloseFile;
	private readonly CheckMenuItem _subViewZoom;
	private readonly MenuItem _menuSetting;
	private readonly MenuItem _menuExit;

	/// <summary>
	/// 현재 열려 있는 책입니다.
	/// </summary>
	private BookBase? Book { get; set; }

	/// <summary>
	/// 현재 보기 모드를 반환합니다. 책이 '따라감' 모드일 경우 전역 설정을 사용합니다.
	/// </summary>
	private ViewMode CurrentViewMode
	{
		get
		{
			if (Book == null || Book.ViewMode == ViewMode.Follow)
				return Configs.ViewMode;
			return Book.ViewMode;
		}
	}

	/// <summary>
	/// 윈도우 및 UI 컨트롤을 초기화하고, 각종 이벤트 핸들러를 등록합니다.
	/// </summary>
	public ReadWindow() : base("DgView")
	{
		#region 디자인
		// 윈도우
#if WINDOWS
		if (Configs.WindowX <= 0 || Configs.WindowY <= 0)
			SetPosition(WindowPosition.Center);
		else
		{
			// 지정된 위치로 이동
			SetPosition(WindowPosition.None);
			Move(Configs.WindowX, Configs.WindowY);
		}
#endif
		SetDefaultSize(Configs.WindowWidth, Configs.WindowHeight);
		SetSizeRequest(600, 400);
		CanFocus = true;
		HasFocus = true;
		Icon = ResL.GetBitmap(ResL.DuViewIcon);
		DeleteEvent += ReadWindow_DeleteEvent;
		WindowStateEvent += ReadWindow_WindowStateEvent;
		KeyPressEvent += ReadWindow_KeyPressEvent;
		KeyReleaseEvent += ReadWindow_KeyReleaseEvent;
		SizeAllocated += ReadWindow_SizeAllocated;

		// 타이틀바 + 메뉴
		var header = new HeaderBar();
		header.ShowCloseButton = true;

		_titleLabel = new Label("두그뷰");
		_titleLabel.Halign = Align.Start;
		_titleLabel.Valign = Align.Center;
		_titleLabel.MarginStart = 12;
		header.PackStart(_titleLabel);

		// 메뉴
		var accel = new AccelGroup();
		AddAccelGroup(accel);

		_mainMenu = new Menu();

		// 파일 열기
		_menuOpenFile = new MenuItem("열기");
		_menuOpenFile.Activated += MenuOpenFile_Activated;
		_menuOpenFile.AddAccelerator("activate", accel, (uint)GdkKey.F3, ModifierType.None, AccelFlags.Visible);
		_mainMenu.Append(_menuOpenFile);

		// 파일 닫기
		_menuCloseFile = new MenuItem("닫기");
		_menuCloseFile.Activated += MenuClose_Activated;
		_menuCloseFile.AddAccelerator("activate", accel, (uint)GdkKey.F4, ModifierType.None, AccelFlags.Visible);
		_mainMenu.Append(_menuCloseFile);

		//
		_mainMenu.Append(Doumi.CreateSeparatorMenuItem());

		// 보기 서브 메뉴
		var menuViewMode = new MenuItem("보기");
		var subView = new Menu();

		// 늘려보기
		_subViewZoom = new CheckMenuItem("늘려보기");
		_subViewZoom.Active = Configs.ViewZoom;
		_subViewZoom.Activated += SubMenuViewZoom_Activated;
		_subViewZoom.AddAccelerator("activate", accel, (uint)GdkKey.Key_0, ModifierType.ControlMask,
			AccelFlags.Visible);
		subView.Append(_subViewZoom);

		//
		subView.Append(Doumi.CreateSeparatorMenuItem());

		// 보기 모드 라디오 메뉴
		subView.Append(Doumi.CreateLabelMenuItem("보는 방법"));
		RadioMenuItem[] viewModeGroup = [];
		var viewModes = new[]
		{
			new { Name = "화면에 맞춤", Value = ViewMode.Fit, HotKey = GdkKey.Key_0 },
			new { Name = "왼쪽에서 오른쪽으로", Value = ViewMode.LeftToRight, HotKey = GdkKey.Key_1 },
			new { Name = "오른쪽에서 왼쪽으로", Value = ViewMode.RightToLeft, HotKey = GdkKey.Key_2 }
		};
		foreach (var v in viewModes)
		{
			var item = new RadioMenuItem(viewModeGroup, v.Name);
			item.Data["tag"] = v.Value; // ViewMode로 설정
			item.Active = (Configs.ViewMode == v.Value);
			item.Toggled += SubMenuViewMode_Toggled;
			item.AddAccelerator("activate", accel, (uint)v.HotKey, ModifierType.None, AccelFlags.Visible);
			subView.Append(item);
			viewModeGroup = item.Group;
		}

		//
		subView.Append(Doumi.CreateSeparatorMenuItem());

		// 품질 라디오 메뉴
		subView.Append(Doumi.CreateLabelMenuItem("그림 품질"));
		RadioMenuItem[] qualityGroup = [];
		var qualities = new[]
		{
			new { Name = "빠른 처리", Value = ViewQuality.Fast },
			new { Name = "기본", Value = ViewQuality.Default },
			new { Name = "높은 품질", Value = ViewQuality.High },
			new { Name = "픽셀 유지", Value = ViewQuality.Nearest },
			new { Name = "양선형 보간", Value = ViewQuality.Bilinear }
		};
		foreach (var q in qualities)
		{
			var item = new RadioMenuItem(qualityGroup, q.Name);
			item.Data["tag"] = q.Value;
			item.Active = (Configs.ViewQuality == q.Value);
			item.Toggled += SubMenuViewQuality_Toggled;
			subView.Append(item);
			qualityGroup = item.Group;
		}

		//
		subView.ShowAll();
		menuViewMode.Submenu = subView;
		_mainMenu.Append(menuViewMode);

		// 설정
		_menuSetting = new MenuItem("설정");
		_menuSetting.Activated += MenuSetting_Activated;
		_menuSetting.AddAccelerator("activate", accel, (uint)GdkKey.F12, ModifierType.None, AccelFlags.Visible);
		_mainMenu.Append(_menuSetting);

		//
		_mainMenu.Append(Doumi.CreateSeparatorMenuItem());

		// 종료
		_menuExit = new MenuItem("종료");
		_menuExit.Activated += (_, _) => { Close(); };
		_mainMenu.Append(_menuExit);
		_mainMenu.ShowAll();

		//
		var menuButton = new MenuButton();
		menuButton.Popup = _mainMenu;
		menuButton.CanFocus = false;
		menuButton.Label = "≡"; // 또는 아이콘 사용

		header.PackEnd(menuButton);

		// 방향 이미지
		_directionImage = new Image();
		_directionImage.Pixbuf = ResL.GetBitmap(ResL.IconFit);
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
		_draw.AddEvents((int)EventMask.ButtonPressMask); // 마우스 버튼 이벤트 활성화
		_draw.ButtonPressEvent += DrawingArea_ButtonPressEvent;
		_draw.ScrollEvent += DrawingArea_ScrollEvent;
		_draw.Drawn += DrawingArea_OnDrawn;

		Gtk.Drag.DestSet(_draw, DestDefaults.All, [
			new TargetEntry("text/uri-list", 0, 0)
		], DragAction.Copy);
		_draw.DragDataReceived += DrawingArea_DragDataReceived;

		var box = new Box(Orientation.Vertical, 0);
		box.PackStart(_draw, true, true, 0);
		Add(box);

		#endregion

		_page_dialog = new PageDialog();

		SetBookInfo();
		ApplyViewMenus();
		ResetFocus();
		ShowAll();
	}

	/// <summary>
	/// 현재 포커스를 윈도우로 강제로 이동시킵니다.
	/// </summary>
	private void ResetFocus() =>
		GrabFocus();

	#region 이벤트 핸들러

	/// <summary>
	/// 윈도우가 닫힐 때 호출됩니다.
	/// 책 리소스 해제, 설정 저장, 타이머 및 대화상자 정리, 전체화면 해제 등 종료 처리를 담당합니다.
	/// </summary>
	private void ReadWindow_DeleteEvent(object sender, DeleteEventArgs a)
	{
		CleanBook();

		_page_dialog.Destroy();

		if ((_window_state & WindowState.Fullscreen) != 0)
		{
			// 전체 화면 모드 해제
			_window_state &= ~WindowState.Fullscreen;
			Unfullscreen();
		}

#if WINDOWS
		//GetPosition(out var x, out var y);
		Window.GetRootOrigin(out var x, out var y);
		Configs.WindowX = x;
		Configs.WindowY = y;
#endif
		GetSize(out var width, out var height);
		Configs.WindowWidth = width;
		Configs.WindowHeight = height;

		Application.Quit();
	}

	/// <summary>
	/// 윈도우 크기 할당 시 페이지 클릭 영역을 재계산합니다.
	/// </summary>
	private void ReadWindow_SizeAllocated(object o, SizeAllocatedArgs args)
	{
		var width = args.Allocation.Width;
		var height = args.Allocation.Height;
		var w4 = width / 4;
		_paging_bound[0] = new BoundRect(0, 0, w4, height); // 첫 번째 페이지 영역
		_paging_bound[1] = new BoundRect(width - w4, 0, w4, height); // 두 번째 페이지 영역
	}

	/// <summary>
	/// 윈도우 상태(최대화, 최소화 등)가 변경될 때 호출됩니다.
	/// 내부 상태를 갱신하고, 화면을 다시 그리도록 요청합니다.
	/// </summary>
	private void ReadWindow_WindowStateEvent(object o, WindowStateEventArgs args)
	{
		if ((args.Event.ChangedMask & WindowState.Maximized) != 0)
		{
			if ((args.Event.NewWindowState & WindowState.Maximized) != 0)
				_window_state |= WindowState.Maximized;
			else
				_window_state &= ~WindowState.Maximized;
		}

		if ((args.Event.ChangedMask & WindowState.Iconified) != 0)
		{
			if ((args.Event.NewWindowState & WindowState.Iconified) != 0)
				_window_state |= WindowState.Iconified;
			else
				_window_state &= ~WindowState.Iconified;
		}

		_draw.QueueDraw(); // 다시 그리기 요청
	}

	/// <summary>
	/// DrawingArea에 파일을 끌어 놓았을 때 호출됩니다.
	/// 놓은 파일의 경로를 추출하여 책을 엽니다.
	/// </summary>
	private void DrawingArea_DragDataReceived(object o, DragDataReceivedArgs args)
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

	private ModifierType _prev_key_mask;
	private GdkKey _prev_key_code;

	/// <summary>
	/// 키가 눌렸을 때 호출됩니다.
	/// 단축키, 페이지 이동, 파일 관리 등 다양한 키보드 입력을 처리합니다.
	/// </summary>
	[GLib.ConnectBefore]
	private void ReadWindow_KeyPressEvent(object o, KeyPressEventArgs args)
	{
		var code = args.Event.Key;
		var mask = args.Event.State & ~ModifierType.Mod2Mask; // Num Lock 무시

		if (code == _prev_key_code && mask == _prev_key_mask)
		{
			// 중복 입력 방지
			return;
		}

#if DEBUG && false
        System.Diagnostics.Debug.WriteLine($"Key pressed: {args.Event.Key} ({args.Event.State})");
#endif

		_prev_key_mask = mask;
		_prev_key_code = code;

		if (mask == ModifierType.ControlMask)
		{
			// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
			switch (code)
			{
				case GdkKey.w or GdkKey.W:
					CloseBook();
					break;

				case GdkKey.s or GdkKey.S:
					SaveRememberBook();
					break;

				case GdkKey.r or GdkKey.R:
					OpenRememberBook();
					break;

				case GdkKey.d or GdkKey.D:
					DeleteBookOrItem();
					break;

				case GdkKey.F3:
					OpenLastBook();
					break;

				// 페이지
				case GdkKey.Up:
					PageControl(BookControl.SeekPrevious10);
					break;

				case GdkKey.Down:
					PageControl(BookControl.SeekNext10);
					break;

				case GdkKey.Left:
					PageControl(BookControl.SeekMinusOne);
					break;

				case GdkKey.Right:
					PageControl(BookControl.SeekPlusOne);
					break;

				case GdkKey.Page_Up:
					OpenBook(BookDirection.Previous);
					break;

				case GdkKey.Page_Down:
					OpenBook(BookDirection.Next);
					break;
			}
		}
		else if (mask == ModifierType.Mod1Mask)
		{
			if (code is GdkKey.Return or GdkKey.KP_Enter)
				ToggleFullScreen();
		}
		else if (mask == ModifierType.ShiftMask)
		{
		}
		else if (mask == (ModifierType.ControlMask | ModifierType.ShiftMask))
		{
			// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
			if (code is GdkKey.z or GdkKey.Z)
				OpenLastBook();
		}
		else
		{
			// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
			switch (code)
			{
				// 끝
				case GdkKey.Escape:
					if (Configs.GeneralEscExit)
						Close();
					break;

				// 페이지
				case GdkKey.Up or GdkKey.comma:
					PageControl(BookControl.SeekMinusOne);
					break;

				case GdkKey.Down or GdkKey.period or GdkKey.question:
					PageControl(BookControl.SeekPlusOne);
					break;

				case GdkKey.Left or GdkKey.KP_Decimal:
					PageControl(BookControl.Previous);
					break;

				case GdkKey.Right or GdkKey.space or GdkKey.KP_0 or GdkKey.KP_Space:
					PageControl(BookControl.Next);
					break;

				case GdkKey.Home:
					PageControl(BookControl.First);
					break;

				case GdkKey.End:
					PageControl(BookControl.Last);
					break;

				case GdkKey.Page_Up:
					PageControl(BookControl.SeekPrevious10);
					break;

				case GdkKey.Page_Down or GdkKey.BackSpace:
					PageControl(BookControl.SeekNext10);
					break;

				case GdkKey.Return:
				case GdkKey.KP_Enter:
					PageControl(BookControl.Select);
					break;

				case GdkKey.Tab:
					if (Book != null) // 혼란 방지: 책이 있을 때만
					{
						// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
						switch (Configs.ViewMode)
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

				case GdkKey.bracketleft:
					OpenBook(BookDirection.Previous);
					break;

				case GdkKey.bracketright:
					OpenBook(BookDirection.Next);
					break;

				case GdkKey.backslash:
					if (!_random_book_ready)
					{
						_random_book_ready = true;
						return;
					}
					OpenRandomBook();
					break;

				case GdkKey.plus or GdkKey.KP_Add or GdkKey.Insert:
					MoveBook();
					break;

				case GdkKey.Delete:
					DeleteBookOrItem();
					break;

				case GdkKey.F2:
					RenameBook();
					break;

				case GdkKey.f or GdkKey.F:
					ToggleFullScreen();
					break;

#if DEBUG
				case GdkKey.F11:
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

		_random_book_ready = false; // 랜덤 북 플래그 초기화
	}

	/// <summary>
	/// 키가 떼어졌을 때 호출됩니다.
	/// 중복 입력 방지용 상태를 초기화합니다.
	/// </summary>
	[GLib.ConnectBefore]
	private void ReadWindow_KeyReleaseEvent(object o, KeyReleaseEventArgs args)
	{
		_prev_key_mask = ModifierType.None;
		_prev_key_code = 0;
	}

	/// <summary>
	/// DrawingArea에서 마우스 휠 스크롤 이벤트가 발생할 때 호출됩니다.
	/// 휠 방향에 따라 페이지를 앞/뒤로 넘깁니다.
	/// </summary>
	private void DrawingArea_ScrollEvent(object o, ScrollEventArgs args)
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

	/// <summary>
	/// DrawingArea에서 버튼이 눌릴 때 호출됩니다. 왼쪽 마우스 버튼이 눌리면 창 이동을 시작하고,
	/// 두 번 클릭, 오른쪽 클릭, 가운데 클릭 등 다양한 마우스 입력을 처리합니다.
	/// </summary>
	/// <remarks>
	/// 이 메서드는 왼쪽 버튼(버튼 1) 클릭 시 창 이동을 시작합니다.
	/// 두 번 클릭(더블 클릭) 이벤트, 오른쪽 버튼 클릭 시 컨텍스트 메뉴 표시,
	/// 가운데 버튼 클릭 시 전체화면 전환 등 마우스 입력에 따라 동작을 분기합니다.
	/// </remarks>
	/// <param name="o">이벤트 소스(일반적으로 DrawingArea 컨트롤)</param>
	/// <param name="args">버튼 클릭 이벤트 데이터(버튼 번호, 위치, 타임스탬프 등 포함)</param>
	private void DrawingArea_ButtonPressEvent(object o, ButtonPressEventArgs args)
	{
		if (args.Event.Type == EventType.TwoButtonPress)
		{
			// 두 번 클릭
			if (Book == null)
			{
				OpenBookDialog();
				return;
			}
			if (Configs.MouseDoubleClickFullScreen)
			{
				Fullscreen();
				return;
			}
		}

		if (args.Event.Button == 1) // 왼쪽 버튼
		{
			if (Configs.MouseUseClickPaging && Book != null)
			{
				// 페이지 클릭 이동
				if (_paging_bound[0].Contains(args.Event.X, args.Event.Y))
				{
					PageControl(BookControl.Previous);
					return;
				}
				if (_paging_bound[1].Contains(args.Event.X, args.Event.Y))
				{
					PageControl(BookControl.Next);
					return;
				}
			}

			// 창 이동 시작
			if (!Configs.MouseDoubleClickFullScreen && (_window_state & WindowState.Fullscreen) == 0)
			{
				// 두번 눌러 전체화면이 아닐 때만 이동 시작
				BeginMoveDrag(
					button: (int)args.Event.Button,
					root_x: (int)args.Event.XRoot,
					root_y: (int)args.Event.YRoot,
					timestamp: args.Event.Time
				);
			}
		}
		else if (args.Event.Button == 3) // 오른쪽 버튼
		{
			// 오른쪽 클릭 메뉴 표시
			_mainMenu.ShowAll();
			_mainMenu.PopupAtPointer(args.Event);
		}
		else if (args.Event.Button == 2) // 가운데 버튼
		{
			ToggleFullScreen();
		}
	}

	/// <summary>
	/// 전체화면 모드로 전환하거나 해제합니다.
	/// </summary>
	private void ToggleFullScreen()
	{
		if ((_window_state & WindowState.Fullscreen) != 0)
		{
			// 전체 화면 모드 해제
			_window_state &= ~WindowState.Fullscreen;
			Unfullscreen();
		}
		else
		{
			// 전체 화면 모드로 전환
			_window_state |= WindowState.Fullscreen;
			Fullscreen();
		}
	}

	#endregion

	#region 메뉴 및 액셀 그룹

	/// <summary>
	/// 보기 메뉴(모드, 품질, 확대 등) 상태를 UI와 동기화합니다.
	/// </summary>
	private void ApplyViewMenus()
	{
		UpdateViewZoom(Configs.ViewZoom);
		UpdateViewMode(CurrentViewMode);
		UpdateViewQuality(Configs.ViewQuality);
	}

	/// <summary>
	/// 확대/축소 보기 상태를 변경합니다.
	/// </summary>
	private void UpdateViewZoom(bool zoom, bool redraw = true)
	{
		Configs.ViewZoom = zoom;
		_subViewZoom.Active = zoom;
		if (redraw)
			_draw.QueueDraw(); // 다시 그리기 요청
	}

	/// <summary>
	/// 보기 모드를 변경하고, 아이콘 및 화면을 갱신합니다.
	/// </summary>
	private void UpdateViewMode(ViewMode mode, bool redraw = true)
	{
		Configs.ViewMode = mode;
		var icon = mode switch
		{
			ViewMode.Fit => ResL.GetBitmap(ResL.IconFit),
			ViewMode.LeftToRight => ResL.GetBitmap(ResL.IconLeftToRight),
			ViewMode.RightToLeft => ResL.GetBitmap(ResL.IconRightToLeft),
			_ => ResL.GetBitmap(ResL.IconFit)
		};
		_directionImage.Pixbuf = icon;

		if (!redraw)
			return;

		Book?.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// 보기 품질을 변경하고, 필요시 화면을 다시 그립니다.
	/// </summary>
	private void UpdateViewQuality(ViewQuality quality, bool redraw = true)
	{
		Configs.ViewQuality = quality;

		if (redraw)
			DrawBook();
	}

	private void MenuOpenFile_Activated(object? s, EventArgs e)
	{
		OpenBookDialog();
	}

	private void MenuClose_Activated(object? s, EventArgs e)
	{
		CloseBook();
	}

	private void SubMenuViewZoom_Activated(object? s, EventArgs e)
	{
		UpdateViewZoom(!_subViewZoom.Active);
	}

	private void SubMenuViewMode_Toggled(object? s, EventArgs e)
	{
		if (s is RadioMenuItem { Active: true } item && item.Data["tag"] is ViewMode mode)
			UpdateViewMode(mode);
	}

	private void SubMenuViewQuality_Toggled(object? s, EventArgs e)
	{
		if (s is RadioMenuItem { Active: true } item && item.Data["tag"] is ViewQuality quality)
			UpdateViewQuality(quality);
	}

	private void MenuSetting_Activated(object? sender, EventArgs e)
	{
		using var dlg = new OptionDialog(this);
		dlg.Run();

		var book = Book;
		book?.PrepareImages();
		DrawBook();
	}

	#endregion

	#region 알림

	/// <summary>
	/// 알림 메시지를 화면에 표시합니다.
	/// </summary>
	private void Notify(string message, int timeout = 2000)
	{
		_notify_text = message;
		if (_notify_id != null)
		{
			GLib.Source.Remove(_notify_id.Value);
			_notify_id = null;

		}
		_notify_id = GLib.Timeout.Add((uint)timeout, () =>
		{
			_notify_text = string.Empty;
			_draw.QueueDraw(); // 다시 그리기 요청
			_notify_id = null;
			return false; // 타이머 제거
		});
		_draw.QueueDraw(); // 다시 그리기 요청
	}

	/// <summary>
	/// 알림 메시지를 그립니다.
	/// </summary>
	private void PaintNotify(Context cr)
	{
		if (string.IsNullOrEmpty(_notify_text))
			return;

		// 폰트 설정
		const string FontFamily = "Sans";
		cr.SelectFontFace(FontFamily, FontSlant.Normal, FontWeight.Normal);
		cr.SetFontSize(40);

		// 텍스트 크기 측정
		var te = cr.TextExtents(_notify_text);

		// DrawingArea 크기 구하기 (가운데 배치 예시)
		var areaWidth = _draw.Allocation.Width;
		var areaHeight = _draw.Allocation.Height;

		var x = (areaWidth - te.Width) / 2 - te.XBearing;
		var y = (areaHeight - te.Height) / 2 - te.YBearing;

		// 배경 사각형 좌표 및 크기 계산 (여백 포함)
		const double padding = 18.0;
		var rectX = x + te.XBearing - padding;
		var rectY = y + te.YBearing - padding;
		var rectW = te.Width + padding * 2;
		var rectH = te.Height + padding * 2;

		// 배경색 칠하기
		cr.SetSourceRGBA(0, 0.12, 0.12, 0.8);
		cr.Rectangle(rectX, rectY, rectW, rectH);
		cr.FillPreserve();
		cr.LineWidth = 3;
		cr.SetSourceRGB(1, 1, 0);
		cr.Stroke(); // 테두리만 그림

		// 텍스트 그리기
		cr.SetSourceRGB(1, 1, 1); // 흰색
		cr.MoveTo(x, y);
		cr.ShowText(_notify_text);
	}

	#endregion

	#region 책 조작

	/// <summary>
	/// 현재 책 정보(파일명, 페이지 등)를 타이틀/정보 라벨에 표시합니다.
	/// </summary>
	private void SetBookInfo(int page = -1)
	{
		var si = new StringBuilder("두그뷰");
		var book = Book;
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

	/// <summary>
	/// 현재 열려 있는 책을 닫고, 리소스를 해제합니다.
	/// </summary>
	private void CleanBook()
	{
		// 애니메이션 스탑

		var book = Book;
		if (book != null)
		{
			Configs.SetRecentlyPage(book);
			book.Dispose();
			Book = null;

			_page_dialog.ResetBook();
			GC.Collect();
		}

		ResetFocus();
	}

	/// <summary>
	/// 책을 닫고, 화면을 초기화합니다.
	/// </summary>
	private void CloseBook()
	{
		var book = Book;
		if (book == null)
			return;

		CleanBook();
		DrawBook();
		SetBookInfo();
	}

	/// <summary>
	/// 파일 열기 대화상자를 표시하고, 사용자가 선택한 파일을 엽니다.
	/// </summary>
	private void OpenBookDialog()
	{
		using var dialog = new FileChooserDialog("파일 열기", this, FileChooserAction.Open);
		dialog.AddButton(Stock.Cancel, ResponseType.Cancel);
		dialog.AddButton(Stock.Open, ResponseType.Accept);

		var flt = new FileFilter();
		flt.Name = "책 파일";
		flt.AddPattern("*.zip");
		flt.AddPattern("*.cbz");
		dialog.AddFilter(flt);

		if (dialog.Run() != (int)ResponseType.Accept)
			return;

		var filename = dialog.Filename;
		OpenBook(filename);
	}

	/// <summary>
	/// 지정한 파일(또는 폴더)을 열고, 책 객체를 생성하여 화면에 표시합니다.
	/// </summary>
	/// <param name="filename">열 파일 또는 폴더 경로</param>
	/// <param name="page">초기 페이지(옵션)</param>
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
			Book = book;

			if (page < 0)
				page = Configs.GetRecentlyPage(book.OnlyFileName);
			book.CurrentPage = page;
			book.PrepareImages();

			SetBookInfo(page);
			_page_dialog.SetBook(book);
			Configs.LastFileName = filename;

			DrawBook();
		}
		else
		{
			Notify("책을 열 수 없어요!");
		}

		ResetFocus();
	}

	/// <summary>
	/// 압축 파일(.zip, .cbz 등)을 열어 BookBase 객체를 생성합니다.
	/// </summary>
	private BookBase? OpenArchive(FileInfo fi, string ext)
	{
		Configs.LastFolder = fi.DirectoryName ?? string.Empty;

		BookBase? book = ext.ToLower() switch
		{
			".zip" or ".cbz" => BookZip.FromFile(fi.FullName),
			_ => null,
		};

		if (book == null)
			Notify("지원하지 않는 압축 파일이예요!");
		return book;
	}

	/// <summary>
	/// 폴더를 열어 BookBase 객체를 생성합니다.
	/// </summary>
	private BookBase? OpenDirectory(DirectoryInfo di)
	{
		Configs.LastFolder = di.Parent?.FullName ?? string.Empty;

		var book = BookFolder.FromFolder(di);
		if (book == null)
			Notify("디렉토리를 열 수 없어요!");
		return book;
	}

	/// <summary>
	/// 현재 책에서 다음/이전 파일을 찾아 열기 시도합니다.
	/// </summary>
	private void OpenBook(BookDirection direction)
	{
		var book = Book;
		if (book == null)
			return;

		var filename = book.FindNextFile(direction);
		if (string.IsNullOrEmpty(filename))
		{
			var wh = direction == BookDirection.Next ? "뒷쪽" : "앞쪽";
			Notify($"{wh} 책을 열 수 없어요!");
			return;
		}

		OpenBook(filename);
	}

	/// <summary>
	/// 마지막으로 열었던 책을 다시 엽니다.
	/// </summary>
	private void OpenLastBook()
	{
		var book = Book;
		if (book != null && book.FileName == Configs.LastFileName)
			return;
		// TODO: 원래 여기 패스코드
		OpenBook(Configs.LastFileName);
	}

	/// <summary>
	/// 임의의 책을 선택하여 엽니다.
	/// </summary>
	private void OpenRandomBook()
	{
		var book = Book;
		if (book == null)
			return;

		var filename = book.FindRandomFile();
		if (string.IsNullOrEmpty(filename))
		{
			Notify("임의의 책을 열 수 없어요!");
			return;
		}

		OpenBook(filename);
	}

	/// <summary>
	/// 기억해둔 책(리멤버 책)을 엽니다.
	/// </summary>
	private void OpenRememberBook()
	{
		var filename = Configs.RememberFileName;
		if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
		{
			Notify("기억하고 있는 책을 열 수 없어요!");
			return;
		}

		var book = Book;
		if (book != null)
		{
			Notify("읽던 책을 먼저 닫으세요");
			return;
		}

		CloseBook();
		OpenBook(filename);
	}

	/// <summary>
	/// 현재 책을 리멤버 책으로 저장합니다.
	/// </summary>
	private void SaveRememberBook()
	{
		var book = Book;
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

		Configs.RememberFileName = book.FileName;
		Notify("읽고 있던 책을 기억했어요");
	}

	/// <summary>
	/// 현재 책 또는 항목(파일)을 삭제합니다.
	/// </summary>
	private void DeleteBookOrItem()
	{
		var book = Book;
		if (book == null)
			return;
		if (!book.CanDeleteFile(out var reason))
			return;
		if (!string.IsNullOrEmpty(reason) && Configs.GeneralConfirmDelete)
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

		var next = book.FindAnyNextFile();

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

	/// <summary>
	/// 현재 책의 파일명을 변경합니다. (이름 바꾸기 대화상자 사용)
	/// </summary>
	private void RenameBook()
	{
		var book = Book;
		if (book == null)
			return;

		// TODO: 패스 코드 적용

		string? filename;
		bool reopen;
		using (var dlg = new RenexDialog(this, book.OnlyFileName))
		{
			if (dlg.Run() != (int)ResponseType.Ok)
				return;
			filename = dlg.Filename;
			reopen = dlg.Reopen;
		}

		if (string.IsNullOrEmpty(filename) || book.OnlyFileName.Equals(filename))
			return;

		var next = reopen ? null : book.FindAnyNextFile();

		if (!book.RenameFile(filename, out var fullPath))
		{
			Notify($"파일 이름을 바꿀 수 없어요!{Environment.NewLine}\"{filename}\"", 3000);
			return;
		}

		book.CurrentPage = 0;
		CloseBook();

		OpenBook(next ?? fullPath);
	}

	/// <summary>
	/// 현재 책을 이동합니다.
	/// </summary>
	private void MoveBook()
	{
		var book = Book;

		// TODO: 패스 코드 적용

		string? filename;
		using (var dlg = new MoveDialog(this))
		{
			if (!dlg.Run(book?.OnlyFileName))
				return;
			filename = dlg.Filename;
		}

		// 책이 없어도 편집 가능하도록 책 검사는 여기서
		if (book == null)
		{
			if (!string.IsNullOrEmpty(filename))
				Notify("열린 책이 없어서 이동은 할 수 없어요");
			return;
		}

		var next = book.FindAnyNextFile();

		if (!book.MoveFile(filename))
		{
			Notify("책을 옮길 수 없어요!", 3000);
			return;
		}

		CloseBook();

		if (string.IsNullOrEmpty(next))
			Notify("다음 책이 없어요");
		else
			OpenBook(next);
	}

	/// <summary>
	/// 페이지 이동, 선택 등 책 조작 명령을 처리합니다.
	/// </summary>
	private void PageControl(BookControl ctrl)
	{
		if (Book == null)
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

	/// <summary>
	/// 지정한 페이지로 이동합니다.
	/// </summary>
	private void PageGoTo(int page)
	{
		var book = Book;
		if (book == null || !book.MovePage(page))
			return;
		book.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// 현재 페이지에서 delta만큼 이동합니다.
	/// </summary>
	private void PageGoDelta(int delta)
	{
		var book = Book;
		if (book == null || !book.MovePage(book.CurrentPage + delta))
			return;
		book.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// 이전 페이지로 이동합니다.
	/// </summary>
	private void PageGoPrev()
	{
		var book = Book;
		if (book == null || !book.MovePrev())
			return;
		book.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// 다음 페이지로 이동합니다.
	/// </summary>
	private void PageGoNext()
	{
		var book = Book;
		if (book == null || !book.MoveNext())
			return;
		book.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// 페이지 선택 대화상자를 띄워 사용자가 원하는 페이지로 이동합니다.
	/// </summary>
	private void PageSelect()
	{
		var book = Book;
		if (book == null)
			return;

		// TODO: 패스 코드 적용

		if (!_page_dialog.Run(this, book.CurrentPage))
			return;

		book.CurrentPage = _page_dialog.SelectedPage;
		book.PrepareImages();
		DrawBook();
	}

	/// <summary>
	/// (애니메이션 등) 그리기 관련 작업을 중지합니다.
	/// </summary>
	private void StopAnimation()
	{
	}

	/// <summary>
	/// (애니메이션 등) 페이지 이미지를 갱신합니다.
	/// </summary>
	private ImageSurface UpdateAnimation(PageImage page)
	{
		// 해야함. 일단 현재 이미지 반환
		return page.GetImage();
	}

	#endregion

	#region 책 그리기

	/// <summary>
	/// DrawingArea의 Drawn 이벤트에서 호출되어, 전체 화면을 그립니다.
	/// </summary>
	private void DrawingArea_OnDrawn(object o, DrawnArgs args)
	{
		_width = _draw.AllocatedWidth;
		_height = _draw.AllocatedHeight;

		var cr = args.Cr;
		cr.Antialias = Antialias.Subpixel;

		cr.SetSourceRGB(0.1, 0.1, 0.1); // 더 어두운 배경
		cr.Paint();
		cr.SetSourceRGB(0.2, 0.4, 0.6);
		cr.Arc(200, 150, 100, 0, 2 * Math.PI);
		cr.Fill();

		PaintBook(cr);
		PaintNotify(cr);
	}

	/// <summary>
	/// 로고 이미지를 화면에 그립니다.
	/// </summary>
	private void DrawLogo(Context cr)
	{
		var img = ResL.GetBitmap(ResL.Housebari);
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

	/// <summary>
	/// 단일 이미지를 화면 크기에 맞게 그립니다.
	/// </summary>
	private void DrawBitmapFit(Context cr, ImageSurface img)
	{
		var (nw, nh) = Doumi.CalcDestSize(Configs.ViewZoom, _width, _height, img.Width, img.Height);
		var rt = Doumi.CalcDestRect(_width, _height, nw, nh);

		cr.Save();
		cr.Translate(rt.X, rt.Y);
		cr.Scale((double)nw / img.Width, (double)nh / img.Height);

		using (var pattern = new SurfacePattern(img))
		{
			pattern.Filter = Doumi.QualityToFilter(Configs.ViewQuality);
			cr.SetSource(pattern);
			cr.Paint();
		}

		// 원래 아래 두줄인데 필터 적용건으로 위로 바뀜
		//cr.SetSourceSurface(img, 0, 0);
		//cr.Paint();
		cr.Restore();
	}

	/// <summary>
	/// 두 이미지를 좌우로 나누어 화면에 그립니다.
	/// </summary>
	private void DrawBitmapHalfHalf(Context cr, ImageSurface l, ImageSurface r)
	{
		var qf = Doumi.QualityToFilter(Configs.ViewQuality);
		var hw = _width / 2;

		// 왼쪽 이미지
		var (lw, lh) = Doumi.CalcDestSize(Configs.ViewZoom, hw, _height, l.Width, l.Height);
		var lb = Doumi.CalcDestRect(hw, _height, lw, lh, HorizAlign.Right);

		cr.Save();
		cr.Translate(lb.X, lb.Y);
		cr.Scale((double)lw / l.Width, (double)lh / l.Height);
		using (var pattern = new SurfacePattern(l))
		{
			pattern.Filter = qf;
			cr.SetSource(pattern);
			cr.Paint();
		}
		//cr.SetSourceSurface(l, 0, 0);
		//cr.Paint();
		cr.Restore();

		// 오른쪽 이미지
		var (rw, rh) = Doumi.CalcDestSize(Configs.ViewZoom, hw, _height, r.Width, r.Height);
		var rb = Doumi.CalcDestRect(hw, _height, rw, rh, HorizAlign.Left);
		rb.X += hw;

		cr.Save();
		cr.Translate(rb.X, rb.Y);
		cr.Scale((double)rw / r.Width, (double)rh / r.Height);
		using (var pattern = new SurfacePattern(r))
		{
			pattern.Filter = qf;
			cr.SetSource(pattern);
			cr.Paint();
		}
		//cr.SetSourceSurface(r, 0, 0);
		//cr.Paint();
		cr.Restore();
	}

	/// <summary>
	/// 화면 전체를 다시 그립니다.
	/// </summary>
	private void DrawBook()
	{
		StopAnimation();

		var book = Book;
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

	/// <summary>
	/// 책 내용을 화면에 그립니다.
	/// </summary>
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

		var book = Book;
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
	}

	#endregion
}

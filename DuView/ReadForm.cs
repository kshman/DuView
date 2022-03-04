namespace DuView;

public partial class ReadForm : Form
{
	private BookBase? Book { get; set; }

	private Bitmap? _bmp;
	private readonly string _init_filename;

	private readonly BadakFormWorker _bfw;
	private readonly PageSelectForm _select;
	private readonly OptionForm _option;

	private readonly Rectangle[] _click_bound = new Rectangle[2];

	private System.Windows.Forms.Timer _notify_timer;

	private string? _exrun_filename;
	private FormWindowState _exrun_windowstate;

	#region 만들기
	public ReadForm(string filename)
	{
		InitializeComponent();

		//
		Opacity = 0;
		SystemButton.Form = this;
		TitleLabel.Font = new Font("Malgun Gothic", TitleLabel.Font.Size, FontStyle.Regular, GraphicsUnit.Point);
		BookCanvas.MouseWheel += BookCanvas_MouseWheel;

		//
		_init_filename = filename;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			//BodyAsTitle = true,
			MoveTopToMaximize = false,
		};
		_select = new PageSelectForm();
		_option = new OptionForm();

		_notify_timer = new() { Interval = 5000 };
		_notify_timer.Tick += NotifyTimerTick;
	}
	#endregion

	#region 폼 명령
	private void ReadForm_Load(object sender, EventArgs e)
	{
		Settings.WhenMainLoad(this);
		ApplyUiSetting();
		ResetFocus();

		//
		if (!string.IsNullOrEmpty(_init_filename))
			OpenBook(_init_filename);
	}

	private void ReadForm_FormClosing(object sender, FormClosingEventArgs e)
	{

	}

	private void ReadForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		if (Book != null)
		{
			Settings.SetRecentlyPage(Book);
			Book.Dispose();
			Book = null;
		}

		Settings.KeepLocationSize(WindowState, Location, Size);
		Settings.SaveFileInfos();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		FormDu.EffectAppear(this);
	}

	protected override void WndProc(ref Message m)
	{
		if (_bfw.WndProc(ref m))
		{
			// 할건 없다
		}
		else if (FormDu.ReceiveCopyDataString(ref m, out var s) && !string.IsNullOrEmpty(s))
		{
			// 파일명 전달받음
			var filename = Converter.DecodingString(s);
			if (filename != null)
				OpenBook(filename);
		}
		else
		{
			if (Settings.GeneralUseMagnetic)
			{
				// 윈도우포스
				FormDu.MagneticDockForm(ref m, this, Settings.MagneticDockSize);
			}

			// 후....
			base.WndProc(ref m);
		}
	}

	public override string Text
	{
		get => base.Text;
		set
		{
			base.Text = value;
			TitleLabel.Text = value;
		}
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

		_click_bound[0] = new Rectangle(rt.Left, rt.Top, w4, rt.Height);
		_click_bound[1] = new Rectangle(rt.Right - w4, rt.Top, w4, rt.Height);

		DrawBook();
	}

	private void ReadForm_DragEnter(object sender, DragEventArgs e)
	{
		e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop) ?
			DragDropEffects.Link : DragDropEffects.None;
	}

	private void ReadForm_DragDrop(object sender, DragEventArgs e)
	{
		if (e.Data?.GetData(DataFormats.FileDrop) is string[] { Length: > 0 } filenames)
		{
			// 하나만 쓴다
			OpenBook(filenames[0]);
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
				PageControl(Types.Controls.SeekMinusOne);
				break;

			case Keys.Down:
			case Keys.OemPeriod:
			case Keys.OemQuestion:
				PageControl(Types.Controls.SeekPlusOne);
				break;

			case Keys.Left:
				PageControl(e.Shift ? Types.Controls.SeekMinusOne : Types.Controls.Previous);
				break;

			case Keys.Right:
			case Keys.NumPad0:
			case Keys.Space:
				PageControl(e.Shift ? Types.Controls.SeekPlusOne : Types.Controls.Next);
				break;

			case Keys.Home:
				PageControl(Types.Controls.First);
				break;

			case Keys.End:
				PageControl(Types.Controls.Last);
				break;

			case Keys.PageUp:
				PageControl(Types.Controls.SeekPrevious10);
				break;

			case Keys.PageDown:
			case Keys.Back:
				PageControl(Types.Controls.SeekNext10);
				break;

			case Keys.Enter:
				if (e.Control)
					_bfw.Maximize();
				else
					PageControl(Types.Controls.Select);
				break;

			// 보기
			case Keys.D0:
				UpdateViewZoom(!Settings.ViewZoom);
				break;

			case Keys.D1:
				UpdateViewMode(Types.ViewMode.FitWidth);
				break;

			case Keys.D3:
				UpdateViewMode(Types.ViewMode.LeftToRight);
				break;

			case Keys.D4:
				UpdateViewMode(Types.ViewMode.RightToLeft);
				break;

			case Keys.D5:
				UpdateViewQuality(Types.ViewQuality.Default);
				break;

			case Keys.Tab:
				if (Book != null)   // 혼란 방지: 책이 있을때만
				{
					if (Settings.ViewMode == Types.ViewMode.LeftToRight)
						UpdateViewMode(Types.ViewMode.RightToLeft);
					else if (Settings.ViewMode == Types.ViewMode.RightToLeft)
						UpdateViewMode(Types.ViewMode.LeftToRight);
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

			// 기능
			case Keys.F:
				if (!e.Alt) // ALT가 눌리면 ALT+F가 호출되야 하기 때문에
					_bfw.Maximize();
				break;
		}
	}
	#endregion

	#region 패널 명령
	private void TopPanel_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void TopPanel_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void TopPanel_MouseMove(object sender, MouseEventArgs e)
	{
		_bfw.DragOnMove(e);
	}
	#endregion

	#region 캔바스 명령
	private void BookCanvas_MouseDown(object sender, MouseEventArgs e)
	{
		if (Settings.MouseUseClickPage && Book != null)
		{
			if (_click_bound[0].Contains(e.Location))
			{
				PageControl(Types.Controls.Previous);
				return;
			}

			if (_click_bound[1].Contains(e.Location))
			{
				PageControl(Types.Controls.Next);
				return;
			}
		}

		if (_bfw.BodyAsTitle)
			_bfw.DragOnDown(e);
		else
		{
			// 마우스 두번 눌림 
			if (e.Clicks == 2)
			{
				if (Book != null)
				{
					// 책이 있음 최대화
					if (Settings.MouseUseDoubleClickState)
						_bfw.Maximize();
				}
				else
				{
					// 책이 없으면 책 열기
					OpenDialogForBook();
				}
			}
		}
	}

	private void BookCanvas_MouseUp(object sender, MouseEventArgs e)
	{
		if (_bfw.BodyAsTitle)
			_bfw.DragOnUp(e);
	}

	private void BookCanvas_MouseMove(object sender, MouseEventArgs e)
	{
		if (Settings.MouseUseClickPage && Book != null)
		{
			if (_click_bound[0].Contains(e.Location))
				BookCanvas.Cursor = Cursors.PanWest;
			else if (_click_bound[1].Contains(e.Location))
				BookCanvas.Cursor = Cursors.PanEast;
			else
				BookCanvas.Cursor = Cursors.Default;
		}

		if (_bfw.BodyAsTitle)
			_bfw.DragOnMove(e);
	}

	private void BookCanvas_MouseWheel(object? sender, MouseEventArgs e)
	{
		// 위 + / 아래 -
		if (e.Delta > 0)
			PageGoPrev();
		else if (e.Delta < 0)
			PageGoNext();
	}

	private void BookCanvas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
	}
	#endregion

	#region 메뉴 아이템
	private void ApplyUiSetting()
	{
		UpdateViewZoom(Settings.ViewZoom);
		UpdateViewMode(Settings.ViewMode);
		UpdateViewQuality(Settings.ViewQuality);

		TopMost = Settings.GeneralAlwaysTop;
	}

	private void UpdateViewZoom(bool zoom, bool redraw = true)
	{
		Settings.ViewZoom = zoom;
		ViewZoomMenuItem.Checked = zoom;

		if (redraw)
			DrawBook();
	}

	private static readonly Bitmap[] s_viewmode_icon = {
		Properties.Resources.viewmode_pitwidth,
		Properties.Resources.viewmode_pitwidth,
		Properties.Resources.viewmode_l2r,
		Properties.Resources.viewmode_r2l,
	};

	private void UpdateViewMode(Types.ViewMode mode, bool redraw = true)
	{
		Settings.ViewMode = mode;
		ViewFitMenuItem.Checked = mode == Types.ViewMode.FitWidth;
		ViewLeftRightMenuItem.Checked = mode == Types.ViewMode.LeftToRight;
		ViewRightLeftMenuItem.Checked = mode == Types.ViewMode.RightToLeft;

		ViewMenuItem.Image = s_viewmode_icon[(int)mode];

		if (redraw)
		{
			Book?.PrepareImages();
			DrawBook();
		}
	}

	private void UpdateViewQuality(Types.ViewQuality quality, bool redraw = true)
	{
		Settings.ViewQuality = quality;
		QualityLowPopupItem.Checked = VwqLowMenuItem.Checked = quality == Types.ViewQuality.Low;
		QualityDefaultPopupItem.Checked = VwqDefaultMenuItem.Checked = quality == Types.ViewQuality.Default;
		QualityBilinearPopupItem.Checked = VwqBilinearMenuItem.Checked = quality == Types.ViewQuality.Bilinear;
		QualityBicubicPopupItem.Checked = VwqBicubicMenuItem.Checked = quality == Types.ViewQuality.Bicubic;
		QualityHighPopupItem.Checked = VwqHighMenuItem.Checked = quality == Types.ViewQuality.High;
		QualityHqBilinearPopupItem.Checked = VwqHqBilinearMenuItem.Checked = quality == Types.ViewQuality.HqBilinear;
		QualityHqBicubicPopupItem.Checked = VwqHqBicubicMenuItem.Checked = quality == Types.ViewQuality.HqBicubic;

		if (redraw)
			DrawBook();
	}

	private void ViewZoomMenuItem_Click(object sender, EventArgs e)
	{
		UpdateViewZoom(!ViewZoomMenuItem.Checked);
	}

	private void ViewQualityMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem { Tag: { } } i)
		{
			var q = (Types.ViewQuality)i.Tag;
			UpdateViewQuality(q);
		}
	}

	private void ViewModeMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem { Tag: { } } i)
		{
			var m = (Types.ViewMode)i.Tag;
			UpdateViewMode(m);
		}
	}

	private void FileExitMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FileOpenMenuItem_Click(object sender, EventArgs e)
	{
		OpenDialogForBook();
	}

	private void FileOpenLastMenuItem_Click(object sender, EventArgs e)
	{
		if (Book != null && Book.FileName == Settings.LastFileName)
			return;

		OpenBook(Settings.LastFileName);
	}

	private void FileOpenExternalMenuItem_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Settings.ExternalRun) || !File.Exists(Settings.ExternalRun))
		{
			ShowNotification(2429, 121);
			return;
		}

		if (Book != null)
		{
			_exrun_filename = Book.FileName;
			_exrun_windowstate = WindowState;

			WindowState = FormWindowState.Minimized;
			CloseBook();

			var ps = new System.Diagnostics.Process();
			ps.StartInfo.FileName = Settings.ExternalRun;
			ps.StartInfo.Arguments = _exrun_filename;
			ps.StartInfo.UseShellExecute = false;
			ps.StartInfo.CreateNoWindow = true;
			ps.EnableRaisingEvents = true;
			ps.Exited += (s, e) => ExternalRun_Exited();
			ps.Start();
		}
	}

	private void ExternalRun_Exited()
	{
		Invoke(new Action(() =>
		{
			if (!string.IsNullOrEmpty(_exrun_filename))
				OpenBook(_exrun_filename);
			WindowState = _exrun_windowstate;
		}));
	}

	private void FileCloseMenuItem_Click(object sender, EventArgs e)
	{
		CloseBook();
	}

	private void FileCopyImageMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (Book is { PageLeft: { } })
			{
				Clipboard.SetImage(Book.PageLeft);
				ShowNotification(101, 102);
			}
		}
		catch
		{
			ShowNotification(101, 102, true);
		}
	}

	private void FileRefreshMenuItem_Click(object sender, EventArgs e)
	{
#if DEBUG
		if (Book != null)
		{
			Book.PrepareImages();
			DrawBook();
		}
		else
		{
			ShowNotification("타이틀 안씀", "이 것은 알림 메시지 테스트!");
		}
#else
		Book?.PrepareImages();
		DrawBook();
#endif
	}

	private void FileOptionMenuItem_Click(object sender, EventArgs e)
	{
		TopMost = false;
		_option.ShowDialog(this, 0);
		TopMost = Settings.GeneralAlwaysTop;

		Book?.PrepareImages();
		DrawBook();
	}

	private void PageControlMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem { Tag: { } } i)
		{
			var c = (Types.Controls)i.Tag;
			PageControl(c);
		}
	}

	private void FileDeleteMenuItem_Click(object sender, EventArgs e)
	{
		DeleteBookOrItem();
	}
	#endregion

	#region 파일 처리
	// 책 닫기
	private void CloseBook()
	{
		if (Book != null)
		{
			Text = Locale.Text(0);

			Settings.SetRecentlyPage(Book);

			Book.Dispose();
			Book = null;

			DrawBook();

			_select.ResetBook();
		}

		ResetFocus();
	}

	// 책 고르기 다이얼로그
	private void OpenDialogForBook()
	{
		var dlg = new OpenFileDialog()
		{
			Title = Locale.Text(104),
			Filter = Locale.Text(105),
			InitialDirectory = Settings.LastFolder,
		};

		if (dlg.ShowDialog() == DialogResult.OK)
			OpenBook(dlg.FileName);
	}

	// 책 열기
	private void OpenBook(string filename, int page = -1)
	{
		BookBase? bk;

		if (File.Exists(filename))
		{
			// 단일 파일 또는 아카이브
			var fi = new FileInfo(filename);
			var ext = fi.Extension.ToLower();

			if (ToolBox.IsArchiveType(ext))             // 압축 버전
				bk = OpenArchive(fi, ext);
			else if (ToolBox.IsValidImageFile(ext))     // 단독 이미지 버전
				bk = null;
			else
				bk = null;
		}
		else if (Directory.Exists(filename))
		{
			// 디렉토리
			var di = new DirectoryInfo(filename);
			bk = OpenFolder(di);
		}
		else
		{
			// 멍미...
			bk = null;
		}

		if (bk != null)
		{
			if (Book != null)
			{
				Settings.SetRecentlyPage(Book);
				Book.Dispose();
			}

			Book = bk;

			if (page < 0)
				page = Settings.GetRecentlyPage(bk.OnlyFileName);

			bk.CurrentPage = page;
			bk.PrepareImages();

			Text = bk.OnlyFileName;
			Settings.LastFileName = filename;

			DrawBook();

			_select.SetBook(bk);
		}
		else
		{
			// 책없나
			ShowNotification(106, 117, true);
		}

		ResetFocus();
	}

	// 아카이브 책 열기
	private BookBase? OpenArchive(FileInfo fi, string ext)
	{
		Settings.LastFolder = fi.DirectoryName ?? string.Empty;

		BookBase? bk = ext switch
		{
			".zip" => BookZip.FromFile(fi.FullName),
			_ => null,
		};

		if (bk == null)
		{
			// 아카이브가 뭔가 잘못됨
			ShowNotification(106, 107, true);
		}

		return bk;
	}

	// 디렉토리 책 열기
	private BookFolder? OpenFolder(DirectoryInfo di)
	{
		Settings.LastFolder = di.Parent?.FullName ?? string.Empty;

		BookFolder? bk = BookFolder.FromFolder(di);

		if (bk == null)
		{
			// 아카이브가 뭔가 잘못됨
			ShowNotification(106, 108, true);
		}

		return bk;
	}

	// 지금 보다 이전 책 열기
	private void OpenPrevBook()
	{
		if (Book == null)
			return;

		var filename = Book.FindNextFile(true);
		if (filename == null)
		{
			ShowNotification(106, 109);
			return;
		}

		OpenBook(filename);
	}

	// 지금의 다음 책 열기
	private void OpenNextBook()
	{
		if (Book == null)
			return;

		var filename = Book.FindNextFile(false);
		if (filename == null)
		{
			ShowNotification(106, 110);
			return;
		}

		OpenBook(filename);
	}

	// 책을 지우거나 파일 지우기
	private void DeleteBookOrItem()
	{
		if (Book == null)
			return;

		if (!Book.CanDeleteFile(out var reason))
			return;

		if (!string.IsNullOrEmpty(reason) && Settings.GeneralConfirmDelete)
		{
			if (MessageBox.Show(this, reason, Locale.Text(114), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;
		}

		var nextfilename = Book.FindNextFile(false) ?? Book.FindNextFile(true) ?? null;

		if (!Book.DeleteFile(out var closebook))
		{
			MessageBox.Show(this, Locale.Text(115), Locale.Text(114), MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		if (closebook)
		{
			// 현재 책을 지우라는 뜻
			Book.CurrentPage = 0;
			CloseBook();

			if (!string.IsNullOrEmpty(nextfilename))
				OpenBook(nextfilename);
		}
		else
		{
			// 예컨데 거짓이면, 파일 단위로 안에서 처리해버렸기 때문이다
			// 그러므로 다시 그림
			Book.PrepareImages();
			DrawBook();
		}
	}
	#endregion

	#region 그리기
	// 로고 그리기
	private static void DrawLogo(Graphics g, int w, int h)
	{
		var img = Properties.Resources.housebari_head_128;

		if (w > img.Width && h > img.Height)
			g.DrawImage(img, w - img.Width - 50, h - img.Height - 50);
		else
		{
			var rt = new Rectangle(0, 0, w, h);
			g.DrawImage(img, rt);
		}
	}

	// 가로로 차게 이미지 그리기
	private static void DrawBitmapFitWidth(Graphics g, Image bmp, Image img, HorizontalAlignment align = HorizontalAlignment.Center)
	{
		(int nw, int nh) = ToolBox.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
		var rt = ToolBox.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);

		g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
	}

	// 두장 그리기
	private static void DrawBitmapHalfAndHalf(Graphics g, Image bmp, Image left, Image right)
	{
		var f = bmp.Width / 2;

		// 왼쪽
		var (w, h) = ToolBox.CalcDestSize(Settings.ViewZoom, f, bmp.Height, left.Width, left.Height);
		var rt = ToolBox.CalcDestRect(f, bmp.Height, w, h, HorizontalAlignment.Right);

		g.DrawImage(left, rt, 0, 0, left.Width, left.Height, GraphicsUnit.Pixel);

		// 오른쪽
		(w, h) = ToolBox.CalcDestSize(Settings.ViewZoom, f, bmp.Height, right.Width, right.Height);
		rt = ToolBox.CalcDestRect(f, bmp.Height, w, h, HorizontalAlignment.Left);
		rt.X += f;

		g.DrawImage(right, rt, 0, 0, right.Width, right.Height, GraphicsUnit.Pixel);
	}

	// 그리기
	private void DrawBook()
	{
		// 먼저 페이지 정보
		if (Book == null)
		{
			PageInfo.Visible = false;
			MaxCacheMenuItem.Text = Locale.Text(1800);
		}
		else
		{
			PageInfo.Text = $"{Book.CurrentPage + 1}/{Book.TotalPage}";
			PageInfo.Visible = true;

			var cache = ToolBox.SizeToString(Book.CacheSize);
			MaxCacheMenuItem.Text = $"[{cache}]";
		}

		// 창이 최소화면 그려션 안된다
		if (WindowState == FormWindowState.Minimized)
			return;

		// 본격 그리기
		var w = BookCanvas.Width;
		var h = BookCanvas.Height;

		if (w == 0 || h == 0)
		{
			ShowNotification(111, 112, true);
			return;
		}

		if (_bmp == null || _bmp.Width != w || _bmp.Height != h)
		{
			_bmp?.Dispose();
			_bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		}

		using (var g = Graphics.FromImage(_bmp))
		{
			g.Clear(Color.FromArgb(10, 10, 10));
			g.InterpolationMode = ToolBox.QualityToInterpolationMode(Settings.ViewQuality);

			if (Book == null)
				DrawLogo(g, w, h);
			else
			{
				if (Settings.ViewMode == Types.ViewMode.FitWidth)
				{
					if (Book.PageLeft != null)
						DrawBitmapFitWidth(g, _bmp, Book.PageLeft);
				}
				else
				{
					if (Book.PageLeft == null)
					{
						if (Book.PageRight == null)
						{
							// 헐 뭐지
							DrawLogo(g, w, h);
						}
						else
						{
							// 오른쪽 한장만 그리는거다
							DrawBitmapFitWidth(g, _bmp, Book.PageRight);
						}
					}
					else if (Book.PageRight == null)
					{
						// 왼쪽 한장만 그리는거다
						DrawBitmapFitWidth(g, _bmp, Book.PageLeft);
					}
					else
					{
						// 양쪽 다
						DrawBitmapHalfAndHalf(g, _bmp, Book.PageLeft, Book.PageRight);
					}
				}
			}

			// 그림위에 그리려면 여기에다가
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
		}

		BookCanvas.Image = _bmp;
	}
	#endregion

	#region 책 조작
	// 조작하기
	private void PageControl(Types.Controls ctrl)
	{
		if (Book == null)
			return;

		switch (ctrl)
		{
			case Types.Controls.Previous:
				PageGoPrev();
				break;

			case Types.Controls.Next:
				PageGoNext();
				break;

			case Types.Controls.First:
				PageGoTo(0);
				break;

			case Types.Controls.Last:
				PageGoTo(int.MaxValue);
				break;

			case Types.Controls.SeekPrevious10:
				PageGoDelta(-10);
				break;

			case Types.Controls.SeekNext10:
				PageGoDelta(10);
				break;

			case Types.Controls.SeekMinusOne:
				PageGoDelta(-1);
				break;

			case Types.Controls.SeekPlusOne:
				PageGoDelta(1);
				break;

			case Types.Controls.ScanPrevious:
				OpenPrevBook();
				break;

			case Types.Controls.ScanNext:
				OpenNextBook();

				break;
			case Types.Controls.Select:
				PageSelect();
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(ctrl), ctrl, null);
		}
	}

	// 쪽 이동
	private void PageGoTo(int page)
	{
		if (Book != null && Book.MovePage(page))
		{
			Book.PrepareImages();
			DrawBook();
		}
	}

	// 지정한 만큼 쪽 이동
	private void PageGoDelta(int delta)
	{
		if (Book != null && Book.MovePage(Book.CurrentPage + delta))
		{
			Book.PrepareImages();
			DrawBook();
		}
	}

	// 이전 쪽으로
	private void PageGoPrev()
	{
		if (Book != null && Book.MovePrev())
		{
			Book.PrepareImages();
			DrawBook();
		}
	}

	// 다음 쪽으로
	private void PageGoNext()
	{
		if (Book != null && Book.MoveNext())
		{
			Book.PrepareImages();
			DrawBook();
		}
	}

	// 쪽 선택
	private void PageSelect()
	{
		if (Book == null)
			return;

		if (_select.ShowDialog(this, Book.CurrentPage) == DialogResult.OK)
		{
			Book.CurrentPage = _select.SelectedPage;
			Book.PrepareImages();
			DrawBook();
		}
	}
	#endregion

	#region 도움
	private void ShowNotification(string title, string mesg, bool iserror = false, int timeout = 2000)
	{
		if (Settings.GeneralUseWinNotify)
			Notifier.ShowBalloonTip(timeout, title, mesg, iserror ? ToolTipIcon.Error : ToolTipIcon.Info);
		else
		{
			if (!NotifyLabel.Visible)
			{
				// 보이게 함
				NotifyLabel.Location = new Point(NotifyLabel.Location.X, (BookCanvas.Height - NotifyLabel.Height) / 2);

				NotifyLabel.Text = mesg;
				ControlDu.EffectFadeIn(NotifyLabel);

				_notify_timer.Interval = timeout;
				_notify_timer.Start();
			}
			else
			{
				// 있는거 메시지 바꿈
				NotifyLabel.Text = mesg;

				_notify_timer.Stop();
				_notify_timer.Interval = timeout;
				_notify_timer.Start();
			}
		}
	}

	private void ShowNotification(int title, int mesg, bool iserror = false, int timeout = 2000)
	{
		ShowNotification(Locale.Text(title), Locale.Text(mesg), iserror, timeout);
	}

	private void NotifyTimerTick(object? sender, EventArgs e)
	{
		_notify_timer.Stop();
		ControlDu.EffectFadeOut(NotifyLabel);
	}
	#endregion // 도움
}


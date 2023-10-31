using System.Drawing.Imaging;
using DuView.Types;

namespace DuView;

public partial class ReadForm : Form, ILocaleTranspose
{
	private BookBase? Book { get; set; }

	private Bitmap? _bmp;
	private readonly string _init_filename;

	private readonly BadakFormWorker _bfw;
	private readonly PageSelectForm _select;
	private readonly OptionForm _option;

	private readonly Rectangle[] _click_bound = new Rectangle[2];

	private readonly System.Windows.Forms.Timer _notify_timer;

	private string? _extern_run_filename;
	private FormWindowState _extern_run_window_state;

	private BookDirection _book_direction = BookDirection.Next;

	private PageImage? _animate;
	private CancellationTokenSource? _animCancel;

	private bool _passmode;
	private Action? _passaction;

	private readonly System.Windows.Forms.Timer _mouse_timer;
	private bool _mouse_hide;

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

		_notify_timer = new System.Windows.Forms.Timer { Interval = 5000 };
		_notify_timer.Tick += NotifyTimerTick;

		_mouse_timer = new System.Windows.Forms.Timer { Interval = 5000 };
		_mouse_timer.Tick += MouseTimerTick;

		//
		LocaleTranspose();
	}

	#endregion 만들기

	#region 폼 명령

	private void ReadForm_Load(object sender, EventArgs e)
	{
		Settings.WhenMainLoad(this);
		ApplyUiSetting();
		ResetFocus();

		//
		PaintBook();

		if (!string.IsNullOrEmpty(_init_filename))
			OpenBook(_init_filename);
	}

	public void LocaleTranspose()
	{
		ToolBox.LocaleTextOnControl(this);
		ToolBox.LocaleTextOnControl(_select);
		ToolBox.LocaleTextOnControl(_option);
	}

	private void ReadForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		//
	}

	private void ReadForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		CleanBook();

		Settings.KeepMoveLocation();
		Settings.KeepLocationSize(WindowState, Location, Size);
		Settings.SaveSettings();
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
		if (_passmode)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					EscapePassMode();
					break;
			}

			return;
		}

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
				if (e.Control)
					_bfw.Maximize();
				else
					PageControl(BookControl.Select);
				break;

			// 보기
			case Keys.D0:
				UpdateViewZoom(!Settings.ViewZoom);
				break;

			case Keys.D1:
				UpdateViewMode(ViewMode.FitWidth);
				break;

			case Keys.D3:
				UpdateViewMode(ViewMode.LeftToRight);
				break;

			case Keys.D4:
				UpdateViewMode(ViewMode.RightToLeft);
				break;

			case Keys.D5:
				UpdateViewQuality(ViewQuality.Default);
				break;

			case Keys.Tab:
				if (Book != null)   // 혼란 방지: 책이 있을때만
				{
					if (Settings.ViewMode == ViewMode.LeftToRight)
						UpdateViewMode(ViewMode.RightToLeft);
					else if (Settings.ViewMode == ViewMode.RightToLeft)
						UpdateViewMode(ViewMode.LeftToRight);
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
					SetRememberBook();
				break;
			case Keys.OemSemicolon: // Oem1
				if (e.Control)
					OpenRememberBook();
				break;

			// 기능
			case Keys.F:
				if (!e.Alt) // ALT가 눌리면 ALT+F가 호출되야 하기 때문에
					_bfw.Maximize();
				break;

			case Keys.L:
				if (e.Control)
					LockPassCode();
				break;

			case Keys.Oemtilde:
				LockPassCode();
				break;

#if DEBUG && true
			default:
				System.Diagnostics.Debug.WriteLine($"키코드: {e.KeyCode}");
				break;
#endif
		}
	}

	#endregion 폼 명령

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

	#endregion 패널 명령

	#region 캔바스 명령

	private void BookCanvas_MouseDown(object sender, MouseEventArgs e)
	{
		if (Settings.MouseUseClickPage && Book != null)
		{
			if (_click_bound[0].Contains(e.Location))
			{
				PageControl(BookControl.Previous);
				return;
			}

			if (_click_bound[1].Contains(e.Location))
			{
				PageControl(BookControl.Next);
				return;
			}
		}

		if (_bfw.BodyAsTitle)
			_bfw.DragOnDown(e);
		else
		{
			// 마우스 두번 눌림
			if (e.Clicks != 2)
				return;

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

		TestMouseHide();
	}

	private void BookCanvas_MouseWheel(object? sender, MouseEventArgs e)
	{
		// 위 + / 아래 -
		if (e.Delta > 0)
			PageGoPrev();
		else if (e.Delta < 0)
			PageGoNext();

		TestMouseHide();
	}

	private void BookCanvas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		//
	}

	#endregion 캔바스 명령

	#region 메뉴 아이템

	private void ApplyUiSetting()
	{
		UpdateViewZoom(Settings.ViewZoom);
		UpdateViewMode(CurrentViewMode);
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

	private void UpdateViewMode(ViewMode mode, bool redraw = true)
	{
		Settings.ViewMode = mode;
		ViewFitMenuItem.Checked = mode == ViewMode.FitWidth;
		ViewLeftRightMenuItem.Checked = mode == ViewMode.LeftToRight;
		ViewRightLeftMenuItem.Checked = mode == ViewMode.RightToLeft;

		ViewMenuItem.Image = s_viewmode_icon[(int)mode];

		if (!redraw)
			return;

		Book?.PrepareImages();
		DrawBook();
	}

	private void UpdateViewQuality(ViewQuality quality, bool redraw = true)
	{
		Settings.ViewQuality = quality;
		QualityLowPopupItem.Checked = VwqLowMenuItem.Checked = quality == ViewQuality.Low;
		QualityDefaultPopupItem.Checked = VwqDefaultMenuItem.Checked = quality == ViewQuality.Default;
		QualityBilinearPopupItem.Checked = VwqBilinearMenuItem.Checked = quality == ViewQuality.Bilinear;
		QualityBicubicPopupItem.Checked = VwqBicubicMenuItem.Checked = quality == ViewQuality.Bicubic;
		QualityHighPopupItem.Checked = VwqHighMenuItem.Checked = quality == ViewQuality.High;
		QualityHqBilinearPopupItem.Checked = VwqHqBilinearMenuItem.Checked = quality == ViewQuality.HqBilinear;
		QualityHqBicubicPopupItem.Checked = VwqHqBicubicMenuItem.Checked = quality == ViewQuality.HqBicubic;

		if (redraw)
			DrawBook();
	}

	private void ViewZoomMenuItem_Click(object sender, EventArgs e)
	{
		UpdateViewZoom(!ViewZoomMenuItem.Checked);
	}

	private void ViewQualityMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem { Tag: { } } i)
			return;

		var q = (ViewQuality)i.Tag;
		UpdateViewQuality(q);
	}

	private void ViewModeMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem { Tag: { } } i)
			return;

		var m = (ViewMode)i.Tag;
		UpdateViewMode(m);
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

		PromptPassModeIfNeed(PassCodeUsage.LastBook, () => OpenBook(Settings.LastFileName));
	}

	private void FileOpenExternalMenuItem_Click(object sender, EventArgs e)
	{
		OpenExternalRun();
	}

	private void FileCloseMenuItem_Click(object sender, EventArgs e)
	{
		CloseBook();
	}

	private void FileRenameMenuItem_Click(object sender, EventArgs e)
	{
		RenameBook();
	}

	private void FileMoveMenuItem_Click(object sender, EventArgs e)
	{
		MoveBook();
	}

	private void FileCopyImageMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (Book is not { PageLeft: not null })
				return;

			Clipboard.SetImage(Book.PageLeft.Image);
			ShowNotification(101, 102);
		}
		catch
		{
			ShowNotification(102);
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
			ShowNotification("이 것은 알림 메시지 테스트!");
		}
#else
		Book?.PrepareImages();
		DrawBook();
#endif
	}

	private void FileOptionMenuItem_Click(object sender, EventArgs e)
	{
		PromptPassModeIfNeed(PassCodeUsage.Option, () =>
		{
			TopMost = false;
			_option.ShowDialog(this, 0);
			TopMost = Settings.GeneralAlwaysTop;

			Book?.PrepareImages();
			DrawBook();
		});
	}

	private void PageControlMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem { Tag: not null } i)
			return;

		var c = (BookControl)i.Tag;
		PageControl(c);
	}

	private void FileDeleteMenuItem_Click(object sender, EventArgs e)
	{
		DeleteBookOrItem();
	}

	#endregion 메뉴 아이템

	#region 파일 처리

	// 책 닫기 공통
	private void CleanBook()
	{
		StopAnimation();

		if (Book != null)
		{
			Settings.SetRecentlyPage(Book);
			Book.Dispose();
			Book = null;

			_select.ResetBook();

			// 강제GC
#if false
			GC.Collect();
#endif
		}

		ResetFocus();
	}

	// 책 닫기
	private void CloseBook()
	{
		if (Book == null)
			return;

		CleanBook();

		TitleLabel.Text = Text = Locale.Text(0);
		DrawBook();
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
		BookBase? bk = null;

		if (File.Exists(filename))
		{
			// 단일 파일 또는 아카이브
			var fi = new FileInfo(filename);
			var ext = fi.Extension.ToLower();

			if (ToolBox.IsArchiveType(ext))             // 압축 버전
				bk = OpenArchive(fi, ext);
			else if (ext.IsValidImageFile()) // 단독 이미지 버전
			{
				var di = fi.Directory;
				if (di != null)
				{
					bk = OpenFolder(di);
					if (bk is BookFolder folder)
					{
						page = folder.GetPageNumber(fi.FullName);
						folder.ViewMode = ViewMode.FitHeight;
					}
				}
			}
		}
		else if (Directory.Exists(filename))
		{
			// 디렉토리
			var di = new DirectoryInfo(filename);
			bk = OpenFolder(di);
		}

		if (bk != null)
		{
			CleanBook();
			Book = bk;

			if (page < 0)
				page = Settings.GetRecentlyPage(bk.OnlyFileName);

			bk.CurrentPage = page;
			bk.PrepareImages();

			TitleLabel.Text = bk.OnlyFileName;
			Settings.LastFileName = filename;

			DrawBook();

			_select.SetBook(bk);
		}
		else
		{
			// 책없나
			ShowNotification(117);
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
			ShowNotification(107);
		}

		return bk;
	}

	// 디렉토리 책 열기
	private BookFolder? OpenFolder(DirectoryInfo di)
	{
		Settings.LastFolder = di.Parent?.FullName ?? string.Empty;

		var bk = BookFolder.FromFolder(di);

		if (bk == null)
		{
			// 아카이브가 뭔가 잘못됨
			ShowNotification(108);
		}

		return bk;
	}

	// 지금 보다 이전 책 열기
	private void OpenPrevBook()
	{
		if (Book == null)
			return;

		var filename = Book.FindNextFile(BookDirection.Previous);
		if (filename == null)
		{
			ShowNotification(106, 109);
			return;
		}

		_book_direction = BookDirection.Previous;
		OpenBook(filename);
	}

	// 지금의 다음 책 열기
	private void OpenNextBook()
	{
		if (Book == null)
			return;

		var filename = Book.FindNextFile(BookDirection.Next);
		if (filename == null)
		{
			ShowNotification(106, 110);
			return;
		}

		_book_direction = BookDirection.Next;
		OpenBook(filename);
	}

	// 책을 지우거나 파일 지우기
	private void DeleteBookOrItem()
	{
		if (Book == null)
			return;

		if (!Book.CanDeleteFile(out var reason))
			return;

		if (!string.IsNullOrEmpty(reason) && Settings.GeneralConfirmDelete &&
			MessageBox.Show(this, reason, Locale.Text(114), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			return;

		var dir = Settings.KeepBookDirection ? _book_direction : BookDirection.Next;
		var nextfilename = Book.FindNextFileAny(dir);

		if (!Book.DeleteFile(out var closebook))
		{
			ShowNotification(115, 3000);
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

	private void OpenExternalRun()
	{
		if (string.IsNullOrEmpty(Settings.ExternalRun) || !File.Exists(Settings.ExternalRun))
		{
			ShowNotification(2429, 121);
			return;
		}

		if (Book == null)
			return;

		_extern_run_filename = Book.FileName;
		_extern_run_window_state = WindowState;

		WindowState = FormWindowState.Minimized;
		CloseBook();

		var ps = new System.Diagnostics.Process();
		ps.StartInfo.FileName = Settings.ExternalRun;
		ps.StartInfo.Arguments = _extern_run_filename;
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
			if (Settings.ReloadAfterExternal && !string.IsNullOrEmpty(_extern_run_filename))
				OpenBook(_extern_run_filename);
			WindowState = _extern_run_window_state;
		});
	}

	private void RenameBook()
	{
		if (Book == null)
			return;

		PromptPassModeIfNeed(PassCodeUsage.RenameBook, () =>
		{
			string? filename;

			if (Settings.ExtendedRenamer)
			{
				var dlg = new RenexForm();
				if (dlg.ShowDialog(this, Book.OnlyFileName) != DialogResult.OK)
					return;

				filename = dlg.Filename;
			}
			else
			{
				var dlg = new RenameForm();
				if (dlg.ShowDialog(this, Book.OnlyFileName) != DialogResult.OK)
					return;

				filename = dlg.Filename;
			}

			if (string.IsNullOrEmpty(filename) || Book.OnlyFileName.Equals(filename))
				return;

			// 설정에 다음 파일을 열면 다음 파일을 아니면 바뀐 이름 책을 열게함
			var dir = Settings.KeepBookDirection ? _book_direction : BookDirection.Next;
			var nextfilename = Settings.RenameOpenNext ? Book.FindNextFileAny(dir) : null;

			// 시작
			if (!Book.RenameFile(filename, out var fullpath))
			{
				ShowNotification(123, 3000);
				return;
			}

			Book.CurrentPage = 0;
			CloseBook();

			OpenBook(nextfilename ?? fullpath);
		});
	}

	private void MoveBook()
	{
		if (Book == null)
			return;

		PromptPassModeIfNeed(PassCodeUsage.MoveBook, () =>
		{
			var dlg = new MoveForm();
			if (dlg.ShowDialog(this, Book.OnlyFileName) != DialogResult.OK)
				return;

			var filename = dlg.Filename;
			var nextfilename = Book.FindNextFileAny(BookDirection.Next);

			//
			if (!Book.MoveFile(filename))
			{
				ShowNotification(129, 3000);
				return;
			}

			CloseBook();

			if (string.IsNullOrEmpty(nextfilename))
				ShowNotification(106, 110);
			else
			{
				_book_direction = BookDirection.Next;
				OpenBook(nextfilename);
			}
		});
	}

	private void SetRememberBook()
	{
		if (Book == null)
			return;

		Settings.RememberFileName = Book.FileName;
		ShowNotification(132);
	}

	private void OpenRememberBook()
	{
		var filename = Settings.RememberFileName;
		if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
			return;

		CloseBook();
		OpenBook(filename);
	}

	#endregion 파일 처리

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

	// WEBP 애니메이션 스레드
	private void OnWebPAnimWorker(object? obj)
	{
		if (obj == null)
			return;

		var token = (CancellationToken)obj;

		while (!token.IsCancellationRequested)
		{
			var duration = _animate?.Animate() ?? -1;
			if (duration < 0)
				break;

			try
			{
				Thread.Sleep(duration);

				if (token.IsCancellationRequested)
					break;

				Invoke(PaintBook);
			}
			catch (ObjectDisposedException)
			{
				break;
			}
			catch (ThreadAbortException)
			{
				break;
			}
		}
	}

	// WEBP 애니메이션 태스크
	private async Task OnWebPAnimTask(CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			var duration = _animate?.Animate() ?? -1;
			if (duration < 0)
				break;

			await Task.Delay(duration, token);

			if (token.IsCancellationRequested)
				break;

			Invoke(PaintBook);
		}
	}

	// 
	private Image UpdateAnimation(PageImage page)
	{
		Image img;

		if (page.Frames != null)
		{
			// 애니메이션
			if (_animate != null && _animate != page)
			{
				if (_animCancel != null)
				{
					_animCancel.Cancel();
					Thread.Sleep(Settings.UseAnimationThread ? _animate.LastDuration : 5);
					_animCancel.Dispose();
					_animCancel = null;
				}
			}

			if (_animate != page)
			{
				_animate = page;
				_animate.InitAnimation();

				_animCancel = new CancellationTokenSource();
				if (Settings.UseAnimationThread)
					ThreadPool.QueueUserWorkItem(OnWebPAnimWorker, _animCancel.Token);
				else
					_ = OnWebPAnimTask(_animCancel.Token);

				img = page.Image;
			}
			else
			{
				// 업데이트
				img = page.GetImage();
			}

			if (Book != null)
				PageInfo.Text = $@"[{page.CurrentFrame + 1}/{page.Frames.Count}] {Book.CurrentPage + 1}/{Book.TotalPage}";
		}
		else
		{
			// 그냥 이미지
			img = page.Image;
		}

		return img;
	}

	//
	private void StopAnimation()
	{
		if (_animate == null)
			return;

		if (_animate.Frames != null)
		{
			// WEBP
			if (_animCancel != null)
			{
				_animCancel.Cancel();
				Thread.Sleep(Settings.UseAnimationThread ? _animate.LastDuration : 10);
				_animCancel.Dispose();
				_animCancel = null;
			}
		}

		_animate = null;
	}

	// 가로로 차게 이미지 그리기
	private void DrawBitmapFitWidth(Graphics g, Image bmp, PageImage page, HorizontalAlignment align = HorizontalAlignment.Center)
	{
		var img = UpdateAnimation(page);
		var (nw, nh) = ToolBox.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
		var rt = ToolBox.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);

		g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
	}

	// 두장 그리기
	private static void DrawBitmapHalfAndHalf(Graphics g, Image bmp, PageImage leftPage, PageImage rightPage)
	{
		var f = bmp.Width / 2;

		// 왼쪽
		var left = leftPage.Image;
		var (w, h) = ToolBox.CalcDestSize(Settings.ViewZoom, f, bmp.Height, left.Width, left.Height);
		var rt = ToolBox.CalcDestRect(f, bmp.Height, w, h, HorizontalAlignment.Right);

		g.DrawImage(left, rt, 0, 0, left.Width, left.Height, GraphicsUnit.Pixel);

		// 오른쪽
		var right = rightPage.Image;
		(w, h) = ToolBox.CalcDestSize(Settings.ViewZoom, f, bmp.Height, right.Width, right.Height);
		rt = ToolBox.CalcDestRect(f, bmp.Height, w, h, HorizontalAlignment.Left);
		rt.X += f;

		g.DrawImage(right, rt, 0, 0, right.Width, right.Height, GraphicsUnit.Pixel);
	}

	// 그리기
	private void DrawBook()
	{
		// 애니메이션 부터
		StopAnimation();

		// 먼저 페이지 정보
		if (Book == null)
		{
			PageInfo.Visible = false;
			MaxCacheMenuItem.Text = Locale.Text(1800);
		}
		else
		{
			PageInfo.Text = $@"{Book.CurrentPage + 1}/{Book.TotalPage}";
			PageInfo.Visible = true;

			var cache = ToolBox.SizeToString(Book.CacheSize);
			MaxCacheMenuItem.Text = $@"[{cache}]";

			if (Book.DisplayEntryTitle)
			{
				var name = Book.GetEntryName(Book.CurrentPage);
				if (name == null)
					TitleLabel.Text = $"{Book.OnlyFileName}";
				else
				{
					var fi = new FileInfo(name);
					TitleLabel.Text = $"{Book.OnlyFileName} - {fi.Name}";
				}
			}
		}

		// 화면 크기 검사
		var w = BookCanvas.Width;
		var h = BookCanvas.Height;

		if (w == 0 || h == 0)
		{
			//ShowNotification(112);
			return;
		}

		PaintBook();
	}

	// 화면에 그리기
	private void PaintBook()
	{
		// 창이 최소화면 그려션 안된다
		if (WindowState == FormWindowState.Minimized)
			return;

		// 본격 그리기
		var w = BookCanvas.Width;
		var h = BookCanvas.Height;

		if (w == 0 || h == 0)
			return;

		if (_bmp == null || _bmp.Width != w || _bmp.Height != h)
		{
			_bmp?.Dispose();
			_bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
		}

		using (var g = Graphics.FromImage(_bmp))
		{
			g.Clear(Color.FromArgb(10, 10, 10));
			g.InterpolationMode = ToolBox.QualityToInterpolationMode(Settings.ViewQuality);

			if (Book == null)
				DrawLogo(g, w, h);
			else
			{
				if (CurrentViewMode == ViewMode.FitWidth)
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

#if false
		using (var g = BookCanvas.CreateGraphics())
			g.DrawImage(_bmp, new Point(0, 0));
#else
		BookCanvas.Image = _bmp;
#endif
	}

	// 그리기 오버라이드
	/// <inheritdoc />
	protected override void OnPaint(PaintEventArgs e)
	{
		PaintBook();
		base.OnPaint(e);
	}
	#endregion 그리기

	#region 책 조작

	// 조작하기
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
		if (Book == null || !Book.MovePage(page))
			return;

		Book.PrepareImages();
		DrawBook();
	}

	// 지정한 만큼 쪽 이동
	private void PageGoDelta(int delta)
	{
		if (Book == null || !Book.MovePage(Book.CurrentPage + delta))
			return;

		Book.PrepareImages();
		DrawBook();
	}

	// 이전 쪽으로
	private void PageGoPrev()
	{
		if (Book == null || !Book.MovePrev())
			return;

		Book.PrepareImages();
		DrawBook();
	}

	// 다음 쪽으로
	private void PageGoNext()
	{
		if (Book == null || !Book.MoveNext())
			return;

		Book.PrepareImages();
		DrawBook();
	}

	// 쪽 선택
	private void PageSelect()
	{
		if (Book == null)
			return;

		if (_select.ShowDialog(this, Book.CurrentPage) != DialogResult.OK)
			return;

		Book.CurrentPage = _select.SelectedPage;
		Book.PrepareImages();
		DrawBook();
	}

	// 뷰 모드
	private ViewMode CurrentViewMode
	{
		get
		{
			if (Book == null || Book.ViewMode == ViewMode.Follow)
				return Settings.ViewMode;
			return Book.ViewMode;
		}
	}

	#endregion 책 조작

	#region 도움

	private void ShowNotification(string mesg, int timeout = 2000)
	{
		if (!NotifyLabel.Visible)
		{
			// 보이게 함
			NotifyLabel.Location = NotifyLabel.Location with
			{
				Y = (BookCanvas.Height - NotifyLabel.Height) / 2
			};

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

	private void ShowNotification(int mesg, int timeout = 2000)
	{
		ShowNotification(Locale.Text(mesg), timeout);
	}

	private void NotifyTimerTick(object? sender, EventArgs e)
	{
		_notify_timer.Stop();
		ControlDu.EffectFadeOut(NotifyLabel);
	}

	private void TestMouseHide()
	{
		if (!_mouse_hide)
			return;

		_mouse_hide = false;
		Cursor.Show();

#if false
		if (Book != null)
			_mouse_timer.Start();
#endif
	}

	private void MouseTimerTick(object? sender, EventArgs e)
	{
		_mouse_timer.Stop();

		if (!Focused)
			return;

		if (_mouse_hide)
			return;

		_mouse_hide = true;
		Cursor.Hide();
	}

	#endregion 도움

	#region 패스 모드

	private void PromptPassModeIfNeed(PassCodeUsage usage, Action action)
	{
		if (!Settings.UsePassCode || Settings.UnlockedPassCode || !Settings.TestPassUsage(usage))
		{
			action.Invoke();
			return;
		}

		_passaction = action;
		_passmode = true;

		PassText.Text = string.Empty;
		PassPanel.Location = new Point((Width - PassPanel.Width) / 2, (Height - PassPanel.Height) / 2);
		PassPanel.Visible = true;

		PassText.Focus();
	}

	private void EscapePassMode()
	{
		_passmode = false;

		PassPanel.Visible = false;
	}

	private void LockPassCode()
	{
		if (!Settings.UsePassCode || !Settings.UnlockedPassCode)
			return;

		Settings.UnlockedPassCode = false;
		ShowNotification(131);
	}

	private void PassText_TextChanged(object sender, EventArgs e)
	{
		if (!_passmode)
			return;

		if (!Settings.UnlockPass(PassText.Text))
			return;

		EscapePassMode();

		_passaction?.Invoke();
	}

	#endregion 패스 모드
}
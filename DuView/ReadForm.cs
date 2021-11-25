﻿using Debug = System.Diagnostics.Debug;

namespace DuView;

public partial class ReadForm : Form
{
	private readonly string _title = "두뷰";

	private BookBase? Book { get; set; }

	private Bitmap? _bmp;
	private readonly string _init_filename;

	private readonly BadakFormWorker _bfw;
	private readonly PageSelectForm _select;

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
	}

	#region 폼 명령
	private void ReadForm_Load(object sender, EventArgs e)
	{
		Settings.WhenLoad(this);
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
			// 윈도우포스
			FormDu.MagneticDockForm(ref m, this, Settings.MagneticDockSize);

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
		DrawBook();
	}

	private void ReadForm_DragEnter(object sender, DragEventArgs e)
	{
		e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop) ?
			DragDropEffects.Link : DragDropEffects.None;
	}

	private void ReadForm_DragDrop(object sender, DragEventArgs e)
	{
		if (e.Data != null && e.Data.GetData(DataFormats.FileDrop) is string[] filenames && filenames.Length > 0)
		{
			// 하나만 쓴다
			OpenBook(filenames[0]);
		}
	}

	/*
	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Tab)
		{
			//ResetFocus();
			//return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}
	*/

	private void ReadForm_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
			// 끝
			case Keys.Escape:
				Close();
				break;

			case Keys.W:
				if (e.Control)
					CloseBook();
				break;

			// 페이지
			case Keys.Subtract:
				PageControl(Types.Controls.SeekMinusOne);
				break;

			case Keys.Add:
				PageControl(Types.Controls.SeekPlusOne);
				break;

			case Keys.Up:
			case Keys.Left:
				PageControl(e.Shift ? Types.Controls.SeekMinusOne : Types.Controls.Previous);
				break;

			case Keys.Down:
			case Keys.Right:
			case Keys.NumPad0:
			case Keys.Decimal:
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
				if (Book != null)   // 혼란 방지 책이 있을때만
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

			case Keys.Delete:
				DeleteBookOrItem();
				break;

			// 기능
			case Keys.F:
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
		if (_bfw.BodyAsTitle)
			_bfw.DragOnDown(e);
		else
		{
			// 최대화만 쓰자
			if (e.Clicks == 2)
				_bfw.Maximize();
		}
	}

	private void BookCanvas_MouseUp(object sender, MouseEventArgs e)
	{
		if (_bfw.BodyAsTitle)
			_bfw.DragOnUp(e);
	}

	private void BookCanvas_MouseMove(object sender, MouseEventArgs e)
	{
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
	}

	private void UpdateViewZoom(bool zoom, bool redraw = true)
	{
		Settings.ViewZoom = zoom;
		ViewZoomMenuItem.Checked = zoom;

		if (redraw)
			DrawBook();
	}

	private static readonly Bitmap[] s_viewmode_icon = new Bitmap[]
	{
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
		if (sender is ToolStripMenuItem i && i.Tag is not null)
		{
			Types.ViewQuality q = (Types.ViewQuality)i.Tag;
			UpdateViewQuality(q);
		}
	}

	private void ViewModeMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem i && i.Tag is not null)
		{
			Types.ViewMode m = (Types.ViewMode)i.Tag;
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

	private void FileCloseMenuItem_Click(object sender, EventArgs e)
	{
		CloseBook();
	}

	private void FileCopyImageMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (Book != null && Book.PageLeft != null)
			{
				Clipboard.SetImage(Book.PageLeft);
				Notifier.ShowBalloonTip(1000, "이미지 복사", "클립보드로 복사했습니다!", ToolTipIcon.Info);
			}
		}
		catch
		{
			Notifier.ShowBalloonTip(1000, "이미지 복자", "클립보드에 넣을 수 없습니다", ToolTipIcon.Error);
		}
	}

	private void FileRefreshMenuItem_Click(object sender, EventArgs e)
	{
		Book?.PrepareImages();
		DrawBook();
	}

	private void PageControlMenuItem_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem i && i.Tag is not null)
		{
			Types.Controls c = (Types.Controls)i.Tag;
			PageControl(c);
		}
	}
	#endregion

	#region 파일 처리
	// 책 닫기
	private void CloseBook()
	{
		if (Book != null)
		{
			Text = _title;

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
			Title = "책이나 이미지를 고르세요",
			Filter = "압축 파일|*.zip|모든 파일|*.*",
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
			Notifier.ShowBalloonTip(1000, "책 열기", "열 수 없는 압축 파일입니다!", ToolTipIcon.Error);
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
			Notifier.ShowBalloonTip(1000, "책 열기", "디렉토리를 열 수 없어요!", ToolTipIcon.Error);
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
			Notifier.ShowBalloonTip(1000, "책 열기", "이전 책이 없어요", ToolTipIcon.Info);
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
			Notifier.ShowBalloonTip(1000, "책 열기", "다음 책이 없어요", ToolTipIcon.Info);
			return;
		}

		OpenBook(filename);
	}

	// 책을 지우거나 파일 지우기
	private void DeleteBookOrItem()
	{

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
	private static void DrawBitmapFitWidth(Graphics g, Bitmap bmp, Image img, HorizontalAlignment align = HorizontalAlignment.Center)
	{
		(int nw, int nh) = ToolBox.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
		var rt = ToolBox.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);

		g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
	}

	// 두장 그리기
	private static void DrawBitmapHalfAndHalf(Graphics g, Bitmap bmp, Image left, Image right)
	{
		int f = bmp.Width / 2;
		int w, h;
		Rectangle rt;

		// 왼쪽
		(w, h) = ToolBox.CalcDestSize(Settings.ViewZoom, f, bmp.Height, left.Width, left.Height);
		rt = ToolBox.CalcDestRect(f, bmp.Height, w, h, HorizontalAlignment.Right);

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
			MaxCacheMenuItem.Text = "캐시";
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
		int w = BookCanvas.Width;
		int h = BookCanvas.Height;

		if (w == 0 || h == 0)
		{
			Notifier.ShowBalloonTip(1000, "View Book", "Image drawing error", ToolTipIcon.Error);
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
				break;
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
}


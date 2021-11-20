using Du;
using Du.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuView
{
	public partial class MainForm : Form
	{
		private readonly string _title = "두뷰";

		private BookBase Book { get; set; }

		private Bitmap _bmp = null;
		private readonly string _init_filename;

		private readonly BadakFormWorker _bfw;
		private PageSelectForm _select;

		public MainForm(string filename)
		{
			InitializeComponent();

			// Malgun Gothic / Microsoft Sans Serif
			TitleLabel.Font = new Font("Malgun Gothic", 21.75F, FontStyle.Regular, GraphicsUnit.Point);

			ImagePictureBox.MouseWheel += ImagePictureBox_MouseWheel;

			SystemButton.Form = this;
			_bfw = new BadakFormWorker(this, SystemButton)
			{
				//BodyAsTitle = true,
				MoveTopToMaximize = false,
			};

			_init_filename = filename;

			_select = new PageSelectForm();
		}

		#region 폼 자산
		private void MainForm_Load(object sender, EventArgs e)
		{
			Settings.WhenLoad(this);
			SetupBySetting();

			//
			ActivateFocus();

			//
			if (!string.IsNullOrEmpty(_init_filename))
				OpenBook(_init_filename);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
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

		protected override void WndProc(ref Message m)
		{
			if (ControlDu.ReceiveCopyDataString(ref m, out var s))
			{
				var filename = Converter.DecodingString(s);
				OpenBook(filename);
			}
			else if (_bfw.WndProc(ref m))
			{
				// 할건 없다
			}
			else
			{
				// 윈도우포스
				ControlDu.MagneticDockForm(ref m, this, Settings.MagneticDockSize);

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

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				// 끝냄
				case Keys.Escape:
					Close();
					break;

				case Keys.W:
					if (e.Control)
						CloseBook();
					break;

				// 페이지 조작
				case Keys.Left:
				case Keys.Up:
					if (e.Shift)
						PageMoveDelta(-1);
					else
						PageMovePrev();
					break;

				case Keys.Right:
				case Keys.Down:
				case Keys.Space:
				case Keys.NumPad0:
					if (e.Shift)
						PageMoveDelta(+1);
					else
						PageMoveNext();
					break;

				case Keys.Home:
					PageMovePage(0);
					break;

				case Keys.End:
					PageMovePage(int.MaxValue);
					break;

				case Keys.PageUp:
					PageMoveDelta(-10);
					break;

				case Keys.PageDown:
				case Keys.Back:
					PageMoveDelta(+10);
					break;

				case Keys.Enter:
					if (e.Alt)
						_bfw.Maximize();
					else
						PageSelect();
					break;

				// 화면 전환
				case Keys.F:
					_bfw.Maximize();
					break;

				// 보기 설정
				case Keys.D0:
					UpdateViewZoom(!Settings.ViewZoom);
					break;

				case Keys.D1:
					UpdateViewMode(Types.ViewMode.FitWidth);
					break;

				case Keys.D3:
					UpdateViewMode(Types.ViewMode.LeftToRight);
					ViewLeftRightMenuItem_Click(null, null);
					break;

				case Keys.D4:
					UpdateViewMode(Types.ViewMode.RightToLeft);
					break;

				case Keys.D5:
					UpdateViewQuality(Types.ViewQuality.Default);
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
			}
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else
				e.Effect = DragDropEffects.None;
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(DataFormats.FileDrop) is string[] filenames && filenames.Length > 0)
			{
				// 하나만 쓴다
				OpenBook(filenames[0]);
			}
		}

		private void MainForm_Layout(object sender, LayoutEventArgs e)
		{
			ActivateFocus();
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{

		}

		private void MainForm_ClientSizeChanged(object sender, EventArgs e)
		{
			ViewBook();
		}

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

		#region 그림 영역 UI
		private void ImagePictureBox_MouseDown(object sender, MouseEventArgs e)
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

		private void ImagePictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (_bfw.BodyAsTitle)
				_bfw.DragOnUp(e);
		}

		private void ImagePictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (_bfw.BodyAsTitle)
				_bfw.DragOnMove(e);
		}

		private void ImagePictureBox_MouseWheel(object sender, MouseEventArgs e)
		{
			// 위 + / 아래 -
			if (e.Delta > 0)
				PageMovePrev();
			else if (e.Delta < 0)
				PageMoveNext();
		}

		private void ImagePictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}
		#endregion

		#region 메뉴 및 명령
		private void ActivateFocus()
		{
			ActiveControl = ImagePictureBox;

			/*
			if (!FocusTextBox.Enabled)
				ImagePictureBox.Focus();
			else
			{
				FocusTextBox.Clear();
				FocusTextBox.Focus();
			}
			*/
		}

		private void SetupBySetting()
		{
			UpdateViewZoom(Settings.ViewZoom, false);
			UpdateViewMode(Settings.ViewMode, false);
			UpdateViewQuality(Settings.ViewQuality, false);
		}

		private void UpdateViewZoom(bool zoom, bool viewbook = true)
		{
			Settings.ViewZoom = zoom;

			ViewZoomMenuItem.Checked = Settings.ViewZoom;

			if (viewbook)
				ViewBook();
		}

		private void UpdateViewMode(Types.ViewMode mode, bool viewbook = true)
		{
			Settings.ViewMode = mode;

			ViewFitMenuItem.Checked = mode == Types.ViewMode.FitWidth;
			ViewLeftRightMenuItem.Checked = mode == Types.ViewMode.LeftToRight;
			ViewRightLeftMenuItem.Checked = mode == Types.ViewMode.RightToLeft;

			if (viewbook)
			{
				Book?.PrepareCurrent();
				ViewBook();
			}
		}

		private void UpdateViewQuality(Types.ViewQuality quality, bool viewbook = true)
		{
			Settings.ViewQuality = quality;

			QualityLowPopupItem.Checked = VwqLowMenuItem.Checked = quality == Types.ViewQuality.Low;
			QualityDefaultPopupItem.Checked = VwqDefaultMenuItem.Checked = quality == Types.ViewQuality.Default;
			QualityBilinearPopupItem.Checked = VwqBilinearMenuItem.Checked = quality == Types.ViewQuality.Bilinear;
			QualityBicubicPopupItem.Checked = VwqBicubicMenuItem.Checked = quality == Types.ViewQuality.Bicubic;
			QualityHighPopupItem.Checked = VwqHighMenuItem.Checked = quality == Types.ViewQuality.High;
			QualityHqBilinearPopupItem.Checked = VwqHqBilinearMenuItem.Checked = quality == Types.ViewQuality.HqBilinear;
			QualityHqBicubicPopupItem.Checked = VwqHqBicubicMenuItem.Checked = quality == Types.ViewQuality.HqBicubic;

			if (viewbook)
				ViewBook();
		}

		private void ViewZoomMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewZoom(!Settings.ViewZoom);
		}

		private void ViewFitMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewMode(Types.ViewMode.FitWidth);
		}

		private void ViewLeftRightMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewMode(Types.ViewMode.LeftToRight);
		}

		private void ViewRightLeftMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewMode(Types.ViewMode.RightToLeft);
		}

		private void VwqLowMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.Low);
		}

		private void VwqDefaultMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.Default);
		}

		private void VwqBilinearMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.Bilinear);
		}

		private void VwqBicubicMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.Bicubic);
		}

		private void VwqHighMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.High);
		}

		private void VwqHqBilinearMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.HqBilinear);
		}

		private void VwqHqBicubicMenuItem_Click(object sender, EventArgs e)
		{
			UpdateViewQuality(Types.ViewQuality.HqBicubic);
		}

		private void FavorityAddMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void FileOpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileWithDialog();
		}

		private void FileOpenLastMenuItem_Click(object sender, EventArgs e)
		{
			if (Book != null)
			{
				if (Book.FileName == Settings.LastFileName)
					return;
			}

			OpenBook(Settings.LastFileName);
		}

		private void FileCloseMenuItem_Click(object sender, EventArgs e)
		{
			CloseBook();
		}

		private void FileExitMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FileCopyImageMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (Book != null && Book.PageLeft != null)
				{
					Clipboard.SetImage(Book.PageLeft);

					Notifier.ShowBalloonTip(1000, "Copy image", "Copied image to clipboard!", ToolTipIcon.Info);
				}
			}
			catch
			{
				Notifier.ShowBalloonTip(1000, "Copy image", "Failed to copy image to clipboard", ToolTipIcon.Error);
			}
		}

		private void FileTestMenuItem_Click(object sender, EventArgs e)
		{
			Book.PrepareCurrent();
			ViewBook();
		}

		private void CtrlPrevPopupItem_Click(object sender, EventArgs e)
		{
			PageMovePrev();
		}

		private void CtrlNextPopupItem_Click(object sender, EventArgs e)
		{
			PageMoveNext();
		}

		private void CtrlHomePopupItem_Click(object sender, EventArgs e)
		{
			PageMovePage(0);
		}

		private void CtrlEndPopupItem_Click(object sender, EventArgs e)
		{
			PageMovePage(int.MaxValue);
		}

		private void CtrlPrev10PopupItem_Click(object sender, EventArgs e)
		{
			PageMoveDelta(-10);
		}

		private void CtrlNext10PopupItem_Click(object sender, EventArgs e)
		{
			PageMoveDelta(+10);
		}

		private void CtrlPrevFilePopupItem_Click(object sender, EventArgs e)
		{
			OpenPrevBook();
		}

		private void CtrlNextFilePopupItem_Click(object sender, EventArgs e)
		{
			OpenNextBook();
		}

		private void MaxCacheMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void PagesPopupItem_Click(object sender, EventArgs e)
		{
			PageSelect();
		}
		#endregion

		#region 파일
		private void CloseBook()
		{
			if (Book != null)
			{
				Text = _title;

				Settings.SetRecentlyPage(Book);

				Book.Dispose();
				Book = null;

				RefreshPageInfo();
				ViewBook();

				_select.ResetBook();
			}

			ActivateFocus();
		}

		private void OpenFileWithDialog()
		{
			var dlg = new OpenFileDialog()
			{
				Title = "Open DoView image",
				Filter = "Compress|*.zip|All files|*.*",
				InitialDirectory = Settings.LastFolder,
			};

			if (dlg.ShowDialog() == DialogResult.OK)
				OpenBook(dlg.FileName);
		}

		private void OpenBook(string filename, int page = -1)
		{
			if (File.Exists(filename))
			{
				// 이건 단일 파일이나 아카이브
				OpenArchive(filename, page);
			}
			else if (Directory.Exists(filename))
			{
				// 이건 디렉토리
			}

			if (Book != null)
			{
				Text = Book.OnlyFileName;

				Settings.LastFileName = filename;

				RefreshPageInfo();
				ViewBook();

				_select.SetBook(Book);
			}

			ActivateFocus();
		}

		private void OpenArchive(string filename, int page = -1)
		{
			FileInfo fi = new FileInfo(filename);
			Settings.LastFolder = fi.DirectoryName;

			BookBase bk;

			switch (fi.Extension.ToLower())
			{
				case ".zip":
					bk = BookZip.FromFile(filename);
					break;

				default:
					bk = null;
					break;
			}

			if (bk == null)
			{
				// 음... 뭘까
				Notifier.ShowBalloonTip(1000, "책 열기", "어떤 책인지 알 수 없어요!", ToolTipIcon.Error);
			}
			else
			{
				if (page < 0)
					page = Settings.GetRecentlyPage(bk.OnlyFileName);

				if (Book != null)
				{
					Settings.SetRecentlyPage(Book);
					Book.Dispose();
				}

				Book = bk;
				Book.CurrentPage = page;
				Book.PrepareCurrent();
			}
		}

		//
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

		//
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
		#endregion

		#region 그리기
		//
		private void DrawLogo(Graphics g, int w, int h)
		{
			var img = Properties.Resources.housebari_head_128;

			if (w > img.Width && h > img.Height)
				g.DrawImage(img, w - img.Width - 50, h - img.Height - 50);
			else
			{
				Rectangle rt = new Rectangle(0, 0, w, h);
				g.DrawImage(img, rt);
			}
		}

		//
		private void DrawBitmapFit(Graphics g, Bitmap bmp, Image img, HorizontalAlignment align = HorizontalAlignment.Center)
		{
			(int nw, int nh) = ToolBox.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
			var rt = ToolBox.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);

			g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
		}

		//
		private void DrawBitmapHalfAndHalf(Graphics g, Bitmap bmp, Image left, Image right)
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
		#endregion

		#region 책 처리
		private void RefreshPageInfo()
		{
			if (Book == null)
			{
				// 페이지 숨김
				PageInfoLabel.Visible = false;
				MaxCacheMenuItem.Text = "캐시";
			}
			else
			{
				PageInfoLabel.Text = $"{Book.CurrentPage + 1}/{Book.TotalPage}";
				PageInfoLabel.Visible = true;

				var cachesize = ToolBox.SizeToString(Book.CacheSize);
				MaxCacheMenuItem.Text = $"[{cachesize}]";
			}
		}

		private void PageMovePrev()
		{
			if (Book != null && Book.MovePrev())
			{
				Book.PrepareCurrent();

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMoveNext()
		{
			if (Book != null && Book.MoveNext())
			{
				Book.PrepareCurrent();

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMovePage(int page)
		{
			if (Book != null && Book.MovePage(page))
			{
				Book.PrepareCurrent();

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMoveDelta(int delta)
		{
			if (Book != null && Book.MovePage(Book.CurrentPage + delta))
			{
				Book.PrepareCurrent();

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageSelect()
		{
			if (Book == null)
				return;

			_select.SelectedPage = Book.CurrentPage;
			if (_select.ShowDialog(this) == DialogResult.OK)
			{
				Book.CurrentPage = _select.SelectedPage;
				Book.PrepareCurrent();

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void ViewBook()
		{
			if (WindowState == FormWindowState.Minimized)
				return;

			int w = ImagePictureBox.Width;
			int h = ImagePictureBox.Height;

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
							DrawBitmapFit(g, _bmp, Book.PageLeft);
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
								DrawBitmapFit(g, _bmp, Book.PageRight);
							}
						}
						else if (Book.PageRight == null)
						{
							// 왼쪽 한장만 그리는거다
							DrawBitmapFit(g, _bmp, Book.PageLeft);
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

			ImagePictureBox.Image = _bmp;
		}
		#endregion
	}
}

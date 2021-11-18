using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuView
{
	public partial class MainForm : DuLib.WinForms.BadakForm
	{
		private BookBase Book { get; set; }

		private Bitmap _bmp = null;

		public MainForm()
		{
			InitializeComponent();

			ImagePictureBox.MouseWheel += ImagePictureBox_MouseWheel;
		}

		#region 폼 자산
		private void MainForm_Load(object sender, EventArgs e)
		{
			//SizeMoveHitTest.BodyAsTitle = true;

			Settings.WhenLoad(this);
			Location = Settings.Window.Location;
			Size = Settings.Window.Size;

			SetupBySetting();

			//
			ActivateFocus();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (Book != null)
			{
				Settings.LastFileName = Book.FileName;
				Settings.LastFilePage = Book.CurrentPage;
				// 책깔피?
				Book.Dispose();
			}

			Settings.WhenClose(this);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				// 끝냄
				case Keys.Escape:
					Close();
					break;

				// 페이지 조작
				case Keys.Left:
				case Keys.Up:
					if (e.Control)
						PageMoveDelta(-1);
					else
						PageMovePrev();
					break;

				case Keys.Right:
				case Keys.Down:
				case Keys.Space:
				case Keys.NumPad0:
					if (e.Control)
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
					PageMoveDelta(+10);
					break;

				// 화면 전환
				case Keys.F:
					BadakMaximize();
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
				OpenArchive(filenames[0]);
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
		#endregion

		#region 메뉴 및 명령
		private void ActivateFocus()
		{
			if (!FocusTextBox.Enabled)
				ImagePictureBox.Focus();
			else
			{
				FocusTextBox.Clear();
				FocusTextBox.Focus();
			}
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
				Book?.PrepareCurrent(Settings.ViewMode);
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

			if (File.Exists(Settings.LastFileName))
				OpenArchive(Settings.LastFileName, Settings.LastFilePage);
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
			Book.PrepareCurrent(Settings.ViewMode);
			ViewBook();
		}
		#endregion

		#region 그림 영역 UI
		private void ImagePictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (SizeMoveHitTest.BodyAsTitle)
				BadakDragOnMouseDown(e);
		}

		private void ImagePictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (SizeMoveHitTest.BodyAsTitle)
				BadakDragOnMouseUp(e);
		}

		private void ImagePictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (SizeMoveHitTest.BodyAsTitle)
				BadakDragOnMouseMove(e);
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

		#region 파일
		private void CloseBook()
		{
			if (Book == null)
				return;

			Settings.LastFileName = Book.FileName;
			Settings.LastFilePage = Book.CurrentPage;

			Book.Close();
			Book = null;

			RefreshPageInfo();
			ViewBook();

			Text = "DuView";

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
				OpenArchive(dlg.FileName);
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
				Notifier.ShowBalloonTip(1000, "Open Book", "Unknown file type", ToolTipIcon.Error);
			}
			else
			{
				if (page < 0)
					page = Settings.GetRecentlyPage(bk.OnlyFileName);

				if (Book != null)
					Book.Close();

				Book = bk;
				Book.CurrentPage = page;
				Book.PrepareCurrent(Settings.ViewMode);

				Settings.LastFileName = filename;
				Settings.LastFilePage = page;

				RefreshPageInfo();
				ViewBook();

				Text = Book.OnlyFileName;
			}

			ActivateFocus();
		}

		//
		private void OpenPrevBook()
		{
			if (Book == null)
				return;

			var filename = Book.FindNextFile(true);
			if (filename==null)
			{
				Notifier.ShowBalloonTip(1000, "Open Book", "No previous book found", ToolTipIcon.Info);
				return;
			}

			OpenArchive(filename);
		}

		//
		private void OpenNextBook()
		{
			if (Book == null)
				return;

			var filename = Book.FindNextFile(false);
			if (filename == null)
			{
				Notifier.ShowBalloonTip(1000, "Open Book", "No next book found", ToolTipIcon.Info);
				return;
			}

			OpenArchive(filename);
		}
		#endregion

		#region 그리기
		//
		private void DrawLogo(Graphics g, Bitmap bmp, int w, int h)
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
				PageInfoLabel.Visible = false;
			else
			{
				var cachesize = ToolBox.SizeToString(Book.CacheSize);
				PageInfoLabel.Text = $"{Book.CurrentPage + 1}/{Book.TotalPage} [{cachesize}]";
				PageInfoLabel.Visible = true;
			}
		}

		private void PageMovePrev()
		{
			if (Book != null && Book.MovePrev(Settings.ViewMode))
			{
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMoveNext()
		{
			if (Book != null && Book.MoveNext(Settings.ViewMode))
			{
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMovePage(int page)
		{
			if (Book != null && Book.MovePage(Settings.ViewMode, page))
			{
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMoveDelta(int delta)
		{
			if (Book != null && Book.MovePage(Settings.ViewMode, Book.CurrentPage + delta))
			{
				Book.PrepareCurrent(Settings.ViewMode);

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
					DrawLogo(g, _bmp, w, h);
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
								DrawLogo(g, _bmp, w, h);
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

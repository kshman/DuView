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
				case Keys.Escape:
					Close();
					break;

				case Keys.Left:
					PageMovePrev();
					break;

				case Keys.NumPad0:
				case Keys.Right:
				case Keys.Space:
					PageMoveNext();
					break;

				case Keys.Home:
					PageMovePage(0);
					break;

				case Keys.End:
					PageMovePage(int.MaxValue);
					break;

				case Keys.F:
					BadakMaximize();
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
			var filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (filenames != null && filenames.Length > 0)
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
			ViewZoomMenuItem.Checked = Settings.ViewZoom;
			ViewModeCheck();
		}

		private void ViewModeCheck()
		{
			ViewFitMenuItem.Checked = Settings.ViewMode == Types.ViewMode.FitWidth;
			ViewLeftRightMenuItem.Checked = Settings.ViewMode == Types.ViewMode.LeftToRight;
			ViewRightLeftMenuItem.Checked = Settings.ViewMode == Types.ViewMode.RightToLeft;

			if (Book != null)
			{
				Book.PrepareCurrent(Settings.ViewMode);
				ViewBook();
			}
		}

		private void ViewZoomMenuItem_Click(object sender, EventArgs e)
		{
			Settings.ViewZoom = !Settings.ViewZoom;
			ViewZoomMenuItem.Checked = Settings.ViewZoom;
			ViewBook();
		}

		private void ViewFitMenuItem_Click(object sender, EventArgs e)
		{
			Settings.ViewMode = Types.ViewMode.FitWidth;
			ViewModeCheck();
		}

		private void ViewLeftRightMenuItem_Click(object sender, EventArgs e)
		{
			Settings.ViewMode = Types.ViewMode.LeftToRight;
			ViewModeCheck();
		}

		private void ViewRightLeftMenuItem_Click(object sender, EventArgs e)
		{
			Settings.ViewMode = Types.ViewMode.RightToLeft;
			ViewModeCheck();
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
		#endregion

		#region 그리기
		//
		private void DrawLogo(Bitmap bmp, int w, int h)
		{
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.FromArgb(10, 10, 10));

				var img = Properties.Resources.housebari_head_128;
				if (w > img.Width && h > img.Height)
					g.DrawImage(img, w - img.Width - 50, h - img.Height - 50);
				else
				{
					Rectangle rt = new Rectangle(0, 0, w, h);
					g.DrawImage(img, rt);
				}
			}
		}

		//
		private void DrawBitmapFit(Bitmap bmp, Image img, HorizontalAlignment align = HorizontalAlignment.Center)
		{
			(int nw, int nh) = ToolBox.CalcDestSize(Settings.ViewZoom, bmp.Width, bmp.Height, img.Width, img.Height);
			var rt = ToolBox.CalcDestRect(bmp.Width, bmp.Height, nw, nh, align);

			using (var g = Graphics.FromImage(bmp))
			{
				g.DrawImage(img, rt, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
			}
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

			if (Book == null)
			{
				Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				DrawLogo(bmp, w, h);
				ImagePictureBox.Image = bmp;
			}
			else
			{
				Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				if (Settings.ViewMode == Types.ViewMode.FitWidth)
				{
					if (Book.PageLeft != null)
						DrawBitmapFit(bmp, Book.PageLeft);
				}
				else
				{
					DrawLogo(bmp, w, h);
				}

				ImagePictureBox.Image = bmp;
			}
		}

		private void PageMovePrev()
		{
			if (Book != null)
			{
				Book.MovePrev(Settings.ViewMode);
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMoveNext()
		{
			if (Book != null)
			{
				Book.MoveNext(Settings.ViewMode);
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}

		private void PageMovePage(int page)
		{
			if (Book != null)
			{
				Book.MovePage(Settings.ViewMode, page);
				Book.PrepareCurrent(Settings.ViewMode);

				RefreshPageInfo();
				ViewBook();
			}
		}
		#endregion
	}
}

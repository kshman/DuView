using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuView
{
	public partial class MainForm : DuLib.WinForms.BadakForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		#region 폼 자산
		private void MainForm_Load(object sender, EventArgs e)
		{
			SizeMoveHitTest.BodyAsTitle = true;

			Settings.WhenLoad(this);
			Location = Settings.Window.Location;
			Size = Settings.Window.Size;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.WhenClose(this);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					Close();
					break;
			}
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			var filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (filenames != null && filenames.Length > 0)
			{
				// 하나만 쓴다
				System.Diagnostics.Debug.WriteLine($"드래그: {filenames[0]}");
			}
		}
		#endregion

		#region 메뉴 및 명령
		private void ViewZoomMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ViewOriginalMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ViewFitMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ViewLeftRightMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ViewRightLeftMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void FavorityAddMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void FileOpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileWithDialog();
		}

		private void FileCloseMenuItem_Click(object sender, EventArgs e)
		{

		}
		#endregion

		#region 그림 영역 UI
		private void ImagePictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			BadakDragOnMouseDown(e);
		}

		private void ImagePictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			BadakDragOnMouseUp(e);
		}

		private void ImagePictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			BadakDragOnMouseMove(e);
		}
		#endregion

		#region 파일
		private void OpenFileWithDialog()
		{
			var dlg = new OpenFileDialog()
			{
				Title = "Open DoView image",
				Filter = "Compress|*.zip|All files|*.*",
				InitialDirectory = Settings.LastFolder,
			};

			if (dlg.ShowDialog() != DialogResult.OK)
				return;
		}
		#endregion
	}
}

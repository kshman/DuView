namespace DuView
{
	partial class MainForm
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.MenuStrip = new DuLib.WinForms.DarkMenuStrip();
			this.ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewOriginalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewFitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewLeftRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewRightLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FavorityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FavorityAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImagePictureBox = new System.Windows.Forms.PictureBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BadakTopPanel.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// BadakTopPanel
			// 
			this.BadakTopPanel.Controls.Add(this.MenuStrip);
			this.BadakTopPanel.Size = new System.Drawing.Size(800, 70);
			this.BadakTopPanel.Controls.SetChildIndex(this.MenuStrip, 0);
			this.BadakTopPanel.Controls.SetChildIndex(this.BadakMinMaxClosePanel, 0);
			// 
			// BadakMinMaxClosePanel
			// 
			this.BadakMinMaxClosePanel.Location = new System.Drawing.Point(677, 2);
			// 
			// MenuStrip
			// 
			this.MenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewMenuItem,
            this.FavorityMenuItem,
            this.FileMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(526, 2);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(151, 24);
			this.MenuStrip.TabIndex = 2;
			this.MenuStrip.Text = "darkMenuStrip1";
			// 
			// ViewMenuItem
			// 
			this.ViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoomMenuItem,
            this.toolStripSeparator1,
            this.ViewOriginalMenuItem,
            this.ViewFitMenuItem,
            this.ViewLeftRightMenuItem,
            this.ViewRightLeftMenuItem});
			this.ViewMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewMenuItem.Name = "ViewMenuItem";
			this.ViewMenuItem.Size = new System.Drawing.Size(45, 20);
			this.ViewMenuItem.Text = "&View";
			// 
			// ViewZoomMenuItem
			// 
			this.ViewZoomMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewZoomMenuItem.Name = "ViewZoomMenuItem";
			this.ViewZoomMenuItem.Size = new System.Drawing.Size(161, 22);
			this.ViewZoomMenuItem.Text = "&Zoom";
			this.ViewZoomMenuItem.Click += new System.EventHandler(this.ViewZoomMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// ViewOriginalMenuItem
			// 
			this.ViewOriginalMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewOriginalMenuItem.Name = "ViewOriginalMenuItem";
			this.ViewOriginalMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.ViewOriginalMenuItem.Size = new System.Drawing.Size(161, 22);
			this.ViewOriginalMenuItem.Text = "&Original";
			this.ViewOriginalMenuItem.Click += new System.EventHandler(this.ViewOriginalMenuItem_Click);
			// 
			// ViewFitMenuItem
			// 
			this.ViewFitMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewFitMenuItem.Name = "ViewFitMenuItem";
			this.ViewFitMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.ViewFitMenuItem.Size = new System.Drawing.Size(161, 22);
			this.ViewFitMenuItem.Text = "&Fit";
			this.ViewFitMenuItem.Click += new System.EventHandler(this.ViewFitMenuItem_Click);
			// 
			// ViewLeftRightMenuItem
			// 
			this.ViewLeftRightMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewLeftRightMenuItem.Name = "ViewLeftRightMenuItem";
			this.ViewLeftRightMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.ViewLeftRightMenuItem.Size = new System.Drawing.Size(161, 22);
			this.ViewLeftRightMenuItem.Text = "Left → Right";
			this.ViewLeftRightMenuItem.Click += new System.EventHandler(this.ViewLeftRightMenuItem_Click);
			// 
			// ViewRightLeftMenuItem
			// 
			this.ViewRightLeftMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewRightLeftMenuItem.Name = "ViewRightLeftMenuItem";
			this.ViewRightLeftMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.ViewRightLeftMenuItem.Size = new System.Drawing.Size(161, 22);
			this.ViewRightLeftMenuItem.Text = "Right → Left";
			this.ViewRightLeftMenuItem.Click += new System.EventHandler(this.ViewRightLeftMenuItem_Click);
			// 
			// FavorityMenuItem
			// 
			this.FavorityMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FavorityAddMenuItem});
			this.FavorityMenuItem.ForeColor = System.Drawing.Color.White;
			this.FavorityMenuItem.Name = "FavorityMenuItem";
			this.FavorityMenuItem.Size = new System.Drawing.Size(61, 20);
			this.FavorityMenuItem.Text = "&Favority";
			// 
			// FavorityAddMenuItem
			// 
			this.FavorityAddMenuItem.ForeColor = System.Drawing.Color.White;
			this.FavorityAddMenuItem.Name = "FavorityAddMenuItem";
			this.FavorityAddMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.FavorityAddMenuItem.Size = new System.Drawing.Size(116, 22);
			this.FavorityAddMenuItem.Text = "&Add";
			this.FavorityAddMenuItem.Click += new System.EventHandler(this.FavorityAddMenuItem_Click);
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOpenMenuItem,
            this.FileCloseMenuItem,
            this.toolStripSeparator2,
            this.FileExitMenuItem});
			this.FileMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileMenuItem.Text = "&File";
			// 
			// FileOpenMenuItem
			// 
			this.FileOpenMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenMenuItem.Name = "FileOpenMenuItem";
			this.FileOpenMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.FileOpenMenuItem.Size = new System.Drawing.Size(180, 22);
			this.FileOpenMenuItem.Text = "&Open";
			this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// FileCloseMenuItem
			// 
			this.FileCloseMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCloseMenuItem.Name = "FileCloseMenuItem";
			this.FileCloseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.FileCloseMenuItem.Size = new System.Drawing.Size(180, 22);
			this.FileCloseMenuItem.Text = "&Close";
			this.FileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.Location = new System.Drawing.Point(6, 70);
			this.ImagePictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.ImagePictureBox.Name = "ImagePictureBox";
			this.ImagePictureBox.Size = new System.Drawing.Size(788, 374);
			this.ImagePictureBox.TabIndex = 2;
			this.ImagePictureBox.TabStop = false;
			this.ImagePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseDown);
			this.ImagePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseMove);
			this.ImagePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseUp);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// FileExitMenuItem
			// 
			this.FileExitMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileExitMenuItem.Name = "FileExitMenuItem";
			this.FileExitMenuItem.Size = new System.Drawing.Size(180, 22);
			this.FileExitMenuItem.Text = "E&xit";
			this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ImagePictureBox);
			this.MainMenuStrip = this.MenuStrip;
			this.Name = "MainForm";
			this.Text = "DuView";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Controls.SetChildIndex(this.BadakTopPanel, 0);
			this.Controls.SetChildIndex(this.ImagePictureBox, 0);
			this.BadakTopPanel.ResumeLayout(false);
			this.BadakTopPanel.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DuLib.WinForms.DarkMenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ViewMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewZoomMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ViewOriginalMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewFitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewLeftRightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewRightLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FavorityMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FavorityAddMenuItem;
		private System.Windows.Forms.PictureBox ImagePictureBox;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
	}
}


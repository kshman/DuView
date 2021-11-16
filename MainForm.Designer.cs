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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MenuStrip = new DuLib.WinForms.DarkMenuStrip();
			this.ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewFitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewLeftRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewRightLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FavorityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FavorityAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenLastMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.FileCopyImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.FileTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImagePictureBox = new System.Windows.Forms.PictureBox();
			this.ContextPopup = new DuLib.WinForms.DarkContextMenuStrip(this.components);
			this.OpenPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ClosePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.PagesPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.reservedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.reservedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyImagePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PageInfoLabel = new System.Windows.Forms.Label();
			this.Notifier = new System.Windows.Forms.NotifyIcon(this.components);
			this.FocusTextBox = new System.Windows.Forms.TextBox();
			this.BadakTopPanel.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
			this.ContextPopup.SuspendLayout();
			this.SuspendLayout();
			// 
			// BadakTopPanel
			// 
			this.BadakTopPanel.Controls.Add(this.PageInfoLabel);
			this.BadakTopPanel.Controls.Add(this.MenuStrip);
			this.BadakTopPanel.Size = new System.Drawing.Size(800, 70);
			this.BadakTopPanel.Controls.SetChildIndex(this.MenuStrip, 0);
			this.BadakTopPanel.Controls.SetChildIndex(this.BadakMinMaxClosePanel, 0);
			this.BadakTopPanel.Controls.SetChildIndex(this.PageInfoLabel, 0);
			// 
			// BadakMinMaxClosePanel
			// 
			this.BadakMinMaxClosePanel.Location = new System.Drawing.Point(675, 0);
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
			this.ViewZoomMenuItem.Size = new System.Drawing.Size(168, 22);
			this.ViewZoomMenuItem.Text = "&Zoom";
			this.ViewZoomMenuItem.Click += new System.EventHandler(this.ViewZoomMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
			// 
			// ViewFitMenuItem
			// 
			this.ViewFitMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewFitMenuItem.Name = "ViewFitMenuItem";
			this.ViewFitMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.ViewFitMenuItem.Size = new System.Drawing.Size(168, 22);
			this.ViewFitMenuItem.Text = "&Fit";
			this.ViewFitMenuItem.Click += new System.EventHandler(this.ViewFitMenuItem_Click);
			// 
			// ViewLeftRightMenuItem
			// 
			this.ViewLeftRightMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewLeftRightMenuItem.Name = "ViewLeftRightMenuItem";
			this.ViewLeftRightMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.ViewLeftRightMenuItem.Size = new System.Drawing.Size(168, 22);
			this.ViewLeftRightMenuItem.Text = "Left → Right";
			this.ViewLeftRightMenuItem.Click += new System.EventHandler(this.ViewLeftRightMenuItem_Click);
			// 
			// ViewRightLeftMenuItem
			// 
			this.ViewRightLeftMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewRightLeftMenuItem.Name = "ViewRightLeftMenuItem";
			this.ViewRightLeftMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.ViewRightLeftMenuItem.Size = new System.Drawing.Size(168, 22);
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
            this.FileOpenLastMenuItem,
            this.FileCloseMenuItem,
            this.toolStripSeparator2,
            this.FileCopyImageMenuItem,
            this.toolStripSeparator3,
            this.FileTestMenuItem,
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
			this.FileOpenMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileOpenMenuItem.Text = "&Open";
			this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// FileOpenLastMenuItem
			// 
			this.FileOpenLastMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenLastMenuItem.Name = "FileOpenLastMenuItem";
			this.FileOpenLastMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.FileOpenLastMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileOpenLastMenuItem.Text = "Open &last book";
			this.FileOpenLastMenuItem.Click += new System.EventHandler(this.FileOpenLastMenuItem_Click);
			// 
			// FileCloseMenuItem
			// 
			this.FileCloseMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCloseMenuItem.Name = "FileCloseMenuItem";
			this.FileCloseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.FileCloseMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileCloseMenuItem.Text = "Clos&e";
			this.FileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
			// 
			// FileCopyImageMenuItem
			// 
			this.FileCopyImageMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCopyImageMenuItem.Name = "FileCopyImageMenuItem";
			this.FileCopyImageMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.FileCopyImageMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileCopyImageMenuItem.Text = "&Copy image";
			this.FileCopyImageMenuItem.Click += new System.EventHandler(this.FileCopyImageMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(194, 6);
			// 
			// FileTestMenuItem
			// 
			this.FileTestMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileTestMenuItem.Name = "FileTestMenuItem";
			this.FileTestMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.FileTestMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileTestMenuItem.Text = "Test";
			this.FileTestMenuItem.Click += new System.EventHandler(this.FileTestMenuItem_Click);
			// 
			// FileExitMenuItem
			// 
			this.FileExitMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileExitMenuItem.Name = "FileExitMenuItem";
			this.FileExitMenuItem.Size = new System.Drawing.Size(197, 22);
			this.FileExitMenuItem.Text = "E&xit";
			this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ImagePictureBox.ContextMenuStrip = this.ContextPopup;
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
			// ContextPopup
			// 
			this.ContextPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenPopupItem,
            this.ClosePopupItem,
            this.toolStripSeparator4,
            this.PagesPopupItem,
            this.toolStripSeparator5,
            this.reservedToolStripMenuItem,
            this.toolStripSeparator6,
            this.reservedToolStripMenuItem1,
            this.toolStripSeparator7,
            this.CopyImagePopupItem,
            this.toolStripSeparator8,
            this.ExitPopupItem});
			this.ContextPopup.Name = "ContextMenuStrip";
			this.ContextPopup.Size = new System.Drawing.Size(182, 188);
			// 
			// OpenPopupItem
			// 
			this.OpenPopupItem.ForeColor = System.Drawing.Color.White;
			this.OpenPopupItem.Name = "OpenPopupItem";
			this.OpenPopupItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.OpenPopupItem.Size = new System.Drawing.Size(181, 22);
			this.OpenPopupItem.Text = "&Open";
			this.OpenPopupItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// ClosePopupItem
			// 
			this.ClosePopupItem.ForeColor = System.Drawing.Color.White;
			this.ClosePopupItem.Name = "ClosePopupItem";
			this.ClosePopupItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.ClosePopupItem.Size = new System.Drawing.Size(181, 22);
			this.ClosePopupItem.Text = "Clos&e";
			this.ClosePopupItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
			// 
			// PagesPopupItem
			// 
			this.PagesPopupItem.ForeColor = System.Drawing.Color.White;
			this.PagesPopupItem.Name = "PagesPopupItem";
			this.PagesPopupItem.Size = new System.Drawing.Size(181, 22);
			this.PagesPopupItem.Text = "Pages";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
			// 
			// reservedToolStripMenuItem
			// 
			this.reservedToolStripMenuItem.ForeColor = System.Drawing.Color.White;
			this.reservedToolStripMenuItem.Name = "reservedToolStripMenuItem";
			this.reservedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.reservedToolStripMenuItem.Text = "Reserved";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(178, 6);
			// 
			// reservedToolStripMenuItem1
			// 
			this.reservedToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
			this.reservedToolStripMenuItem1.Name = "reservedToolStripMenuItem1";
			this.reservedToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
			this.reservedToolStripMenuItem1.Text = "Reserved";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(178, 6);
			// 
			// CopyImagePopupItem
			// 
			this.CopyImagePopupItem.ForeColor = System.Drawing.Color.White;
			this.CopyImagePopupItem.Name = "CopyImagePopupItem";
			this.CopyImagePopupItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.CopyImagePopupItem.Size = new System.Drawing.Size(181, 22);
			this.CopyImagePopupItem.Text = "&Copy image";
			this.CopyImagePopupItem.Click += new System.EventHandler(this.FileCopyImageMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(178, 6);
			// 
			// ExitPopupItem
			// 
			this.ExitPopupItem.ForeColor = System.Drawing.Color.White;
			this.ExitPopupItem.Name = "ExitPopupItem";
			this.ExitPopupItem.Size = new System.Drawing.Size(181, 22);
			this.ExitPopupItem.Text = "E&xit";
			this.ExitPopupItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// PageInfoLabel
			// 
			this.PageInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PageInfoLabel.AutoSize = true;
			this.PageInfoLabel.ForeColor = System.Drawing.Color.White;
			this.PageInfoLabel.Location = new System.Drawing.Point(763, 54);
			this.PageInfoLabel.Name = "PageInfoLabel";
			this.PageInfoLabel.Size = new System.Drawing.Size(33, 12);
			this.PageInfoLabel.TabIndex = 6;
			this.PageInfoLabel.Text = "page";
			this.PageInfoLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.PageInfoLabel.Visible = false;
			// 
			// Notifier
			// 
			this.Notifier.ContextMenuStrip = this.ContextPopup;
			this.Notifier.Icon = ((System.Drawing.Icon)(resources.GetObject("Notifier.Icon")));
			this.Notifier.Text = "DuView";
			this.Notifier.Visible = true;
			// 
			// FocusTextBox
			// 
			this.FocusTextBox.Location = new System.Drawing.Point(12, 76);
			this.FocusTextBox.Name = "FocusTextBox";
			this.FocusTextBox.Size = new System.Drawing.Size(92, 21);
			this.FocusTextBox.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ImagePictureBox);
			this.Controls.Add(this.FocusTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.MenuStrip;
			this.MinimumSize = new System.Drawing.Size(300, 250);
			this.Name = "MainForm";
			this.Text = "DuView";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainForm_Layout);
			this.Controls.SetChildIndex(this.FocusTextBox, 0);
			this.Controls.SetChildIndex(this.BadakTopPanel, 0);
			this.Controls.SetChildIndex(this.ImagePictureBox, 0);
			this.BadakTopPanel.ResumeLayout(false);
			this.BadakTopPanel.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
			this.ContextPopup.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DuLib.WinForms.DarkMenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ViewMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewZoomMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
		private System.Windows.Forms.ToolStripMenuItem FileOpenLastMenuItem;
		private System.Windows.Forms.Label PageInfoLabel;
		private System.Windows.Forms.ToolStripMenuItem FileCopyImageMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.NotifyIcon Notifier;
		private DuLib.WinForms.DarkContextMenuStrip ContextPopup;
		private System.Windows.Forms.ToolStripMenuItem OpenPopupItem;
		private System.Windows.Forms.ToolStripMenuItem ClosePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem PagesPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem reservedToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem reservedToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem CopyImagePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem ExitPopupItem;
		private System.Windows.Forms.ToolStripMenuItem FileTestMenuItem;
		private System.Windows.Forms.TextBox FocusTextBox;
	}
}


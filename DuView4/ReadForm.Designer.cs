namespace DuView
{
	partial class ReadForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadForm));
			this.TopPanel = new System.Windows.Forms.Panel();
			this.PageInfo = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.MenuStrip = new Du.WinForms.BadakMenuStrip();
			this.ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewQualityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqLowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqDefaultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqBilinearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqBicubicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqHighMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.VwqHqBilinearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.VwqHqBicubicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewFitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewLeftRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewRightLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PageSelectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.PageAddFavMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenLastMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpenExternalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.FileRenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileMoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileCopyImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.FileDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.FileRefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MaxCacheMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Notifier = new System.Windows.Forms.NotifyIcon(this.components);
			this.BookCanvas = new System.Windows.Forms.PictureBox();
			this.ContextPopup = new Du.WinForms.BadakContextMenuStrip(this.components);
			this.OpenPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RenamePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ClosePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ControlPopItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlPrevPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlNextPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlHomePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlEndPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.CtrlPrev10PopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlNext10PopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.CtrlPrevFilePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlNextFilePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PagesPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.QualityPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityLowPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityDefaultPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityBilinearPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityBicubicPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityHighPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.QualityHqBilinearPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.QualityHqBicubicPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.DeletePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyImagePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.OptionPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NotifyLabel = new System.Windows.Forms.Label();
			this.TopPanel.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BookCanvas)).BeginInit();
			this.ContextPopup.SuspendLayout();
			this.SuspendLayout();
			// 
			// TopPanel
			// 
			this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.TopPanel.Controls.Add(this.PageInfo);
			this.TopPanel.Controls.Add(this.TitleLabel);
			this.TopPanel.Controls.Add(this.SystemButton);
			this.TopPanel.Controls.Add(this.MenuStrip);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(686, 61);
			this.TopPanel.TabIndex = 0;
			this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// PageInfo
			// 
			this.PageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PageInfo.ForeColor = System.Drawing.Color.White;
			this.PageInfo.Location = new System.Drawing.Point(555, 43);
			this.PageInfo.Name = "PageInfo";
			this.PageInfo.Size = new System.Drawing.Size(128, 13);
			this.PageInfo.TabIndex = 4;
			this.PageInfo.Text = Du.Globalization.Locale.Text(0);
			this.PageInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.PageInfo.Visible = false;
			this.PageInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.PageInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.PageInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// TitleLabel
			// 
			this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(9, 29);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(537, 31);
			this.TitleLabel.TabIndex = 3;
			this.TitleLabel.Text = Du.Globalization.Locale.Text(0);
			this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(555, 0);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(129, 26);
			this.SystemButton.MinimumSize = new System.Drawing.Size(129, 26);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = true;
			this.SystemButton.ShowMinimize = true;
			this.SystemButton.Size = new System.Drawing.Size(129, 26);
			this.SystemButton.TabIndex = 2;
			this.SystemButton.TabStop = false;
			// 
			// MenuStrip
			// 
			this.MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ViewMenuItem,
			this.PageMenuItem,
			this.FileMenuItem,
			this.MaxCacheMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(3, 3);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.MenuStrip.Size = new System.Drawing.Size(363, 24);
			this.MenuStrip.TabIndex = 1;
			this.MenuStrip.Text = Du.Globalization.Locale.Text(95);
			// 
			// ViewMenuItem
			// 
			this.ViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ViewZoomMenuItem,
			this.ViewQualityMenuItem,
			this.toolStripSeparator1,
			this.ViewFitMenuItem,
			this.ViewLeftRightMenuItem,
			this.ViewRightLeftMenuItem});
			this.ViewMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewMenuItem.Image = global::DuView.Properties.Resources.viewmode_pitwidth;
			this.ViewMenuItem.Name = "ViewMenuItem";
			this.ViewMenuItem.Size = new System.Drawing.Size(71, 20);
			this.ViewMenuItem.Text = Du.Globalization.Locale.Text(1100);
			// 
			// ViewZoomMenuItem
			// 
			this.ViewZoomMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewZoomMenuItem.Name = "ViewZoomMenuItem";
			this.ViewZoomMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
			this.ViewZoomMenuItem.Size = new System.Drawing.Size(151, 22);
			this.ViewZoomMenuItem.Text = Du.Globalization.Locale.Text(1101);
			this.ViewZoomMenuItem.Click += new System.EventHandler(this.ViewZoomMenuItem_Click);
			// 
			// ViewQualityMenuItem
			// 
			this.ViewQualityMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.VwqLowMenuItem,
			this.VwqDefaultMenuItem,
			this.VwqBilinearMenuItem,
			this.VwqBicubicMenuItem,
			this.VwqHighMenuItem,
			this.toolStripSeparator9,
			this.VwqHqBilinearMenuItem,
			this.VwqHqBicubicMenuItem});
			this.ViewQualityMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewQualityMenuItem.Name = "ViewQualityMenuItem";
			this.ViewQualityMenuItem.Size = new System.Drawing.Size(151, 22);
			this.ViewQualityMenuItem.Text = Du.Globalization.Locale.Text(1102);
			// 
			// VwqLowMenuItem
			// 
			this.VwqLowMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqLowMenuItem.Name = "VwqLowMenuItem";
			this.VwqLowMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqLowMenuItem.Tag = DuView.Types.ViewQuality.Low;
			this.VwqLowMenuItem.Text = Du.Globalization.Locale.Text(2101);
			this.VwqLowMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// VwqDefaultMenuItem
			// 
			this.VwqDefaultMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqDefaultMenuItem.Name = "VwqDefaultMenuItem";
			this.VwqDefaultMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.VwqDefaultMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqDefaultMenuItem.Tag = DuView.Types.ViewQuality.Default;
			this.VwqDefaultMenuItem.Text = Du.Globalization.Locale.Text(2102);
			this.VwqDefaultMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// VwqBilinearMenuItem
			// 
			this.VwqBilinearMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqBilinearMenuItem.Name = "VwqBilinearMenuItem";
			this.VwqBilinearMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqBilinearMenuItem.Tag = DuView.Types.ViewQuality.Bilinear;
			this.VwqBilinearMenuItem.Text = Du.Globalization.Locale.Text(2103);
			this.VwqBilinearMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// VwqBicubicMenuItem
			// 
			this.VwqBicubicMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqBicubicMenuItem.Name = "VwqBicubicMenuItem";
			this.VwqBicubicMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqBicubicMenuItem.Tag = DuView.Types.ViewQuality.Bicubic;
			this.VwqBicubicMenuItem.Text = Du.Globalization.Locale.Text(2104);
			this.VwqBicubicMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// VwqHighMenuItem
			// 
			this.VwqHighMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHighMenuItem.Name = "VwqHighMenuItem";
			this.VwqHighMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqHighMenuItem.Tag = DuView.Types.ViewQuality.High;
			this.VwqHighMenuItem.Text = Du.Globalization.Locale.Text(2105);
			this.VwqHighMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(148, 6);
			// 
			// VwqHqBilinearMenuItem
			// 
			this.VwqHqBilinearMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHqBilinearMenuItem.Name = "VwqHqBilinearMenuItem";
			this.VwqHqBilinearMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqHqBilinearMenuItem.Tag = DuView.Types.ViewQuality.HqBilinear;
			this.VwqHqBilinearMenuItem.Text = Du.Globalization.Locale.Text(2106);
			this.VwqHqBilinearMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// VwqHqBicubicMenuItem
			// 
			this.VwqHqBicubicMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHqBicubicMenuItem.Name = "VwqHqBicubicMenuItem";
			this.VwqHqBicubicMenuItem.Size = new System.Drawing.Size(151, 22);
			this.VwqHqBicubicMenuItem.Tag = DuView.Types.ViewQuality.HqBicubic;
			this.VwqHqBicubicMenuItem.Text = Du.Globalization.Locale.Text(2107);
			this.VwqHqBicubicMenuItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
			// 
			// ViewFitMenuItem
			// 
			this.ViewFitMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewFitMenuItem.Name = "ViewFitMenuItem";
			this.ViewFitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.ViewFitMenuItem.Size = new System.Drawing.Size(151, 22);
			this.ViewFitMenuItem.Tag = DuView.Types.ViewMode.FitWidth;
			this.ViewFitMenuItem.Text = Du.Globalization.Locale.Text(1103);
			this.ViewFitMenuItem.Click += new System.EventHandler(this.ViewModeMenuItem_Click);
			// 
			// ViewLeftRightMenuItem
			// 
			this.ViewLeftRightMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewLeftRightMenuItem.Name = "ViewLeftRightMenuItem";
			this.ViewLeftRightMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.ViewLeftRightMenuItem.Size = new System.Drawing.Size(151, 22);
			this.ViewLeftRightMenuItem.Tag = DuView.Types.ViewMode.LeftToRight;
			this.ViewLeftRightMenuItem.Text = Du.Globalization.Locale.Text(1105);
			this.ViewLeftRightMenuItem.Click += new System.EventHandler(this.ViewModeMenuItem_Click);
			// 
			// ViewRightLeftMenuItem
			// 
			this.ViewRightLeftMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewRightLeftMenuItem.Name = "ViewRightLeftMenuItem";
			this.ViewRightLeftMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.ViewRightLeftMenuItem.Size = new System.Drawing.Size(151, 22);
			this.ViewRightLeftMenuItem.Tag = DuView.Types.ViewMode.RightToLeft;
			this.ViewRightLeftMenuItem.Text = Du.Globalization.Locale.Text(1106);
			this.ViewRightLeftMenuItem.Click += new System.EventHandler(this.ViewModeMenuItem_Click);
			// 
			// PageMenuItem
			// 
			this.PageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.PageSelectMenuItem,
			this.toolStripSeparator13,
			this.PageAddFavMenuItem});
			this.PageMenuItem.ForeColor = System.Drawing.Color.White;
			this.PageMenuItem.Name = "PageMenuItem";
			this.PageMenuItem.Size = new System.Drawing.Size(55, 20);
			this.PageMenuItem.Text = Du.Globalization.Locale.Text(1200);
			// 
			// PageSelectMenuItem
			// 
			this.PageSelectMenuItem.ForeColor = System.Drawing.Color.White;
			this.PageSelectMenuItem.Name = "PageSelectMenuItem";
			this.PageSelectMenuItem.Size = new System.Drawing.Size(130, 22);
			this.PageSelectMenuItem.Tag = DuView.Types.Controls.Select;
			this.PageSelectMenuItem.Text = Du.Globalization.Locale.Text(1201);
			this.PageSelectMenuItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(127, 6);
			// 
			// PageAddFavMenuItem
			// 
			this.PageAddFavMenuItem.ForeColor = System.Drawing.Color.White;
			this.PageAddFavMenuItem.Name = "PageAddFavMenuItem";
			this.PageAddFavMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.PageAddFavMenuItem.Size = new System.Drawing.Size(130, 22);
			this.PageAddFavMenuItem.Text = Du.Globalization.Locale.Text(1202);
			// 
			// FileMenuItem
			// 
			this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.FileOpenMenuItem,
			this.FileOpenLastMenuItem,
			this.FileOpenExternalMenuItem,
			this.FileCloseMenuItem,
			this.toolStripSeparator2,
			this.FileRenameMenuItem,
			this.FileMoveMenuItem,
			this.FileCopyImageMenuItem,
			this.toolStripSeparator3,
			this.FileDeleteMenuItem,
			this.toolStripSeparator14,
			this.FileRefreshMenuItem,
			this.FileOptionMenuItem,
			this.FileExitMenuItem});
			this.FileMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new System.Drawing.Size(55, 20);
			this.FileMenuItem.Text = Du.Globalization.Locale.Text(1300);
			// 
			// FileOpenMenuItem
			// 
			this.FileOpenMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenMenuItem.Name = "FileOpenMenuItem";
			this.FileOpenMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.FileOpenMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileOpenMenuItem.Text = Du.Globalization.Locale.Text(1301);
			this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// FileOpenLastMenuItem
			// 
			this.FileOpenLastMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenLastMenuItem.Name = "FileOpenLastMenuItem";
			this.FileOpenLastMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
			| System.Windows.Forms.Keys.Z)));
			this.FileOpenLastMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileOpenLastMenuItem.Text = Du.Globalization.Locale.Text(1302);
			this.FileOpenLastMenuItem.Click += new System.EventHandler(this.FileOpenLastMenuItem_Click);
			// 
			// FileOpenExternalMenuItem
			// 
			this.FileOpenExternalMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenExternalMenuItem.Name = "FileOpenExternalMenuItem";
			this.FileOpenExternalMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.FileOpenExternalMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileOpenExternalMenuItem.Text = Du.Globalization.Locale.Text(1309);
			this.FileOpenExternalMenuItem.Click += new System.EventHandler(this.FileOpenExternalMenuItem_Click);
			// 
			// FileCloseMenuItem
			// 
			this.FileCloseMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCloseMenuItem.Name = "FileCloseMenuItem";
			this.FileCloseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.FileCloseMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileCloseMenuItem.Text = Du.Globalization.Locale.Text(1303);
			this.FileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
			// 
			// FileRenameMenuItem
			// 
			this.FileRenameMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileRenameMenuItem.Name = "FileRenameMenuItem";
			this.FileRenameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.FileRenameMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileRenameMenuItem.Text = Du.Globalization.Locale.Text(1310);
			this.FileRenameMenuItem.Click += new System.EventHandler(this.FileRenameMenuItem_Click);
			// 
			// FileMoveMenuItem
			// 
			this.FileMoveMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileMoveMenuItem.Name = "FileMoveMenuItem";
			this.FileMoveMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.FileMoveMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileMoveMenuItem.Text = Du.Globalization.Locale.Text(1311);
			this.FileMoveMenuItem.Click += new System.EventHandler(this.FileMoveMenuItem_Click);
			// 
			// FileCopyImageMenuItem
			// 
			this.FileCopyImageMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCopyImageMenuItem.Name = "FileCopyImageMenuItem";
			this.FileCopyImageMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.FileCopyImageMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileCopyImageMenuItem.Text = Du.Globalization.Locale.Text(1304);
			this.FileCopyImageMenuItem.Click += new System.EventHandler(this.FileCopyImageMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
			// 
			// FileDeleteMenuItem
			// 
			this.FileDeleteMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileDeleteMenuItem.Name = "FileDeleteMenuItem";
			this.FileDeleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.FileDeleteMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileDeleteMenuItem.Text = Du.Globalization.Locale.Text(1307);
			this.FileDeleteMenuItem.Click += new System.EventHandler(this.FileDeleteMenuItem_Click);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(181, 6);
			// 
			// FileRefreshMenuItem
			// 
			this.FileRefreshMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileRefreshMenuItem.Name = "FileRefreshMenuItem";
			this.FileRefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.FileRefreshMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileRefreshMenuItem.Text = Du.Globalization.Locale.Text(1305);
			this.FileRefreshMenuItem.Click += new System.EventHandler(this.FileRefreshMenuItem_Click);
			// 
			// FileOptionMenuItem
			// 
			this.FileOptionMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOptionMenuItem.Name = "FileOptionMenuItem";
			this.FileOptionMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.FileOptionMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileOptionMenuItem.Text = Du.Globalization.Locale.Text(1308);
			this.FileOptionMenuItem.Click += new System.EventHandler(this.FileOptionMenuItem_Click);
			// 
			// FileExitMenuItem
			// 
			this.FileExitMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileExitMenuItem.Name = "FileExitMenuItem";
			this.FileExitMenuItem.Size = new System.Drawing.Size(184, 22);
			this.FileExitMenuItem.Text = Du.Globalization.Locale.Text(1306);
			this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// MaxCacheMenuItem
			// 
			this.MaxCacheMenuItem.ForeColor = System.Drawing.Color.White;
			this.MaxCacheMenuItem.Name = "MaxCacheMenuItem";
			this.MaxCacheMenuItem.Size = new System.Drawing.Size(55, 20);
			this.MaxCacheMenuItem.Text = Du.Globalization.Locale.Text(1800);
			// 
			// Notifier
			// 
			this.Notifier.Icon = ((System.Drawing.Icon)(resources.GetObject("Notifier.Icon")));
			this.Notifier.Text = Du.Globalization.Locale.Text(0);
			this.Notifier.Visible = true;
			// 
			// BookCanvas
			// 
			this.BookCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.BookCanvas.ContextMenuStrip = this.ContextPopup;
			this.BookCanvas.Location = new System.Drawing.Point(5, 62);
			this.BookCanvas.Margin = new System.Windows.Forms.Padding(0);
			this.BookCanvas.Name = "BookCanvas";
			this.BookCanvas.Size = new System.Drawing.Size(675, 322);
			this.BookCanvas.TabIndex = 1;
			this.BookCanvas.TabStop = false;
			this.BookCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BookCanvas_MouseDown);
			this.BookCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BookCanvas_MouseMove);
			this.BookCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BookCanvas_MouseUp);
			this.BookCanvas.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.BookCanvas_PreviewKeyDown);
			// 
			// ContextPopup
			// 
			this.ContextPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.OpenPopupItem,
			this.RenamePopupItem,
			this.ClosePopupItem,
			this.toolStripSeparator4,
			this.ControlPopItem,
			this.PagesPopupItem,
			this.toolStripSeparator5,
			this.QualityPopupItem,
			this.toolStripSeparator6,
			this.DeletePopupItem,
			this.toolStripSeparator7,
			this.CopyImagePopupItem,
			this.toolStripSeparator8,
			this.OptionPopupItem,
			this.ExitPopupItem});
			this.ContextPopup.Name = "ContextPopup";
			this.ContextPopup.Size = new System.Drawing.Size(111, 254);
			// 
			// OpenPopupItem
			// 
			this.OpenPopupItem.ForeColor = System.Drawing.Color.White;
			this.OpenPopupItem.Name = "OpenPopupItem";
			this.OpenPopupItem.Size = new System.Drawing.Size(110, 22);
			this.OpenPopupItem.Text = Du.Globalization.Locale.Text(1301);
			this.OpenPopupItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// RenamePopupItem
			// 
			this.RenamePopupItem.ForeColor = System.Drawing.Color.White;
			this.RenamePopupItem.Name = "RenamePopupItem";
			this.RenamePopupItem.Size = new System.Drawing.Size(110, 22);
			this.RenamePopupItem.Text = Du.Globalization.Locale.Text(1309);
			this.RenamePopupItem.Click += new System.EventHandler(this.FileOpenExternalMenuItem_Click);
			// 
			// ClosePopupItem
			// 
			this.ClosePopupItem.ForeColor = System.Drawing.Color.White;
			this.ClosePopupItem.Name = "ClosePopupItem";
			this.ClosePopupItem.Size = new System.Drawing.Size(110, 22);
			this.ClosePopupItem.Text = Du.Globalization.Locale.Text(1303);
			this.ClosePopupItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(107, 6);
			// 
			// ControlPopItem
			// 
			this.ControlPopItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CtrlPrevPopupItem,
			this.CtrlNextPopupItem,
			this.CtrlHomePopupItem,
			this.CtrlEndPopupItem,
			this.toolStripSeparator11,
			this.CtrlPrev10PopupItem,
			this.CtrlNext10PopupItem,
			this.toolStripSeparator12,
			this.CtrlPrevFilePopupItem,
			this.CtrlNextFilePopupItem});
			this.ControlPopItem.ForeColor = System.Drawing.Color.White;
			this.ControlPopItem.Name = "ControlPopItem";
			this.ControlPopItem.Size = new System.Drawing.Size(110, 22);
			this.ControlPopItem.Text = Du.Globalization.Locale.Text(2200);
			// 
			// CtrlPrevPopupItem
			// 
			this.CtrlPrevPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrevPopupItem.Name = "CtrlPrevPopupItem";
			this.CtrlPrevPopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlPrevPopupItem.Tag = DuView.Types.Controls.Previous;
			this.CtrlPrevPopupItem.Text = Du.Globalization.Locale.Text(2201);
			this.CtrlPrevPopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// CtrlNextPopupItem
			// 
			this.CtrlNextPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNextPopupItem.Name = "CtrlNextPopupItem";
			this.CtrlNextPopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlNextPopupItem.Tag = DuView.Types.Controls.Next;
			this.CtrlNextPopupItem.Text = Du.Globalization.Locale.Text(2202);
			this.CtrlNextPopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// CtrlHomePopupItem
			// 
			this.CtrlHomePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlHomePopupItem.Name = "CtrlHomePopupItem";
			this.CtrlHomePopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlHomePopupItem.Tag = DuView.Types.Controls.First;
			this.CtrlHomePopupItem.Text = Du.Globalization.Locale.Text(2203);
			this.CtrlHomePopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// CtrlEndPopupItem
			// 
			this.CtrlEndPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlEndPopupItem.Name = "CtrlEndPopupItem";
			this.CtrlEndPopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlEndPopupItem.Tag = DuView.Types.Controls.Last;
			this.CtrlEndPopupItem.Text = Du.Globalization.Locale.Text(2204);
			this.CtrlEndPopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(107, 6);
			// 
			// CtrlPrev10PopupItem
			// 
			this.CtrlPrev10PopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrev10PopupItem.Name = "CtrlPrev10PopupItem";
			this.CtrlPrev10PopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlPrev10PopupItem.Tag = DuView.Types.Controls.SeekPrevious10;
			this.CtrlPrev10PopupItem.Text = Du.Globalization.Locale.Text(2205);
			this.CtrlPrev10PopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// CtrlNext10PopupItem
			// 
			this.CtrlNext10PopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNext10PopupItem.Name = "CtrlNext10PopupItem";
			this.CtrlNext10PopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlNext10PopupItem.Tag = DuView.Types.Controls.SeekNext10;
			this.CtrlNext10PopupItem.Text = Du.Globalization.Locale.Text(2206);
			this.CtrlNext10PopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(107, 6);
			// 
			// CtrlPrevFilePopupItem
			// 
			this.CtrlPrevFilePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrevFilePopupItem.Name = "CtrlPrevFilePopupItem";
			this.CtrlPrevFilePopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlPrevFilePopupItem.Tag = DuView.Types.Controls.ScanPrevious;
			this.CtrlPrevFilePopupItem.Text = Du.Globalization.Locale.Text(2207);
			this.CtrlPrevFilePopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// CtrlNextFilePopupItem
			// 
			this.CtrlNextFilePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNextFilePopupItem.Name = "CtrlNextFilePopupItem";
			this.CtrlNextFilePopupItem.Size = new System.Drawing.Size(110, 22);
			this.CtrlNextFilePopupItem.Tag = DuView.Types.Controls.ScanNext;
			this.CtrlNextFilePopupItem.Text = Du.Globalization.Locale.Text(2208);
			this.CtrlNextFilePopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// PagesPopupItem
			// 
			this.PagesPopupItem.ForeColor = System.Drawing.Color.White;
			this.PagesPopupItem.Name = "PagesPopupItem";
			this.PagesPopupItem.Size = new System.Drawing.Size(110, 22);
			this.PagesPopupItem.Tag = DuView.Types.Controls.Select;
			this.PagesPopupItem.Text = Du.Globalization.Locale.Text(1201);
			this.PagesPopupItem.Click += new System.EventHandler(this.PageControlMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(107, 6);
			// 
			// QualityPopupItem
			// 
			this.QualityPopupItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.QualityLowPopupItem,
			this.QualityDefaultPopupItem,
			this.QualityBilinearPopupItem,
			this.QualityBicubicPopupItem,
			this.QualityHighPopupItem,
			this.toolStripSeparator10,
			this.QualityHqBilinearPopupItem,
			this.QualityHqBicubicPopupItem});
			this.QualityPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityPopupItem.Name = "QualityPopupItem";
			this.QualityPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityPopupItem.Text = Du.Globalization.Locale.Text(2100);
			// 
			// QualityLowPopupItem
			// 
			this.QualityLowPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityLowPopupItem.Name = "QualityLowPopupItem";
			this.QualityLowPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityLowPopupItem.Tag = DuView.Types.ViewQuality.Low;
			this.QualityLowPopupItem.Text = Du.Globalization.Locale.Text(2101);
			this.QualityLowPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// QualityDefaultPopupItem
			// 
			this.QualityDefaultPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityDefaultPopupItem.Name = "QualityDefaultPopupItem";
			this.QualityDefaultPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityDefaultPopupItem.Tag = DuView.Types.ViewQuality.Default;
			this.QualityDefaultPopupItem.Text = Du.Globalization.Locale.Text(2102);
			this.QualityDefaultPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// QualityBilinearPopupItem
			// 
			this.QualityBilinearPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityBilinearPopupItem.Name = "QualityBilinearPopupItem";
			this.QualityBilinearPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityBilinearPopupItem.Tag = DuView.Types.ViewQuality.Bilinear;
			this.QualityBilinearPopupItem.Text = Du.Globalization.Locale.Text(2103);
			this.QualityBilinearPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// QualityBicubicPopupItem
			// 
			this.QualityBicubicPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityBicubicPopupItem.Name = "QualityBicubicPopupItem";
			this.QualityBicubicPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityBicubicPopupItem.Tag = DuView.Types.ViewQuality.Bicubic;
			this.QualityBicubicPopupItem.Text = Du.Globalization.Locale.Text(2104);
			this.QualityBicubicPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// QualityHighPopupItem
			// 
			this.QualityHighPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHighPopupItem.Name = "QualityHighPopupItem";
			this.QualityHighPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityHighPopupItem.Tag = DuView.Types.ViewQuality.High;
			this.QualityHighPopupItem.Text = Du.Globalization.Locale.Text(2105);
			this.QualityHighPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(107, 6);
			// 
			// QualityHqBilinearPopupItem
			// 
			this.QualityHqBilinearPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHqBilinearPopupItem.Name = "QualityHqBilinearPopupItem";
			this.QualityHqBilinearPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityHqBilinearPopupItem.Tag = DuView.Types.ViewQuality.HqBilinear;
			this.QualityHqBilinearPopupItem.Text = Du.Globalization.Locale.Text(2106);
			this.QualityHqBilinearPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// QualityHqBicubicPopupItem
			// 
			this.QualityHqBicubicPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHqBicubicPopupItem.Name = "QualityHqBicubicPopupItem";
			this.QualityHqBicubicPopupItem.Size = new System.Drawing.Size(110, 22);
			this.QualityHqBicubicPopupItem.Tag = DuView.Types.ViewQuality.HqBicubic;
			this.QualityHqBicubicPopupItem.Text = Du.Globalization.Locale.Text(2107);
			this.QualityHqBicubicPopupItem.Click += new System.EventHandler(this.ViewQualityMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(107, 6);
			// 
			// DeletePopupItem
			// 
			this.DeletePopupItem.ForeColor = System.Drawing.Color.White;
			this.DeletePopupItem.Name = "DeletePopupItem";
			this.DeletePopupItem.Size = new System.Drawing.Size(110, 22);
			this.DeletePopupItem.Text = Du.Globalization.Locale.Text(1307);
			this.DeletePopupItem.Click += new System.EventHandler(this.FileDeleteMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(107, 6);
			// 
			// CopyImagePopupItem
			// 
			this.CopyImagePopupItem.ForeColor = System.Drawing.Color.White;
			this.CopyImagePopupItem.Name = "CopyImagePopupItem";
			this.CopyImagePopupItem.Size = new System.Drawing.Size(110, 22);
			this.CopyImagePopupItem.Text = Du.Globalization.Locale.Text(1304);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(107, 6);
			// 
			// OptionPopupItem
			// 
			this.OptionPopupItem.ForeColor = System.Drawing.Color.White;
			this.OptionPopupItem.Name = "OptionPopupItem";
			this.OptionPopupItem.Size = new System.Drawing.Size(110, 22);
			this.OptionPopupItem.Text = Du.Globalization.Locale.Text(1308);
			this.OptionPopupItem.Click += new System.EventHandler(this.FileOptionMenuItem_Click);
			// 
			// ExitPopupItem
			// 
			this.ExitPopupItem.ForeColor = System.Drawing.Color.White;
			this.ExitPopupItem.Name = "ExitPopupItem";
			this.ExitPopupItem.Size = new System.Drawing.Size(110, 22);
			this.ExitPopupItem.Text = Du.Globalization.Locale.Text(1306);
			this.ExitPopupItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// NotifyLabel
			// 
			this.NotifyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.NotifyLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.NotifyLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.NotifyLabel.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.NotifyLabel.ForeColor = System.Drawing.Color.White;
			this.NotifyLabel.Location = new System.Drawing.Point(21, 139);
			this.NotifyLabel.Name = "NotifyLabel";
			this.NotifyLabel.Size = new System.Drawing.Size(643, 52);
			this.NotifyLabel.TabIndex = 5;
			this.NotifyLabel.Text = "This is notify message";
			this.NotifyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.NotifyLabel.Visible = false;
			// 
			// ReadForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(686, 390);
			this.Controls.Add(this.NotifyLabel);
			this.Controls.Add(this.BookCanvas);
			this.Controls.Add(this.TopPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.MenuStrip;
			this.MinimumSize = new System.Drawing.Size(214, 217);
			this.Name = "ReadForm";
			this.Text = Du.Globalization.Locale.Text(0);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReadForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReadForm_FormClosed);
			this.Load += new System.EventHandler(this.ReadForm_Load);
			this.ClientSizeChanged += new System.EventHandler(this.ReadForm_ClientSizeChanged);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ReadForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ReadForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadForm_KeyDown);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ReadForm_Layout);
			this.TopPanel.ResumeLayout(false);
			this.TopPanel.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BookCanvas)).EndInit();
			this.ContextPopup.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel TopPanel;
		private Du.WinForms.BadakMenuStrip MenuStrip;
		private System.Windows.Forms.Label PageInfo;
		private System.Windows.Forms.Label TitleLabel;
		private Du.WinForms.BadakSystemButton SystemButton;
		private System.Windows.Forms.NotifyIcon Notifier;
		private System.Windows.Forms.PictureBox BookCanvas;
		private Du.WinForms.BadakContextMenuStrip ContextPopup;
		private System.Windows.Forms.ToolStripMenuItem ViewMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewZoomMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ViewFitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewLeftRightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewRightLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PageMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PageAddFavMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileOpenLastMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileCopyImageMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem OpenPopupItem;
		private System.Windows.Forms.ToolStripMenuItem ClosePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem PagesPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem QualityPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem DeletePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem CopyImagePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem ExitPopupItem;
		private System.Windows.Forms.ToolStripMenuItem OptionPopupItem;
		private System.Windows.Forms.ToolStripMenuItem FileRefreshMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileOptionMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewQualityMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqLowMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqDefaultMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqBilinearMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqBicubicMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqHighMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem VwqHqBilinearMenuItem;
		private System.Windows.Forms.ToolStripMenuItem VwqHqBicubicMenuItem;
		private System.Windows.Forms.ToolStripMenuItem QualityLowPopupItem;
		private System.Windows.Forms.ToolStripMenuItem QualityDefaultPopupItem;
		private System.Windows.Forms.ToolStripMenuItem QualityBilinearPopupItem;
		private System.Windows.Forms.ToolStripMenuItem QualityBicubicPopupItem;
		private System.Windows.Forms.ToolStripMenuItem QualityHighPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem QualityHqBilinearPopupItem;
		private System.Windows.Forms.ToolStripMenuItem QualityHqBicubicPopupItem;
		private System.Windows.Forms.ToolStripMenuItem ControlPopItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlPrevPopupItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlNextPopupItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlHomePopupItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlEndPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem CtrlPrev10PopupItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlNext10PopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem CtrlPrevFilePopupItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlNextFilePopupItem;
		private System.Windows.Forms.ToolStripMenuItem MaxCacheMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PageSelectMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem FileDeleteMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.Label NotifyLabel;
		private System.Windows.Forms.ToolStripMenuItem FileOpenExternalMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileRenameMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RenamePopupItem;
		private System.Windows.Forms.ToolStripMenuItem FileMoveMenuItem;
	}
}


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
			components = new System.ComponentModel.Container();
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadForm));
			TopPanel = new Panel();
			PageInfo = new Label();
			TitleLabel = new Label();
			SystemButton = new BadakSystemButton();
			MenuStrip = new BadakMenuStrip();
			ViewMenuItem = new ToolStripMenuItem();
			ViewZoomMenuItem = new ToolStripMenuItem();
			ViewQualityMenuItem = new ToolStripMenuItem();
			VwqLowMenuItem = new ToolStripMenuItem();
			VwqDefaultMenuItem = new ToolStripMenuItem();
			VwqBilinearMenuItem = new ToolStripMenuItem();
			VwqBicubicMenuItem = new ToolStripMenuItem();
			VwqHighMenuItem = new ToolStripMenuItem();
			toolStripSeparator9 = new ToolStripSeparator();
			VwqHqBilinearMenuItem = new ToolStripMenuItem();
			VwqHqBicubicMenuItem = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			ViewFitMenuItem = new ToolStripMenuItem();
			ViewLeftRightMenuItem = new ToolStripMenuItem();
			ViewRightLeftMenuItem = new ToolStripMenuItem();
			PageMenuItem = new ToolStripMenuItem();
			PageSelectMenuItem = new ToolStripMenuItem();
			toolStripSeparator13 = new ToolStripSeparator();
			PageAddFavMenuItem = new ToolStripMenuItem();
			FileMenuItem = new ToolStripMenuItem();
			FileOpenMenuItem = new ToolStripMenuItem();
			FileOpenLastMenuItem = new ToolStripMenuItem();
			FileOpenExternalMenuItem = new ToolStripMenuItem();
			FileCloseMenuItem = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			FileRenameMenuItem = new ToolStripMenuItem();
			FileCopyImageMenuItem = new ToolStripMenuItem();
			toolStripSeparator3 = new ToolStripSeparator();
			FileDeleteMenuItem = new ToolStripMenuItem();
			toolStripSeparator14 = new ToolStripSeparator();
			FileMoveMenuItem = new ToolStripMenuItem();
			FileRefreshMenuItem = new ToolStripMenuItem();
			FileOptionMenuItem = new ToolStripMenuItem();
			FileExitMenuItem = new ToolStripMenuItem();
			MaxCacheMenuItem = new ToolStripMenuItem();
			BookCanvas = new PictureBox();
			ContextPopup = new BadakContextMenuStrip(components);
			OpenPopupItem = new ToolStripMenuItem();
			RenamePopupItem = new ToolStripMenuItem();
			ClosePopupItem = new ToolStripMenuItem();
			toolStripSeparator4 = new ToolStripSeparator();
			ControlPopItem = new ToolStripMenuItem();
			CtrlPrevPopupItem = new ToolStripMenuItem();
			CtrlNextPopupItem = new ToolStripMenuItem();
			CtrlHomePopupItem = new ToolStripMenuItem();
			CtrlEndPopupItem = new ToolStripMenuItem();
			toolStripSeparator11 = new ToolStripSeparator();
			CtrlPrev10PopupItem = new ToolStripMenuItem();
			CtrlNext10PopupItem = new ToolStripMenuItem();
			toolStripSeparator12 = new ToolStripSeparator();
			CtrlPrevFilePopupItem = new ToolStripMenuItem();
			CtrlNextFilePopupItem = new ToolStripMenuItem();
			PagesPopupItem = new ToolStripMenuItem();
			toolStripSeparator5 = new ToolStripSeparator();
			QualityPopupItem = new ToolStripMenuItem();
			QualityLowPopupItem = new ToolStripMenuItem();
			QualityDefaultPopupItem = new ToolStripMenuItem();
			QualityBilinearPopupItem = new ToolStripMenuItem();
			QualityBicubicPopupItem = new ToolStripMenuItem();
			QualityHighPopupItem = new ToolStripMenuItem();
			toolStripSeparator10 = new ToolStripSeparator();
			QualityHqBilinearPopupItem = new ToolStripMenuItem();
			QualityHqBicubicPopupItem = new ToolStripMenuItem();
			toolStripSeparator6 = new ToolStripSeparator();
			DeletePopupItem = new ToolStripMenuItem();
			toolStripSeparator7 = new ToolStripSeparator();
			CopyImagePopupItem = new ToolStripMenuItem();
			toolStripSeparator8 = new ToolStripSeparator();
			OptionPopupItem = new ToolStripMenuItem();
			ExitPopupItem = new ToolStripMenuItem();
			NotifyLabel = new Label();
			PassPanel = new Panel();
			PassText = new TextBox();
			PassLabel = new Label();
			TopPanel.SuspendLayout();
			MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)BookCanvas).BeginInit();
			ContextPopup.SuspendLayout();
			PassPanel.SuspendLayout();
			SuspendLayout();
			// 
			// TopPanel
			// 
			TopPanel.BackColor = Color.FromArgb(30, 30, 30);
			TopPanel.Controls.Add(PageInfo);
			TopPanel.Controls.Add(TitleLabel);
			TopPanel.Controls.Add(SystemButton);
			TopPanel.Controls.Add(MenuStrip);
			TopPanel.Dock = DockStyle.Top;
			TopPanel.Location = new Point(0, 0);
			TopPanel.Name = "TopPanel";
			TopPanel.Size = new Size(800, 70);
			TopPanel.TabIndex = 0;
			TopPanel.MouseDown += TopPanel_MouseDown;
			TopPanel.MouseMove += TopPanel_MouseMove;
			TopPanel.MouseUp += TopPanel_MouseUp;
			// 
			// PageInfo
			// 
			PageInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			PageInfo.ForeColor = Color.White;
			PageInfo.Location = new Point(648, 50);
			PageInfo.Name = "PageInfo";
			PageInfo.Size = new Size(149, 15);
			PageInfo.TabIndex = 4;
			PageInfo.Text = "0";
			PageInfo.TextAlign = ContentAlignment.TopRight;
			PageInfo.Visible = false;
			PageInfo.MouseDown += TopPanel_MouseDown;
			PageInfo.MouseMove += TopPanel_MouseMove;
			PageInfo.MouseUp += TopPanel_MouseUp;
			// 
			// TitleLabel
			// 
			TitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TitleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			TitleLabel.ForeColor = Color.White;
			TitleLabel.Location = new Point(11, 33);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(627, 36);
			TitleLabel.TabIndex = 3;
			TitleLabel.Text = "0";
			TitleLabel.MouseDown += TopPanel_MouseDown;
			TitleLabel.MouseMove += TopPanel_MouseMove;
			TitleLabel.MouseUp += TopPanel_MouseUp;
			// 
			// SystemButton
			// 
			SystemButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			SystemButton.BackColor = Color.Transparent;
			SystemButton.Form = null;
			SystemButton.Location = new Point(648, 0);
			SystemButton.Margin = new Padding(0);
			SystemButton.MaximumSize = new Size(150, 30);
			SystemButton.MinimumSize = new Size(150, 30);
			SystemButton.Name = "SystemButton";
			SystemButton.ShowClose = true;
			SystemButton.ShowMaximize = true;
			SystemButton.ShowMinimize = true;
			SystemButton.Size = new Size(150, 30);
			SystemButton.TabIndex = 2;
			SystemButton.TabStop = false;
			// 
			// MenuStrip
			// 
			MenuStrip.Dock = DockStyle.None;
			MenuStrip.Items.AddRange(new ToolStripItem[] { ViewMenuItem, PageMenuItem, FileMenuItem, MaxCacheMenuItem });
			MenuStrip.Location = new Point(3, 3);
			MenuStrip.Name = "MenuStrip";
			MenuStrip.Size = new Size(212, 24);
			MenuStrip.TabIndex = 1;
			MenuStrip.Text = "95";
			// 
			// ViewMenuItem
			// 
			ViewMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ViewZoomMenuItem, ViewQualityMenuItem, toolStripSeparator1, ViewFitMenuItem, ViewLeftRightMenuItem, ViewRightLeftMenuItem });
			ViewMenuItem.ForeColor = Color.White;
			ViewMenuItem.Image = Properties.Resources.viewmode_pitwidth;
			ViewMenuItem.Name = "ViewMenuItem";
			ViewMenuItem.Size = new Size(63, 20);
			ViewMenuItem.Text = "1100";
			// 
			// ViewZoomMenuItem
			// 
			ViewZoomMenuItem.ForeColor = Color.White;
			ViewZoomMenuItem.Name = "ViewZoomMenuItem";
			ViewZoomMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
			ViewZoomMenuItem.Size = new Size(143, 22);
			ViewZoomMenuItem.Text = "1101";
			ViewZoomMenuItem.Click += ViewZoomMenuItem_Click;
			// 
			// ViewQualityMenuItem
			// 
			ViewQualityMenuItem.DropDownItems.AddRange(new ToolStripItem[] { VwqLowMenuItem, VwqDefaultMenuItem, VwqBilinearMenuItem, VwqBicubicMenuItem, VwqHighMenuItem, toolStripSeparator9, VwqHqBilinearMenuItem, VwqHqBicubicMenuItem });
			ViewQualityMenuItem.ForeColor = Color.White;
			ViewQualityMenuItem.Name = "ViewQualityMenuItem";
			ViewQualityMenuItem.Size = new Size(143, 22);
			ViewQualityMenuItem.Text = "1102";
			// 
			// VwqLowMenuItem
			// 
			VwqLowMenuItem.ForeColor = Color.White;
			VwqLowMenuItem.Name = "VwqLowMenuItem";
			VwqLowMenuItem.Size = new Size(143, 22);
			VwqLowMenuItem.Tag = Types.ViewQuality.Low;
			VwqLowMenuItem.Text = "2101";
			VwqLowMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// VwqDefaultMenuItem
			// 
			VwqDefaultMenuItem.ForeColor = Color.White;
			VwqDefaultMenuItem.Name = "VwqDefaultMenuItem";
			VwqDefaultMenuItem.ShortcutKeys = Keys.Control | Keys.D5;
			VwqDefaultMenuItem.Size = new Size(143, 22);
			VwqDefaultMenuItem.Tag = Types.ViewQuality.Default;
			VwqDefaultMenuItem.Text = "2102";
			VwqDefaultMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// VwqBilinearMenuItem
			// 
			VwqBilinearMenuItem.ForeColor = Color.White;
			VwqBilinearMenuItem.Name = "VwqBilinearMenuItem";
			VwqBilinearMenuItem.Size = new Size(143, 22);
			VwqBilinearMenuItem.Tag = Types.ViewQuality.Bilinear;
			VwqBilinearMenuItem.Text = "2103";
			VwqBilinearMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// VwqBicubicMenuItem
			// 
			VwqBicubicMenuItem.ForeColor = Color.White;
			VwqBicubicMenuItem.Name = "VwqBicubicMenuItem";
			VwqBicubicMenuItem.Size = new Size(143, 22);
			VwqBicubicMenuItem.Tag = Types.ViewQuality.Bicubic;
			VwqBicubicMenuItem.Text = "2104";
			VwqBicubicMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// VwqHighMenuItem
			// 
			VwqHighMenuItem.ForeColor = Color.White;
			VwqHighMenuItem.Name = "VwqHighMenuItem";
			VwqHighMenuItem.Size = new Size(143, 22);
			VwqHighMenuItem.Tag = Types.ViewQuality.High;
			VwqHighMenuItem.Text = "2105";
			VwqHighMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// toolStripSeparator9
			// 
			toolStripSeparator9.Name = "toolStripSeparator9";
			toolStripSeparator9.Size = new Size(140, 6);
			// 
			// VwqHqBilinearMenuItem
			// 
			VwqHqBilinearMenuItem.ForeColor = Color.White;
			VwqHqBilinearMenuItem.Name = "VwqHqBilinearMenuItem";
			VwqHqBilinearMenuItem.Size = new Size(143, 22);
			VwqHqBilinearMenuItem.Tag = Types.ViewQuality.HqBilinear;
			VwqHqBilinearMenuItem.Text = "2106";
			VwqHqBilinearMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// VwqHqBicubicMenuItem
			// 
			VwqHqBicubicMenuItem.ForeColor = Color.White;
			VwqHqBicubicMenuItem.Name = "VwqHqBicubicMenuItem";
			VwqHqBicubicMenuItem.Size = new Size(143, 22);
			VwqHqBicubicMenuItem.Tag = Types.ViewQuality.HqBicubic;
			VwqHqBicubicMenuItem.Text = "2107";
			VwqHqBicubicMenuItem.Click += ViewQualityMenuItem_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(140, 6);
			// 
			// ViewFitMenuItem
			// 
			ViewFitMenuItem.ForeColor = Color.White;
			ViewFitMenuItem.Name = "ViewFitMenuItem";
			ViewFitMenuItem.ShortcutKeys = Keys.Control | Keys.D1;
			ViewFitMenuItem.Size = new Size(143, 22);
			ViewFitMenuItem.Tag = Types.ViewMode.FitWidth;
			ViewFitMenuItem.Text = "1103";
			ViewFitMenuItem.Click += ViewModeMenuItem_Click;
			// 
			// ViewLeftRightMenuItem
			// 
			ViewLeftRightMenuItem.ForeColor = Color.White;
			ViewLeftRightMenuItem.Name = "ViewLeftRightMenuItem";
			ViewLeftRightMenuItem.ShortcutKeys = Keys.Control | Keys.D3;
			ViewLeftRightMenuItem.Size = new Size(143, 22);
			ViewLeftRightMenuItem.Tag = Types.ViewMode.LeftToRight;
			ViewLeftRightMenuItem.Text = "1105";
			ViewLeftRightMenuItem.Click += ViewModeMenuItem_Click;
			// 
			// ViewRightLeftMenuItem
			// 
			ViewRightLeftMenuItem.ForeColor = Color.White;
			ViewRightLeftMenuItem.Name = "ViewRightLeftMenuItem";
			ViewRightLeftMenuItem.ShortcutKeys = Keys.Control | Keys.D4;
			ViewRightLeftMenuItem.Size = new Size(143, 22);
			ViewRightLeftMenuItem.Tag = Types.ViewMode.RightToLeft;
			ViewRightLeftMenuItem.Text = "1106";
			ViewRightLeftMenuItem.Click += ViewModeMenuItem_Click;
			// 
			// PageMenuItem
			// 
			PageMenuItem.DropDownItems.AddRange(new ToolStripItem[] { PageSelectMenuItem, toolStripSeparator13, PageAddFavMenuItem });
			PageMenuItem.ForeColor = Color.White;
			PageMenuItem.Name = "PageMenuItem";
			PageMenuItem.Size = new Size(47, 20);
			PageMenuItem.Text = "1200";
			// 
			// PageSelectMenuItem
			// 
			PageSelectMenuItem.ForeColor = Color.White;
			PageSelectMenuItem.Name = "PageSelectMenuItem";
			PageSelectMenuItem.Size = new Size(122, 22);
			PageSelectMenuItem.Tag = Types.BookControl.Select;
			PageSelectMenuItem.Text = "1201";
			PageSelectMenuItem.Click += PageControlMenuItem_Click;
			// 
			// toolStripSeparator13
			// 
			toolStripSeparator13.Name = "toolStripSeparator13";
			toolStripSeparator13.Size = new Size(119, 6);
			// 
			// PageAddFavMenuItem
			// 
			PageAddFavMenuItem.ForeColor = Color.White;
			PageAddFavMenuItem.Name = "PageAddFavMenuItem";
			PageAddFavMenuItem.ShortcutKeys = Keys.F8;
			PageAddFavMenuItem.Size = new Size(122, 22);
			PageAddFavMenuItem.Text = "1202";
			// 
			// FileMenuItem
			// 
			FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { FileOpenMenuItem, FileOpenLastMenuItem, FileOpenExternalMenuItem, FileCloseMenuItem, toolStripSeparator2, FileRenameMenuItem, FileCopyImageMenuItem, toolStripSeparator3, FileDeleteMenuItem, toolStripSeparator14, FileMoveMenuItem, FileRefreshMenuItem, FileOptionMenuItem, FileExitMenuItem });
			FileMenuItem.ForeColor = Color.White;
			FileMenuItem.Name = "FileMenuItem";
			FileMenuItem.Size = new Size(47, 20);
			FileMenuItem.Text = "1300";
			// 
			// FileOpenMenuItem
			// 
			FileOpenMenuItem.ForeColor = Color.White;
			FileOpenMenuItem.Name = "FileOpenMenuItem";
			FileOpenMenuItem.ShortcutKeys = Keys.F3;
			FileOpenMenuItem.Size = new Size(176, 22);
			FileOpenMenuItem.Text = "1301";
			FileOpenMenuItem.Click += FileOpenMenuItem_Click;
			// 
			// FileOpenLastMenuItem
			// 
			FileOpenLastMenuItem.ForeColor = Color.White;
			FileOpenLastMenuItem.Name = "FileOpenLastMenuItem";
			FileOpenLastMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
			FileOpenLastMenuItem.Size = new Size(176, 22);
			FileOpenLastMenuItem.Text = "1302";
			FileOpenLastMenuItem.Click += FileOpenLastMenuItem_Click;
			// 
			// FileOpenExternalMenuItem
			// 
			FileOpenExternalMenuItem.ForeColor = Color.White;
			FileOpenExternalMenuItem.Name = "FileOpenExternalMenuItem";
			FileOpenExternalMenuItem.ShortcutKeys = Keys.F11;
			FileOpenExternalMenuItem.Size = new Size(176, 22);
			FileOpenExternalMenuItem.Text = "1309";
			FileOpenExternalMenuItem.Click += FileOpenExternalMenuItem_Click;
			// 
			// FileCloseMenuItem
			// 
			FileCloseMenuItem.ForeColor = Color.White;
			FileCloseMenuItem.Name = "FileCloseMenuItem";
			FileCloseMenuItem.ShortcutKeys = Keys.F4;
			FileCloseMenuItem.Size = new Size(176, 22);
			FileCloseMenuItem.Text = "1303";
			FileCloseMenuItem.Click += FileCloseMenuItem_Click;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(173, 6);
			// 
			// FileRenameMenuItem
			// 
			FileRenameMenuItem.ForeColor = Color.White;
			FileRenameMenuItem.Name = "FileRenameMenuItem";
			FileRenameMenuItem.ShortcutKeys = Keys.F2;
			FileRenameMenuItem.Size = new Size(176, 22);
			FileRenameMenuItem.Text = "1310";
			FileRenameMenuItem.Click += FileRenameMenuItem_Click;
			// 
			// FileCopyImageMenuItem
			// 
			FileCopyImageMenuItem.ForeColor = Color.White;
			FileCopyImageMenuItem.Name = "FileCopyImageMenuItem";
			FileCopyImageMenuItem.ShortcutKeys = Keys.Control | Keys.C;
			FileCopyImageMenuItem.Size = new Size(176, 22);
			FileCopyImageMenuItem.Text = "1304";
			FileCopyImageMenuItem.Click += FileCopyImageMenuItem_Click;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new Size(173, 6);
			// 
			// FileDeleteMenuItem
			// 
			FileDeleteMenuItem.ForeColor = Color.White;
			FileDeleteMenuItem.Name = "FileDeleteMenuItem";
			FileDeleteMenuItem.ShortcutKeys = Keys.Delete;
			FileDeleteMenuItem.Size = new Size(176, 22);
			FileDeleteMenuItem.Text = "1307";
			FileDeleteMenuItem.Click += FileDeleteMenuItem_Click;
			// 
			// toolStripSeparator14
			// 
			toolStripSeparator14.Name = "toolStripSeparator14";
			toolStripSeparator14.Size = new Size(173, 6);
			// 
			// FileMoveMenuItem
			// 
			FileMoveMenuItem.ForeColor = Color.White;
			FileMoveMenuItem.Name = "FileMoveMenuItem";
			FileMoveMenuItem.ShortcutKeys = Keys.F6;
			FileMoveMenuItem.Size = new Size(176, 22);
			FileMoveMenuItem.Text = "1316";
			FileMoveMenuItem.Click += FileMoveMenuItem_Click;
			// 
			// FileRefreshMenuItem
			// 
			FileRefreshMenuItem.ForeColor = Color.White;
			FileRefreshMenuItem.Name = "FileRefreshMenuItem";
			FileRefreshMenuItem.ShortcutKeys = Keys.F5;
			FileRefreshMenuItem.Size = new Size(176, 22);
			FileRefreshMenuItem.Text = "1305";
			FileRefreshMenuItem.Click += FileRefreshMenuItem_Click;
			// 
			// FileOptionMenuItem
			// 
			FileOptionMenuItem.ForeColor = Color.White;
			FileOptionMenuItem.Name = "FileOptionMenuItem";
			FileOptionMenuItem.ShortcutKeys = Keys.F12;
			FileOptionMenuItem.Size = new Size(176, 22);
			FileOptionMenuItem.Text = "1308";
			FileOptionMenuItem.Click += FileOptionMenuItem_Click;
			// 
			// FileExitMenuItem
			// 
			FileExitMenuItem.ForeColor = Color.White;
			FileExitMenuItem.Name = "FileExitMenuItem";
			FileExitMenuItem.Size = new Size(176, 22);
			FileExitMenuItem.Text = "1306";
			FileExitMenuItem.Click += FileExitMenuItem_Click;
			// 
			// MaxCacheMenuItem
			// 
			MaxCacheMenuItem.ForeColor = Color.White;
			MaxCacheMenuItem.Name = "MaxCacheMenuItem";
			MaxCacheMenuItem.Size = new Size(47, 20);
			MaxCacheMenuItem.Text = "1800";
			// 
			// BookCanvas
			// 
			BookCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			BookCanvas.ContextMenuStrip = ContextPopup;
			BookCanvas.Location = new Point(6, 72);
			BookCanvas.Margin = new Padding(0);
			BookCanvas.Name = "BookCanvas";
			BookCanvas.Size = new Size(788, 372);
			BookCanvas.TabIndex = 1;
			BookCanvas.TabStop = false;
			BookCanvas.MouseDown += BookCanvas_MouseDown;
			BookCanvas.MouseMove += BookCanvas_MouseMove;
			BookCanvas.MouseUp += BookCanvas_MouseUp;
			BookCanvas.PreviewKeyDown += BookCanvas_PreviewKeyDown;
			// 
			// ContextPopup
			// 
			ContextPopup.Items.AddRange(new ToolStripItem[] { OpenPopupItem, RenamePopupItem, ClosePopupItem, toolStripSeparator4, ControlPopItem, PagesPopupItem, toolStripSeparator5, QualityPopupItem, toolStripSeparator6, DeletePopupItem, toolStripSeparator7, CopyImagePopupItem, toolStripSeparator8, OptionPopupItem, ExitPopupItem });
			ContextPopup.Name = "ContextPopup";
			ContextPopup.Size = new Size(103, 254);
			// 
			// OpenPopupItem
			// 
			OpenPopupItem.ForeColor = Color.White;
			OpenPopupItem.Name = "OpenPopupItem";
			OpenPopupItem.Size = new Size(102, 22);
			OpenPopupItem.Text = "1301";
			OpenPopupItem.Click += FileOpenMenuItem_Click;
			// 
			// RenamePopupItem
			// 
			RenamePopupItem.ForeColor = Color.White;
			RenamePopupItem.Name = "RenamePopupItem";
			RenamePopupItem.Size = new Size(102, 22);
			RenamePopupItem.Text = "1309";
			RenamePopupItem.Click += FileOpenExternalMenuItem_Click;
			// 
			// ClosePopupItem
			// 
			ClosePopupItem.ForeColor = Color.White;
			ClosePopupItem.Name = "ClosePopupItem";
			ClosePopupItem.Size = new Size(102, 22);
			ClosePopupItem.Text = "1303";
			ClosePopupItem.Click += FileCloseMenuItem_Click;
			// 
			// toolStripSeparator4
			// 
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new Size(99, 6);
			// 
			// ControlPopItem
			// 
			ControlPopItem.DropDownItems.AddRange(new ToolStripItem[] { CtrlPrevPopupItem, CtrlNextPopupItem, CtrlHomePopupItem, CtrlEndPopupItem, toolStripSeparator11, CtrlPrev10PopupItem, CtrlNext10PopupItem, toolStripSeparator12, CtrlPrevFilePopupItem, CtrlNextFilePopupItem });
			ControlPopItem.ForeColor = Color.White;
			ControlPopItem.Name = "ControlPopItem";
			ControlPopItem.Size = new Size(102, 22);
			ControlPopItem.Text = "2200";
			// 
			// CtrlPrevPopupItem
			// 
			CtrlPrevPopupItem.ForeColor = Color.White;
			CtrlPrevPopupItem.Name = "CtrlPrevPopupItem";
			CtrlPrevPopupItem.Size = new Size(102, 22);
			CtrlPrevPopupItem.Tag = Types.BookControl.Previous;
			CtrlPrevPopupItem.Text = "2201";
			CtrlPrevPopupItem.Click += PageControlMenuItem_Click;
			// 
			// CtrlNextPopupItem
			// 
			CtrlNextPopupItem.ForeColor = Color.White;
			CtrlNextPopupItem.Name = "CtrlNextPopupItem";
			CtrlNextPopupItem.Size = new Size(102, 22);
			CtrlNextPopupItem.Tag = Types.BookControl.Next;
			CtrlNextPopupItem.Text = "2202";
			CtrlNextPopupItem.Click += PageControlMenuItem_Click;
			// 
			// CtrlHomePopupItem
			// 
			CtrlHomePopupItem.ForeColor = Color.White;
			CtrlHomePopupItem.Name = "CtrlHomePopupItem";
			CtrlHomePopupItem.Size = new Size(102, 22);
			CtrlHomePopupItem.Tag = Types.BookControl.First;
			CtrlHomePopupItem.Text = "2203";
			CtrlHomePopupItem.Click += PageControlMenuItem_Click;
			// 
			// CtrlEndPopupItem
			// 
			CtrlEndPopupItem.ForeColor = Color.White;
			CtrlEndPopupItem.Name = "CtrlEndPopupItem";
			CtrlEndPopupItem.Size = new Size(102, 22);
			CtrlEndPopupItem.Tag = Types.BookControl.Last;
			CtrlEndPopupItem.Text = "2204";
			CtrlEndPopupItem.Click += PageControlMenuItem_Click;
			// 
			// toolStripSeparator11
			// 
			toolStripSeparator11.Name = "toolStripSeparator11";
			toolStripSeparator11.Size = new Size(99, 6);
			// 
			// CtrlPrev10PopupItem
			// 
			CtrlPrev10PopupItem.ForeColor = Color.White;
			CtrlPrev10PopupItem.Name = "CtrlPrev10PopupItem";
			CtrlPrev10PopupItem.Size = new Size(102, 22);
			CtrlPrev10PopupItem.Tag = Types.BookControl.SeekPrevious10;
			CtrlPrev10PopupItem.Text = "2205";
			CtrlPrev10PopupItem.Click += PageControlMenuItem_Click;
			// 
			// CtrlNext10PopupItem
			// 
			CtrlNext10PopupItem.ForeColor = Color.White;
			CtrlNext10PopupItem.Name = "CtrlNext10PopupItem";
			CtrlNext10PopupItem.Size = new Size(102, 22);
			CtrlNext10PopupItem.Tag = Types.BookControl.SeekNext10;
			CtrlNext10PopupItem.Text = "2206";
			CtrlNext10PopupItem.Click += PageControlMenuItem_Click;
			// 
			// toolStripSeparator12
			// 
			toolStripSeparator12.Name = "toolStripSeparator12";
			toolStripSeparator12.Size = new Size(99, 6);
			// 
			// CtrlPrevFilePopupItem
			// 
			CtrlPrevFilePopupItem.ForeColor = Color.White;
			CtrlPrevFilePopupItem.Name = "CtrlPrevFilePopupItem";
			CtrlPrevFilePopupItem.Size = new Size(102, 22);
			CtrlPrevFilePopupItem.Tag = Types.BookControl.ScanPrevious;
			CtrlPrevFilePopupItem.Text = "2207";
			CtrlPrevFilePopupItem.Click += PageControlMenuItem_Click;
			// 
			// CtrlNextFilePopupItem
			// 
			CtrlNextFilePopupItem.ForeColor = Color.White;
			CtrlNextFilePopupItem.Name = "CtrlNextFilePopupItem";
			CtrlNextFilePopupItem.Size = new Size(102, 22);
			CtrlNextFilePopupItem.Tag = Types.BookControl.ScanNext;
			CtrlNextFilePopupItem.Text = "2208";
			CtrlNextFilePopupItem.Click += PageControlMenuItem_Click;
			// 
			// PagesPopupItem
			// 
			PagesPopupItem.ForeColor = Color.White;
			PagesPopupItem.Name = "PagesPopupItem";
			PagesPopupItem.Size = new Size(102, 22);
			PagesPopupItem.Tag = Types.BookControl.Select;
			PagesPopupItem.Text = "1201";
			PagesPopupItem.Click += PageControlMenuItem_Click;
			// 
			// toolStripSeparator5
			// 
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new Size(99, 6);
			// 
			// QualityPopupItem
			// 
			QualityPopupItem.DropDownItems.AddRange(new ToolStripItem[] { QualityLowPopupItem, QualityDefaultPopupItem, QualityBilinearPopupItem, QualityBicubicPopupItem, QualityHighPopupItem, toolStripSeparator10, QualityHqBilinearPopupItem, QualityHqBicubicPopupItem });
			QualityPopupItem.ForeColor = Color.White;
			QualityPopupItem.Name = "QualityPopupItem";
			QualityPopupItem.Size = new Size(102, 22);
			QualityPopupItem.Text = "2100";
			// 
			// QualityLowPopupItem
			// 
			QualityLowPopupItem.ForeColor = Color.White;
			QualityLowPopupItem.Name = "QualityLowPopupItem";
			QualityLowPopupItem.Size = new Size(102, 22);
			QualityLowPopupItem.Tag = Types.ViewQuality.Low;
			QualityLowPopupItem.Text = "2101";
			QualityLowPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// QualityDefaultPopupItem
			// 
			QualityDefaultPopupItem.ForeColor = Color.White;
			QualityDefaultPopupItem.Name = "QualityDefaultPopupItem";
			QualityDefaultPopupItem.Size = new Size(102, 22);
			QualityDefaultPopupItem.Tag = Types.ViewQuality.Default;
			QualityDefaultPopupItem.Text = "2102";
			QualityDefaultPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// QualityBilinearPopupItem
			// 
			QualityBilinearPopupItem.ForeColor = Color.White;
			QualityBilinearPopupItem.Name = "QualityBilinearPopupItem";
			QualityBilinearPopupItem.Size = new Size(102, 22);
			QualityBilinearPopupItem.Tag = Types.ViewQuality.Bilinear;
			QualityBilinearPopupItem.Text = "2103";
			QualityBilinearPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// QualityBicubicPopupItem
			// 
			QualityBicubicPopupItem.ForeColor = Color.White;
			QualityBicubicPopupItem.Name = "QualityBicubicPopupItem";
			QualityBicubicPopupItem.Size = new Size(102, 22);
			QualityBicubicPopupItem.Tag = Types.ViewQuality.Bicubic;
			QualityBicubicPopupItem.Text = "2104";
			QualityBicubicPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// QualityHighPopupItem
			// 
			QualityHighPopupItem.ForeColor = Color.White;
			QualityHighPopupItem.Name = "QualityHighPopupItem";
			QualityHighPopupItem.Size = new Size(102, 22);
			QualityHighPopupItem.Tag = Types.ViewQuality.High;
			QualityHighPopupItem.Text = "2105";
			QualityHighPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// toolStripSeparator10
			// 
			toolStripSeparator10.Name = "toolStripSeparator10";
			toolStripSeparator10.Size = new Size(99, 6);
			// 
			// QualityHqBilinearPopupItem
			// 
			QualityHqBilinearPopupItem.ForeColor = Color.White;
			QualityHqBilinearPopupItem.Name = "QualityHqBilinearPopupItem";
			QualityHqBilinearPopupItem.Size = new Size(102, 22);
			QualityHqBilinearPopupItem.Tag = Types.ViewQuality.HqBilinear;
			QualityHqBilinearPopupItem.Text = "2106";
			QualityHqBilinearPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// QualityHqBicubicPopupItem
			// 
			QualityHqBicubicPopupItem.ForeColor = Color.White;
			QualityHqBicubicPopupItem.Name = "QualityHqBicubicPopupItem";
			QualityHqBicubicPopupItem.Size = new Size(102, 22);
			QualityHqBicubicPopupItem.Tag = Types.ViewQuality.HqBicubic;
			QualityHqBicubicPopupItem.Text = "2107";
			QualityHqBicubicPopupItem.Click += ViewQualityMenuItem_Click;
			// 
			// toolStripSeparator6
			// 
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new Size(99, 6);
			// 
			// DeletePopupItem
			// 
			DeletePopupItem.ForeColor = Color.White;
			DeletePopupItem.Name = "DeletePopupItem";
			DeletePopupItem.Size = new Size(102, 22);
			DeletePopupItem.Text = "1307";
			DeletePopupItem.Click += FileDeleteMenuItem_Click;
			// 
			// toolStripSeparator7
			// 
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new Size(99, 6);
			// 
			// CopyImagePopupItem
			// 
			CopyImagePopupItem.ForeColor = Color.White;
			CopyImagePopupItem.Name = "CopyImagePopupItem";
			CopyImagePopupItem.Size = new Size(102, 22);
			CopyImagePopupItem.Text = "1304";
			CopyImagePopupItem.Click += FileCopyImageMenuItem_Click;
			// 
			// toolStripSeparator8
			// 
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new Size(99, 6);
			// 
			// OptionPopupItem
			// 
			OptionPopupItem.ForeColor = Color.White;
			OptionPopupItem.Name = "OptionPopupItem";
			OptionPopupItem.Size = new Size(102, 22);
			OptionPopupItem.Text = "1308";
			OptionPopupItem.Click += FileOptionMenuItem_Click;
			// 
			// ExitPopupItem
			// 
			ExitPopupItem.ForeColor = Color.White;
			ExitPopupItem.Name = "ExitPopupItem";
			ExitPopupItem.Size = new Size(102, 22);
			ExitPopupItem.Text = "1306";
			ExitPopupItem.Click += FileExitMenuItem_Click;
			// 
			// NotifyLabel
			// 
			NotifyLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			NotifyLabel.BackColor = Color.FromArgb(0, 32, 32);
			NotifyLabel.BorderStyle = BorderStyle.Fixed3D;
			NotifyLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			NotifyLabel.ForeColor = Color.White;
			NotifyLabel.Location = new Point(25, 160);
			NotifyLabel.Name = "NotifyLabel";
			NotifyLabel.Size = new Size(750, 60);
			NotifyLabel.TabIndex = 5;
			NotifyLabel.Text = "This is notify message";
			NotifyLabel.TextAlign = ContentAlignment.MiddleCenter;
			NotifyLabel.Visible = false;
			// 
			// PassPanel
			// 
			PassPanel.Anchor = AnchorStyles.None;
			PassPanel.BackColor = Color.SlateBlue;
			PassPanel.BorderStyle = BorderStyle.FixedSingle;
			PassPanel.Controls.Add(PassText);
			PassPanel.Controls.Add(PassLabel);
			PassPanel.Location = new Point(585, 367);
			PassPanel.Name = "PassPanel";
			PassPanel.Size = new Size(203, 71);
			PassPanel.TabIndex = 6;
			PassPanel.Visible = false;
			// 
			// PassText
			// 
			PassText.BackColor = Color.Sienna;
			PassText.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			PassText.ForeColor = Color.White;
			PassText.Location = new Point(3, 28);
			PassText.MaxLength = 12;
			PassText.Name = "PassText";
			PassText.PasswordChar = '●';
			PassText.Size = new Size(195, 33);
			PassText.TabIndex = 1;
			PassText.TextChanged += PassText_TextChanged;
			// 
			// PassLabel
			// 
			PassLabel.AutoSize = true;
			PassLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			PassLabel.ForeColor = Color.White;
			PassLabel.Location = new Point(3, 0);
			PassLabel.Name = "PassLabel";
			PassLabel.Size = new Size(45, 25);
			PassLabel.TabIndex = 0;
			PassLabel.Text = "130";
			// 
			// ReadForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(60, 60, 60);
			ClientSize = new Size(800, 450);
			Controls.Add(PassPanel);
			Controls.Add(NotifyLabel);
			Controls.Add(BookCanvas);
			Controls.Add(TopPanel);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			MainMenuStrip = MenuStrip;
			MinimumSize = new Size(250, 250);
			Name = "ReadForm";
			Text = "0";
			FormClosing += ReadForm_FormClosing;
			FormClosed += ReadForm_FormClosed;
			Load += ReadForm_Load;
			ClientSizeChanged += ReadForm_ClientSizeChanged;
			DragDrop += ReadForm_DragDrop;
			DragEnter += ReadForm_DragEnter;
			KeyDown += ReadForm_KeyDown;
			Layout += ReadForm_Layout;
			TopPanel.ResumeLayout(false);
			TopPanel.PerformLayout();
			MenuStrip.ResumeLayout(false);
			MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)BookCanvas).EndInit();
			ContextPopup.ResumeLayout(false);
			PassPanel.ResumeLayout(false);
			PassPanel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel TopPanel;
		private BadakMenuStrip MenuStrip;
		private Label PageInfo;
		private Label TitleLabel;
		private BadakSystemButton SystemButton;
		private PictureBox BookCanvas;
		private BadakContextMenuStrip ContextPopup;
		private ToolStripMenuItem ViewMenuItem;
		private ToolStripMenuItem ViewZoomMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem ViewFitMenuItem;
		private ToolStripMenuItem ViewLeftRightMenuItem;
		private ToolStripMenuItem ViewRightLeftMenuItem;
		private ToolStripMenuItem PageMenuItem;
		private ToolStripMenuItem PageAddFavMenuItem;
		private ToolStripMenuItem FileMenuItem;
		private ToolStripMenuItem FileOpenMenuItem;
		private ToolStripMenuItem FileCloseMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem FileExitMenuItem;
		private ToolStripMenuItem FileOpenLastMenuItem;
		private ToolStripMenuItem FileCopyImageMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem OpenPopupItem;
		private ToolStripMenuItem ClosePopupItem;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripMenuItem PagesPopupItem;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem QualityPopupItem;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem DeletePopupItem;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripMenuItem CopyImagePopupItem;
		private ToolStripSeparator toolStripSeparator8;
		private ToolStripMenuItem ExitPopupItem;
		private ToolStripMenuItem OptionPopupItem;
		private ToolStripMenuItem FileRefreshMenuItem;
		private ToolStripMenuItem FileOptionMenuItem;
		private ToolStripMenuItem ViewQualityMenuItem;
		private ToolStripMenuItem VwqLowMenuItem;
		private ToolStripMenuItem VwqDefaultMenuItem;
		private ToolStripMenuItem VwqBilinearMenuItem;
		private ToolStripMenuItem VwqBicubicMenuItem;
		private ToolStripMenuItem VwqHighMenuItem;
		private ToolStripSeparator toolStripSeparator9;
		private ToolStripMenuItem VwqHqBilinearMenuItem;
		private ToolStripMenuItem VwqHqBicubicMenuItem;
		private ToolStripMenuItem QualityLowPopupItem;
		private ToolStripMenuItem QualityDefaultPopupItem;
		private ToolStripMenuItem QualityBilinearPopupItem;
		private ToolStripMenuItem QualityBicubicPopupItem;
		private ToolStripMenuItem QualityHighPopupItem;
		private ToolStripSeparator toolStripSeparator10;
		private ToolStripMenuItem QualityHqBilinearPopupItem;
		private ToolStripMenuItem QualityHqBicubicPopupItem;
		private ToolStripMenuItem ControlPopItem;
		private ToolStripMenuItem CtrlPrevPopupItem;
		private ToolStripMenuItem CtrlNextPopupItem;
		private ToolStripMenuItem CtrlHomePopupItem;
		private ToolStripMenuItem CtrlEndPopupItem;
		private ToolStripSeparator toolStripSeparator11;
		private ToolStripMenuItem CtrlPrev10PopupItem;
		private ToolStripMenuItem CtrlNext10PopupItem;
		private ToolStripSeparator toolStripSeparator12;
		private ToolStripMenuItem CtrlPrevFilePopupItem;
		private ToolStripMenuItem CtrlNextFilePopupItem;
		private ToolStripMenuItem MaxCacheMenuItem;
		private ToolStripMenuItem PageSelectMenuItem;
		private ToolStripSeparator toolStripSeparator13;
		private ToolStripMenuItem FileDeleteMenuItem;
		private ToolStripSeparator toolStripSeparator14;
		private Label NotifyLabel;
		private ToolStripMenuItem FileOpenExternalMenuItem;
		private ToolStripMenuItem FileRenameMenuItem;
		private ToolStripMenuItem RenamePopupItem;
		private ToolStripMenuItem FileMoveMenuItem;
		private Panel PassPanel;
		private TextBox PassText;
		private Label PassLabel;
	}
}

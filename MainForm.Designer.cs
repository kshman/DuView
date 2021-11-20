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
			this.MaxCacheMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImagePictureBox = new System.Windows.Forms.PictureBox();
			this.ContextPopup = new Du.WinForms.BadakContextMenuStrip(this.components);
			this.OpenPopupItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.reservedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyImagePopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitPopupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PageInfoLabel = new System.Windows.Forms.Label();
			this.Notifier = new System.Windows.Forms.NotifyIcon(this.components);
			this.TopPanel = new System.Windows.Forms.Panel();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
			this.ContextPopup.SuspendLayout();
			this.TopPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewMenuItem,
            this.FavorityMenuItem,
            this.FileMenuItem,
            this.MaxCacheMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(3, 3);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(248, 24);
			this.MenuStrip.TabIndex = 2;
			this.MenuStrip.Text = "MenuStrip";
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
			this.ViewMenuItem.Name = "ViewMenuItem";
			this.ViewMenuItem.Size = new System.Drawing.Size(59, 20);
			this.ViewMenuItem.Text = "보기(&V)";
			// 
			// ViewZoomMenuItem
			// 
			this.ViewZoomMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewZoomMenuItem.Name = "ViewZoomMenuItem";
			this.ViewZoomMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
			this.ViewZoomMenuItem.Size = new System.Drawing.Size(209, 22);
			this.ViewZoomMenuItem.Text = "늘려보기(&Z)";
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
			this.ViewQualityMenuItem.Size = new System.Drawing.Size(209, 22);
			this.ViewQualityMenuItem.Text = "보기 품질";
			// 
			// VwqLowMenuItem
			// 
			this.VwqLowMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqLowMenuItem.Name = "VwqLowMenuItem";
			this.VwqLowMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqLowMenuItem.Text = "낮음";
			this.VwqLowMenuItem.Click += new System.EventHandler(this.VwqLowMenuItem_Click);
			// 
			// VwqDefaultMenuItem
			// 
			this.VwqDefaultMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqDefaultMenuItem.Name = "VwqDefaultMenuItem";
			this.VwqDefaultMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.VwqDefaultMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqDefaultMenuItem.Text = "기본";
			this.VwqDefaultMenuItem.Click += new System.EventHandler(this.VwqDefaultMenuItem_Click);
			// 
			// VwqBilinearMenuItem
			// 
			this.VwqBilinearMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqBilinearMenuItem.Name = "VwqBilinearMenuItem";
			this.VwqBilinearMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqBilinearMenuItem.Text = "이중 선형";
			this.VwqBilinearMenuItem.Click += new System.EventHandler(this.VwqBilinearMenuItem_Click);
			// 
			// VwqBicubicMenuItem
			// 
			this.VwqBicubicMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqBicubicMenuItem.Name = "VwqBicubicMenuItem";
			this.VwqBicubicMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqBicubicMenuItem.Text = "이중 큐빅";
			this.VwqBicubicMenuItem.Click += new System.EventHandler(this.VwqBicubicMenuItem_Click);
			// 
			// VwqHighMenuItem
			// 
			this.VwqHighMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHighMenuItem.Name = "VwqHighMenuItem";
			this.VwqHighMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqHighMenuItem.Text = "아주 높음";
			this.VwqHighMenuItem.Click += new System.EventHandler(this.VwqHighMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(191, 6);
			// 
			// VwqHqBilinearMenuItem
			// 
			this.VwqHqBilinearMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHqBilinearMenuItem.Name = "VwqHqBilinearMenuItem";
			this.VwqHqBilinearMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqHqBilinearMenuItem.Text = "높은 품질의 이중 선형";
			this.VwqHqBilinearMenuItem.Click += new System.EventHandler(this.VwqHqBilinearMenuItem_Click);
			// 
			// VwqHqBicubicMenuItem
			// 
			this.VwqHqBicubicMenuItem.ForeColor = System.Drawing.Color.White;
			this.VwqHqBicubicMenuItem.Name = "VwqHqBicubicMenuItem";
			this.VwqHqBicubicMenuItem.Size = new System.Drawing.Size(194, 22);
			this.VwqHqBicubicMenuItem.Text = "높은 품질의 이중 큐빅";
			this.VwqHqBicubicMenuItem.Click += new System.EventHandler(this.VwqHqBicubicMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
			// 
			// ViewFitMenuItem
			// 
			this.ViewFitMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewFitMenuItem.Name = "ViewFitMenuItem";
			this.ViewFitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.ViewFitMenuItem.Size = new System.Drawing.Size(209, 22);
			this.ViewFitMenuItem.Text = "가로 맞춤(&H)";
			this.ViewFitMenuItem.Click += new System.EventHandler(this.ViewFitMenuItem_Click);
			// 
			// ViewLeftRightMenuItem
			// 
			this.ViewLeftRightMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewLeftRightMenuItem.Name = "ViewLeftRightMenuItem";
			this.ViewLeftRightMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.ViewLeftRightMenuItem.Size = new System.Drawing.Size(209, 22);
			this.ViewLeftRightMenuItem.Text = "왼쪽 → 오른쪽(&L)";
			this.ViewLeftRightMenuItem.Click += new System.EventHandler(this.ViewLeftRightMenuItem_Click);
			// 
			// ViewRightLeftMenuItem
			// 
			this.ViewRightLeftMenuItem.ForeColor = System.Drawing.Color.White;
			this.ViewRightLeftMenuItem.Name = "ViewRightLeftMenuItem";
			this.ViewRightLeftMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.ViewRightLeftMenuItem.Size = new System.Drawing.Size(209, 22);
			this.ViewRightLeftMenuItem.Text = "오른쪽 → 왼쪽(&R)";
			this.ViewRightLeftMenuItem.Click += new System.EventHandler(this.ViewRightLeftMenuItem_Click);
			// 
			// FavorityMenuItem
			// 
			this.FavorityMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FavorityAddMenuItem});
			this.FavorityMenuItem.ForeColor = System.Drawing.Color.White;
			this.FavorityMenuItem.Name = "FavorityMenuItem";
			this.FavorityMenuItem.Size = new System.Drawing.Size(81, 20);
			this.FavorityMenuItem.Text = "즐겨찾기(&F)";
			// 
			// FavorityAddMenuItem
			// 
			this.FavorityAddMenuItem.ForeColor = System.Drawing.Color.White;
			this.FavorityAddMenuItem.Name = "FavorityAddMenuItem";
			this.FavorityAddMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.FavorityAddMenuItem.Size = new System.Drawing.Size(134, 22);
			this.FavorityAddMenuItem.Text = "추가(&A)";
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
			this.FileMenuItem.Size = new System.Drawing.Size(57, 20);
			this.FileMenuItem.Text = "파일(&F)";
			// 
			// FileOpenMenuItem
			// 
			this.FileOpenMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenMenuItem.Name = "FileOpenMenuItem";
			this.FileOpenMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.FileOpenMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileOpenMenuItem.Text = "열기(&O)";
			this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// FileOpenLastMenuItem
			// 
			this.FileOpenLastMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileOpenLastMenuItem.Name = "FileOpenLastMenuItem";
			this.FileOpenLastMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
			this.FileOpenLastMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileOpenLastMenuItem.Text = "마지막 책 열기(&L)";
			this.FileOpenLastMenuItem.Click += new System.EventHandler(this.FileOpenLastMenuItem_Click);
			// 
			// FileCloseMenuItem
			// 
			this.FileCloseMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCloseMenuItem.Name = "FileCloseMenuItem";
			this.FileCloseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.FileCloseMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileCloseMenuItem.Text = "닫기(&E)";
			this.FileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
			// 
			// FileCopyImageMenuItem
			// 
			this.FileCopyImageMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileCopyImageMenuItem.Name = "FileCopyImageMenuItem";
			this.FileCopyImageMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.FileCopyImageMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileCopyImageMenuItem.Text = "이미지 복사(&C)";
			this.FileCopyImageMenuItem.Click += new System.EventHandler(this.FileCopyImageMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(239, 6);
			// 
			// FileTestMenuItem
			// 
			this.FileTestMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileTestMenuItem.Name = "FileTestMenuItem";
			this.FileTestMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.FileTestMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileTestMenuItem.Text = "테스트용 입니다";
			this.FileTestMenuItem.Click += new System.EventHandler(this.FileTestMenuItem_Click);
			// 
			// FileExitMenuItem
			// 
			this.FileExitMenuItem.ForeColor = System.Drawing.Color.White;
			this.FileExitMenuItem.Name = "FileExitMenuItem";
			this.FileExitMenuItem.Size = new System.Drawing.Size(242, 22);
			this.FileExitMenuItem.Text = "끝내기(&X)";
			this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// MaxCacheMenuItem
			// 
			this.MaxCacheMenuItem.ForeColor = System.Drawing.Color.White;
			this.MaxCacheMenuItem.Name = "MaxCacheMenuItem";
			this.MaxCacheMenuItem.Size = new System.Drawing.Size(43, 20);
			this.MaxCacheMenuItem.Text = "캐시";
			this.MaxCacheMenuItem.Click += new System.EventHandler(this.MaxCacheMenuItem_Click);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ImagePictureBox.ContextMenuStrip = this.ContextPopup;
			this.ImagePictureBox.Location = new System.Drawing.Point(6, 75);
			this.ImagePictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.ImagePictureBox.Name = "ImagePictureBox";
			this.ImagePictureBox.Size = new System.Drawing.Size(788, 370);
			this.ImagePictureBox.TabIndex = 2;
			this.ImagePictureBox.TabStop = false;
			this.ImagePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseDown);
			this.ImagePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseMove);
			this.ImagePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseUp);
			this.ImagePictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ImagePictureBox_PreviewKeyDown);
			// 
			// ContextPopup
			// 
			this.ContextPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenPopupItem,
            this.ClosePopupItem,
            this.toolStripSeparator4,
            this.ControlPopItem,
            this.PagesPopupItem,
            this.toolStripSeparator5,
            this.QualityPopupItem,
            this.toolStripSeparator6,
            this.reservedToolStripMenuItem1,
            this.toolStripSeparator7,
            this.CopyImagePopupItem,
            this.toolStripSeparator8,
            this.ExitPopupItem});
			this.ContextPopup.Name = "ContextMenuStrip";
			this.ContextPopup.Size = new System.Drawing.Size(197, 232);
			// 
			// OpenPopupItem
			// 
			this.OpenPopupItem.ForeColor = System.Drawing.Color.White;
			this.OpenPopupItem.Name = "OpenPopupItem";
			this.OpenPopupItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.OpenPopupItem.Size = new System.Drawing.Size(196, 22);
			this.OpenPopupItem.Text = "열기(&O)";
			this.OpenPopupItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
			// 
			// ClosePopupItem
			// 
			this.ClosePopupItem.ForeColor = System.Drawing.Color.White;
			this.ClosePopupItem.Name = "ClosePopupItem";
			this.ClosePopupItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.ClosePopupItem.Size = new System.Drawing.Size(196, 22);
			this.ClosePopupItem.Text = "닫기(&E)";
			this.ClosePopupItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(193, 6);
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
			this.ControlPopItem.Size = new System.Drawing.Size(196, 22);
			this.ControlPopItem.Text = "보기 조작";
			// 
			// CtrlPrevPopupItem
			// 
			this.CtrlPrevPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrevPopupItem.Name = "CtrlPrevPopupItem";
			this.CtrlPrevPopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlPrevPopupItem.Text = "이전 페이지";
			this.CtrlPrevPopupItem.Click += new System.EventHandler(this.CtrlPrevPopupItem_Click);
			// 
			// CtrlNextPopupItem
			// 
			this.CtrlNextPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNextPopupItem.Name = "CtrlNextPopupItem";
			this.CtrlNextPopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlNextPopupItem.Text = "다음 페이지";
			this.CtrlNextPopupItem.Click += new System.EventHandler(this.CtrlNextPopupItem_Click);
			// 
			// CtrlHomePopupItem
			// 
			this.CtrlHomePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlHomePopupItem.Name = "CtrlHomePopupItem";
			this.CtrlHomePopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlHomePopupItem.Text = "첫번째 페이지";
			this.CtrlHomePopupItem.Click += new System.EventHandler(this.CtrlHomePopupItem_Click);
			// 
			// CtrlEndPopupItem
			// 
			this.CtrlEndPopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlEndPopupItem.Name = "CtrlEndPopupItem";
			this.CtrlEndPopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlEndPopupItem.Text = "마지박 페이지";
			this.CtrlEndPopupItem.Click += new System.EventHandler(this.CtrlEndPopupItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(173, 6);
			// 
			// CtrlPrev10PopupItem
			// 
			this.CtrlPrev10PopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrev10PopupItem.Name = "CtrlPrev10PopupItem";
			this.CtrlPrev10PopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlPrev10PopupItem.Text = "10페이지 이전으로";
			this.CtrlPrev10PopupItem.Click += new System.EventHandler(this.CtrlPrev10PopupItem_Click);
			// 
			// CtrlNext10PopupItem
			// 
			this.CtrlNext10PopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNext10PopupItem.Name = "CtrlNext10PopupItem";
			this.CtrlNext10PopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlNext10PopupItem.Text = "10페이지 다음으로";
			this.CtrlNext10PopupItem.Click += new System.EventHandler(this.CtrlNext10PopupItem_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(173, 6);
			// 
			// CtrlPrevFilePopupItem
			// 
			this.CtrlPrevFilePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlPrevFilePopupItem.Name = "CtrlPrevFilePopupItem";
			this.CtrlPrevFilePopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlPrevFilePopupItem.Text = "이전 파일/폴더";
			this.CtrlPrevFilePopupItem.Click += new System.EventHandler(this.CtrlPrevFilePopupItem_Click);
			// 
			// CtrlNextFilePopupItem
			// 
			this.CtrlNextFilePopupItem.ForeColor = System.Drawing.Color.White;
			this.CtrlNextFilePopupItem.Name = "CtrlNextFilePopupItem";
			this.CtrlNextFilePopupItem.Size = new System.Drawing.Size(176, 22);
			this.CtrlNextFilePopupItem.Text = "다음 파일/폴더";
			this.CtrlNextFilePopupItem.Click += new System.EventHandler(this.CtrlNextFilePopupItem_Click);
			// 
			// PagesPopupItem
			// 
			this.PagesPopupItem.ForeColor = System.Drawing.Color.White;
			this.PagesPopupItem.Name = "PagesPopupItem";
			this.PagesPopupItem.Size = new System.Drawing.Size(196, 22);
			this.PagesPopupItem.Text = "페이지 목록";
			this.PagesPopupItem.Click += new System.EventHandler(this.PagesPopupItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(193, 6);
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
			this.QualityPopupItem.Size = new System.Drawing.Size(196, 22);
			this.QualityPopupItem.Text = "보기 품질";
			// 
			// QualityLowPopupItem
			// 
			this.QualityLowPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityLowPopupItem.Name = "QualityLowPopupItem";
			this.QualityLowPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityLowPopupItem.Text = "낮음";
			this.QualityLowPopupItem.Click += new System.EventHandler(this.VwqLowMenuItem_Click);
			// 
			// QualityDefaultPopupItem
			// 
			this.QualityDefaultPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityDefaultPopupItem.Name = "QualityDefaultPopupItem";
			this.QualityDefaultPopupItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.QualityDefaultPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityDefaultPopupItem.Text = "기본";
			this.QualityDefaultPopupItem.Click += new System.EventHandler(this.VwqDefaultMenuItem_Click);
			// 
			// QualityBilinearPopupItem
			// 
			this.QualityBilinearPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityBilinearPopupItem.Name = "QualityBilinearPopupItem";
			this.QualityBilinearPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityBilinearPopupItem.Text = "이중 선형";
			this.QualityBilinearPopupItem.Click += new System.EventHandler(this.VwqBilinearMenuItem_Click);
			// 
			// QualityBicubicPopupItem
			// 
			this.QualityBicubicPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityBicubicPopupItem.Name = "QualityBicubicPopupItem";
			this.QualityBicubicPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityBicubicPopupItem.Text = "이중 큐빅";
			this.QualityBicubicPopupItem.Click += new System.EventHandler(this.VwqBicubicMenuItem_Click);
			// 
			// QualityHighPopupItem
			// 
			this.QualityHighPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHighPopupItem.Name = "QualityHighPopupItem";
			this.QualityHighPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityHighPopupItem.Text = "아주 높음";
			this.QualityHighPopupItem.Click += new System.EventHandler(this.VwqHighMenuItem_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(191, 6);
			// 
			// QualityHqBilinearPopupItem
			// 
			this.QualityHqBilinearPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHqBilinearPopupItem.Name = "QualityHqBilinearPopupItem";
			this.QualityHqBilinearPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityHqBilinearPopupItem.Text = "높은 품질의 이중 선형";
			this.QualityHqBilinearPopupItem.Click += new System.EventHandler(this.VwqHqBilinearMenuItem_Click);
			// 
			// QualityHqBicubicPopupItem
			// 
			this.QualityHqBicubicPopupItem.ForeColor = System.Drawing.Color.White;
			this.QualityHqBicubicPopupItem.Name = "QualityHqBicubicPopupItem";
			this.QualityHqBicubicPopupItem.Size = new System.Drawing.Size(194, 22);
			this.QualityHqBicubicPopupItem.Text = "높은 품질의 이중 큐빅";
			this.QualityHqBicubicPopupItem.Click += new System.EventHandler(this.VwqHqBicubicMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(193, 6);
			// 
			// reservedToolStripMenuItem1
			// 
			this.reservedToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
			this.reservedToolStripMenuItem1.Name = "reservedToolStripMenuItem1";
			this.reservedToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.reservedToolStripMenuItem1.Text = "(기능없음)";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(193, 6);
			// 
			// CopyImagePopupItem
			// 
			this.CopyImagePopupItem.ForeColor = System.Drawing.Color.White;
			this.CopyImagePopupItem.Name = "CopyImagePopupItem";
			this.CopyImagePopupItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.CopyImagePopupItem.Size = new System.Drawing.Size(196, 22);
			this.CopyImagePopupItem.Text = "이미지 복사(&C)";
			this.CopyImagePopupItem.Click += new System.EventHandler(this.FileCopyImageMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(193, 6);
			// 
			// ExitPopupItem
			// 
			this.ExitPopupItem.ForeColor = System.Drawing.Color.White;
			this.ExitPopupItem.Name = "ExitPopupItem";
			this.ExitPopupItem.Size = new System.Drawing.Size(196, 22);
			this.ExitPopupItem.Text = "끝내기(&X)";
			this.ExitPopupItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
			// 
			// PageInfoLabel
			// 
			this.PageInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PageInfoLabel.ForeColor = System.Drawing.Color.White;
			this.PageInfoLabel.Location = new System.Drawing.Point(688, 55);
			this.PageInfoLabel.Name = "PageInfoLabel";
			this.PageInfoLabel.Size = new System.Drawing.Size(106, 12);
			this.PageInfoLabel.TabIndex = 6;
			this.PageInfoLabel.Text = "페이지";
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
			// TopPanel
			// 
			this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.TopPanel.Controls.Add(this.TitleLabel);
			this.TopPanel.Controls.Add(this.SystemButton);
			this.TopPanel.Controls.Add(this.PageInfoLabel);
			this.TopPanel.Controls.Add(this.MenuStrip);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Margin = new System.Windows.Forms.Padding(4);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(800, 75);
			this.TopPanel.TabIndex = 2;
			this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// TitleLabel
			// 
			this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(12, 33);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(670, 37);
			this.TitleLabel.TabIndex = 9;
			this.TitleLabel.Text = "두뷰";
			this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
			this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
			this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(648, 0);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = true;
			this.SystemButton.ShowMinimize = true;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 8;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.ImagePictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.MenuStrip;
			this.MinimumSize = new System.Drawing.Size(300, 300);
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
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
			this.ContextPopup.ResumeLayout(false);
			this.TopPanel.ResumeLayout(false);
			this.TopPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Du.WinForms.BadakMenuStrip MenuStrip;
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
		private Du.WinForms.BadakContextMenuStrip ContextPopup;
		private System.Windows.Forms.ToolStripMenuItem OpenPopupItem;
		private System.Windows.Forms.ToolStripMenuItem ClosePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem PagesPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem QualityPopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem reservedToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem CopyImagePopupItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem ExitPopupItem;
		private System.Windows.Forms.ToolStripMenuItem FileTestMenuItem;
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
		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.ToolStripMenuItem MaxCacheMenuItem;
		private System.Windows.Forms.Label TitleLabel;
		private Du.WinForms.BadakSystemButton SystemButton;
	}
}


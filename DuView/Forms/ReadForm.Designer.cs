using System.ComponentModel;

namespace DuView.Forms;

partial class ReadForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        components = new Container();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(ReadForm));
        BookCanvas = new PictureBox();
        MainPopup = new ContextMenuStrip(components);
        FileMenuItem = new ToolStripMenuItem();
        FileOpenMenuItem = new ToolStripMenuItem();
        FileLastMenuItem = new ToolStripMenuItem();
        FilePrevMenuItem = new ToolStripMenuItem();
        FileNextMenuItem = new ToolStripMenuItem();
        toolStripSeparator7 = new ToolStripSeparator();
        FileCloseMenuItem = new ToolStripMenuItem();
        FileExternalMenuItem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        FileRenameMenuItem = new ToolStripMenuItem();
        FileMoveMenuItem = new ToolStripMenuItem();
        toolStripSeparator6 = new ToolStripSeparator();
        FileDeleteMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        QualityMenuItem = new ToolStripMenuItem();
        QtLowMenuItem = new ToolStripMenuItem();
        QtNormalMenuItem = new ToolStripMenuItem();
        QtBilinearMenuItem = new ToolStripMenuItem();
        QtBicubicMenuItem = new ToolStripMenuItem();
        QtHighMenuItem = new ToolStripMenuItem();
        QtHighBilinearMenuItem = new ToolStripMenuItem();
        QtHighBicubicMenuItem = new ToolStripMenuItem();
        ViewMenuItem = new ToolStripMenuItem();
        ViewCopyMenuItem = new ToolStripMenuItem();
        toolStripSeparator5 = new ToolStripSeparator();
        ViewZoomMenuItem = new ToolStripMenuItem();
        toolStripSeparator3 = new ToolStripSeparator();
        ViewFitMenuItem = new ToolStripMenuItem();
        ViewLeftMenuItem = new ToolStripMenuItem();
        ViewRightMenuItem = new ToolStripMenuItem();
        PageMenuItem = new ToolStripMenuItem();
        PageGotoMenuItem = new ToolStripMenuItem();
        toolStripSeparator8 = new ToolStripSeparator();
        PageHomeMenuItem = new ToolStripMenuItem();
        PageEndMenuItem = new ToolStripMenuItem();
        toolStripSeparator9 = new ToolStripSeparator();
        PagePrevTenMenuItem = new ToolStripMenuItem();
        PageNextTenMenuItem = new ToolStripMenuItem();
        toolStripSeparator10 = new ToolStripSeparator();
        SettingMenuItem = new ToolStripMenuItem();
        ExitMenuItem = new ToolStripMenuItem();
        NotifyLabel = new Label();
        ((ISupportInitialize)BookCanvas).BeginInit();
        MainPopup.SuspendLayout();
        SuspendLayout();
        // 
        // BookCanvas
        // 
        BookCanvas.ContextMenuStrip = MainPopup;
        BookCanvas.Location = new Point(10, 10);
        BookCanvas.Margin = new Padding(2);
        BookCanvas.Name = "BookCanvas";
        BookCanvas.Size = new Size(590, 85);
        BookCanvas.TabIndex = 0;
        BookCanvas.TabStop = false;
        BookCanvas.MouseDown += BookCanvas_MouseDown;
        BookCanvas.MouseMove += BookCanvas_MouseMove;
        BookCanvas.MouseUp += BookCanvas_MouseUp;
        // 
        // MainPopup
        // 
        MainPopup.BackColor = Color.FromArgb(255, 224, 192);
        MainPopup.ImageScalingSize = new Size(20, 20);
        MainPopup.Items.AddRange(new ToolStripItem[] { FileMenuItem, toolStripSeparator1, QualityMenuItem, ViewMenuItem, PageMenuItem, toolStripSeparator10, SettingMenuItem, ExitMenuItem });
        MainPopup.Name = "contextMenuStrip1";
        MainPopup.Size = new Size(141, 148);
        // 
        // FileMenuItem
        // 
        FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { FileOpenMenuItem, FileLastMenuItem, FilePrevMenuItem, FileNextMenuItem, toolStripSeparator7, FileCloseMenuItem, FileExternalMenuItem, toolStripSeparator2, FileRenameMenuItem, FileMoveMenuItem, toolStripSeparator6, FileDeleteMenuItem });
        FileMenuItem.Name = "FileMenuItem";
        FileMenuItem.Size = new Size(140, 22);
        FileMenuItem.Text = "파일(&F)";
        // 
        // FileOpenMenuItem
        // 
        FileOpenMenuItem.Name = "FileOpenMenuItem";
        FileOpenMenuItem.ShortcutKeys = Keys.F3;
        FileOpenMenuItem.Size = new Size(214, 22);
        FileOpenMenuItem.Text = "열기(&O)";
        FileOpenMenuItem.Click += FileOpenMenuItem_Click;
        // 
        // FileLastMenuItem
        // 
        FileLastMenuItem.Name = "FileLastMenuItem";
        FileLastMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
        FileLastMenuItem.Size = new Size(214, 22);
        FileLastMenuItem.Text = "다시 열기(&E)";
        FileLastMenuItem.Click += FileLastMenuItem_Click;
        // 
        // FilePrevMenuItem
        // 
        FilePrevMenuItem.Name = "FilePrevMenuItem";
        FilePrevMenuItem.ShortcutKeys = Keys.Control | Keys.P;
        FilePrevMenuItem.Size = new Size(214, 22);
        FilePrevMenuItem.Text = "이전 파일로(&P)";
        FilePrevMenuItem.Click += FilePrevMenuItem_Click;
        // 
        // FileNextMenuItem
        // 
        FileNextMenuItem.Name = "FileNextMenuItem";
        FileNextMenuItem.ShortcutKeys = Keys.Control | Keys.N;
        FileNextMenuItem.Size = new Size(214, 22);
        FileNextMenuItem.Text = "다음 파일로(&N)";
        FileNextMenuItem.Click += FileNextMenuItem_Click;
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new Size(211, 6);
        // 
        // FileCloseMenuItem
        // 
        FileCloseMenuItem.Name = "FileCloseMenuItem";
        FileCloseMenuItem.ShortcutKeys = Keys.F4;
        FileCloseMenuItem.Size = new Size(214, 22);
        FileCloseMenuItem.Text = "닫기(&C)";
        FileCloseMenuItem.Click += FileCloseMenuItem_Click;
        // 
        // FileExternalMenuItem
        // 
        FileExternalMenuItem.Name = "FileExternalMenuItem";
        FileExternalMenuItem.ShortcutKeys = Keys.F9;
        FileExternalMenuItem.Size = new Size(214, 22);
        FileExternalMenuItem.Text = "외부 프로그램으로(&X)";
        FileExternalMenuItem.Click += FileExternalMenuItem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(211, 6);
        // 
        // FileRenameMenuItem
        // 
        FileRenameMenuItem.Name = "FileRenameMenuItem";
        FileRenameMenuItem.ShortcutKeys = Keys.F2;
        FileRenameMenuItem.Size = new Size(214, 22);
        FileRenameMenuItem.Text = "이름 바꾸기(&R)";
        FileRenameMenuItem.Click += FileRenameMenuItem_Click;
        // 
        // FileMoveMenuItem
        // 
        FileMoveMenuItem.Name = "FileMoveMenuItem";
        FileMoveMenuItem.ShortcutKeys = Keys.F6;
        FileMoveMenuItem.Size = new Size(214, 22);
        FileMoveMenuItem.Text = "이동(&M)";
        FileMoveMenuItem.Click += FileMoveMenuItem_Click;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new Size(211, 6);
        // 
        // FileDeleteMenuItem
        // 
        FileDeleteMenuItem.Name = "FileDeleteMenuItem";
        FileDeleteMenuItem.ShortcutKeys = Keys.Delete;
        FileDeleteMenuItem.Size = new Size(214, 22);
        FileDeleteMenuItem.Text = "삭제(&D)";
        FileDeleteMenuItem.Click += FileDeleteMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(137, 6);
        // 
        // QualityMenuItem
        // 
        QualityMenuItem.DropDownItems.AddRange(new ToolStripItem[] { QtLowMenuItem, QtNormalMenuItem, QtBilinearMenuItem, QtBicubicMenuItem, QtHighMenuItem, QtHighBilinearMenuItem, QtHighBicubicMenuItem });
        QualityMenuItem.Name = "QualityMenuItem";
        QualityMenuItem.Size = new Size(140, 22);
        QualityMenuItem.Text = "품질(&Q)";
        // 
        // QtLowMenuItem
        // 
        QtLowMenuItem.Name = "QtLowMenuItem";
        QtLowMenuItem.Size = new Size(180, 22);
        QtLowMenuItem.Tag = ViewQuality.Low;
        QtLowMenuItem.Text = "낮음";
        QtLowMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtNormalMenuItem
        // 
        QtNormalMenuItem.Name = "QtNormalMenuItem";
        QtNormalMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
        QtNormalMenuItem.Size = new Size(180, 22);
        QtNormalMenuItem.Tag = ViewQuality.Default;
        QtNormalMenuItem.Text = "보통";
        QtNormalMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtBilinearMenuItem
        // 
        QtBilinearMenuItem.Name = "QtBilinearMenuItem";
        QtBilinearMenuItem.Size = new Size(180, 22);
        QtBilinearMenuItem.Tag = ViewQuality.Bilinear;
        QtBilinearMenuItem.Text = "이중선형";
        QtBilinearMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtBicubicMenuItem
        // 
        QtBicubicMenuItem.Name = "QtBicubicMenuItem";
        QtBicubicMenuItem.Size = new Size(180, 22);
        QtBicubicMenuItem.Tag = ViewQuality.Bicubic;
        QtBicubicMenuItem.Text = "바이큐빅";
        QtBicubicMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtHighMenuItem
        // 
        QtHighMenuItem.Name = "QtHighMenuItem";
        QtHighMenuItem.Size = new Size(180, 22);
        QtHighMenuItem.Tag = ViewQuality.High;
        QtHighMenuItem.Text = "높음";
        QtHighMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtHighBilinearMenuItem
        // 
        QtHighBilinearMenuItem.Name = "QtHighBilinearMenuItem";
        QtHighBilinearMenuItem.Size = new Size(180, 22);
        QtHighBilinearMenuItem.Tag = ViewQuality.HqBilinear;
        QtHighBilinearMenuItem.Text = "높은 이중선형";
        QtHighBilinearMenuItem.Click += QualityMenuItems_Click;
        // 
        // QtHighBicubicMenuItem
        // 
        QtHighBicubicMenuItem.Name = "QtHighBicubicMenuItem";
        QtHighBicubicMenuItem.Size = new Size(180, 22);
        QtHighBicubicMenuItem.Tag = ViewQuality.HqBicubic;
        QtHighBicubicMenuItem.Text = "높은 바이큐빅";
        QtHighBicubicMenuItem.Click += QualityMenuItems_Click;
        // 
        // ViewMenuItem
        // 
        ViewMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ViewCopyMenuItem, toolStripSeparator5, ViewZoomMenuItem, toolStripSeparator3, ViewFitMenuItem, ViewLeftMenuItem, ViewRightMenuItem });
        ViewMenuItem.Name = "ViewMenuItem";
        ViewMenuItem.Size = new Size(140, 22);
        ViewMenuItem.Text = "보기(&V)";
        // 
        // ViewCopyMenuItem
        // 
        ViewCopyMenuItem.Name = "ViewCopyMenuItem";
        ViewCopyMenuItem.ShortcutKeys = Keys.Control | Keys.C;
        ViewCopyMenuItem.Size = new Size(209, 22);
        ViewCopyMenuItem.Text = "클립보드로(&C)";
        ViewCopyMenuItem.Click += ViewCopyMenuItem_Click;
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new Size(206, 6);
        // 
        // ViewZoomMenuItem
        // 
        ViewZoomMenuItem.Name = "ViewZoomMenuItem";
        ViewZoomMenuItem.ShortcutKeys = Keys.Control | Keys.M;
        ViewZoomMenuItem.Size = new Size(209, 22);
        ViewZoomMenuItem.Text = "늘려 보기(&Z)";
        ViewZoomMenuItem.Click += ViewZoomMenuItem_Click;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new Size(206, 6);
        // 
        // ViewFitMenuItem
        // 
        ViewFitMenuItem.Name = "ViewFitMenuItem";
        ViewFitMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
        ViewFitMenuItem.Size = new Size(209, 22);
        ViewFitMenuItem.Tag = ViewMode.Fit;
        ViewFitMenuItem.Text = "크기 맞춤(&F)";
        ViewFitMenuItem.Click += ViewModeMenuItems_Click;
        // 
        // ViewLeftMenuItem
        // 
        ViewLeftMenuItem.Name = "ViewLeftMenuItem";
        ViewLeftMenuItem.ShortcutKeys = Keys.Control | Keys.D1;
        ViewLeftMenuItem.Size = new Size(209, 22);
        ViewLeftMenuItem.Tag = ViewMode.LeftToRight;
        ViewLeftMenuItem.Text = "왼쪽 → 오른쪽(&L)";
        ViewLeftMenuItem.Click += ViewModeMenuItems_Click;
        // 
        // ViewRightMenuItem
        // 
        ViewRightMenuItem.Name = "ViewRightMenuItem";
        ViewRightMenuItem.ShortcutKeys = Keys.Control | Keys.D2;
        ViewRightMenuItem.Size = new Size(209, 22);
        ViewRightMenuItem.Tag = ViewMode.RightToLeft;
        ViewRightMenuItem.Text = "오른쪽 → 왼쪽(&R)";
        ViewRightMenuItem.Click += ViewModeMenuItems_Click;
        // 
        // PageMenuItem
        // 
        PageMenuItem.DropDownItems.AddRange(new ToolStripItem[] { PageGotoMenuItem, toolStripSeparator8, PageHomeMenuItem, PageEndMenuItem, toolStripSeparator9, PagePrevTenMenuItem, PageNextTenMenuItem });
        PageMenuItem.Name = "PageMenuItem";
        PageMenuItem.Size = new Size(140, 22);
        PageMenuItem.Text = "쪽(&P)";
        // 
        // PageGotoMenuItem
        // 
        PageGotoMenuItem.Name = "PageGotoMenuItem";
        PageGotoMenuItem.Size = new Size(180, 22);
        PageGotoMenuItem.Tag = BookControl.Select;
        PageGotoMenuItem.Text = "이동(&G)";
        PageGotoMenuItem.Click += PageMenuItems_Click;
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        toolStripSeparator8.Size = new Size(177, 6);
        // 
        // PageHomeMenuItem
        // 
        PageHomeMenuItem.Name = "PageHomeMenuItem";
        PageHomeMenuItem.Size = new Size(180, 22);
        PageHomeMenuItem.Tag = BookControl.First;
        PageHomeMenuItem.Text = "맨 처음으로(&H)";
        PageHomeMenuItem.Click += PageMenuItems_Click;
        // 
        // PageEndMenuItem
        // 
        PageEndMenuItem.Name = "PageEndMenuItem";
        PageEndMenuItem.Size = new Size(180, 22);
        PageEndMenuItem.Tag = BookControl.Last;
        PageEndMenuItem.Text = "맨 끝으로(&E)";
        PageEndMenuItem.Click += PageMenuItems_Click;
        // 
        // toolStripSeparator9
        // 
        toolStripSeparator9.Name = "toolStripSeparator9";
        toolStripSeparator9.Size = new Size(177, 6);
        // 
        // PagePrevTenMenuItem
        // 
        PagePrevTenMenuItem.Name = "PagePrevTenMenuItem";
        PagePrevTenMenuItem.Size = new Size(180, 22);
        PagePrevTenMenuItem.Tag = BookControl.SeekPrevious10;
        PagePrevTenMenuItem.Text = "이전 10장(&P)";
        PagePrevTenMenuItem.Click += PageMenuItems_Click;
        // 
        // PageNextTenMenuItem
        // 
        PageNextTenMenuItem.Name = "PageNextTenMenuItem";
        PageNextTenMenuItem.Size = new Size(180, 22);
        PageNextTenMenuItem.Tag = BookControl.SeekNext10;
        PageNextTenMenuItem.Text = "다음 10장(&N)";
        PageNextTenMenuItem.Click += PageMenuItems_Click;
        // 
        // toolStripSeparator10
        // 
        toolStripSeparator10.Name = "toolStripSeparator10";
        toolStripSeparator10.Size = new Size(137, 6);
        // 
        // SettingMenuItem
        // 
        SettingMenuItem.Name = "SettingMenuItem";
        SettingMenuItem.ShortcutKeys = Keys.F12;
        SettingMenuItem.Size = new Size(140, 22);
        SettingMenuItem.Text = "설정(&S)";
        SettingMenuItem.Click += SettingMenuItem_Click;
        // 
        // ExitMenuItem
        // 
        ExitMenuItem.Name = "ExitMenuItem";
        ExitMenuItem.Size = new Size(140, 22);
        ExitMenuItem.Text = "끝내기(&X)";
        ExitMenuItem.Click += ExitMenuItem_Click;
        // 
        // NotifyLabel
        // 
        NotifyLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        NotifyLabel.BackColor = Color.FromArgb(0, 32, 32);
        NotifyLabel.BorderStyle = BorderStyle.Fixed3D;
        NotifyLabel.Font = new Font("맑은 고딕", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
        NotifyLabel.ForeColor = Color.White;
        NotifyLabel.Location = new Point(19, 120);
        NotifyLabel.Margin = new Padding(2, 0, 2, 0);
        NotifyLabel.Name = "NotifyLabel";
        NotifyLabel.Size = new Size(572, 45);
        NotifyLabel.TabIndex = 2;
        NotifyLabel.Text = "알림 메시지예요!";
        NotifyLabel.TextAlign = ContentAlignment.MiddleCenter;
        NotifyLabel.Visible = false;
        // 
        // ReadForm
        // 
        AllowDrop = true;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(60, 60, 60);
        ClientSize = new Size(608, 377);
        Controls.Add(NotifyLabel);
        Controls.Add(BookCanvas);
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        Margin = new Padding(2);
        MinimumSize = new Size(198, 197);
        Name = "ReadForm";
        Text = "ReadForm";
        FormClosing += ReadForm_FormClosing;
        FormClosed += ReadForm_FormClosed;
        Load += ReadForm_Load;
        ClientSizeChanged += ReadForm_ClientSizeChanged;
        DragDrop += ReadForm_DragDrop;
        DragEnter += ReadForm_DragEnter;
        KeyDown += ReadForm_KeyDown;
        Layout += ReadForm_Layout;
        ((ISupportInitialize)BookCanvas).EndInit();
        MainPopup.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;

    private System.Windows.Forms.ToolStripMenuItem FilePrevMenuItem;

    private System.Windows.Forms.ToolStripMenuItem FileNextMenuItem;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;

    private System.Windows.Forms.ToolStripMenuItem PageHomeMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PageEndMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PageNextTenMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PagePrevTenMenuItem;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

    private System.Windows.Forms.ToolStripMenuItem FileMoveMenuItem;

    private System.Windows.Forms.ToolStripMenuItem PageMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PageGotoMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileLastMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileExternalMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileCloseMenuItem;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    private System.Windows.Forms.ToolStripMenuItem ViewFitMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewRightMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewLeftMenuItem;

    private System.Windows.Forms.ToolStripMenuItem ViewZoomMenuItem;

    private System.Windows.Forms.Label NotifyLabel;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem FileDeleteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewCopyMenuItem;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem QualityMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtLowMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtNormalMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtBilinearMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtBicubicMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtHighMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtHighBilinearMenuItem;
    private System.Windows.Forms.ToolStripMenuItem QtHighBicubicMenuItem;

    private System.Windows.Forms.ToolStripMenuItem FileRenameMenuItem;

    private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;

    private System.Windows.Forms.PictureBox BookCanvas;
    private System.Windows.Forms.ContextMenuStrip MainPopup;

    #endregion
}
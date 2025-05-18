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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadForm));
        BookCanvas = new System.Windows.Forms.PictureBox();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        cmiFile = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileRename = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        cmiQt = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtLow = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtNormal = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtBilinear = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtBicubic = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtHigh = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtHighBilinear = new System.Windows.Forms.ToolStripMenuItem();
        cmiQtHighBicubic = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        cmiFileDelete = new System.Windows.Forms.ToolStripMenuItem();
        cmiView = new System.Windows.Forms.ToolStripMenuItem();
        cmiViewCopy = new System.Windows.Forms.ToolStripMenuItem();
        NotifyLabel = new System.Windows.Forms.Label();
        cmiViewZoom = new System.Windows.Forms.ToolStripMenuItem();
        cmiViewByWidth = new System.Windows.Forms.ToolStripMenuItem();
        cmiViewByHeight = new System.Windows.Forms.ToolStripMenuItem();
        cmiViewFromLeft = new System.Windows.Forms.ToolStripMenuItem();
        cmiViewFromRight = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        cmiPage = new System.Windows.Forms.ToolStripMenuItem();
        cmiPageGoto = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileLast = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileExternal = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileClose = new System.Windows.Forms.ToolStripMenuItem();
        cmiFileMove = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        cmiPageHome = new System.Windows.Forms.ToolStripMenuItem();
        cmiPageEnd = new System.Windows.Forms.ToolStripMenuItem();
        cmiPageNextTen = new System.Windows.Forms.ToolStripMenuItem();
        cmiPagePrevTen = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
        toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        cmiFileNext = new System.Windows.Forms.ToolStripMenuItem();
        cmiFilePrev = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        cmiSetting = new System.Windows.Forms.ToolStripMenuItem();
        cmiExit = new System.Windows.Forms.ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)BookCanvas).BeginInit();
        contextMenuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // BookCanvas
        // 
        BookCanvas.ContextMenuStrip = contextMenuStrip1;
        BookCanvas.Location = new System.Drawing.Point(13, 13);
        BookCanvas.Name = "BookCanvas";
        BookCanvas.Size = new System.Drawing.Size(758, 113);
        BookCanvas.TabIndex = 0;
        BookCanvas.TabStop = false;
        BookCanvas.MouseDown += BookCanvas_MouseDown;
        BookCanvas.MouseMove += BookCanvas_MouseMove;
        BookCanvas.MouseUp += BookCanvas_MouseUp;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { cmiFile, toolStripSeparator1, cmiQt, cmiView, cmiPage, toolStripSeparator10, cmiSetting, cmiExit });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(143, 160);
        // 
        // cmiFile
        // 
        cmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cmiFileOpen, cmiFileLast, cmiFilePrev, cmiFileNext, toolStripSeparator7, cmiFileClose, cmiFileExternal, toolStripSeparator2, cmiFileRename, cmiFileMove, toolStripSeparator6, cmiFileDelete });
        cmiFile.Name = "cmiFile";
        cmiFile.Size = new System.Drawing.Size(142, 24);
        cmiFile.Text = "파일(&F)";
        // 
        // cmiFileOpen
        // 
        cmiFileOpen.Name = "cmiFileOpen";
        cmiFileOpen.Size = new System.Drawing.Size(236, 26);
        cmiFileOpen.Text = "열기(&O)";
        cmiFileOpen.Click += cmiFileOpen_Click;
        // 
        // cmiFileRename
        // 
        cmiFileRename.Name = "cmiFileRename";
        cmiFileRename.Size = new System.Drawing.Size(236, 26);
        cmiFileRename.Text = "이름 바꾸기(&R)";
        cmiFileRename.Click += cmiFileRename_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
        // 
        // cmiQt
        // 
        cmiQt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cmiQtLow, cmiQtNormal, cmiQtBilinear, cmiQtBicubic, cmiQtHigh, cmiQtHighBilinear, cmiQtHighBicubic });
        cmiQt.Name = "cmiQt";
        cmiQt.Size = new System.Drawing.Size(142, 24);
        cmiQt.Text = "품질(&Q)";
        // 
        // cmiQtLow
        // 
        cmiQtLow.Name = "cmiQtLow";
        cmiQtLow.Size = new System.Drawing.Size(224, 26);
        cmiQtLow.Text = "낮음";
        cmiQtLow.Click += cmiQtLow_Click;
        // 
        // cmiQtNormal
        // 
        cmiQtNormal.Name = "cmiQtNormal";
        cmiQtNormal.Size = new System.Drawing.Size(224, 26);
        cmiQtNormal.Text = "보통";
        cmiQtNormal.Click += cmiQtNormal_Click;
        // 
        // cmiQtBilinear
        // 
        cmiQtBilinear.Name = "cmiQtBilinear";
        cmiQtBilinear.Size = new System.Drawing.Size(224, 26);
        cmiQtBilinear.Text = "이중선형";
        cmiQtBilinear.Click += cmiQtBilinear_Click;
        // 
        // cmiQtBicubic
        // 
        cmiQtBicubic.Name = "cmiQtBicubic";
        cmiQtBicubic.Size = new System.Drawing.Size(224, 26);
        cmiQtBicubic.Text = "바이큐빅";
        cmiQtBicubic.Click += cmiQtBicubic_Click;
        // 
        // cmiQtHigh
        // 
        cmiQtHigh.Name = "cmiQtHigh";
        cmiQtHigh.Size = new System.Drawing.Size(224, 26);
        cmiQtHigh.Text = "높음";
        cmiQtHigh.Click += cmiQtHigh_Click;
        // 
        // cmiQtHighBilinear
        // 
        cmiQtHighBilinear.Name = "cmiQtHighBilinear";
        cmiQtHighBilinear.Size = new System.Drawing.Size(224, 26);
        cmiQtHighBilinear.Text = "높은 이중선형";
        cmiQtHighBilinear.Click += cmiQtHighBilinear_Click;
        // 
        // cmiQtHighBicubic
        // 
        cmiQtHighBicubic.Name = "cmiQtHighBicubic";
        cmiQtHighBicubic.Size = new System.Drawing.Size(224, 26);
        cmiQtHighBicubic.Text = "높은 바이큐빅";
        cmiQtHighBicubic.Click += cmiQtHighBicubic_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new System.Drawing.Size(233, 6);
        // 
        // cmiFileDelete
        // 
        cmiFileDelete.Name = "cmiFileDelete";
        cmiFileDelete.Size = new System.Drawing.Size(236, 26);
        cmiFileDelete.Text = "삭제(&D)";
        cmiFileDelete.Click += cmiFileDelete_Click;
        // 
        // cmiView
        // 
        cmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cmiViewCopy, toolStripSeparator5, cmiViewZoom, toolStripSeparator3, cmiViewByWidth, cmiViewByHeight, toolStripSeparator4, cmiViewFromLeft, cmiViewFromRight });
        cmiView.Name = "cmiView";
        cmiView.Size = new System.Drawing.Size(142, 24);
        cmiView.Text = "보기(&V)";
        // 
        // cmiViewCopy
        // 
        cmiViewCopy.Name = "cmiViewCopy";
        cmiViewCopy.Size = new System.Drawing.Size(224, 26);
        cmiViewCopy.Text = "클립보드로(&C)";
        cmiViewCopy.Click += cmiViewCopy_Click;
        // 
        // NotifyLabel
        // 
        NotifyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        NotifyLabel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)0)), ((int)((byte)32)), ((int)((byte)32)));
        NotifyLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        NotifyLabel.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)129));
        NotifyLabel.ForeColor = System.Drawing.Color.White;
        NotifyLabel.Location = new System.Drawing.Point(25, 160);
        NotifyLabel.Name = "NotifyLabel";
        NotifyLabel.Size = new System.Drawing.Size(735, 60);
        NotifyLabel.TabIndex = 2;
        NotifyLabel.Text = "알림 메시지예요!";
        NotifyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        NotifyLabel.Visible = false;
        // 
        // cmiViewZoom
        // 
        cmiViewZoom.Name = "cmiViewZoom";
        cmiViewZoom.Size = new System.Drawing.Size(224, 26);
        cmiViewZoom.Text = "늘려 보기(&Z)";
        cmiViewZoom.Click += cmiViewZoom_Click;
        // 
        // cmiViewByWidth
        // 
        cmiViewByWidth.Name = "cmiViewByWidth";
        cmiViewByWidth.Size = new System.Drawing.Size(224, 26);
        cmiViewByWidth.Text = "가로 맞춤(&W)";
        cmiViewByWidth.Click += cmiViewByWidth_Click;
        // 
        // cmiViewByHeight
        // 
        cmiViewByHeight.Name = "cmiViewByHeight";
        cmiViewByHeight.Size = new System.Drawing.Size(224, 26);
        cmiViewByHeight.Text = "세로 맞춤(&H)";
        cmiViewByHeight.Click += cmiViewByHeight_Click;
        // 
        // cmiViewFromLeft
        // 
        cmiViewFromLeft.Name = "cmiViewFromLeft";
        cmiViewFromLeft.Size = new System.Drawing.Size(224, 26);
        cmiViewFromLeft.Text = "왼쪽 → 오른쪽(&L)";
        cmiViewFromLeft.Click += cmiViewFromLeft_Click;
        // 
        // cmiViewFromRight
        // 
        cmiViewFromRight.Name = "cmiViewFromRight";
        cmiViewFromRight.Size = new System.Drawing.Size(224, 26);
        cmiViewFromRight.Text = "오른쪽 → 왼쪽(&R)";
        cmiViewFromRight.Click += cmiViewFromRight_Click;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new System.Drawing.Size(221, 6);
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new System.Drawing.Size(221, 6);
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new System.Drawing.Size(221, 6);
        // 
        // cmiPage
        // 
        cmiPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cmiPageGoto, toolStripSeparator8, cmiPageHome, cmiPageEnd, toolStripSeparator9, cmiPagePrevTen, cmiPageNextTen });
        cmiPage.Name = "cmiPage";
        cmiPage.Size = new System.Drawing.Size(142, 24);
        cmiPage.Text = "쪽(&P)";
        // 
        // cmiPageGoto
        // 
        cmiPageGoto.Name = "cmiPageGoto";
        cmiPageGoto.Size = new System.Drawing.Size(224, 26);
        cmiPageGoto.Text = "이동(&G)";
        cmiPageGoto.Click += cmiPageGoto_Click;
        // 
        // cmiFileLast
        // 
        cmiFileLast.Name = "cmiFileLast";
        cmiFileLast.Size = new System.Drawing.Size(236, 26);
        cmiFileLast.Text = "다시 열기(&E)";
        cmiFileLast.Click += cmiFileLast_Click;
        // 
        // cmiFileExternal
        // 
        cmiFileExternal.Name = "cmiFileExternal";
        cmiFileExternal.Size = new System.Drawing.Size(236, 26);
        cmiFileExternal.Text = "외부 프로그램으로(&X)";
        cmiFileExternal.Click += cmiFileExternal_Click;
        // 
        // cmiFileClose
        // 
        cmiFileClose.Name = "cmiFileClose";
        cmiFileClose.Size = new System.Drawing.Size(236, 26);
        cmiFileClose.Text = "닫기(&C)";
        cmiFileClose.Click += cmiFileClose_Click;
        // 
        // cmiFileMove
        // 
        cmiFileMove.Name = "cmiFileMove";
        cmiFileMove.Size = new System.Drawing.Size(236, 26);
        cmiFileMove.Text = "이동(&M)";
        cmiFileMove.Click += cmiFileMove_Click;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new System.Drawing.Size(233, 6);
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new System.Drawing.Size(233, 6);
        // 
        // cmiPageHome
        // 
        cmiPageHome.Name = "cmiPageHome";
        cmiPageHome.Size = new System.Drawing.Size(224, 26);
        cmiPageHome.Text = "맨 처음으로(&H)";
        cmiPageHome.Click += cmiPageHome_Click;
        // 
        // cmiPageEnd
        // 
        cmiPageEnd.Name = "cmiPageEnd";
        cmiPageEnd.Size = new System.Drawing.Size(224, 26);
        cmiPageEnd.Text = "맨 끝으로(&E)";
        cmiPageEnd.Click += cmiPageEnd_Click;
        // 
        // cmiPageNextTen
        // 
        cmiPageNextTen.Name = "cmiPageNextTen";
        cmiPageNextTen.Size = new System.Drawing.Size(224, 26);
        cmiPageNextTen.Text = "다음 10장(&N)";
        cmiPageNextTen.Click += cmiPageNextTen_Click;
        // 
        // cmiPagePrevTen
        // 
        cmiPagePrevTen.Name = "cmiPagePrevTen";
        cmiPagePrevTen.Size = new System.Drawing.Size(224, 26);
        cmiPagePrevTen.Text = "이전 10장(&P)";
        cmiPagePrevTen.Click += cmiPagePrevTen_Click;
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        toolStripSeparator8.Size = new System.Drawing.Size(221, 6);
        // 
        // toolStripSeparator9
        // 
        toolStripSeparator9.Name = "toolStripSeparator9";
        toolStripSeparator9.Size = new System.Drawing.Size(221, 6);
        // 
        // cmiFileNext
        // 
        cmiFileNext.Name = "cmiFileNext";
        cmiFileNext.Size = new System.Drawing.Size(236, 26);
        cmiFileNext.Text = "다음 파일로(&N)";
        cmiFileNext.Click += cmiFileNext_Click;
        // 
        // cmiFilePrev
        // 
        cmiFilePrev.Name = "cmiFilePrev";
        cmiFilePrev.Size = new System.Drawing.Size(236, 26);
        cmiFilePrev.Text = "이전 파일로(&P)";
        cmiFilePrev.Click += cmiFilePrev_Click;
        // 
        // toolStripSeparator10
        // 
        toolStripSeparator10.Name = "toolStripSeparator10";
        toolStripSeparator10.Size = new System.Drawing.Size(139, 6);
        // 
        // cmiSetting
        // 
        cmiSetting.Name = "cmiSetting";
        cmiSetting.Size = new System.Drawing.Size(142, 24);
        cmiSetting.Text = "설정(&S)";
        cmiSetting.Click += cmiSetting_Click;
        // 
        // cmiExit
        // 
        cmiExit.Name = "cmiExit";
        cmiExit.Size = new System.Drawing.Size(142, 24);
        cmiExit.Text = "끝내기(&X)";
        cmiExit.Click += cmiExit_Click;
        // 
        // ReadForm
        // 
        AllowDrop = true;
        AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(((int)((byte)60)), ((int)((byte)60)), ((int)((byte)60)));
        ClientSize = new System.Drawing.Size(782, 503);
        Controls.Add(NotifyLabel);
        Controls.Add(BookCanvas);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        KeyPreview = true;
        MinimumSize = new System.Drawing.Size(250, 250);
        Text = "ReadForm";
        FormClosing += ReadForm_FormClosing;
        FormClosed += ReadForm_FormClosed;
        Load += ReadForm_Load;
        ClientSizeChanged += ReadForm_ClientSizeChanged;
        DragDrop += ReadForm_DragDrop;
        DragEnter += ReadForm_DragEnter;
        KeyDown += ReadForm_KeyDown;
        Layout += ReadForm_Layout;
        ((System.ComponentModel.ISupportInitialize)BookCanvas).EndInit();
        contextMenuStrip1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripMenuItem cmiSetting;
    private System.Windows.Forms.ToolStripMenuItem cmiExit;

    private System.Windows.Forms.ToolStripMenuItem cmiFilePrev;

    private System.Windows.Forms.ToolStripMenuItem cmiFileNext;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;

    private System.Windows.Forms.ToolStripMenuItem cmiPageHome;
    private System.Windows.Forms.ToolStripMenuItem cmiPageEnd;
    private System.Windows.Forms.ToolStripMenuItem cmiPageNextTen;
    private System.Windows.Forms.ToolStripMenuItem cmiPagePrevTen;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

    private System.Windows.Forms.ToolStripMenuItem cmiFileMove;

    private System.Windows.Forms.ToolStripMenuItem cmiPage;
    private System.Windows.Forms.ToolStripMenuItem cmiPageGoto;
    private System.Windows.Forms.ToolStripMenuItem cmiFileLast;
    private System.Windows.Forms.ToolStripMenuItem cmiFileExternal;
    private System.Windows.Forms.ToolStripMenuItem cmiFileClose;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    private System.Windows.Forms.ToolStripMenuItem cmiViewFromRight;

    private System.Windows.Forms.ToolStripMenuItem cmiViewByWidth;
    private System.Windows.Forms.ToolStripMenuItem cmiViewByHeight;
    private System.Windows.Forms.ToolStripMenuItem cmiViewFromLeft;

    private System.Windows.Forms.ToolStripMenuItem cmiViewZoom;

    private System.Windows.Forms.Label NotifyLabel;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem cmiFileDelete;
    private System.Windows.Forms.ToolStripMenuItem cmiView;
    private System.Windows.Forms.ToolStripMenuItem cmiViewCopy;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem cmiQt;
    private System.Windows.Forms.ToolStripMenuItem cmiQtLow;
    private System.Windows.Forms.ToolStripMenuItem cmiQtNormal;
    private System.Windows.Forms.ToolStripMenuItem cmiQtBilinear;
    private System.Windows.Forms.ToolStripMenuItem cmiQtBicubic;
    private System.Windows.Forms.ToolStripMenuItem cmiQtHigh;
    private System.Windows.Forms.ToolStripMenuItem cmiQtHighBilinear;
    private System.Windows.Forms.ToolStripMenuItem cmiQtHighBicubic;

    private System.Windows.Forms.ToolStripMenuItem cmiFileRename;

    private System.Windows.Forms.ToolStripMenuItem cmiFile;
    private System.Windows.Forms.ToolStripMenuItem cmiFileOpen;

    private System.Windows.Forms.PictureBox BookCanvas;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    #endregion
}
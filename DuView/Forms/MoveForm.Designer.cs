using DuView.Properties;

namespace DuView.Forms;

partial class MoveForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveForm));
        DoOkButton = new Button();
        DoCancelButton = new Button();
        BrowseButton = new Button();
        TitleLabel = new Label();
        DestText = new TextBox();
        MoveImageList = new ImageList(components);
        MoveList = new ReorderListView();
        NumberColumn = new ColumnHeader();
        AliasColumn = new ColumnHeader();
        PathColumn = new ColumnHeader();
        MoveMenu = new ContextMenuStrip(components);
        MoveAddMenuItem = new ToolStripMenuItem();
        MoveChangeMenuItem = new ToolStripMenuItem();
        MoveAliasMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        MoveDeleteMenuItem = new ToolStripMenuItem();
        MoveMenu.SuspendLayout();
        SuspendLayout();
        // 
        // DoOkButton
        // 
        DoOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        DoOkButton.DialogResult = DialogResult.OK;
        DoOkButton.FlatStyle = FlatStyle.Flat;
        DoOkButton.ForeColor = Color.White;
        DoOkButton.Location = new Point(212, 508);
        DoOkButton.Margin = new Padding(4, 3, 4, 3);
        DoOkButton.Name = "DoOkButton";
        DoOkButton.Size = new Size(130, 30);
        DoOkButton.TabIndex = 0;
        DoOkButton.Text = "선택";
        DoOkButton.UseVisualStyleBackColor = true;
        // 
        // DoCancelButton
        // 
        DoCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        DoCancelButton.DialogResult = DialogResult.Cancel;
        DoCancelButton.FlatStyle = FlatStyle.Flat;
        DoCancelButton.ForeColor = Color.White;
        DoCancelButton.Location = new Point(350, 508);
        DoCancelButton.Margin = new Padding(4, 3, 4, 3);
        DoCancelButton.Name = "DoCancelButton";
        DoCancelButton.Size = new Size(130, 30);
        DoCancelButton.TabIndex = 1;
        DoCancelButton.Text = "취소";
        DoCancelButton.UseVisualStyleBackColor = true;
        // 
        // BrowseButton
        // 
        BrowseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        BrowseButton.FlatStyle = FlatStyle.Flat;
        BrowseButton.ForeColor = Color.White;
        BrowseButton.Location = new Point(13, 508);
        BrowseButton.Margin = new Padding(4, 3, 4, 3);
        BrowseButton.Name = "BrowseButton";
        BrowseButton.Size = new Size(173, 30);
        BrowseButton.TabIndex = 2;
        BrowseButton.Text = "찾아보기";
        BrowseButton.UseVisualStyleBackColor = true;
        BrowseButton.Click += BrowseButton_Click;
        // 
        // TitleLabel
        // 
        TitleLabel.AutoSize = true;
        TitleLabel.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
        TitleLabel.ForeColor = Color.White;
        TitleLabel.Location = new Point(5, 5);
        TitleLabel.Name = "TitleLabel";
        TitleLabel.Size = new Size(166, 21);
        TitleLabel.TabIndex = 3;
        TitleLabel.Text = "이동할 곳을 고르세요";
        // 
        // DestText
        // 
        DestText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        DestText.Location = new Point(5, 479);
        DestText.Name = "DestText";
        DestText.Size = new Size(488, 23);
        DestText.TabIndex = 4;
        DestText.Enter += DestText_Enter;
        // 
        // MoveImageList
        // 
        MoveImageList.ColorDepth = ColorDepth.Depth16Bit;
        MoveImageList.ImageStream = (ImageListStreamer)resources.GetObject("MoveImageList.ImageStream");
        MoveImageList.TransparentColor = Color.Transparent;
        MoveImageList.Images.SetKeyName(0, "folder-blue.png");
        // 
        // MoveList
        // 
        MoveList.AllowItemReorder = true;
        MoveList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        MoveList.BorderStyle = BorderStyle.FixedSingle;
        MoveList.Columns.AddRange(new ColumnHeader[] { NumberColumn, AliasColumn, PathColumn });
        MoveList.ContextMenuStrip = MoveMenu;
        MoveList.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
        MoveList.FullRowSelect = true;
        MoveList.GridLines = true;
        MoveList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        MoveList.LargeImageList = MoveImageList;
        MoveList.Location = new Point(5, 35);
        MoveList.Margin = new Padding(4, 3, 4, 3);
        MoveList.MultiSelect = false;
        MoveList.Name = "MoveList";
        MoveList.Size = new Size(488, 438);
        MoveList.SmallImageList = MoveImageList;
        MoveList.TabIndex = 5;
        MoveList.UseCompatibleStateImageBehavior = false;
        MoveList.View = View.Details;
        MoveList.ItemReordered += MoveList_ItemReordered;
        MoveList.SubItemBeginEdit += MoveList_SubItemBeginEdit;
        MoveList.SubItemEndEdit += MoveList_SubItemEndEdit;
        MoveList.SelectedIndexChanged += MoveList_SelectedIndexChanged;
        MoveList.DoubleClick += MoveList_DoubleClick;
        // 
        // NumberColumn
        // 
        NumberColumn.Text = "번호";
        NumberColumn.Width = 80;
        // 
        // AliasColumn
        // 
        AliasColumn.Text = "별명";
        AliasColumn.Width = 220;
        // 
        // PathColumn
        // 
        PathColumn.Text = "경로";
        PathColumn.Width = 150;
        // 
        // MoveMenu
        // 
        MoveMenu.Items.AddRange(new ToolStripItem[] { MoveAddMenuItem, MoveChangeMenuItem, MoveAliasMenuItem, toolStripSeparator1, MoveDeleteMenuItem });
        MoveMenu.Name = "MoveMenu";
        MoveMenu.Size = new Size(181, 120);
        MoveMenu.Opening += MoveMenu_Opening;
        // 
        // MoveAddMenuItem
        // 
        MoveAddMenuItem.Name = "MoveAddMenuItem";
        MoveAddMenuItem.Size = new Size(180, 22);
        MoveAddMenuItem.Text = "위치 추가(&A)";
        MoveAddMenuItem.Click += MoveAddMenuItem_Click;
        // 
        // MoveChangeMenuItem
        // 
        MoveChangeMenuItem.Name = "MoveChangeMenuItem";
        MoveChangeMenuItem.Size = new Size(180, 22);
        MoveChangeMenuItem.Text = "다른 위치로(&C)";
        MoveChangeMenuItem.Click += MoveChangeMenuItem_Click;
        // 
        // MoveAliasMenuItem
        // 
        MoveAliasMenuItem.Name = "MoveAliasMenuItem";
        MoveAliasMenuItem.Size = new Size(180, 22);
        MoveAliasMenuItem.Text = "별명 바꾸기(&S)";
        MoveAliasMenuItem.Click += MoveAliasMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(151, 6);
        // 
        // MoveDeleteMenuItem
        // 
        MoveDeleteMenuItem.Name = "MoveDeleteMenuItem";
        MoveDeleteMenuItem.Size = new Size(180, 22);
        MoveDeleteMenuItem.Text = "위치 삭제(&D)";
        MoveDeleteMenuItem.Click += MoveDeleteMenuItem_Click;
        // 
        // MoveForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(60, 60, 60);
        ClientSize = new Size(500, 550);
        Controls.Add(MoveList);
        Controls.Add(DestText);
        Controls.Add(TitleLabel);
        Controls.Add(BrowseButton);
        Controls.Add(DoCancelButton);
        Controls.Add(DoOkButton);
        FormBorderStyle = FormBorderStyle.None;
        KeyPreview = true;
        Margin = new Padding(4, 3, 4, 3);
        Name = "MoveForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "이동";
        FormClosing += MoveForm_FormClosing;
        Load += MoveForm_Load;
        KeyDown += MoveForm_KeyDown;
        MoveMenu.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button DoOkButton;
    private Button DoCancelButton;
    private Button BrowseButton;
    private Label TitleLabel;
    private TextBox DestText;
    private ImageList MoveImageList;
    private ReorderListView MoveList;
    private ColumnHeader NumberColumn;
    private ColumnHeader AliasColumn;
    private ColumnHeader PathColumn;
    private ContextMenuStrip MoveMenu;
    private ToolStripMenuItem MoveAddMenuItem;
    private ToolStripMenuItem MoveChangeMenuItem;
    private ToolStripMenuItem MoveAliasMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem MoveDeleteMenuItem;
}
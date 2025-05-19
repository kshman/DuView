namespace DuView.Forms;

partial class PageForm
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
        DoOkButton = new Button();
        DoCancelButton = new Button();
        PageInfoLabel = new Label();
        PageList = new ListView();
        FileNameColumn = new ColumnHeader();
        DateColumn = new ColumnHeader();
        SizeColumn = new ColumnHeader();
        SuspendLayout();
        // 
        // DoOkButton
        // 
        DoOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        DoOkButton.DialogResult = DialogResult.OK;
        DoOkButton.FlatStyle = FlatStyle.Flat;
        DoOkButton.ForeColor = Color.White;
        DoOkButton.Location = new Point(202, 454);
        DoOkButton.Name = "DoOkButton";
        DoOkButton.Size = new Size(220, 45);
        DoOkButton.TabIndex = 0;
        DoOkButton.Text = "선택";
        DoOkButton.UseVisualStyleBackColor = true;
        DoOkButton.Click += DoOkButton_Click;
        // 
        // DoCancelButton
        // 
        DoCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        DoCancelButton.DialogResult = DialogResult.Cancel;
        DoCancelButton.FlatStyle = FlatStyle.Flat;
        DoCancelButton.ForeColor = Color.White;
        DoCancelButton.Location = new Point(12, 454);
        DoCancelButton.Name = "DoCancelButton";
        DoCancelButton.Size = new Size(70, 45);
        DoCancelButton.TabIndex = 1;
        DoCancelButton.Text = "취소";
        DoCancelButton.UseVisualStyleBackColor = true;
        // 
        // PageInfoLabel
        // 
        PageInfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        PageInfoLabel.Font = new Font("맑은 고딕", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 129);
        PageInfoLabel.ForeColor = Color.White;
        PageInfoLabel.Location = new Point(12, 9);
        PageInfoLabel.Name = "PageInfoLabel";
        PageInfoLabel.Size = new Size(410, 27);
        PageInfoLabel.TabIndex = 2;
        PageInfoLabel.Text = "페이지";
        // 
        // PageList
        // 
        PageList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        PageList.AutoArrange = false;
        PageList.BorderStyle = BorderStyle.FixedSingle;
        PageList.Columns.AddRange(new ColumnHeader[] { FileNameColumn, DateColumn, SizeColumn });
        PageList.FullRowSelect = true;
        PageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        PageList.Location = new Point(12, 39);
        PageList.MultiSelect = false;
        PageList.Name = "PageList";
        PageList.Size = new Size(410, 409);
        PageList.TabIndex = 3;
        PageList.UseCompatibleStateImageBehavior = false;
        PageList.View = View.Details;
        PageList.MouseDoubleClick += PageList_MouseDoubleClick;
        // 
        // FileNameColumn
        // 
        FileNameColumn.Text = "파일 이름";
        FileNameColumn.Width = 200;
        // 
        // DateColumn
        // 
        DateColumn.Text = "날짜";
        DateColumn.Width = 100;
        // 
        // SizeColumn
        // 
        SizeColumn.Text = "크기";
        SizeColumn.Width = 80;
        // 
        // PageForm
        // 
        AcceptButton = DoOkButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(60, 60, 60);
        CancelButton = DoCancelButton;
        ClientSize = new Size(434, 511);
        Controls.Add(PageList);
        Controls.Add(PageInfoLabel);
        Controls.Add(DoCancelButton);
        Controls.Add(DoOkButton);
        FormBorderStyle = FormBorderStyle.None;
        Name = "PageForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "페이지 선택";
        Shown += PageForm_Shown;
        ResumeLayout(false);
    }

    #endregion

    private Button DoOkButton;
    private Button DoCancelButton;
    private Label PageInfoLabel;
    private ListView PageList;
    private ColumnHeader FileNameColumn;
    private ColumnHeader DateColumn;
    private ColumnHeader SizeColumn;
}
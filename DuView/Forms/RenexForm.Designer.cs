namespace DuView.Forms;

partial class RenexForm
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
        TitleLabel = new Label();
        RenameLabel = new Label();
        OriginalLabel = new Label();
        BookNameLabel = new Label();
        AuthorText = new TextBox();
        TitleText = new TextBox();
        IndexText = new TextBox();
        ExtraText = new TextBox();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        SuspendLayout();
        // 
        // DoOkButton
        // 
        DoOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        DoOkButton.DialogResult = DialogResult.OK;
        DoOkButton.FlatStyle = FlatStyle.Flat;
        DoOkButton.ForeColor = Color.White;
        DoOkButton.Location = new Point(523, 286);
        DoOkButton.Margin = new Padding(4, 3, 4, 3);
        DoOkButton.Name = "DoOkButton";
        DoOkButton.Size = new Size(191, 45);
        DoOkButton.TabIndex = 5;
        DoOkButton.Text = "바꾸기";
        DoOkButton.UseVisualStyleBackColor = true;
        // 
        // DoCancelButton
        // 
        DoCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        DoCancelButton.DialogResult = DialogResult.Cancel;
        DoCancelButton.FlatStyle = FlatStyle.Flat;
        DoCancelButton.ForeColor = Color.White;
        DoCancelButton.Location = new Point(4, 286);
        DoCancelButton.Margin = new Padding(4, 3, 4, 3);
        DoCancelButton.Name = "DoCancelButton";
        DoCancelButton.Size = new Size(69, 45);
        DoCancelButton.TabIndex = 6;
        DoCancelButton.Text = "취소";
        DoCancelButton.UseVisualStyleBackColor = true;
        // 
        // TitleLabel
        // 
        TitleLabel.AutoSize = true;
        TitleLabel.Font = new Font("맑은 고딕", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 129);
        TitleLabel.ForeColor = Color.White;
        TitleLabel.Location = new Point(4, 3);
        TitleLabel.Margin = new Padding(4, 0, 4, 0);
        TitleLabel.Name = "TitleLabel";
        TitleLabel.Size = new Size(161, 30);
        TitleLabel.TabIndex = 0;
        TitleLabel.Text = "책 이름 바꾸기";
        // 
        // RenameLabel
        // 
        RenameLabel.ForeColor = Color.White;
        RenameLabel.Location = new Point(35, 179);
        RenameLabel.Margin = new Padding(4, 0, 4, 0);
        RenameLabel.Name = "RenameLabel";
        RenameLabel.Size = new Size(93, 30);
        RenameLabel.TabIndex = 3;
        RenameLabel.Text = "작가";
        RenameLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // OriginalLabel
        // 
        OriginalLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        OriginalLabel.BorderStyle = BorderStyle.FixedSingle;
        OriginalLabel.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
        OriginalLabel.ForeColor = Color.White;
        OriginalLabel.Location = new Point(134, 39);
        OriginalLabel.Margin = new Padding(4, 0, 4, 0);
        OriginalLabel.Name = "OriginalLabel";
        OriginalLabel.Size = new Size(580, 30);
        OriginalLabel.TabIndex = 5;
        OriginalLabel.Text = "--";
        // 
        // BookNameLabel
        // 
        BookNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        BookNameLabel.BorderStyle = BorderStyle.FixedSingle;
        BookNameLabel.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
        BookNameLabel.ForeColor = Color.White;
        BookNameLabel.Location = new Point(134, 80);
        BookNameLabel.Margin = new Padding(4, 0, 4, 0);
        BookNameLabel.Name = "BookNameLabel";
        BookNameLabel.Size = new Size(580, 30);
        BookNameLabel.TabIndex = 9;
        BookNameLabel.Text = "--";
        // 
        // AuthorText
        // 
        AuthorText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        AuthorText.Font = new Font("맑은 고딕", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
        AuthorText.Location = new Point(134, 174);
        AuthorText.Margin = new Padding(4, 3, 4, 3);
        AuthorText.Name = "AuthorText";
        AuthorText.Size = new Size(303, 36);
        AuthorText.TabIndex = 2;
        AuthorText.TextChanged += Texts_TextChanged;
        // 
        // TitleText
        // 
        TitleText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        TitleText.Font = new Font("맑은 고딕", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
        TitleText.Location = new Point(134, 127);
        TitleText.Margin = new Padding(4, 3, 4, 3);
        TitleText.Name = "TitleText";
        TitleText.Size = new Size(580, 36);
        TitleText.TabIndex = 1;
        TitleText.TextChanged += Texts_TextChanged;
        // 
        // IndexText
        // 
        IndexText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        IndexText.Font = new Font("맑은 고딕", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
        IndexText.Location = new Point(561, 174);
        IndexText.Margin = new Padding(4, 3, 4, 3);
        IndexText.Name = "IndexText";
        IndexText.Size = new Size(153, 36);
        IndexText.TabIndex = 3;
        IndexText.TextChanged += Texts_TextChanged;
        // 
        // ExtraText
        // 
        ExtraText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        ExtraText.Font = new Font("맑은 고딕", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
        ExtraText.Location = new Point(134, 222);
        ExtraText.Margin = new Padding(4, 3, 4, 3);
        ExtraText.Name = "ExtraText";
        ExtraText.Size = new Size(579, 36);
        ExtraText.TabIndex = 4;
        ExtraText.TextChanged += Texts_TextChanged;
        // 
        // label1
        // 
        label1.ForeColor = Color.White;
        label1.Location = new Point(35, 132);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(93, 30);
        label1.TabIndex = 10;
        label1.Text = "책 이름";
        label1.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label2
        // 
        label2.ForeColor = Color.White;
        label2.Location = new Point(462, 179);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(93, 30);
        label2.TabIndex = 11;
        label2.Text = "번호";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label3
        // 
        label3.ForeColor = Color.White;
        label3.Location = new Point(14, 233);
        label3.Margin = new Padding(4, 0, 4, 0);
        label3.Name = "label3";
        label3.Size = new Size(114, 30);
        label3.TabIndex = 12;
        label3.Text = "추가 정보";
        label3.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label4
        // 
        label4.ForeColor = Color.White;
        label4.Location = new Point(35, 80);
        label4.Margin = new Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new Size(93, 30);
        label4.TabIndex = 13;
        label4.Text = "바꿀 이름";
        label4.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label5
        // 
        label5.ForeColor = Color.White;
        label5.Location = new Point(35, 39);
        label5.Margin = new Padding(4, 0, 4, 0);
        label5.Name = "label5";
        label5.Size = new Size(93, 30);
        label5.TabIndex = 14;
        label5.Text = "원래 이름";
        label5.TextAlign = ContentAlignment.MiddleRight;
        // 
        // RenexForm
        // 
        AcceptButton = DoOkButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(60, 60, 60);
        CancelButton = DoCancelButton;
        ClientSize = new Size(725, 335);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(AuthorText);
        Controls.Add(ExtraText);
        Controls.Add(IndexText);
        Controls.Add(TitleText);
        Controls.Add(BookNameLabel);
        Controls.Add(OriginalLabel);
        Controls.Add(RenameLabel);
        Controls.Add(TitleLabel);
        Controls.Add(DoCancelButton);
        Controls.Add(DoOkButton);
        FormBorderStyle = FormBorderStyle.None;
        KeyPreview = true;
        Margin = new Padding(4, 3, 4, 3);
        Name = "RenexForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "확장 이름 바꾸기";
        FormClosing += RenexForm_FormClosing;
        Load += RenexForm_Load;
        KeyDown += RenexForm_KeyDown;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button DoOkButton;
    private Button DoCancelButton;
    private Label TitleLabel;
    private Label RenameLabel;
    private Label OriginalLabel;
    private Label BookNameLabel;
    private TextBox AuthorText;
    private TextBox TitleText;
    private TextBox IndexText;
    private TextBox ExtraText;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
}
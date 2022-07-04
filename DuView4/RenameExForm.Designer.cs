namespace DuView
{
	partial class RenameExForm
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
			this.TitleLabel = new System.Windows.Forms.Label();
			this.DoOkNextButton = new System.Windows.Forms.Button();
			this.DoCancelButton = new System.Windows.Forms.Button();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.RenameLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.AuthorText = new System.Windows.Forms.TextBox();
			this.OriginalNameLabel = new System.Windows.Forms.Label();
			this.TitleText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.IndexText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.AdditionalText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.BookNameLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SearchButton = new System.Windows.Forms.Button();
			this.DoOkReopenButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(3, 3);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(73, 30);
			this.TitleLabel.TabIndex = 1;
			this.TitleLabel.Text = Du.Globalization.Locale.Text(2240);
			// 
			// DoOkNextButton
			// 
			this.DoOkNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkNextButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkNextButton.ForeColor = System.Drawing.Color.White;
			this.DoOkNextButton.Location = new System.Drawing.Point(448, 248);
			this.DoOkNextButton.Name = "DoOkNextButton";
			this.DoOkNextButton.Size = new System.Drawing.Size(164, 39);
			this.DoOkNextButton.TabIndex = 7;
			this.DoOkNextButton.Text = Du.Globalization.Locale.Text(2250);
			this.DoOkNextButton.UseVisualStyleBackColor = true;
			this.DoOkNextButton.Click += new System.EventHandler(this.DoOkButton_Click);
			// 
			// DoCancelButton
			// 
			this.DoCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.DoCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoCancelButton.ForeColor = System.Drawing.Color.White;
			this.DoCancelButton.Location = new System.Drawing.Point(213, 248);
			this.DoCancelButton.Name = "DoCancelButton";
			this.DoCancelButton.Size = new System.Drawing.Size(59, 39);
			this.DoCancelButton.TabIndex = 8;
			this.DoCancelButton.Text = Du.Globalization.Locale.Text(98);
			this.DoCancelButton.UseVisualStyleBackColor = true;
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(494, 0);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(129, 26);
			this.SystemButton.MinimumSize = new System.Drawing.Size(129, 26);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = false;
			this.SystemButton.ShowMinimize = false;
			this.SystemButton.Size = new System.Drawing.Size(129, 26);
			this.SystemButton.TabIndex = 5;
			this.SystemButton.TabStop = false;
			// 
			// RenameLabel
			// 
			this.RenameLabel.ForeColor = System.Drawing.Color.White;
			this.RenameLabel.Location = new System.Drawing.Point(30, 155);
			this.RenameLabel.Name = "RenameLabel";
			this.RenameLabel.Size = new System.Drawing.Size(80, 26);
			this.RenameLabel.TabIndex = 6;
			this.RenameLabel.Text = Du.Globalization.Locale.Text(2246);
			this.RenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(30, 34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 26);
			this.label6.TabIndex = 7;
			this.label6.Text = Du.Globalization.Locale.Text(2241);
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// AuthorText
			// 
			this.AuthorText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.AuthorText.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.AuthorText.Location = new System.Drawing.Point(115, 151);
			this.AuthorText.Name = "AuthorText";
			this.AuthorText.Size = new System.Drawing.Size(260, 35);
			this.AuthorText.TabIndex = 2;
			this.AuthorText.TextChanged += new System.EventHandler(this.BuildRename_TextChanged);
			// 
			// OriginalNameLabel
			// 
			this.OriginalNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.OriginalNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OriginalNameLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.OriginalNameLabel.ForeColor = System.Drawing.Color.White;
			this.OriginalNameLabel.Location = new System.Drawing.Point(115, 34);
			this.OriginalNameLabel.Name = "OriginalNameLabel";
			this.OriginalNameLabel.Size = new System.Drawing.Size(497, 26);
			this.OriginalNameLabel.TabIndex = 9;
			this.OriginalNameLabel.Text = "--";
			// 
			// TitleText
			// 
			this.TitleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TitleText.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.TitleText.Location = new System.Drawing.Point(115, 110);
			this.TitleText.Name = "TitleText";
			this.TitleText.Size = new System.Drawing.Size(498, 35);
			this.TitleText.TabIndex = 1;
			this.TitleText.TextChanged += new System.EventHandler(this.BuildRename_TextChanged);
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(30, 114);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 26);
			this.label1.TabIndex = 13;
			this.label1.Text = Du.Globalization.Locale.Text(2247);
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// IndexText
			// 
			this.IndexText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.IndexText.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.IndexText.Location = new System.Drawing.Point(481, 151);
			this.IndexText.Name = "IndexText";
			this.IndexText.Size = new System.Drawing.Size(132, 35);
			this.IndexText.TabIndex = 3;
			this.IndexText.TextChanged += new System.EventHandler(this.BuildRename_TextChanged);
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(396, 155);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 26);
			this.label2.TabIndex = 15;
			this.label2.Text = Du.Globalization.Locale.Text(2248);
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// AdditionalText
			// 
			this.AdditionalText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.AdditionalText.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
			this.AdditionalText.Location = new System.Drawing.Point(115, 192);
			this.AdditionalText.Name = "AdditionalText";
			this.AdditionalText.Size = new System.Drawing.Size(497, 35);
			this.AdditionalText.TabIndex = 4;
			this.AdditionalText.TextChanged += new System.EventHandler(this.BuildRename_TextChanged);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(12, 202);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 26);
			this.label3.TabIndex = 17;
			this.label3.Text = Du.Globalization.Locale.Text(2249);
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BookNameLabel
			// 
			this.BookNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.BookNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BookNameLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.BookNameLabel.ForeColor = System.Drawing.Color.White;
			this.BookNameLabel.Location = new System.Drawing.Point(115, 69);
			this.BookNameLabel.Name = "BookNameLabel";
			this.BookNameLabel.Size = new System.Drawing.Size(497, 26);
			this.BookNameLabel.TabIndex = 20;
			this.BookNameLabel.Text = "--";
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(30, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 26);
			this.label5.TabIndex = 19;
			this.label5.Text = Du.Globalization.Locale.Text(2242);
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SearchButton
			// 
			this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchButton.ForeColor = System.Drawing.Color.White;
			this.SearchButton.Location = new System.Drawing.Point(8, 248);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(132, 39);
			this.SearchButton.TabIndex = 21;
			this.SearchButton.Text = Du.Globalization.Locale.Text(127);
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// DoOkReopenButton
			// 
			this.DoOkReopenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkReopenButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkReopenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkReopenButton.ForeColor = System.Drawing.Color.White;
			this.DoOkReopenButton.Location = new System.Drawing.Point(278, 248);
			this.DoOkReopenButton.Name = "DoOkReopenButton";
			this.DoOkReopenButton.Size = new System.Drawing.Size(164, 39);
			this.DoOkReopenButton.TabIndex = 22;
			this.DoOkReopenButton.Text = Du.Globalization.Locale.Text(2251);
			this.DoOkReopenButton.UseVisualStyleBackColor = true;
			this.DoOkReopenButton.Click += new System.EventHandler(this.DoOkReopenButton_Click);
			// 
			// RenameExForm
			// 
			this.AcceptButton = this.DoOkNextButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.DoCancelButton;
			this.ClientSize = new System.Drawing.Size(622, 290);
			this.ControlBox = false;
			this.Controls.Add(this.DoOkReopenButton);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.BookNameLabel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.AdditionalText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.IndexText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TitleText);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OriginalNameLabel);
			this.Controls.Add(this.AuthorText);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.RenameLabel);
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.DoOkNextButton);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.DoCancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(6857, 500);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(343, 182);
			this.Name = "RenameExForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = Du.Globalization.Locale.Text(2240);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RenameForm_FormClosing);
			this.Load += new System.EventHandler(this.RenameForm_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenameForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenameForm_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenameForm_MouseUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Button DoOkNextButton;
		private System.Windows.Forms.Button DoCancelButton;
		private Du.WinForms.BadakSystemButton SystemButton;
		private System.Windows.Forms.Label RenameLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox AuthorText;
		private System.Windows.Forms.Label OriginalNameLabel;

		#endregion

		private System.Windows.Forms.TextBox TitleText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox IndexText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox AdditionalText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label BookNameLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.Button DoOkReopenButton;
	}
}
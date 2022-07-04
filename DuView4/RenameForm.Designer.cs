namespace DuView
{
	partial class RenameForm
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
			this.DoOkButton = new System.Windows.Forms.Button();
			this.DoCancelButton = new System.Windows.Forms.Button();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.RenameLabel = new System.Windows.Forms.Label();
			this.CurrentLabel = new System.Windows.Forms.Label();
			this.RenameText = new System.Windows.Forms.TextBox();
			this.CurrentText = new System.Windows.Forms.Label();
			this.RenameAfterLabel = new System.Windows.Forms.Label();
			this.OpenNextRadio = new System.Windows.Forms.RadioButton();
			this.OpenAgainRadio = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(4, 4);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(73, 30);
			this.TitleLabel.TabIndex = 1;
			this.TitleLabel.Text = Du.Globalization.Locale.Text(2240);
			// 
			// DoOkButton
			// 
			this.DoOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkButton.ForeColor = System.Drawing.Color.White;
			this.DoOkButton.Location = new System.Drawing.Point(368, 161);
			this.DoOkButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DoOkButton.Name = "DoOkButton";
			this.DoOkButton.Size = new System.Drawing.Size(220, 45);
			this.DoOkButton.TabIndex = 3;
			this.DoOkButton.Text = Du.Globalization.Locale.Text(97);
			this.DoOkButton.UseVisualStyleBackColor = true;
			this.DoOkButton.Click += new System.EventHandler(this.DoOkButton_Click);
			// 
			// DoCancelButton
			// 
			this.DoCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.DoCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoCancelButton.ForeColor = System.Drawing.Color.White;
			this.DoCancelButton.Location = new System.Drawing.Point(293, 161);
			this.DoCancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DoCancelButton.Name = "DoCancelButton";
			this.DoCancelButton.Size = new System.Drawing.Size(69, 45);
			this.DoCancelButton.TabIndex = 4;
			this.DoCancelButton.Text = Du.Globalization.Locale.Text(98);
			this.DoCancelButton.UseVisualStyleBackColor = true;
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(450, 0);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = false;
			this.SystemButton.ShowMinimize = false;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 5;
			this.SystemButton.TabStop = false;
			// 
			// RenameLabel
			// 
			this.RenameLabel.ForeColor = System.Drawing.Color.White;
			this.RenameLabel.Location = new System.Drawing.Point(35, 83);
			this.RenameLabel.Name = "RenameLabel";
			this.RenameLabel.Size = new System.Drawing.Size(93, 30);
			this.RenameLabel.TabIndex = 6;
			this.RenameLabel.Text = Du.Globalization.Locale.Text(2242);
			this.RenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// CurrentLabel
			// 
			this.CurrentLabel.ForeColor = System.Drawing.Color.White;
			this.CurrentLabel.Location = new System.Drawing.Point(35, 39);
			this.CurrentLabel.Name = "CurrentLabel";
			this.CurrentLabel.Size = new System.Drawing.Size(93, 30);
			this.CurrentLabel.TabIndex = 7;
			this.CurrentLabel.Text = Du.Globalization.Locale.Text(2241);
			this.CurrentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// RenameText
			// 
			this.RenameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.RenameText.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RenameText.Location = new System.Drawing.Point(134, 78);
			this.RenameText.Name = "RenameText";
			this.RenameText.Size = new System.Drawing.Size(454, 35);
			this.RenameText.TabIndex = 8;
			// 
			// CurrentText
			// 
			this.CurrentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.CurrentText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CurrentText.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CurrentText.ForeColor = System.Drawing.Color.White;
			this.CurrentText.Location = new System.Drawing.Point(134, 39);
			this.CurrentText.Name = "CurrentText";
			this.CurrentText.Size = new System.Drawing.Size(454, 30);
			this.CurrentText.TabIndex = 9;
			this.CurrentText.Text = "--";
			// 
			// RenameAfterLabel
			// 
			this.RenameAfterLabel.ForeColor = System.Drawing.Color.White;
			this.RenameAfterLabel.Location = new System.Drawing.Point(35, 124);
			this.RenameAfterLabel.Name = "RenameAfterLabel";
			this.RenameAfterLabel.Size = new System.Drawing.Size(93, 30);
			this.RenameAfterLabel.TabIndex = 10;
			this.RenameAfterLabel.Text = Du.Globalization.Locale.Text(2243);
			this.RenameAfterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// OpenNextRadio
			// 
			this.OpenNextRadio.AutoSize = true;
			this.OpenNextRadio.ForeColor = System.Drawing.Color.White;
			this.OpenNextRadio.Location = new System.Drawing.Point(134, 130);
			this.OpenNextRadio.Name = "OpenNextRadio";
			this.OpenNextRadio.Size = new System.Drawing.Size(57, 19);
			this.OpenNextRadio.TabIndex = 11;
			this.OpenNextRadio.TabStop = true;
			this.OpenNextRadio.Text = Du.Globalization.Locale.Text(2244);
			this.OpenNextRadio.UseVisualStyleBackColor = true;
			this.OpenNextRadio.CheckedChanged += new System.EventHandler(this.OpenNextRadio_CheckedChanged);
			// 
			// OpenAgainRadio
			// 
			this.OpenAgainRadio.AutoSize = true;
			this.OpenAgainRadio.ForeColor = System.Drawing.Color.White;
			this.OpenAgainRadio.Location = new System.Drawing.Point(280, 130);
			this.OpenAgainRadio.Name = "OpenAgainRadio";
			this.OpenAgainRadio.Size = new System.Drawing.Size(57, 19);
			this.OpenAgainRadio.TabIndex = 12;
			this.OpenAgainRadio.TabStop = true;
			this.OpenAgainRadio.Text = Du.Globalization.Locale.Text(2245);
			this.OpenAgainRadio.UseVisualStyleBackColor = true;
			this.OpenAgainRadio.CheckedChanged += new System.EventHandler(this.OpenAgainRadio_CheckedChanged);
			// 
			// RenameForm
			// 
			this.AcceptButton = this.DoOkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.DoCancelButton;
			this.ClientSize = new System.Drawing.Size(600, 210);
			this.ControlBox = false;
			this.Controls.Add(this.OpenAgainRadio);
			this.Controls.Add(this.OpenNextRadio);
			this.Controls.Add(this.RenameAfterLabel);
			this.Controls.Add(this.CurrentText);
			this.Controls.Add(this.RenameText);
			this.Controls.Add(this.CurrentLabel);
			this.Controls.Add(this.RenameLabel);
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.DoOkButton);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.DoCancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(8000, 210);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 210);
			this.Name = "RenameForm";
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

		#endregion

		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Button DoOkButton;
		private System.Windows.Forms.Button DoCancelButton;
		private Du.WinForms.BadakSystemButton SystemButton;
		private System.Windows.Forms.Label RenameLabel;
		private System.Windows.Forms.Label CurrentLabel;
		private System.Windows.Forms.TextBox RenameText;
		private System.Windows.Forms.Label CurrentText;
		private System.Windows.Forms.Label RenameAfterLabel;
		private System.Windows.Forms.RadioButton OpenNextRadio;
		private System.Windows.Forms.RadioButton OpenAgainRadio;
	}
}
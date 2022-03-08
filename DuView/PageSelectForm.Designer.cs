namespace DuView
{
	partial class PageSelectForm
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
			this.PageInfoLabel = new System.Windows.Forms.Label();
			this.PageList = new System.Windows.Forms.ListView();
			this.PlFileNameColumn = new System.Windows.Forms.ColumnHeader();
			this.PlDateColumn = new System.Windows.Forms.ColumnHeader();
			this.PlSizeColumn = new System.Windows.Forms.ColumnHeader();
			this.DoOkButton = new System.Windows.Forms.Button();
			this.DoCancelButton = new System.Windows.Forms.Button();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.SuspendLayout();
			// 
			// PageInfoLabel
			// 
			this.PageInfoLabel.AutoSize = true;
			this.PageInfoLabel.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PageInfoLabel.ForeColor = System.Drawing.Color.White;
			this.PageInfoLabel.Location = new System.Drawing.Point(4, 4);
			this.PageInfoLabel.Name = "PageInfoLabel";
			this.PageInfoLabel.Size = new System.Drawing.Size(76, 30);
			this.PageInfoLabel.TabIndex = 1;
			this.PageInfoLabel.Text = Locale.Text(2221);
			this.PageInfoLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseDown);
			this.PageInfoLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseMove);
			this.PageInfoLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseUp);
			// 
			// PageList
			// 
			this.PageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PageList.AutoArrange = false;
			this.PageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlFileNameColumn,
            this.PlDateColumn,
            this.PlSizeColumn});
			this.PageList.FullRowSelect = true;
			this.PageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.PageList.Location = new System.Drawing.Point(12, 45);
			this.PageList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.PageList.MultiSelect = false;
			this.PageList.Name = "PageList";
			this.PageList.Size = new System.Drawing.Size(476, 504);
			this.PageList.TabIndex = 2;
			this.PageList.UseCompatibleStateImageBehavior = false;
			this.PageList.View = System.Windows.Forms.View.Details;
			this.PageList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PageList_MouseDoubleClick);
			this.PageList.Resize += new System.EventHandler(this.PageList_Resize);
			// 
			// PlFileNameColumn
			// 
			this.PlFileNameColumn.Text = Locale.Text(2222);
			this.PlFileNameColumn.Width = 76;
			// 
			// PlDateColumn
			// 
			this.PlDateColumn.Text = Locale.Text(2223);
			this.PlDateColumn.Width = 150;
			// 
			// PlSizeColumn
			// 
			this.PlSizeColumn.Text = Locale.Text(2224);
			this.PlSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PlSizeColumn.Width = 75;
			// 
			// DoOkButton
			// 
			this.DoOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkButton.ForeColor = System.Drawing.Color.White;
			this.DoOkButton.Location = new System.Drawing.Point(268, 558);
			this.DoOkButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DoOkButton.Name = "DoOkButton";
			this.DoOkButton.Size = new System.Drawing.Size(220, 45);
			this.DoOkButton.TabIndex = 3;
			this.DoOkButton.Text = Locale.Text(97);
			this.DoOkButton.UseVisualStyleBackColor = true;
			this.DoOkButton.Click += new System.EventHandler(this.DoOkButton_Click);
			// 
			// DoCancelButton
			// 
			this.DoCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.DoCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoCancelButton.ForeColor = System.Drawing.Color.White;
			this.DoCancelButton.Location = new System.Drawing.Point(30, 490);
			this.DoCancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DoCancelButton.Name = "DoCancelButton";
			this.DoCancelButton.Size = new System.Drawing.Size(69, 45);
			this.DoCancelButton.TabIndex = 4;
			this.DoCancelButton.Text = Locale.Text(98);
			this.DoCancelButton.UseVisualStyleBackColor = true;
			// 
			// SystemButton
			// 
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(349, 2);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = true;
			this.SystemButton.ShowMinimize = true;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 5;
			// 
			// PageSelectForm
			// 
			this.AcceptButton = this.DoOkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.DoCancelButton;
			this.ClientSize = new System.Drawing.Size(500, 618);
			this.ControlBox = false;
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.DoOkButton);
			this.Controls.Add(this.PageList);
			this.Controls.Add(this.PageInfoLabel);
			this.Controls.Add(this.DoCancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PageSelectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = Locale.Text(2220);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageSelectForm_FormClosing);
			this.Load += new System.EventHandler(this.PageSelectForm_Load);
			this.Shown += new System.EventHandler(this.PageSelectForm_Shown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PageSelectForm_MouseUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label PageInfoLabel;
		private ListView PageList;
		private ColumnHeader PlFileNameColumn;
		private ColumnHeader PlDateColumn;
		private ColumnHeader PlSizeColumn;
		private Button DoOkButton;
		private Button DoCancelButton;
		private BadakSystemButton SystemButton;
	}
}
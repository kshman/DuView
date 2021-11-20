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
			this.PlFileNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.PlDateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.PlSizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DoOkButton = new System.Windows.Forms.Button();
			this.DoCancelButton = new System.Windows.Forms.Button();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.SuspendLayout();
			// 
			// PageInfoLabel
			// 
			this.PageInfoLabel.AutoSize = true;
			this.PageInfoLabel.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.PageInfoLabel.ForeColor = System.Drawing.Color.White;
			this.PageInfoLabel.Location = new System.Drawing.Point(4, 3);
			this.PageInfoLabel.Name = "PageInfoLabel";
			this.PageInfoLabel.Size = new System.Drawing.Size(70, 30);
			this.PageInfoLabel.TabIndex = 1;
			this.PageInfoLabel.Text = "label1";
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
			this.PageList.HideSelection = false;
			this.PageList.Location = new System.Drawing.Point(12, 36);
			this.PageList.MultiSelect = false;
			this.PageList.Name = "PageList";
			this.PageList.Size = new System.Drawing.Size(476, 460);
			this.PageList.TabIndex = 2;
			this.PageList.UseCompatibleStateImageBehavior = false;
			this.PageList.View = System.Windows.Forms.View.Details;
			this.PageList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PageList_MouseDoubleClick);
			this.PageList.Resize += new System.EventHandler(this.PageList_Resize);
			// 
			// PlFileNameColumn
			// 
			this.PlFileNameColumn.Text = "이미지 이름";
			this.PlFileNameColumn.Width = 76;
			// 
			// PlDateColumn
			// 
			this.PlDateColumn.Text = "날짜";
			this.PlDateColumn.Width = 145;
			// 
			// PlSizeColumn
			// 
			this.PlSizeColumn.Text = "크기";
			this.PlSizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PlSizeColumn.Width = 75;
			// 
			// DoOkButton
			// 
			this.DoOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkButton.ForeColor = System.Drawing.Color.White;
			this.DoOkButton.Location = new System.Drawing.Point(268, 502);
			this.DoOkButton.Name = "DoOkButton";
			this.DoOkButton.Size = new System.Drawing.Size(220, 36);
			this.DoOkButton.TabIndex = 3;
			this.DoOkButton.Text = "선택";
			this.DoOkButton.UseVisualStyleBackColor = true;
			this.DoOkButton.Click += new System.EventHandler(this.DoOkButton_Click);
			// 
			// DoCancelButton
			// 
			this.DoCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.DoCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoCancelButton.ForeColor = System.Drawing.Color.White;
			this.DoCancelButton.Location = new System.Drawing.Point(419, 460);
			this.DoCancelButton.Name = "DoCancelButton";
			this.DoCancelButton.Size = new System.Drawing.Size(69, 36);
			this.DoCancelButton.TabIndex = 4;
			this.DoCancelButton.Text = "취소";
			this.DoCancelButton.UseVisualStyleBackColor = true;
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(349, 2);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = false;
			this.SystemButton.ShowMinimize = false;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 0;
			this.SystemButton.CloseOrder += new System.EventHandler(this.SystemButton_CloseOrder);
			// 
			// PageSelectForm
			// 
			this.AcceptButton = this.DoOkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.DoCancelButton;
			this.ClientSize = new System.Drawing.Size(500, 550);
			this.ControlBox = false;
			this.Controls.Add(this.DoOkButton);
			this.Controls.Add(this.PageList);
			this.Controls.Add(this.PageInfoLabel);
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.DoCancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PageSelectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "페이지 선택";
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

		private Du.WinForms.BadakSystemButton SystemButton;
		private System.Windows.Forms.Label PageInfoLabel;
		private System.Windows.Forms.ListView PageList;
		private System.Windows.Forms.ColumnHeader PlFileNameColumn;
		private System.Windows.Forms.ColumnHeader PlDateColumn;
		private System.Windows.Forms.ColumnHeader PlSizeColumn;
		private System.Windows.Forms.Button DoOkButton;
		private System.Windows.Forms.Button DoCancelButton;
	}
}
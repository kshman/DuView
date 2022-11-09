namespace DuView
{
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveForm));
			this.MoveList = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.MoveMenuStrip = new Du.WinForms.BadakContextMenuStrip(this.components);
			this.MoveAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MoveChangeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.MoveDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MoveImageList = new System.Windows.Forms.ImageList(this.components);
			this.BrowseButton = new System.Windows.Forms.Button();
			this.OkDoItButton = new System.Windows.Forms.Button();
			this.NoCancelButton = new System.Windows.Forms.Button();
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.DestLocationText = new System.Windows.Forms.TextBox();
			this.MoveMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MoveList
			// 
			this.MoveList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MoveList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MoveList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.MoveList.ContextMenuStrip = this.MoveMenuStrip;
			this.MoveList.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MoveList.FullRowSelect = true;
			this.MoveList.GridLines = true;
			this.MoveList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.MoveList.LargeImageList = this.MoveImageList;
			this.MoveList.Location = new System.Drawing.Point(5, 35);
			this.MoveList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MoveList.MultiSelect = false;
			this.MoveList.Name = "MoveList";
			this.MoveList.Size = new System.Drawing.Size(488, 438);
			this.MoveList.SmallImageList = this.MoveImageList;
			this.MoveList.TabIndex = 0;
			this.MoveList.UseCompatibleStateImageBehavior = false;
			this.MoveList.View = System.Windows.Forms.View.Details;
			this.MoveList.SelectedIndexChanged += new System.EventHandler(this.MoveList_SelectedIndexChanged);
			this.MoveList.Resize += new System.EventHandler(this.MoveList_Resize);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "1314";
			this.columnHeader1.Width = 380;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "1315";
			// 
			// MoveMenuStrip
			// 
			this.MoveMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveAddMenuItem,
            this.MoveChangeMenuItem,
            this.toolStripSeparator1,
            this.MoveDeleteMenuItem});
			this.MoveMenuStrip.Name = "MoveMenuStrip";
			this.MoveMenuStrip.Size = new System.Drawing.Size(103, 76);
			this.MoveMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.MoveMenuStrip_Opening);
			// 
			// MoveAddMenuItem
			// 
			this.MoveAddMenuItem.ForeColor = System.Drawing.Color.White;
			this.MoveAddMenuItem.Name = "MoveAddMenuItem";
			this.MoveAddMenuItem.Size = new System.Drawing.Size(102, 22);
			this.MoveAddMenuItem.Text = "1312";
			this.MoveAddMenuItem.Click += new System.EventHandler(this.MoveAddMenuItem_Click);
			// 
			// MoveChangeMenuItem
			// 
			this.MoveChangeMenuItem.ForeColor = System.Drawing.Color.White;
			this.MoveChangeMenuItem.Name = "MoveChangeMenuItem";
			this.MoveChangeMenuItem.Size = new System.Drawing.Size(102, 22);
			this.MoveChangeMenuItem.Text = "1317";
			this.MoveChangeMenuItem.Click += new System.EventHandler(this.MoveChangeMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(99, 6);
			// 
			// MoveDeleteMenuItem
			// 
			this.MoveDeleteMenuItem.ForeColor = System.Drawing.Color.White;
			this.MoveDeleteMenuItem.Name = "MoveDeleteMenuItem";
			this.MoveDeleteMenuItem.Size = new System.Drawing.Size(102, 22);
			this.MoveDeleteMenuItem.Text = "1307";
			this.MoveDeleteMenuItem.Click += new System.EventHandler(this.MoveDeleteMenuItem_Click);
			// 
			// MoveImageList
			// 
			this.MoveImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.MoveImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MoveImageList.ImageStream")));
			this.MoveImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.MoveImageList.Images.SetKeyName(0, "folder-blue.png");
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BrowseButton.ForeColor = System.Drawing.Color.White;
			this.BrowseButton.Location = new System.Drawing.Point(13, 508);
			this.BrowseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(173, 30);
			this.BrowseButton.TabIndex = 1;
			this.BrowseButton.Text = "1311";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// OkDoItButton
			// 
			this.OkDoItButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkDoItButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkDoItButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkDoItButton.ForeColor = System.Drawing.Color.White;
			this.OkDoItButton.Location = new System.Drawing.Point(212, 508);
			this.OkDoItButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.OkDoItButton.Name = "OkDoItButton";
			this.OkDoItButton.Size = new System.Drawing.Size(130, 30);
			this.OkDoItButton.TabIndex = 2;
			this.OkDoItButton.Text = "97";
			this.OkDoItButton.UseVisualStyleBackColor = true;
			this.OkDoItButton.Click += new System.EventHandler(this.OkDoItButton_Click);
			// 
			// NoCancelButton
			// 
			this.NoCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.NoCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.NoCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.NoCancelButton.ForeColor = System.Drawing.Color.White;
			this.NoCancelButton.Location = new System.Drawing.Point(350, 508);
			this.NoCancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.NoCancelButton.Name = "NoCancelButton";
			this.NoCancelButton.Size = new System.Drawing.Size(130, 30);
			this.NoCancelButton.TabIndex = 3;
			this.NoCancelButton.Text = "98";
			this.NoCancelButton.UseVisualStyleBackColor = true;
			this.NoCancelButton.Click += new System.EventHandler(this.NoCancelButton_Click);
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(350, 2);
			this.SystemButton.Margin = new System.Windows.Forms.Padding(0);
			this.SystemButton.MaximumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.MinimumSize = new System.Drawing.Size(150, 30);
			this.SystemButton.Name = "SystemButton";
			this.SystemButton.ShowClose = true;
			this.SystemButton.ShowMaximize = false;
			this.SystemButton.ShowMinimize = false;
			this.SystemButton.Size = new System.Drawing.Size(150, 30);
			this.SystemButton.TabIndex = 4;
			this.SystemButton.TabStop = false;
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(5, 5);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(46, 21);
			this.TitleLabel.TabIndex = 5;
			this.TitleLabel.Text = "1313";
			this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseDown);
			this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseMove);
			this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseUp);
			// 
			// DestLocationText
			// 
			this.DestLocationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DestLocationText.Location = new System.Drawing.Point(5, 479);
			this.DestLocationText.Name = "DestLocationText";
			this.DestLocationText.Size = new System.Drawing.Size(488, 23);
			this.DestLocationText.TabIndex = 6;
			this.DestLocationText.Enter += new System.EventHandler(this.DestLocationText_Enter);
			// 
			// MoveForm
			// 
			this.AcceptButton = this.OkDoItButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.NoCancelButton;
			this.ClientSize = new System.Drawing.Size(500, 550);
			this.ControlBox = false;
			this.Controls.Add(this.DestLocationText);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.NoCancelButton);
			this.Controls.Add(this.OkDoItButton);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.MoveList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 550);
			this.Name = "MoveForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MoveForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoveForm_FormClosing);
			this.Load += new System.EventHandler(this.MoveForm_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveForm_MouseUp);
			this.MoveMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ListView MoveList;
		private Button BrowseButton;
		private Button OkDoItButton;
		private Button NoCancelButton;
		private BadakContextMenuStrip MoveMenuStrip;
		private ToolStripMenuItem MoveAddMenuItem;
		private ToolStripMenuItem MoveDeleteMenuItem;
		private ImageList MoveImageList;
		private BadakSystemButton SystemButton;
		private Label TitleLabel;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private TextBox DestLocationText;
		private ToolStripMenuItem MoveChangeMenuItem;
		private ToolStripSeparator toolStripSeparator1;
	}
}
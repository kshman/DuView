namespace DuView
{
	partial class OptionForm
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
			this.SystemButton = new Du.WinForms.BadakSystemButton();
			this.OptionTab = new System.Windows.Forms.TabControl();
			this.GeneralPage = new System.Windows.Forms.TabPage();
			this.ReloadExternalExitCheck = new System.Windows.Forms.CheckBox();
			this.ExternalRunButton = new System.Windows.Forms.Button();
			this.ExternalRunText = new System.Windows.Forms.TextBox();
			this.ExternalRunLabel = new System.Windows.Forms.Label();
			this.CacheSizeValue = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.CacheMeasureLabel = new System.Windows.Forms.Label();
			this.CacheSizeLabel = new System.Windows.Forms.Label();
			this.UpdateNotifyCheck = new System.Windows.Forms.CheckBox();
			this.AlwayTopCheck = new System.Windows.Forms.CheckBox();
			this.ConfirmDeleteCheck = new System.Windows.Forms.CheckBox();
			this.EscExitCheck = new System.Windows.Forms.CheckBox();
			this.RunOnceCheck = new System.Windows.Forms.CheckBox();
			this.UseMagneticCheck = new System.Windows.Forms.CheckBox();
			this.ViewPage = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.KmPage = new System.Windows.Forms.TabPage();
			this.UseClickToPageCheck = new System.Windows.Forms.CheckBox();
			this.UseDoubleClickStateCheck = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.PadPage = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.SecurityPage = new System.Windows.Forms.TabPage();
			this.LocalePage = new System.Windows.Forms.TabPage();
			this.CreditScroll = new Du.WinForms.ScrollingBox();
			this.LocaleToRestartLabel = new System.Windows.Forms.Label();
			this.LocalesList = new System.Windows.Forms.ListBox();
			this.OptionTab.SuspendLayout();
			this.GeneralPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CacheSizeValue)).BeginInit();
			this.ViewPage.SuspendLayout();
			this.KmPage.SuspendLayout();
			this.PadPage.SuspendLayout();
			this.LocalePage.SuspendLayout();
			this.SuspendLayout();
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.ForeColor = System.Drawing.Color.White;
			this.TitleLabel.Location = new System.Drawing.Point(5, 5);
			this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(41, 20);
			this.TitleLabel.TabIndex = 1;
			this.TitleLabel.Text = "2400";
			this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseDown);
			this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseMove);
			this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseUp);
			// 
			// DoOkButton
			// 
			this.DoOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DoOkButton.ForeColor = System.Drawing.Color.White;
			this.DoOkButton.Location = new System.Drawing.Point(419, 560);
			this.DoOkButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DoOkButton.Name = "DoOkButton";
			this.DoOkButton.Size = new System.Drawing.Size(204, 34);
			this.DoOkButton.TabIndex = 3;
			this.DoOkButton.Text = "94";
			this.DoOkButton.UseVisualStyleBackColor = true;
			this.DoOkButton.Click += new System.EventHandler(this.DoOkButton_Click);
			// 
			// SystemButton
			// 
			this.SystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SystemButton.BackColor = System.Drawing.Color.Transparent;
			this.SystemButton.Form = null;
			this.SystemButton.Location = new System.Drawing.Point(489, 2);
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
			// OptionTab
			// 
			this.OptionTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OptionTab.Controls.Add(this.GeneralPage);
			this.OptionTab.Controls.Add(this.ViewPage);
			this.OptionTab.Controls.Add(this.KmPage);
			this.OptionTab.Controls.Add(this.PadPage);
			this.OptionTab.Controls.Add(this.SecurityPage);
			this.OptionTab.Controls.Add(this.LocalePage);
			this.OptionTab.Location = new System.Drawing.Point(15, 47);
			this.OptionTab.Margin = new System.Windows.Forms.Padding(4);
			this.OptionTab.Name = "OptionTab";
			this.OptionTab.SelectedIndex = 0;
			this.OptionTab.Size = new System.Drawing.Size(609, 504);
			this.OptionTab.TabIndex = 6;
			// 
			// GeneralPage
			// 
			this.GeneralPage.AutoScroll = true;
			this.GeneralPage.Controls.Add(this.ReloadExternalExitCheck);
			this.GeneralPage.Controls.Add(this.ExternalRunButton);
			this.GeneralPage.Controls.Add(this.ExternalRunText);
			this.GeneralPage.Controls.Add(this.ExternalRunLabel);
			this.GeneralPage.Controls.Add(this.CacheSizeValue);
			this.GeneralPage.Controls.Add(this.label1);
			this.GeneralPage.Controls.Add(this.CacheMeasureLabel);
			this.GeneralPage.Controls.Add(this.CacheSizeLabel);
			this.GeneralPage.Controls.Add(this.UpdateNotifyCheck);
			this.GeneralPage.Controls.Add(this.AlwayTopCheck);
			this.GeneralPage.Controls.Add(this.ConfirmDeleteCheck);
			this.GeneralPage.Controls.Add(this.EscExitCheck);
			this.GeneralPage.Controls.Add(this.RunOnceCheck);
			this.GeneralPage.Controls.Add(this.UseMagneticCheck);
			this.GeneralPage.Location = new System.Drawing.Point(4, 29);
			this.GeneralPage.Margin = new System.Windows.Forms.Padding(4);
			this.GeneralPage.Name = "GeneralPage";
			this.GeneralPage.Padding = new System.Windows.Forms.Padding(4);
			this.GeneralPage.Size = new System.Drawing.Size(601, 471);
			this.GeneralPage.TabIndex = 0;
			this.GeneralPage.Text = "2401";
			this.GeneralPage.UseVisualStyleBackColor = true;
			// 
			// ReloadExternalExitCheck
			// 
			this.ReloadExternalExitCheck.AutoSize = true;
			this.ReloadExternalExitCheck.Location = new System.Drawing.Point(320, 232);
			this.ReloadExternalExitCheck.Name = "ReloadExternalExitCheck";
			this.ReloadExternalExitCheck.Size = new System.Drawing.Size(60, 24);
			this.ReloadExternalExitCheck.TabIndex = 15;
			this.ReloadExternalExitCheck.Text = "2430";
			this.ReloadExternalExitCheck.UseVisualStyleBackColor = true;
			this.ReloadExternalExitCheck.CheckedChanged += new System.EventHandler(this.ReloadExternalExitCheck_CheckedChanged);
			// 
			// ExternalRunButton
			// 
			this.ExternalRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ExternalRunButton.Location = new System.Drawing.Point(502, 202);
			this.ExternalRunButton.Name = "ExternalRunButton";
			this.ExternalRunButton.Size = new System.Drawing.Size(92, 28);
			this.ExternalRunButton.TabIndex = 14;
			this.ExternalRunButton.Text = "118";
			this.ExternalRunButton.UseVisualStyleBackColor = true;
			this.ExternalRunButton.Click += new System.EventHandler(this.ExternalRunButton_Click);
			// 
			// ExternalRunText
			// 
			this.ExternalRunText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ExternalRunText.Location = new System.Drawing.Point(320, 169);
			this.ExternalRunText.Name = "ExternalRunText";
			this.ExternalRunText.Size = new System.Drawing.Size(274, 27);
			this.ExternalRunText.TabIndex = 13;
			// 
			// ExternalRunLabel
			// 
			this.ExternalRunLabel.AutoSize = true;
			this.ExternalRunLabel.Location = new System.Drawing.Point(299, 146);
			this.ExternalRunLabel.Name = "ExternalRunLabel";
			this.ExternalRunLabel.Size = new System.Drawing.Size(41, 20);
			this.ExternalRunLabel.TabIndex = 12;
			this.ExternalRunLabel.Text = "2429";
			// 
			// CacheSizeValue
			// 
			this.CacheSizeValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheSizeValue.Location = new System.Drawing.Point(320, 63);
			this.CacheSizeValue.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.CacheSizeValue.Name = "CacheSizeValue";
			this.CacheSizeValue.Size = new System.Drawing.Size(187, 27);
			this.CacheSizeValue.TabIndex = 11;
			this.CacheSizeValue.ThousandsSeparator = true;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(433, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "(0 ~ 1024)";
			// 
			// CacheMeasureLabel
			// 
			this.CacheMeasureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheMeasureLabel.AutoSize = true;
			this.CacheMeasureLabel.Location = new System.Drawing.Point(522, 66);
			this.CacheMeasureLabel.Name = "CacheMeasureLabel";
			this.CacheMeasureLabel.Size = new System.Drawing.Size(41, 20);
			this.CacheMeasureLabel.TabIndex = 9;
			this.CacheMeasureLabel.Text = "2428";
			// 
			// CacheSizeLabel
			// 
			this.CacheSizeLabel.AutoSize = true;
			this.CacheSizeLabel.Location = new System.Drawing.Point(299, 40);
			this.CacheSizeLabel.Name = "CacheSizeLabel";
			this.CacheSizeLabel.Size = new System.Drawing.Size(41, 20);
			this.CacheSizeLabel.TabIndex = 7;
			this.CacheSizeLabel.Text = "2427";
			// 
			// UpdateNotifyCheck
			// 
			this.UpdateNotifyCheck.AutoSize = true;
			this.UpdateNotifyCheck.Location = new System.Drawing.Point(17, 275);
			this.UpdateNotifyCheck.Name = "UpdateNotifyCheck";
			this.UpdateNotifyCheck.Size = new System.Drawing.Size(60, 24);
			this.UpdateNotifyCheck.TabIndex = 6;
			this.UpdateNotifyCheck.Text = "2426";
			this.UpdateNotifyCheck.UseVisualStyleBackColor = true;
			this.UpdateNotifyCheck.CheckedChanged += new System.EventHandler(this.UpdateNotifyCheck_CheckedChanged);
			// 
			// AlwayTopCheck
			// 
			this.AlwayTopCheck.AutoSize = true;
			this.AlwayTopCheck.Location = new System.Drawing.Point(17, 232);
			this.AlwayTopCheck.Name = "AlwayTopCheck";
			this.AlwayTopCheck.Size = new System.Drawing.Size(60, 24);
			this.AlwayTopCheck.TabIndex = 5;
			this.AlwayTopCheck.Text = "2425";
			this.AlwayTopCheck.UseVisualStyleBackColor = true;
			this.AlwayTopCheck.CheckedChanged += new System.EventHandler(this.AlwayTopCheck_CheckedChanged);
			// 
			// ConfirmDeleteCheck
			// 
			this.ConfirmDeleteCheck.AutoSize = true;
			this.ConfirmDeleteCheck.Location = new System.Drawing.Point(17, 189);
			this.ConfirmDeleteCheck.Name = "ConfirmDeleteCheck";
			this.ConfirmDeleteCheck.Size = new System.Drawing.Size(60, 24);
			this.ConfirmDeleteCheck.TabIndex = 4;
			this.ConfirmDeleteCheck.Text = "2424";
			this.ConfirmDeleteCheck.UseVisualStyleBackColor = true;
			this.ConfirmDeleteCheck.CheckedChanged += new System.EventHandler(this.ConfirmDeleteCheck_CheckedChanged);
			// 
			// EscExitCheck
			// 
			this.EscExitCheck.AutoSize = true;
			this.EscExitCheck.Location = new System.Drawing.Point(17, 60);
			this.EscExitCheck.Name = "EscExitCheck";
			this.EscExitCheck.Size = new System.Drawing.Size(60, 24);
			this.EscExitCheck.TabIndex = 2;
			this.EscExitCheck.Text = "2421";
			this.EscExitCheck.UseVisualStyleBackColor = true;
			this.EscExitCheck.CheckedChanged += new System.EventHandler(this.EscExitCheck_CheckedChanged);
			// 
			// RunOnceCheck
			// 
			this.RunOnceCheck.AutoSize = true;
			this.RunOnceCheck.Location = new System.Drawing.Point(17, 17);
			this.RunOnceCheck.Name = "RunOnceCheck";
			this.RunOnceCheck.Size = new System.Drawing.Size(60, 24);
			this.RunOnceCheck.TabIndex = 1;
			this.RunOnceCheck.Text = "2420";
			this.RunOnceCheck.UseVisualStyleBackColor = true;
			this.RunOnceCheck.CheckedChanged += new System.EventHandler(this.RunOnceCheck_CheckedChanged);
			// 
			// UseMagneticCheck
			// 
			this.UseMagneticCheck.AutoSize = true;
			this.UseMagneticCheck.Location = new System.Drawing.Point(17, 146);
			this.UseMagneticCheck.Name = "UseMagneticCheck";
			this.UseMagneticCheck.Size = new System.Drawing.Size(60, 24);
			this.UseMagneticCheck.TabIndex = 0;
			this.UseMagneticCheck.Text = "2423";
			this.UseMagneticCheck.UseVisualStyleBackColor = true;
			this.UseMagneticCheck.CheckedChanged += new System.EventHandler(this.UseMagneticCheck_CheckedChanged);
			// 
			// ViewPage
			// 
			this.ViewPage.AutoScroll = true;
			this.ViewPage.Controls.Add(this.label2);
			this.ViewPage.Location = new System.Drawing.Point(4, 24);
			this.ViewPage.Margin = new System.Windows.Forms.Padding(4);
			this.ViewPage.Name = "ViewPage";
			this.ViewPage.Padding = new System.Windows.Forms.Padding(4);
			this.ViewPage.Size = new System.Drawing.Size(601, 476);
			this.ViewPage.TabIndex = 1;
			this.ViewPage.Text = "2402";
			this.ViewPage.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "2410";
			// 
			// KmPage
			// 
			this.KmPage.AutoScroll = true;
			this.KmPage.Controls.Add(this.UseClickToPageCheck);
			this.KmPage.Controls.Add(this.UseDoubleClickStateCheck);
			this.KmPage.Controls.Add(this.label3);
			this.KmPage.Location = new System.Drawing.Point(4, 24);
			this.KmPage.Margin = new System.Windows.Forms.Padding(4);
			this.KmPage.Name = "KmPage";
			this.KmPage.Size = new System.Drawing.Size(601, 476);
			this.KmPage.TabIndex = 2;
			this.KmPage.Text = "2403";
			this.KmPage.UseVisualStyleBackColor = true;
			// 
			// UseClickToPageCheck
			// 
			this.UseClickToPageCheck.AutoSize = true;
			this.UseClickToPageCheck.Location = new System.Drawing.Point(17, 60);
			this.UseClickToPageCheck.Name = "UseClickToPageCheck";
			this.UseClickToPageCheck.Size = new System.Drawing.Size(60, 24);
			this.UseClickToPageCheck.TabIndex = 3;
			this.UseClickToPageCheck.Text = "2441";
			this.UseClickToPageCheck.UseVisualStyleBackColor = true;
			this.UseClickToPageCheck.CheckedChanged += new System.EventHandler(this.UseClickToPageCheck_CheckedChanged);
			// 
			// UseDoubleClickStateCheck
			// 
			this.UseDoubleClickStateCheck.AutoSize = true;
			this.UseDoubleClickStateCheck.Location = new System.Drawing.Point(17, 17);
			this.UseDoubleClickStateCheck.Name = "UseDoubleClickStateCheck";
			this.UseDoubleClickStateCheck.Size = new System.Drawing.Size(60, 24);
			this.UseDoubleClickStateCheck.TabIndex = 2;
			this.UseDoubleClickStateCheck.Text = "2440";
			this.UseDoubleClickStateCheck.UseVisualStyleBackColor = true;
			this.UseDoubleClickStateCheck.CheckedChanged += new System.EventHandler(this.UseDoubleClickStateCheck_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 20);
			this.label3.TabIndex = 1;
			this.label3.Text = "2410";
			// 
			// PadPage
			// 
			this.PadPage.AutoScroll = true;
			this.PadPage.Controls.Add(this.label4);
			this.PadPage.Location = new System.Drawing.Point(4, 24);
			this.PadPage.Margin = new System.Windows.Forms.Padding(4);
			this.PadPage.Name = "PadPage";
			this.PadPage.Size = new System.Drawing.Size(601, 476);
			this.PadPage.TabIndex = 4;
			this.PadPage.Text = "2404";
			this.PadPage.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 20);
			this.label4.TabIndex = 2;
			this.label4.Text = "2410";
			// 
			// SecurityPage
			// 
			this.SecurityPage.AutoScroll = true;
			this.SecurityPage.Location = new System.Drawing.Point(4, 24);
			this.SecurityPage.Margin = new System.Windows.Forms.Padding(4);
			this.SecurityPage.Name = "SecurityPage";
			this.SecurityPage.Size = new System.Drawing.Size(601, 476);
			this.SecurityPage.TabIndex = 3;
			this.SecurityPage.Text = "2405";
			this.SecurityPage.UseVisualStyleBackColor = true;
			// 
			// LocalePage
			// 
			this.LocalePage.Controls.Add(this.CreditScroll);
			this.LocalePage.Controls.Add(this.LocaleToRestartLabel);
			this.LocalePage.Controls.Add(this.LocalesList);
			this.LocalePage.Location = new System.Drawing.Point(4, 24);
			this.LocalePage.Name = "LocalePage";
			this.LocalePage.Size = new System.Drawing.Size(601, 476);
			this.LocalePage.TabIndex = 5;
			this.LocalePage.Text = "2406";
			this.LocalePage.UseVisualStyleBackColor = true;
			// 
			// CreditScroll
			// 
			this.CreditScroll.Alignment = System.Drawing.StringAlignment.Center;
			this.CreditScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CreditScroll.BackColor = System.Drawing.Color.Black;
			this.CreditScroll.ForeColor = System.Drawing.Color.White;
			this.CreditScroll.Location = new System.Drawing.Point(179, 3);
			this.CreditScroll.Name = "CreditScroll";
			this.CreditScroll.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.CreditScroll.Size = new System.Drawing.Size(419, 431);
			this.CreditScroll.TabIndex = 2;
			// 
			// LocaleToRestartLabel
			// 
			this.LocaleToRestartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LocaleToRestartLabel.AutoSize = true;
			this.LocaleToRestartLabel.Location = new System.Drawing.Point(179, 437);
			this.LocaleToRestartLabel.Name = "LocaleToRestartLabel";
			this.LocaleToRestartLabel.Size = new System.Drawing.Size(33, 20);
			this.LocaleToRestartLabel.TabIndex = 1;
			this.LocaleToRestartLabel.Text = "125";
			// 
			// LocalesList
			// 
			this.LocalesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.LocalesList.FormattingEnabled = true;
			this.LocalesList.ItemHeight = 20;
			this.LocalesList.Location = new System.Drawing.Point(3, 3);
			this.LocalesList.Name = "LocalesList";
			this.LocalesList.Size = new System.Drawing.Size(170, 424);
			this.LocalesList.TabIndex = 0;
			this.LocalesList.SelectedIndexChanged += new System.EventHandler(this.LocalesList_SelectedIndexChanged);
			// 
			// OptionForm
			// 
			this.AcceptButton = this.DoOkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.CancelButton = this.DoOkButton;
			this.ClientSize = new System.Drawing.Size(640, 600);
			this.ControlBox = false;
			this.Controls.Add(this.OptionTab);
			this.Controls.Add(this.SystemButton);
			this.Controls.Add(this.DoOkButton);
			this.Controls.Add(this.TitleLabel);
			this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 300);
			this.Name = "OptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "2400";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionForm_FormClosing);
			this.Load += new System.EventHandler(this.OptionForm_Load);
			this.Shown += new System.EventHandler(this.OptionForm_Shown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OptionForm_MouseUp);
			this.OptionTab.ResumeLayout(false);
			this.GeneralPage.ResumeLayout(false);
			this.GeneralPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CacheSizeValue)).EndInit();
			this.ViewPage.ResumeLayout(false);
			this.ViewPage.PerformLayout();
			this.KmPage.ResumeLayout(false);
			this.KmPage.PerformLayout();
			this.PadPage.ResumeLayout(false);
			this.PadPage.PerformLayout();
			this.LocalePage.ResumeLayout(false);
			this.LocalePage.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label TitleLabel;
		private Button DoOkButton;
		private BadakSystemButton SystemButton;
		private TabControl OptionTab;
		private TabPage GeneralPage;
		private TabPage ViewPage;
		private TabPage KmPage;
		private TabPage PadPage;
		private TabPage SecurityPage;
		private CheckBox RunOnceCheck;
		private CheckBox UseMagneticCheck;
		private Label label1;
		private Label CacheMeasureLabel;
		private Label CacheSizeLabel;
		private CheckBox UpdateNotifyCheck;
		private CheckBox AlwayTopCheck;
		private CheckBox ConfirmDeleteCheck;
		private CheckBox EscExitCheck;
		private Label label2;
		private Label label3;
		private Label label4;
		private NumericUpDown CacheSizeValue;
		private CheckBox UseClickToPageCheck;
		private CheckBox UseDoubleClickStateCheck;
		private Button ExternalRunButton;
		private TextBox ExternalRunText;
		private Label ExternalRunLabel;
		private CheckBox ReloadExternalExitCheck;
		private TabPage LocalePage;
		private Label LocaleToRestartLabel;
		private ListBox LocalesList;
		private ScrollingBox CreditScroll;
	}
}

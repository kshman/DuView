﻿namespace DuView;

public partial class OptionForm : Form
{
	private readonly BadakFormWorker _bfw;
	private IEnumerable<string> _locales = Locale.GetLocaleList();

	public OptionForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};

		// 일단 안쓰는 탭 처리
		((Control)ViewPage).Enabled = false;
		((Control)PadPage).Enabled = false;
	}

	private void OptionForm_Load(object sender, EventArgs e)
	{
		LocalesList.Items.Add(Locale.Text(126));
		foreach (var l in _locales)
			LocalesList.Items.Add(l);
	}

	private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		// 일반
		Settings.MaxPageCache = (int)CacheSizeValue.Value;

		if (ExternalRunText.TextLength > 0)
			Settings.ExternalRun = ExternalRunText.Text;
	}

	private void OptionForm_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void OptionForm_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void OptionForm_MouseMove(object sender, MouseEventArgs e)
	{
		_bfw.DragOnMove(e);
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		FormDu.EffectAppear(this);
	}

	protected override void WndProc(ref Message m)
	{
		if (!_bfw.WndProc(ref m))
			base.WndProc(ref m);
	}

	private void OptionForm_Shown(object sender, EventArgs e)
	{
		// 기본
		RunOnceCheck.Checked = Settings.GeneralRunOnce;
		EscExitCheck.Checked = Settings.GeneralEscExit;
		UseMagneticCheck.Checked = Settings.GeneralUseMagnetic;
		UseWinNoifyCheck.Checked = Settings.GeneralUseWinNotify;
		ConfirmDeleteCheck.Checked = Settings.GeneralConfirmDelete;
		AlwayTopCheck.Checked = Settings.GeneralAlwaysTop;
		UpdateNotifyCheck.Checked = Settings.GeneralUpdateNotify;
		CacheSizeValue.Value = Settings.MaxPageCache;
		ExternalRunText.Text = Settings.ExternalRun;
		ReloadExternalExitCheck.Checked = Settings.ReloadAfterExternal;

		// 로캘
		var lcl = _locales.ToList().IndexOf(Settings.Language) + 1;
		LocalesList.SelectedIndex = lcl;
	}

	private void DoOkButton_Click(object sender, EventArgs e)
	{

	}

	public DialogResult ShowDialog(IWin32Window owner, int dummy)
	{
		_ = dummy;

		Opacity = 0;

		return ShowDialog(owner);
	}

	#region 기본
	private void RunOnceCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralRunOnce = RunOnceCheck.Checked;
	}

	private void EscExitCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralEscExit = EscExitCheck.Checked;
	}

	private void UseWinNoifyCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralUseWinNotify = UseWinNoifyCheck.Checked;
	}

	private void UseMagneticCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralUseMagnetic = UseMagneticCheck.Checked;
	}

	private void ConfirmDeleteCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralConfirmDelete = ConfirmDeleteCheck.Checked;
	}

	private void AlwayTopCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralAlwaysTop = AlwayTopCheck.Checked;
	}

	private void UpdateNotifyCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralUpdateNotify = UpdateNotifyCheck.Checked;
	}

	private void ExternalRunButton_Click(object sender, EventArgs e)
	{
		var dlg = new OpenFileDialog()
		{
			Title = Locale.Text(120),
			Filter = Locale.Text(119),
			InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
		};

		if (dlg.ShowDialog() == DialogResult.OK)
			ExternalRunText.Text = dlg.FileName;
	}

	private void ReloadExternalExitCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.ReloadAfterExternal = ReloadExternalExitCheck.Checked;
	}
	#endregion // 기본

	private void UseDoubleClickStateCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.MouseUseDoubleClickState = UseDoubleClickStateCheck.Checked;
	}

	private void UseClickToPageCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.MouseUseClickPage = UseClickToPageCheck.Checked;
	}

	private void LocalesList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (LocalesList.SelectedIndex == 0)
			Settings.Language = string.Empty;
		else
		{
			if (LocalesList.SelectedItem is string s)
				Settings.Language = s;
		}
	}
}

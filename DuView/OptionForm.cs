using DuView.Properties;

namespace DuView;

public partial class OptionForm : Form, ILocaleTranspose
{
	private readonly BadakFormWorker _bfw;
	private readonly IEnumerable<string> _locales = Locale.GetLocaleList();

	#region 컨스트럭터랑 폼 이벤트
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

		// 크레디트
		foreach (var cs in Resources.CreditsWithSeosi.Split("\\n"))
			CreditScroll.Items.Add(cs.Length != 0 ? cs : " ");

		LocaleTranspose();
	}

	private readonly int[] c_password_usage_items = { 2445, 2446, 2447, 2448, 2449 };

	private void OptionForm_Load(object sender, EventArgs e)
	{
		// 보안
		PasswordUsageList.BeginUpdate();
		PasswordUsageList.Items.Clear();
		foreach (var c in c_password_usage_items)
			PasswordUsageList.Items.Add(Locale.Text(c));
		PasswordUsageList.EndUpdate();

		// 로캘
		LocalesList.BeginUpdate();
		LocalesList.Items.Clear();
		LocalesList.Items.Add(Locale.Text(126));
		foreach (var l in _locales)
			LocalesList.Items.Add(l);
		LocalesList.EndUpdate();
	}

	public void LocaleTranspose()
	{
		ToolBox.LocaleTextOnControl(this);
	}

	private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		// 기본
		Settings.MaxPageCache = (int)CacheSizeValue.Value;

		if (ExternalRunText.TextLength > 0)
			Settings.ExternalRun = ExternalRunText.Text;

		if (ExternalBrowserText.TextLength > 0)
			Settings.FirefoxRun = ExternalBrowserText.Text;

		// 보안
		Settings.PassCode = PasswordText.Text;
		var passusages = (from int ul in PasswordUsageList.SelectedIndices select (Types.PassCodeUsage)ul).ToList();
		Settings.CommitPassUsage(passusages);

		// 일단 저장할까
		Settings.SaveSettings();
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
		// 일반
		RunOnceCheck.Checked = Settings.GeneralRunOnce;
		EscExitCheck.Checked = Settings.GeneralEscExit;
		UseMagneticCheck.Checked = Settings.GeneralUseMagnetic;
		ConfirmDeleteCheck.Checked = Settings.GeneralConfirmDelete;
		AlwayTopCheck.Checked = Settings.GeneralAlwaysTop;
		UpdateNotifyCheck.Checked = Settings.GeneralUpdateNotify;
		ExtendedRenamerCheck.Checked = Settings.ExtendedRenamer;
		CacheSizeValue.Value = Settings.MaxPageCache;
		ExternalRunText.Text = Settings.ExternalRun;
		ReloadExternalExitCheck.Checked = Settings.ReloadAfterExternal;
		ExternalBrowserText.Text = Settings.FirefoxRun;

		// 키보드/마우스
		UseClickToPageCheck.Checked = Settings.MouseUseClickPage;
		UseDoubleClickStateCheck.Checked = Settings.MouseUseDoubleClickState;

		// 보안
		UsePasswordCheck.Checked = Settings.UsePassCode;
		PasswordText.Text = Settings.PassCode;
		PasswordText.Enabled = Settings.UsePassCode;
		PasswordUsageList.Enabled = Settings.UsePassCode;
		foreach (var u in Settings.GetPassUsageArray())
			PasswordUsageList.SelectedIndices.Add((int)u);

		// 로캘
		var lcl = _locales.ToList().IndexOf(Settings.Language) + 1;
		LocalesList.SelectedIndex = lcl;
	}

	private void DoOkButton_Click(object sender, EventArgs e)
	{

	}

	public void ShowDialog(IWin32Window owner, int dummy)
	{
		_ = dummy;

		Opacity = 0;

		ShowDialog(owner);
	}
	#endregion

	#region 탭: 일반
	private void RunOnceCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralRunOnce = RunOnceCheck.Checked;
	}

	private void EscExitCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.GeneralEscExit = EscExitCheck.Checked;
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

	private void FirefoxRunButton_Click(object sender, EventArgs e)
	{
		var dlg = new OpenFileDialog()
		{
			Title = Locale.Text(120),
			Filter = Locale.Text(119),
			InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
		};

		if (dlg.ShowDialog() == DialogResult.OK)
			ExternalBrowserText.Text=dlg.FileName;
	}

	private void ExtendedRenamerCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.ExtendedRenamer = ExtendedRenamerCheck.Checked;
	}
	#endregion // 기본

	#region 탭: 키보드/마우스
	private void UseDoubleClickStateCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.MouseUseDoubleClickState = UseDoubleClickStateCheck.Checked;
	}

	private void UseClickToPageCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.MouseUseClickPage = UseClickToPageCheck.Checked;
	}
	#endregion

	#region 탭: 보안
	private void UsePasswordCheck_CheckedChanged(object sender, EventArgs e)
	{
		Settings.UsePassCode = UsePasswordCheck.Checked;
		PasswordText.Enabled = UsePasswordCheck.Checked;
		PasswordUsageList.Enabled = UsePasswordCheck.Checked;
	}

	private void PasswordText_TextChanged(object sender, EventArgs e)
	{
		// 헐 필요없나
	}

	private void PasswordUsageList_SelectedIndexChanged(object sender, EventArgs e)
	{
		// 헐 필요없나
	}
	#endregion

	#region 탭: 언어
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
	#endregion
}

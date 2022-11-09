namespace DuView;

public partial class RenameForm : Form
{
	public string Filename { get; private set; } = string.Empty;

	private readonly BadakFormWorker _bfw;

	public RenameForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};
	}

	private void RenameForm_Load(object sender, EventArgs e)
	{
		if (Settings.RenameOpenNext)
			OpenNextRadio.Checked = true;
		else
			OpenAgainRadio.Checked = true;

		ActiveControl = RenameText;

		//
		ToolBox.LocaleTextOnControl(this);
	}

	private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		Filename = RenameText.Text;
	}

	private void RenameForm_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void RenameForm_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void RenameForm_MouseMove(object sender, MouseEventArgs e)
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

	private void DoOkButton_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
	}

	public DialogResult ShowDialog(IWin32Window owner, string filename)
	{
		Opacity = 0;

		Filename = (string)filename.Clone();

		CurrentText.Text = filename;
		RenameText.Text = Filename;

		return ShowDialog(owner);
	}

	private void OpenNextRadio_CheckedChanged(object sender, EventArgs e)
	{
		Settings.RenameOpenNext = true;
	}

	private void OpenAgainRadio_CheckedChanged(object sender, EventArgs e)
	{
		Settings.RenameOpenNext = false;
	}
}

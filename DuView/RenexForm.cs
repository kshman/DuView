using System.Text;

namespace DuView;

public partial class RenexForm : Form, ILocaleTranspose
{
	public string Filename { get; private set; } = string.Empty;

	private readonly BadakFormWorker _bfw;

	private string? _extension;

	public RenexForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};
	}

	private void RenexForm_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Settings.FirefoxRun))
			SearchButton.Enabled = false;

		ActiveControl = TitleText;

		LocaleTranspose();
	}

	public void LocaleTranspose()
	{
		ToolBox.LocaleTextOnControl(this);
	}

	private void RenexForm_FormClosing(object sender, FormClosingEventArgs e)
	{

	}

	private void RenexForm_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void RenexForm_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void RenexForm_MouseMove(object sender, MouseEventArgs e)
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

	private void DoOkReopenButton_Click(object sender, EventArgs e)
	{
		Settings.RenameOpenNext = false;
		DialogResult = DialogResult.OK;
	}

	private void DoOkNextButton_Click(object sender, EventArgs e)
	{
		Settings.RenameOpenNext = true;
		DialogResult = DialogResult.OK;
	}

	public DialogResult ShowDialog(IWin32Window owner, string filename)
	{
		Opacity = 0;

		Filename = (string)filename.Clone();

		OriginalNameLabel.Text = filename;
		ParseFilename(filename);

		return ShowDialog(owner);
	}

	private void TitleText_TextChanged(object sender, EventArgs e)
	{
		MakeFilename();
	}

	private void SearchButton_Click(object sender, EventArgs e)
	{
		var name = OriginalNameLabel.Text;
		if (string.IsNullOrEmpty(name))
			return;

		var dot = name.LastIndexOf('.');
		if (dot > 0)
			name = name[..dot];

		var keyword = name.Replace('"', '\'');

		var ps = new System.Diagnostics.Process();
		ps.StartInfo.FileName = Settings.FirefoxRun;
		// -search 키워드
		// -private-window --new-tab
		ps.StartInfo.Arguments = $"-search \"{keyword}\" --new-tab --foreground";
		ps.StartInfo.UseShellExecute = false;
		ps.StartInfo.CreateNoWindow = true;
		ps.EnableRaisingEvents = false;
		ps.Start();
	}

	//
	private void MakeFilename()
	{
		var sb = new StringBuilder();

		// 작가
		if (AuthorText.TextLength > 0)
			sb.AppendFormat("[{0}] ", AuthorText.Text);

		// 책이름
		if (TitleText.TextLength > 0)
			sb.Append(TitleText.Text);

		// 순번
		if (IndexText.TextLength > 0)
			sb.AppendFormat(" {0}", IndexText.Text);

		// 추가 정보
		if (AdditionalText.TextLength > 0)
			sb.AppendFormat(" ({0})", AdditionalText.Text);

		// 확장자
		if (_extension?.Length > 0)
			sb.Append(_extension);

		Filename = sb.ToString();
		BookNameLabel.Text = Filename;
	}

	//
	private void ParseFilename(string filename)
	{
		string ws;
		int e, n, l;

		// 확장자
		e = filename.LastIndexOf('.');
		if (e < 0)
			ws = filename;
		else
		{
			_extension = filename[e..];
			ws = filename[..e];
		}

		try
		{
			// 작가
			n = ws.IndexOf('[');
			if (n >= 0)
			{
				l = ws.IndexOf(']');
				if (l > n)
				{
					AuthorText.Text = ws.Substring(n + 1, l - n - 1).Trim();
					ws = ws[(l + 1)..].TrimStart();
				}
			}

			// 추가
			n = ws.LastIndexOf('(');
			if (n >= 0)
			{
				l = ws.LastIndexOf(')');
				if (l > n)
				{
					AdditionalText.Text = ws.Substring(n + 1, l - n - 1).Trim();
					ws = ws[..(n - 1)].Trim();
				}
			}

			// 순번
			n = ws.LastIndexOf(' ');
			if (n >= 0)
			{
				var s = ws[(n + 1)..];
				if (int.TryParse(s, out l))
				{
					IndexText.Text = ws[(n + 1)..].Trim();
					ws = ws[..n].Trim();
				}
			}

			// 책이름
			TitleText.Text = ws.Trim();
		}
		catch
		{
			// 오류나면 그냥 통짜로
			AuthorText.Text = string.Empty;
			TitleText.Text = string.Empty;
			IndexText.Text = string.Empty;
			AdditionalText.Text = string.Empty;

			if (e < 0)
				TitleText.Text = filename;
			else
				TitleText.Text = filename[..e];
		}
	}
}

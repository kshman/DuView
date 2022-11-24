namespace DuView;

public partial class MoveForm : Form, ILocaleTranspose
{
	public string Destination { get; private set; }
	public string Filename { get; private set; } = string.Empty;

	private readonly BadakFormWorker _bfw;
	private static string s_last_location = string.Empty;

	public MoveForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};

		Destination = Settings.RecentlyPath;
		ControlDu.DoubleBuffered(MoveList, true);
	}

	private void MoveForm_Load(object sender, EventArgs e)
	{
		LocaleTranspose();

		RefreshMoveList();
	}

	public void LocaleTranspose()
	{
		ToolBox.LocaleTextOnControl(this);
	}

	private void MoveForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DialogResult == DialogResult.OK)
		{
			var s = DestLocationText.Text;
			if (!Directory.Exists(s))
			{
				e.Cancel = true;
				return;
			}

			s_last_location = s;
			Destination = s;
			Filename = Path.Combine(s, Filename);
		}
	}

	private void MoveForm_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void MoveForm_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void MoveForm_MouseMove(object sender, MouseEventArgs e)
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

	public DialogResult ShowDialog(IWin32Window owner, string filename)
	{
		Opacity = 0;

		Filename = filename;

		return ShowDialog(owner);
	}

	private void OkDoItButton_Click(object sender, EventArgs e)
	{
	}

	private void NoCancelButton_Click(object sender, EventArgs e)
	{
	}

	private void BrowseButton_Click(object sender, EventArgs e)
	{
		var loc = DestLocationText.Text;
		if (string.IsNullOrEmpty(loc) || !Directory.Exists(loc))
		{
			if (Directory.Exists(Settings.RecentlyPath))
				loc = Settings.RecentlyPath;
			else
				loc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		if (!OpenFolderDialog(loc, out var folder))
			return;

		DestLocationText.Text = folder;
	}

	private void DestLocationText_Enter(object sender, EventArgs e)
	{
		DestLocationText.SelectAll();
	}

	private void MoveMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		bool enable = MoveList.SelectedItems.Count == 1;
		MoveDeleteMenuItem.Enabled = enable;
		MoveChangeMenuItem.Enabled = enable;
	}

	private void MoveList_Resize(object sender, EventArgs e)
	{
		MoveList.Columns[0].Width = -1;
	}

	private void MoveList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		DestLocationText.Text = MoveList.SelectedItems[0].Text;
	}

	private void MoveAddMenuItem_Click(object sender, EventArgs e)
	{
		if (!OpenFolderDialog(DestLocationText.Text, out var folder, true))
			return;

		if (EnsureMoveItem(folder))
			return;

		Settings.AddMoveLocation(folder);

		var index = MoveList.Items.Count;
		var li = new ListViewItem(new[]
		{
			folder,
			index.ToString(),
		}, 0);
		li.Tag = index;
		MoveList.Items.Add(li);

		MoveList.Columns[0].Width = -1;

		EnsureMoveItem(index);
	}

	private void MoveChangeMenuItem_Click(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		var loc = MoveList.SelectedItems[0].Text;
		if (!OpenFolderDialog(loc, out var folder))
			return;

		if (EnsureMoveItem(folder))
			return;

		var index = (int)MoveList.SelectedItems[0].Tag;
		if (index < MoveList.Items.Count)
		{
			Settings.SetMoveLocation(index, folder);
			MoveList.SelectedItems[0].Text = folder;
			EnsureMoveItem(index);
		}
	}

	private void MoveDeleteMenuItem_Click(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		var index = (int)MoveList.SelectedItems[0].Tag;
		if (index < MoveList.Items.Count)
		{
			Settings.DeleteMoveLocation(index);
			RefreshMoveList();
		}
	}

	private void RefreshMoveList()
	{
		// 먼저 목록 만들고
		MoveList.BeginUpdate();
		MoveList.Items.Clear();
		var lst = Settings.GetMoveLocations();
		for (int i = 0; i < lst.Length; i++)
		{
			var li = new ListViewItem(new[]
			{
				lst[i],
				i.ToString(),
			}, 0);
			li.Tag = i;
			MoveList.Items.Add(li);
		}
		MoveList.EndUpdate();

		MoveList.Columns[0].Width = -1;

		// 선택
		if (!EnsureMoveItem(s_last_location) && lst.Length > 0)
			EnsureMoveItem(0);
	}

	private bool EnsureMoveItem(string loc)
	{
		if (!string.IsNullOrEmpty(loc))
		{
			for (int i = 0; i < MoveList.Items.Count; i++)
			{
				if (loc.Equals(MoveList.Items[i].Text))
				{
					EnsureMoveItem(i);
					return true;
				}
			}
		}

		return false;
	}

	private void EnsureMoveItem(int index)
	{
		if (index < MoveList.Items.Count)
		{
			var item = MoveList.Items[index];

			item.Focused = true;
			item.Selected = true;
			item.EnsureVisible();

			DestLocationText.Text = item.Text;
		}
	}

	private static bool OpenFolderDialog(string path, out string ret, bool shownewfolder = false)
	{
		FolderBrowserDialog dlg = new()
		{
			ShowNewFolderButton = shownewfolder,
			SelectedPath = path,
		};

		if (dlg.ShowDialog() != DialogResult.OK)
		{
			ret = path;
			return false;
		}

		ret = dlg.SelectedPath;
		return true;
	}
}

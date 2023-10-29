namespace DuView;

public partial class MoveForm : Form, ILocaleTranspose
{
	public string Filename { get; private set; } = string.Empty;

	private static string s_last_location = string.Empty;

	private readonly BadakFormWorker _bfw;
	private bool _child_focus;

	private bool _save_settings;

	public MoveForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};

		ControlDu.DoubleBuffered(MoveList, true);

		LocaleTranspose();
	}

	private void MoveForm_Load(object sender, EventArgs e)
	{
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
			Filename = Path.Combine(s, Filename);
		}

		if (!_save_settings) 
			return;

		Settings.KeepMoveLocation();
		Settings.SaveSettings();
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

	private void MoveForm_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
			case Keys.Escape:
			{
				if (!_child_focus)
				{
					NoCancelButton_Click(sender, e);
					e.SuppressKeyPress = true;
				}
				break;
			}
			case Keys.Enter:
			{
				if (!_child_focus)
				{
					OkDoItButton_Click(sender, e);
					e.SuppressKeyPress = true;
				}
				break;
			}
		}
	}

	private void OkDoItButton_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		Close();
	}

	private void NoCancelButton_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void BrowseButton_Click(object sender, EventArgs e)
	{
		var loc = DestLocationText.Text;
		if (string.IsNullOrEmpty(loc) || !Directory.Exists(loc))
		{
			loc = Directory.Exists(Settings.RecentlyPath) ?
				Settings.RecentlyPath :
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
		var enable = MoveList.SelectedItems.Count == 1;
		MoveDeleteMenuItem.Enabled = enable;
		MoveChangeMenuItem.Enabled = enable;
		MoveSetAliasMenuItem.Enabled = enable;
	}

	private void MoveList_Resize(object sender, EventArgs e)
	{
		//MoveList.Columns[0].Width = -1;
	}

	private void MoveList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		DestLocationText.Text = MoveList.SelectedItems[0].SubItems[2].Text;
	}

	private void MoveList_DoubleClick(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		OkDoItButton_Click(sender, e);
	}

	private void MoveList_ItemReordered(object? sender, ItemReorderDragEventArgs e)
	{
		if (e.BeforeIndex < 0 || e.AfterIndex < 0 || e.BeforeIndex == e.AfterIndex)
			return;

		Settings.ReIndexMoveLocation(e.BeforeIndex, e.AfterIndex);
		RefreshMoveList(false);
		EnsureMoveItem(e.AfterIndex > e.BeforeIndex ? e.AfterIndex - 1 : e.AfterIndex);

		_save_settings = true;
	}

	private void MoveList_SubItemClick(object sender, SubItemEventArgs e)
	{
		/*
		if (e.SubItemIndex != 1)
			return;

		// 여기서 안하고 메뉴로 한다
		//MoveList.EditSubItem(e.Item, e.SubItemIndex);
		*/
	}

	private void MoveList_SubItemBeginEdit(object sender, SubItemEventArgs e)
	{
		_child_focus = true;
	}

	private void MoveList_SubItemEndEdit(object sender, SubItemEndEditEventArgs e)
	{
		_child_focus = false;

		if (e.Cancel)
			return;

		if (e.SubItemIndex != 1)
			return;

		var item = e.Item;
		var index = Convert.ToInt32(item.Text) - 1;
		Settings.SetMoveLocation(index, item.SubItems[2].Text, item.SubItems[1].Text);

		_save_settings = true;
	}

	private void MoveAddMenuItem_Click(object sender, EventArgs e)
	{
		if (!OpenFolderDialog(DestLocationText.Text, out var folder, true))
			return;

		if (EnsureMoveItem(folder))
			return;

		var di = new DirectoryInfo(folder);
		Settings.AddMoveLocation(folder, di.Name);

		var index = MoveList.Items.Count;
		var li = new ListViewItem(new[]
		{
			(index + 1).ToString(),
			di.Name,
			folder,
		}, 0);
		MoveList.Items.Add(li);

		//MoveList.Columns[0].Width = -1;

		EnsureMoveItem(index);

		_save_settings = true;
	}

	private void MoveChangeMenuItem_Click(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		var loc = MoveList.SelectedItems[0].SubItems[2].Text;
		if (!OpenFolderDialog(loc, out var folder))
			return;

		if (EnsureMoveItem(folder))
			return;

		var index = Convert.ToInt32(MoveList.SelectedItems[0].Text) - 1;
		if (index >= MoveList.Items.Count)
			return;

		var di = new DirectoryInfo(folder);
		Settings.SetMoveLocation(index, folder, di.Name);
		MoveList.SelectedItems[0].SubItems[2].Text = folder;
		EnsureMoveItem(index);

		_save_settings = true;
	}

	private void MoveDeleteMenuItem_Click(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		var index = Convert.ToInt32(MoveList.SelectedItems[0].Text) - 1;
		if (index >= MoveList.Items.Count)
			return;

		Settings.DeleteMoveLocation(index);
		RefreshMoveList();

		_save_settings = true;
	}

	private void MoveSetAliasMenuItem_Click(object sender, EventArgs e)
	{
		if (MoveList.SelectedItems.Count != 1)
			return;

		MoveList.EditSubItem(MoveList.SelectedItems[0], 1);
	}

	private void RefreshMoveList(bool ensure = true)
	{
		// 먼저 목록 만들고
		MoveList.BeginUpdate();
		MoveList.Items.Clear();
		var lst = Settings.GetMoveLocations();
		for (var i = 0; i < lst.Length; i++)
		{
			var li = new ListViewItem(new[]
			{
				(i + 1).ToString(),
				lst[i].Value,
				lst[i].Key,
			}, 0);
			MoveList.Items.Add(li);
		}
		MoveList.EndUpdate();

		//MoveList.Columns[2].Width = -1;

		// 선택
		if (ensure && !EnsureMoveItem(s_last_location) && lst.Length > 0)
			EnsureMoveItem(0);
	}

	private bool EnsureMoveItem(string loc)
	{
		if (string.IsNullOrEmpty(loc))
			return false;

		for (var i = 0; i < MoveList.Items.Count; i++)
		{
			if (!loc.Equals(MoveList.Items[i].SubItems[2].Text))
				continue;
			EnsureMoveItem(i);
			return true;
		}

		return false;
	}

	private void EnsureMoveItem(int index)
	{
		if (index >= MoveList.Items.Count)
			return;

		var item = MoveList.Items[index];
		item.Focused = true;
		item.Selected = true;
		item.EnsureVisible();

		DestLocationText.Text = item.SubItems[2].Text;
	}

	private static bool OpenFolderDialog(string path, out string ret, bool showNewFolder = false)
	{
		FolderBrowserDialog dlg = new()
		{
			ShowNewFolderButton = showNewFolder,
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

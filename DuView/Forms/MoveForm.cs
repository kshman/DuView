// ReSharper disable MissingXmlDoc

using System.ComponentModel;

namespace DuView.Forms;

public partial class MoveForm : Form
{
    // 마지막 선택
    private static string s_last_location = string.Empty;

    //
    private bool _child_focus;
    private bool _save_settings;

    [DefaultValue("")] public string Filename { get; private set; } = string.Empty;

    public MoveForm()
    {
        InitializeComponent();

        DuControl.DoubleBuffered(MoveList, true);
    }

    private void MoveForm_Load(object sender, EventArgs e)
    {
        RefreshList();
    }

    private void MoveForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
        {
            var s = DestText.Text;
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

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        DuForm.EffectAppear(this);
    }

    private void MoveForm_KeyDown(object sender, KeyEventArgs e)
    {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (e.KeyCode)
        {
            case Keys.Escape:
                if (!_child_focus)
                {
                    e.SuppressKeyPress = true;
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                break;
            case Keys.Enter:
                if (!_child_focus)
                {
                    e.SuppressKeyPress = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                break;
        }
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
        var loc = DestText.Text;
        if (loc.EmptyString() || !Directory.Exists(loc))
        {
            loc = Directory.Exists(Settings.RecentlyPath) ?
                Settings.RecentlyPath :
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        if (!OpenFolderDialog(loc, out var folder))
            return;

        DestText.Text = folder;
    }

    private void DestText_Enter(object sender, EventArgs e)
    {
        DestText.SelectAll();
    }

    private void MoveList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MoveList.SelectedItems.Count != 1)
            return;

        DestText.Text = MoveList.SelectedItems[0].SubItems[2].Text;
    }

    private void MoveList_DoubleClick(object sender, EventArgs e)
    {
        if (MoveList.SelectedItems.Count != 1)
            return;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void MoveList_ItemReordered(object sender, ItemReorderDragEventArgs e)
    {
        if (e.BeforeIndex < 0 || e.AfterIndex < 0 || e.BeforeIndex == e.AfterIndex)
            return;

        Settings.IndexingMoveLocation(e.BeforeIndex, e.AfterIndex);
        RefreshList(false);
        EnsureMoveItem(e.AfterIndex > e.BeforeIndex ? e.AfterIndex - 1 : e.AfterIndex);

        _save_settings = true;
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

    private void MoveMenu_Opening(object sender, CancelEventArgs e)
    {
        var enable = MoveList.SelectedItems.Count == 1;
        MoveDeleteMenuItem.Enabled = enable;
        MoveChangeMenuItem.Enabled = enable;
        MoveAliasMenuItem.Enabled = enable;
    }

    private void MoveAddMenuItem_Click(object sender, EventArgs e)
    {
        if (!OpenFolderDialog(DestText.Text, out var folder, true))
            return;

        if (EnsureMoveItem(folder))
            return;

        var di = new DirectoryInfo(folder);
        Settings.AddMoveLocation(folder, di.Name);

        var index = MoveList.Items.Count;
        var li = new ListViewItem([
            (index + 1).ToString(),
            di.Name,
            folder
        ], 0);
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

    private void MoveAliasMenuItem_Click(object sender, EventArgs e)
    {
        if (MoveList.SelectedItems.Count != 1)
            return;

        MoveList.EditSubItem(MoveList.SelectedItems[0], 1);
    }

    private void MoveDeleteMenuItem_Click(object sender, EventArgs e)
    {
        if (MoveList.SelectedItems.Count != 1)
            return;

        var index = Convert.ToInt32(MoveList.SelectedItems[0].Text) - 1;
        if (index >= MoveList.Items.Count)
            return;

        Settings.DeleteMoveLocation(index);
        RefreshList();

        _save_settings = true;
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

        DestText.Text = item.SubItems[2].Text;
    }

    // 목록 갱신
    private void RefreshList(bool ensure = true)
    {
        MoveList.BeginUpdate();
        MoveList.Items.Clear();
        var lst = Settings.GetMoveLocations();
        for (var i = 0; i < lst.Length; i++)
        {
            var li = new ListViewItem([
                (i + 1).ToString(),
                lst[i].Value,
                lst[i].Key
            ], 0);
            MoveList.Items.Add(li);
        }
        MoveList.EndUpdate();

        //MoveList.Columns[2].Width = -1;

        // 선택
        if (ensure && !EnsureMoveItem(s_last_location) && lst.Length > 0)
            EnsureMoveItem(0);

        ActiveControl = MoveList;
    }

    //  폴더 다이얼로그 열기
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

    // 다이얼로그 열기
    public DialogResult ShowDialog(IWin32Window owner, string filename)
    {
        Opacity = 0;

        Filename = filename;

        return ShowDialog(owner);
    }
}

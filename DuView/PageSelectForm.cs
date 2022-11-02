namespace DuView;

public partial class PageSelectForm : Form
{
	public int SelectedPage { get; private set; }

	private readonly BadakFormWorker _bfw;

	public PageSelectForm()
	{
		InitializeComponent();

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton)
		{
			MoveTopToMaximize = false,
			BodyAsTitle = true,
		};

		ControlDu.DoubleBuffered(PageList, true);
	}

	private void PageSelectForm_Load(object sender, EventArgs e)
	{
		//
		ToolBox.GlobalizationLocaleText(this);

	}

	private void PageSelectForm_FormClosing(object sender, FormClosingEventArgs e)
	{

	}

	private void PageSelectForm_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void PageSelectForm_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void PageSelectForm_MouseMove(object sender, MouseEventArgs e)
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

	private void SelectToCurrentPage()
	{
		if (PageList.SelectedItems.Count == 1)
		{
			var li = PageList.SelectedItems[0];
			SelectedPage = (int)li.Tag;
		}
	}

	private void DoOkButton_Click(object sender, EventArgs e)
	{
		SelectToCurrentPage();
	}

	private void PageList_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (PageList.SelectedItems.Count == 1)
		{
			SelectToCurrentPage();

			DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void PageList_Resize(object sender, EventArgs e)
	{
		PageList.Columns[0].Width = -2;
	}

	private void PageSelectForm_Shown(object sender, EventArgs e)
	{
		int page;

		if (SelectedPage < 0)
			page = 0;
		else if (SelectedPage >= PageList.Items.Count)
			page = PageList.Items.Count - 1;
		else
			page = SelectedPage;

		PageList.Focus();
		PageList.Items[page].Focused = true;
		PageList.Items[page].Selected = true;

		var ensure = page + 12;
		if (ensure > PageList.Items.Count - 1)
			ensure = PageList.Items.Count - 1;
		PageList.Items[ensure].EnsureVisible();

		ActiveControl = PageList;
	}

	public void SetBook(BookBase book)
	{
		PageInfoLabel.Text = $"{Locale.Text(2225)} {book.TotalPage}";

		PageList.BeginUpdate();
		PageList.Items.Clear();

		var entries = book.GetEntriesInfo();
		var n = 0;
		foreach (var e in entries)
		{
			var li = new ListViewItem(new[]
			{
				e.Name ?? string.Empty,	// name이 널일리가 없는데 c#이 용서하지 않는다
				e.DateTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
				ToolBox.SizeToString(e.Size)
			})
			{
				Tag = n++
			};
			PageList.Items.Add(li);
		}

		PageList.EndUpdate();
	}

	public void ResetBook()
	{
		PageInfoLabel.Text = Locale.Text(2226);

		PageList.Items.Clear();
	}

	public DialogResult ShowDialog(IWin32Window owner, int selected_page)
	{
		Opacity = 0;
		SelectedPage = selected_page;

		return ShowDialog(owner);
	}
}


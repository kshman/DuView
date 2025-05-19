// ReSharper disable MissingXmlDoc

using System.ComponentModel;

namespace DuView.Forms;

/// <summary>
/// 페이지 선택 폼
/// </summary>
public partial class PageForm : Form
{
    /// <summary>
    /// 선택한 페이지
    /// </summary>
    [DefaultValue(0)]
    public int SelectedPage { get; private set; }

    public PageForm()
    {
        InitializeComponent();

        DuControl.DoubleBuffered(PageList, true);
    }

    /// <inheritdoc/>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        DuForm.EffectAppear(this);
    }

    // 현재 페이지 선택
    private void MakeSelectedPage()
    {
        if (PageList.SelectedItems.Count != 1)
            return;
        var li = PageList.SelectedItems[0];
        SelectedPage = (int)(li.Tag ?? -1);
    }

    private void DoOkButton_Click(object sender, EventArgs e)
    {
        MakeSelectedPage();
    }

    private void PageList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (PageList.SelectedItems.Count != 1)
            return;
        MakeSelectedPage();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void PageForm_Shown(object sender, EventArgs e)
    {
        var page = SelectedPage < 0 ? 0 : SelectedPage >= PageList.Items.Count ? PageList.Items.Count - 1 : SelectedPage;
        PageList.Focus();
        PageList.Items[page].Focused = true;
        PageList.Items[page].Selected = true;

        var ensure = page + 12;
        if (ensure >= PageList.Items.Count - 1)
            ensure = PageList.Items.Count - 1;
        PageList.Items[ensure].EnsureVisible();

        ActiveControl = PageList;
    }

    /// <summary>
    /// 책 설정
    /// </summary>
    /// <param name="book"></param>
    public void SetBook(BookBase book)
    {
        PageInfoLabel.Text = $@"{Resources.TotalPagesColon} {book.TotalPage}";

        PageList.BeginUpdate();
        PageList.Items.Clear();

        var entries = book.GetEntriesInfo();
        var n = 0;
        foreach (var e in entries)
        {
            var li = new ListViewItem(
                [
                    e.Name ?? string.Empty,
                    e.DateTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    Doumi.SizeToString(e.Size)
                ])
            { Tag = n++ };
            PageList.Items.Add(li);
        }

        PageList.EndUpdate();
    }

    /// <summary>
    /// 책 초기화
    /// </summary>
    public void ResetBook()
    {
        PageInfoLabel.Text = Resources.NoBook;
        PageList.Items.Clear();
    }

    /// <summary>
    /// 다이얼로그 표시
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public DialogResult ShowDialog(IWin32Window owner, int page)
    {
        Opacity = 0;
        SelectedPage = page;
        return ShowDialog(owner);
    }
}

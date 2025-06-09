namespace DgView.Forms;

/// <summary>
/// 페이지를 선택할 수 있는 대화상자 창을 제공합니다.
/// 파일 목록, 날짜, 크기 등의 정보를 표시하며, 사용자가 원하는 페이지를 선택할 수 있습니다.
/// </summary>
public class PageDialog : Window
{
    private readonly Label _pageInfoLabel;
    private readonly TreeView _pageList;
    private readonly ListStore _listStore;

    private bool _result;

	/// <summary>
	/// 선택된 페이지의 인덱스를 가져옵니다.
	/// </summary>
	public int SelectedPage { get; private set; }

    /// <summary>
    /// <see cref="PageDialog"/> 클래스의 새 인스턴스를 초기화합니다.
    /// </summary>
    public PageDialog() :
	    base("페이지 선택")
    {
		#region 디자인
		// 윈도우
		SetDefaultSize(434, 511);
		Resizable = false;
		SkipTaskbarHint = true;

		// ESC 키로 창 닫기(감추기)
		KeyPressEvent += (_, args) =>
		{
			if (args.Event.Key == GdkKey.Escape)
			{
				_result = false;
				Hide();
			}
		};

		// 상단 정보 라벨
		_pageInfoLabel = new Label("페이지")
		{
			Halign = Align.Start,
			Margin = 8
		};

		// 트리뷰
		_pageList = new TreeView();
		_pageList.HeadersVisible = true;
		_pageList.Selection.Mode = SelectionMode.Single;
		_pageList.RowActivated += (_, _) => SetResultAndHide(true);
		_pageList.Margin = 8;

		// 트리뷰를 스크롤로 감싸기
		var scrolled = new ScrolledWindow();
		scrolled.Add(_pageList);
		scrolled.SetSizeRequest(400, 350); // 적당한 크기로 제한

		// 트리뷰 컬럼
		var fileNameCol = new TreeViewColumn { Title = "파일 이름" };
		var dateCol = new TreeViewColumn { Title = "날짜" };
		var sizeCol = new TreeViewColumn { Title = "크기" };
		var fileNameCell = new CellRendererText();
		var dateCell = new CellRendererText();
		var sizeCell = new CellRendererText();
		fileNameCol.PackStart(fileNameCell, true);
		dateCol.PackStart(dateCell, true);
		sizeCol.PackStart(sizeCell, true);
		fileNameCol.AddAttribute(fileNameCell, "text", 0);
		dateCol.AddAttribute(dateCell, "text", 1);
		sizeCol.AddAttribute(sizeCell, "text", 2);
		_pageList.AppendColumn(fileNameCol);
		_pageList.AppendColumn(dateCol);
		_pageList.AppendColumn(sizeCol);
		_listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(int));
		_pageList.Model = _listStore;

		// 버튼, 윈도우라 만들어줘야 함
		var okButton = new Button(Stock.JumpTo);
		okButton.Halign = Align.End;
		okButton.Margin = 8;
		okButton.Clicked += (_, _) => SetResultAndHide(true);

		var cancelButton = new Button(Stock.Cancel);
		cancelButton.Halign = Align.Start;
		cancelButton.Margin = 8;
		cancelButton.Clicked += (_, _) => SetResultAndHide(false, false);

		var buttonBox = new Box(Orientation.Horizontal, 8);
		buttonBox.PackStart(cancelButton, false, false, 0);
		buttonBox.PackEnd(okButton, false, false, 0);

		// 레이아웃
		var vbox = new Box(Orientation.Vertical, 8);
		vbox.PackStart(_pageInfoLabel, false, false, 0);
		vbox.PackStart(scrolled, true, true, 0);
		vbox.PackEnd(buttonBox, false, false, 0);
		Add(vbox);
		#endregion

		_pageList.GrabFocus();
        ShowAll();
        Hide();
    }

    /// <summary>
    /// 지정한 <see cref="BookBase"/> 객체의 페이지 정보를 대화상자에 설정합니다.
    /// </summary>
    /// <param name="book">책 정보 객체</param>
    public void SetBook(BookBase book)
    {
        _pageInfoLabel.Text = $"총 페이지: {book.TotalPage}";
        _listStore.Clear();
        var entries = book.GetEntriesInfo();
        var n = 0;
        foreach (var e in entries)
        {
            _listStore.AppendValues(
                e.Name ?? "<알 수 없음>",
                e.DateTime.ToString("d"),
                Doumi.SizeToString(e.Size),
                n++
            );
        }
    }

    /// <summary>
    /// 대화상자의 책 정보를 초기화(리셋)합니다.
    /// </summary>
    public void ResetBook()
    {
        _pageInfoLabel.Text = "열린 책 없음";
        _listStore.Clear();
	}

    // 결과를 설정하고 창을 숨깁니다.
    private void SetResultAndHide(bool result, bool makeSelectedPage = true)
    {
	    _result = result;
	    if (makeSelectedPage)
		    MakeSelectedPage();
	    Hide();
    }

	// 선택된 페이지를 기준으로 트리뷰의 선택을 갱신합니다.
	// SelectedPage가 -1이면 첫 페이지로, 범위를 벗어나면 마지막 페이지로 설정합니다.
	private void RefreshSelection()
    {
        // 페이지 포커스 및 선택
        var childCount = _listStore.IterNChildren();
        var page = SelectedPage < 0 ? 0 : SelectedPage >= childCount ? childCount - 1 : SelectedPage;
        if (!_listStore.GetIterFirst(out var iter)) 
            return;
        for (var i = 0; i < page; i++)
            _listStore.IterNext(ref iter);
        _pageList.Selection.SelectIter(iter);
        _pageList.GrabFocus();
        // 선택된 항목이 보이도록 스크롤
        var path = _listStore.GetPath(iter);
        _pageList.ScrollToCell(path, null, false, 0, 0);
    }

	// 선택된 페이지를 기준으로 SelectedPage 속성을 설정합니다.
	private void MakeSelectedPage()
    {
        if (!_pageList.Selection.GetSelected(out var model, out var iter)) 
            return;
        var path = model.GetPath(iter);
        SelectedPage = path.Indices[0];
    }

    /// <summary>
    /// 페이지 선택 대화상자를 표시하고, 사용자의 선택 결과를 반환합니다.
    /// </summary>
    /// <param name="parent">부모 창(옵션)</param>
    /// <param name="page">초기 선택할 페이지 인덱스</param>
    /// <returns>사용자가 확인(OK)을 선택하면 true, 취소하면 false를 반환합니다.</returns>
    public bool Run(Window? parent, int page)
    {
        if (parent != null)
            TransientFor = parent;

        SelectedPage = page;
        RefreshSelection();
        
        Modal = true;
        Visible = true;
        Show();
        
        // 메시지 루프: 창이 닫힐 때까지 대기
        while (Visible)
        {
            while (Application.EventsPending())
                Application.RunIteration();
        }
        
        return _result;
    }
}

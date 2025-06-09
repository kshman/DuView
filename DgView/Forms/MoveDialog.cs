using System.Text;

namespace DgView.Forms;

/// <summary>
/// 책 이동을 위한 대화상자를 제공하는 클래스입니다.
/// 사용자는 미리 등록된 위치 목록에서 선택하거나, 새 위치를 추가/수정/삭제할 수 있습니다.
/// </summary>
public class MoveDialog : Dialog
{
	private bool _child_focus;
	private bool _should_save;

	private ResponseType _response = ResponseType.Cancel;

	private readonly Button _browseButton;
	private readonly Entry _destText;
	private readonly TreeView _moveList;
	private readonly ListStore _moveListStore;
	private readonly Menu _moveMenu;
	private readonly MenuItem _moveAddMenuItem;
	private readonly MenuItem _moveChangeMenuItem;
	private readonly MenuItem _moveAliasMenuItem;
	private readonly MenuItem _moveDeleteMenuItem;

	private TreePath? _dragSourcePath;

	/// <summary>
	/// 이동할 최종 파일 이름을 가져옵니다.
	/// </summary>
	public string Filename { get; private set; } = string.Empty;

	/// <summary>
	/// MoveDialog를 생성합니다.
	/// </summary>
	/// <param name="parent">부모 윈도우</param>
	public MoveDialog(Window? parent) :
		base("책 이동하기", parent, DialogFlags.Modal)
	{
		#region 디자인
		// 다이얼로그
		SetDefaultSize(500, 550);
		Resizable = false;
		SkipTaskbarHint = true;
		Icon = ResL.GetBitmap(ResL.IconMove);
		DeleteEvent += MoveDialog_DeleteEvent;
		Response += MoveDialog_Response;
		KeyPressEvent += MoveDialog_KeyPressEvent;

		// 메인 레이아웃
		var mainVBox = new Box(Orientation.Vertical, 0);

		// 타이틀 라벨
		var titleLabel = new Label("이동할 곳을 고르세요")
		{
			Halign = Align.Start,
			MarginStart = 5,
			MarginTop = 5,
			MarginBottom = 5
		};
		titleLabel.StyleContext.AddClass("bold-label");
		mainVBox.PackStart(titleLabel, false, false, 0);

		// 트리뷰
		_moveList = new TreeView();
		_moveListStore = new ListStore(typeof(string), typeof(string), typeof(string));
		_moveList.Model = _moveListStore;
		_moveList.HeadersVisible = true;
		_moveList.Selection.Mode = SelectionMode.Single;
		_moveList.AddEvents((int)Gdk.EventMask.ButtonPressMask);
		_moveList.DragDataGet += MoveList_DragDataGet;
		_moveList.DragDataReceived += MoveList_DragDataReceived;

		// 트리뷰 끌어 놓기
		var dndTarget = new TargetEntry("application/x-tree-row", TargetFlags.Widget, 0);
		_moveList.EnableModelDragSource(Gdk.ModifierType.Button1Mask, [dndTarget], Gdk.DragAction.Move);
		_moveList.EnableModelDragDest([dndTarget], Gdk.DragAction.Move);

		// 컬럼
		var numberTextRenderer = new CellRendererText();
		var numberCol = new TreeViewColumn("번호", numberTextRenderer, "text", 0)
		{
			Sizing = TreeViewColumnSizing.Fixed,
			FixedWidth = 80
		};
		_moveList.AppendColumn(numberCol);

		var aliasTextRenderer = new CellRendererText { Editable = true };
		aliasTextRenderer.Edited += AliasCell_Edited;
		var aliasCol = new TreeViewColumn("별명", aliasTextRenderer, "text", 1)
		{
			Sizing = TreeViewColumnSizing.Fixed,
			FixedWidth = 150
		};
		_moveList.AppendColumn(aliasCol);

		var pathTextRenderer = new CellRendererText();
		var pathCol = new TreeViewColumn("경로", pathTextRenderer, "text", 2)
		{
			Expand = true
		};
		_moveList.AppendColumn(pathCol);

		// 트리뷰를 스크롤로 감싸기
		var scrolledWindow = new ScrolledWindow
		{
			ShadowType = ShadowType.In,
			HscrollbarPolicy = PolicyType.Automatic,
			VscrollbarPolicy = PolicyType.Automatic,
			MarginStart = 5,
			MarginEnd = 5
		};
		scrolledWindow.Add(_moveList);
		mainVBox.PackStart(scrolledWindow, true, true, 0);

		// 대상 경로 입력란
		_destText = new Entry
		{
			MarginStart = 5,
			MarginEnd = 5,
			MarginBottom = 5
		};
		_destText.Activated += (_, _) => { Respond(ResponseType.Ok); };
		mainVBox.PackStart(_destText, false, false, 0);

		// 찾아보기 버튼
		var bottomHBox = new Box(Orientation.Horizontal, 6)
		{
			MarginStart = 5,
			MarginEnd = 5,
			MarginBottom = 5
		};

		_browseButton = new Button("찾아보기");
		_browseButton.Clicked += BrowseButton_Click;
		bottomHBox.PackStart(_browseButton, false, false, 0);
		mainVBox.PackStart(bottomHBox, false, false, 0);

		// 콘텐츠 영역에 메인 레이아웃 추가
		ContentArea.PackStart(mainVBox, true, true, 0);

		// 메뉴
		_moveMenu = new Menu();
		_moveAddMenuItem = new MenuItem("위치 추가(_A)");
		_moveAddMenuItem.Activated += MoveAddMenuItem_Click;
		_moveMenu.Append(_moveAddMenuItem);

		_moveChangeMenuItem = new MenuItem("다른 위치로(_C)");
		_moveChangeMenuItem.Activated += MoveChangeMenuItem_Click;
		_moveMenu.Append(_moveChangeMenuItem);

		_moveAliasMenuItem = new MenuItem("별명 바꾸기(_S)");
		_moveAliasMenuItem.Activated += MoveAliasMenuItem_Click;
		_moveMenu.Append(_moveAliasMenuItem);

		_moveMenu.Append(Doumi.CreateSeparatorMenuItem());

		_moveDeleteMenuItem = new MenuItem("위치 삭제(_D)");
		_moveDeleteMenuItem.Activated += MoveDeleteMenuItem_Click;
		_moveMenu.Append(_moveDeleteMenuItem);

		_moveList.ButtonPressEvent += MoveList_ButtonPressEvent;
		_moveList.RowActivated += MoveList_RowActivated;
		_moveList.CursorChanged += MoveList_CursorChanged;
		_moveMenu.ShowAll();

		// 다이얼로그 버튼
		AddButton(Stock.Cancel, ResponseType.Cancel);
		AddButton(Stock.Ok, ResponseType.Ok);
		DefaultResponse = ResponseType.Ok;
		#endregion

		RefreshList();

		ShowAll();
		_moveList.GrabFocus();
	}

	/// <summary>
	/// 대화상자가 닫힐 때 호출되어, 선택된 경로의 유효성을 검사하고 설정을 저장합니다.
	/// </summary>
	[GLib.ConnectBefore]
	private void MoveDialog_DeleteEvent(object sender, DeleteEventArgs args)
	{
		if (_response != ResponseType.Ok)
			return;

		var s = _destText.Text;
		if (!Directory.Exists(s))
		{
			Doumi.MessageDialog(this, "선택한 디렉토리가 존재하지 않습니다.", "이럴수가", MessageType.Error);
			args.RetVal = true;
			return;
		}

		Configs.MoveLocationLast = s;
		Filename = System.IO.Path.Combine(s, Filename);

		if (_should_save)
			Configs.MoveLocationCommit();
	}

	/// <summary>
	/// 대화상자의 응답(OK/Cancel 등)을 처리합니다.
	/// </summary>
	private void MoveDialog_Response(object o, ResponseArgs args)
	{
		_response = args.ResponseId;

		var deleteArgs = new DeleteEventArgs();
		MoveDialog_DeleteEvent(this, deleteArgs);

		if (deleteArgs.RetVal is true)
		{
			args.RetVal = true;
			return;
		}

		Destroy();
	}

	/// <summary>
	/// 키 입력 이벤트를 처리합니다. ESC로 취소, Enter로 확인 동작을 지원합니다.
	/// </summary>
	private void MoveDialog_KeyPressEvent(object o, KeyPressEventArgs args)
	{
		switch (args.Event.Key)
		{
			case GdkKey.Escape:
			{
				if (!_child_focus)
				{
					args.RetVal = true;
					Respond(ResponseType.Cancel);
				}
				break;
			}

			case GdkKey.Return or GdkKey.KP_Enter:
			{
				if (!_destText.HasFocus && !_child_focus)
				{
					args.RetVal = true;
					Respond(ResponseType.Ok);
				}
				break;
			}
		}
	}

	/// <summary>
	/// "찾아보기" 버튼 클릭 시 폴더 선택 대화상자를 엽니다.
	/// </summary>
	private void BrowseButton_Click(object? sender, EventArgs e)
	{
		var loc = _destText.Text;
		if (string.IsNullOrEmpty(loc) || !Directory.Exists(loc))
		{
			loc = Configs.MoveLocationLast;
			if (string.IsNullOrEmpty(loc) || !Directory.Exists(loc))
				loc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		if (!OpenDirectoryChooserDialog(loc, out var folder))
			return;

		_destText.Text = folder;
	}

	/// <summary>
	/// 목록에서 항목을 선택하면 경로를 입력란에 표시합니다.
	/// </summary>
	private void MoveList_CursorChanged(object? sender, EventArgs e)
	{
		if (!_moveList.Selection.GetSelected(out var iter))
			return;

		var path = (string)_moveListStore.GetValue(iter, 2);
		_destText.Text = path;
	}

	/// <summary>
	/// 목록에서 행을 더블클릭하면 확인(OK) 동작을 수행합니다.
	/// </summary>
	private void MoveList_RowActivated(object o, RowActivatedArgs args)
	{
		if (_moveList.Selection.GetSelected(out _))
			Respond(ResponseType.Ok);
	}

	/// <summary>
	/// 별명 셀 편집이 끝나면 Configs에 반영합니다.
	/// </summary>
	private void AliasCell_Edited(object o, EditedArgs args)
	{
		_child_focus = false;

		_moveListStore.GetIterFromString(out var iter, args.Path);
		if (TreeIter.Zero.Equals(iter))
			return;

		var index = Alter.ToInt(_moveListStore.GetValue(iter, 0) as string) - 1;
		var path = (string)_moveListStore.GetValue(iter, 2);

		Configs.MoveLocationSet(index, path, args.NewText);
		_moveListStore.SetValue(iter, 1, args.NewText);

		_should_save = true;
	}

	/// <summary>
	/// 마우스 오른쪽 버튼 클릭 시 컨텍스트 메뉴를 표시합니다.
	/// </summary>
	[GLib.ConnectBefore]
	private void MoveList_ButtonPressEvent(object o, ButtonPressEventArgs args)
	{
		if (args.Event.Button != 3)
			return;

		if (_moveList.GetPathAtPos((int)args.Event.X, (int)args.Event.Y, out var path))
			_moveList.Selection.SelectPath(path);
		else
			_moveList.Selection.UnselectAll();

		var enable = _moveList.Selection.CountSelectedRows() == 1;
		_moveDeleteMenuItem.Sensitive = enable;
		_moveChangeMenuItem.Sensitive = enable;
		_moveAliasMenuItem.Sensitive = enable;
		_moveMenu.Popup();
	}

	/// <summary>
	/// 끌기 시작 시 소스 인덱스를 저장합니다.
	/// </summary>
	private void MoveList_DragDataGet(object o, DragDataGetArgs args)
	{
		if (!_moveList.Selection.GetSelected(out var iter))
			return;

		_dragSourcePath = _moveList.Model.GetPath(iter);
		args.SelectionData.Set(args.SelectionData.Target, 8, Encoding.UTF8.GetBytes(_dragSourcePath.ToString()));
	}

	/// <summary>
	/// 끌어 놓기로 항목 이동 시 Configs에 반영하고 목록을 갱신합니다.
	/// </summary>
	private void MoveList_DragDataReceived(object o, DragDataReceivedArgs args)
	{
		if (!_moveList.GetDestRowAtPos(args.X, args.Y, out var destPath, out var pos))
			return;

		if (_dragSourcePath == null || destPath == null || _dragSourcePath.Equals(destPath))
			return;

		var beforeIndex = _dragSourcePath.Indices[0];
		var afterIndex = destPath.Indices[0];

		if (pos == TreeViewDropPosition.After)
			afterIndex++;

		if (_moveListStore.GetIter(out var srcIter, _dragSourcePath) &&
			_moveListStore.GetIter(out var destIter, destPath))
		{
			// 값 복사
			var values = new object[3];
			for (var i = 0; i < 3; i++)
				values[i] = _moveListStore.GetValue(srcIter, i);

			// 원래 위치 삭제
			_moveListStore.Remove(ref srcIter);

			// 새 위치에 삽입
			var newIter = pos == TreeViewDropPosition.Before ? _moveListStore.InsertBefore(destIter) : _moveListStore.InsertAfter(destIter);

			for (var i = 0; i < 3; i++)
				_moveListStore.SetValue(newIter, i, values[i]);

			//
			Configs.MoveLocationIndexing(beforeIndex, afterIndex);
			RefreshList(false);
			EnsureMoveItem(afterIndex > beforeIndex ? afterIndex - 1 : afterIndex);
		}

		_dragSourcePath = null;
	}

	/// <summary>
	/// 위치 추가 메뉴 클릭 시 폴더 선택 후 위치를 추가합니다.
	/// </summary>
	private void MoveAddMenuItem_Click(object? sender, EventArgs e)
	{
		if (!OpenDirectoryChooserDialog(_destText.Text, out var folder))
			return;

		if (EnsureMoveItem(folder))
			return;

		var di = new DirectoryInfo(folder);
		Configs.MoveLocationAdd(folder, di.Name);

		RefreshList();
		EnsureMoveItem(folder);

		_should_save = true;
	}

	/// <summary>
	/// 위치 변경 메뉴 클릭 시 폴더 선택 후 위치를 변경합니다.
	/// </summary>
	private void MoveChangeMenuItem_Click(object? sender, EventArgs e)
	{
		if (!_moveList.Selection.GetSelected(out var iter))
			return;

		var loc = (string)_moveListStore.GetValue(iter, 2);
		if (!OpenDirectoryChooserDialog(loc, out var folder))
			return;

		if (EnsureMoveItem(folder))
			return;

		var index = Alter.ToInt(_moveListStore.GetValue(iter, 0) as string) - 1;

		var di = new DirectoryInfo(folder);
		Configs.MoveLocationSet(index, folder, di.Name);

		_moveListStore.SetValue(iter, 1, di.Name);
		_moveListStore.SetValue(iter, 2, folder);

		EnsureMoveItem(folder);

		_should_save = true;
	}

	/// <summary>
	/// 별명 바꾸기 메뉴 클릭 시 셀 편집 모드로 전환합니다.
	/// </summary>
	private void MoveAliasMenuItem_Click(object? sender, EventArgs e)
	{
		if (!_moveList.Selection.GetSelected(out var iter))
			return;

		var path = _moveListStore.GetPath(iter);
		var aliasColumn = _moveList.GetColumn(1);

		_child_focus = true;
		_moveList.SetCursor(path, aliasColumn, true);
	}

	/// <summary>
	/// 위치 삭제 메뉴 클릭 시 해당 위치를 삭제합니다.
	/// </summary>
	private void MoveDeleteMenuItem_Click(object? sender, EventArgs e)
	{
		if (!_moveList.Selection.GetSelected(out var iter))
			return;

		var index = Alter.ToInt(_moveListStore.GetValue(iter, 0) as string) - 1;

		Configs.MoveLocationDelete(index);
		RefreshList();

		_should_save = true;
	}

	/// <summary>
	/// 지정한 경로가 목록에 있으면 해당 항목을 선택합니다.
	/// </summary>
	private bool EnsureMoveItem(string loc)
	{
		if (string.IsNullOrEmpty(loc))
			return false;

		if (!_moveListStore.GetIterFirst(out var iter))
			return false;

		do
		{
			if (!loc.Equals((string)_moveListStore.GetValue(iter, 2)))
				continue;

			EnsureMoveItem(iter);
			return true;
		} while (_moveListStore.IterNext(ref iter));

		return false;
	}

	/// <summary>
	/// 지정한 TreeIter의 항목을 선택하고 스크롤합니다.
	/// </summary>
	private void EnsureMoveItem(TreeIter iter)
	{
		var path = _moveListStore.GetPath(iter);
		_moveList.Selection.SelectIter(iter);
		_moveList.ScrollToCell(path, null, true, 0.5f, 0.0f);
		_moveList.GrabFocus();
		_destText.Text = (string)_moveListStore.GetValue(iter, 2);
	}

	/// <summary>
	/// 지정한 인덱스의 항목을 선택하고 스크롤합니다.
	/// </summary>
	private void EnsureMoveItem(int index)
	{
		if (_moveListStore.IterNthChild(out var iter, index))
			EnsureMoveItem(iter);
	}

	/// <summary>
	/// 위치 목록을 새로고침합니다.
	/// </summary>
	/// <param name="ensure">선택 항목을 보장할지 여부</param>
	private void RefreshList(bool ensure = true)
	{
		_moveListStore.Clear();
		var lst = Configs.MoveLocationGetAll();
		foreach (var item in lst)
			_moveListStore.AppendValues((item.No + 1).ToString(), item.Alias, item.Path);

		if (ensure)
		{
			var found = false;
			if (!string.IsNullOrEmpty(Configs.MoveLocationLast))
				found = EnsureMoveItem(Configs.MoveLocationLast);
			if (!found && lst.Length > 0)
				EnsureMoveItem(0);
		}
		_moveList.GrabFocus();
	}

	/// <summary>
	/// 폴더 선택 대화상자를 열고, 선택된 경로를 반환합니다.
	/// </summary>
	/// <param name="path">초기 폴더 경로</param>
	/// <param name="ret">선택된 폴더 경로</param>
	/// <returns>선택 성공 여부</returns>
	private bool OpenDirectoryChooserDialog(string path, out string ret)
	{
		ret = path;
		var dlg = new FileChooserDialog(
			"디렉토리 선택",
			this,
			FileChooserAction.SelectFolder,
			Stock.Cancel, ResponseType.Cancel,
			Stock.Open, ResponseType.Ok);

		dlg.SetCurrentFolder(path);

		var result = false;
		if (dlg.Run() == (int)ResponseType.Ok)
		{
			ret = dlg.Filename;
			result = true;
		}
		dlg.Destroy();

		return result;
	}

	/// <summary>
	/// 파일 이름을 지정하여 대화상자를 실행합니다.
	/// </summary>
	/// <param name="filename">이동할 파일 이름</param>
	/// <returns>이동이 성공하면 true</returns>
	public bool Run(string? filename)
	{
		if (!string.IsNullOrEmpty(filename))
			Filename = filename;
		return Run() == (int)ResponseType.Ok && !string.IsNullOrEmpty(Filename);
	}
}

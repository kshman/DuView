// ReSharper disable MissingXmlDoc

using System.Text;

namespace DgView.Forms;

public class MoveDialog : Dialog
{
    public string Filename { get; private set; } = string.Empty;

    private static string s_last_location = string.Empty;
    private bool _child_focus;
    private bool _save_settings;

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
    private int? _dragSourceIndex;

    public MoveDialog(Window? parent)
        : base("책 이동하기", parent, DialogFlags.Modal)
    {
        #region 디자인
        SetDefaultSize(500, 550);
        //Modal = true;
        //WindowPosition = parent != null ? WindowPosition.CenterOnParent : WindowPosition.Center;
        Resizable = false;
        SkipTaskbarHint = true;

        Icon = Doumi.GetResourcePixmap("DgView.Resources.icon_move.png");

        var mainVBox = new Box(Orientation.Vertical, 0);

        var titleLabel = new Label("이동할 곳을 고르세요")
        {
            Halign = Align.Start,
            MarginStart = 5,
            MarginTop = 5,
            MarginBottom = 5
        };
        titleLabel.StyleContext.AddClass("bold-label");
        mainVBox.PackStart(titleLabel, false, false, 0);

        _moveList = new TreeView();
        _moveListStore = new ListStore(typeof(string), typeof(string), typeof(string));
        _moveList.Model = _moveListStore;
        _moveList.HeadersVisible = true;
        _moveList.Selection.Mode = SelectionMode.Single;
        _moveList.AddEvents((int)Gdk.EventMask.ButtonPressMask);
        _moveList.DragDataGet += MoveList_DragDataGet;
        _moveList.DragDataReceived += MoveList_DragDataReceived;

        var dndTarget = new TargetEntry("application/x-tree-row", TargetFlags.Widget, 0);
        _moveList.EnableModelDragSource(Gdk.ModifierType.Button1Mask, [dndTarget], Gdk.DragAction.Move);
        _moveList.EnableModelDragDest([dndTarget], Gdk.DragAction.Move);

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

        _destText = new Entry
        {
            MarginStart = 5,
            MarginEnd = 5,
            MarginBottom = 5
        };
        _destText.Activated += (_, _) => { Respond(ResponseType.Ok); };
        mainVBox.PackStart(_destText, false, false, 0);

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

        ContentArea.PackStart(mainVBox, true, true, 0);

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

        var toolStripSeparator1 = new SeparatorMenuItem();
        _moveMenu.Append(toolStripSeparator1);

        _moveDeleteMenuItem = new MenuItem("위치 삭제(_D)");
        _moveDeleteMenuItem.Activated += MoveDeleteMenuItem_Click;
        _moveMenu.Append(_moveDeleteMenuItem);
        _moveMenu.ShowAll();

        _moveList.ButtonPressEvent += MoveList_ButtonPressEvent;
        _moveList.RowActivated += MoveList_RowActivated;
        _moveList.CursorChanged += MoveList_CursorChanged;

        DeleteEvent += MoveDialog_DeleteEvent;
        Response += MoveDialog_Response;
        KeyPressEvent += MoveDialog_KeyPressEvent;

        AddButton(Stock.Cancel, ResponseType.Cancel);
        AddButton(Stock.Ok, ResponseType.Ok);
        DefaultResponse = ResponseType.Ok;
        #endregion

        RefreshList();

        ShowAll();
        _moveList.GrabFocus();
    }

    [GLib.ConnectBefore]
    private void MoveDialog_DeleteEvent(object sender, DeleteEventArgs args)
    {
        if (_response == ResponseType.Ok)
        {
            var s = _destText.Text;
            if (!Directory.Exists(s))
            {
                var md = new MessageDialog(this,
                    DialogFlags.Modal | DialogFlags.DestroyWithParent,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "선택한 디렉토리가 존재하지 않습니다.");
                md.Run();
                md.Destroy();
                args.RetVal = true;
                return;
            }

            s_last_location = s;
            Filename = System.IO.Path.Combine(s, Filename);
        }

        if (!_save_settings)
            return;

        Configs.KeepMoveLocation();
        Configs.SaveConfigs();
    }

    private void MoveDialog_Response(object o, ResponseArgs args)
    {
        _response = args.ResponseId;

        // OK나 Cancel 버튼 클릭 시 DeleteEvent와 동일한 처리 호출
        var deleteArgs = new DeleteEventArgs();
        MoveDialog_DeleteEvent(this, deleteArgs);

        if (deleteArgs.RetVal is true)
            return;

        Destroy();
    }

    private void MoveDialog_KeyPressEvent(object o, KeyPressEventArgs args)
    {
        if (args.Event.Key == Gdk.Key.Escape)
        {
            if (!_child_focus)
            {
                args.RetVal = true;
                Respond(ResponseType.Cancel);
            }
        }
        else if (args.Event.Key is Gdk.Key.Return or Gdk.Key.KP_Enter)
        {
            if (!_destText.HasFocus && !_child_focus)
            {
                args.RetVal = true;
                Respond(ResponseType.Ok);
            }
        }
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        var loc = _destText.Text;
        if (string.IsNullOrEmpty(loc) || !Directory.Exists(loc))
        {
            loc = Directory.Exists(Configs.RecentlyPath) ?
                Configs.RecentlyPath :
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        if (!OpenDirectoryChooserDialog(loc, out var folder))
            return;

        _destText.Text = folder;
    }

    private void MoveList_CursorChanged(object? sender, EventArgs e)
    {
        if (!_moveList.Selection.GetSelected(out var iter))
            return;

        var path = (string)_moveListStore.GetValue(iter, 2);
        _destText.Text = path;
    }

    private void MoveList_RowActivated(object o, RowActivatedArgs args)
    {
        if (_moveList.Selection.GetSelected(out _))
            Respond(ResponseType.Ok);
    }

    private void AliasCell_Edited(object o, EditedArgs args)
    {
        _child_focus = false;

        _moveListStore.GetIterFromString(out var iter, args.Path);
        if (TreeIter.Zero.Equals(iter))
            return;

        var index = Alter.ToInt(_moveListStore.GetValue(iter, 0) as string) - 1;
        var path = (string)_moveListStore.GetValue(iter, 2);

        Configs.SetMoveLocation(index, path, args.NewText);
        _moveListStore.SetValue(iter, 1, args.NewText);

        _save_settings = true;
    }

    [GLib.ConnectBefore]
    private void MoveList_ButtonPressEvent(object o, ButtonPressEventArgs args)
    {
        System.Diagnostics.Debug.WriteLine($"{DateTime.Now}: 버튼 눌림: {args.Event.Button}");

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
    private void MoveList_DragDataGet(object o, DragDataGetArgs args)
    {
        if (!_moveList.Selection.GetSelected(out var iter))
            return;

        _dragSourcePath = _moveList.Model.GetPath(iter);
        _dragSourceIndex = _dragSourcePath.Indices[0];
        args.SelectionData.Set(args.SelectionData.Target, 8, Encoding.UTF8.GetBytes(_dragSourcePath.ToString()));
    }

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
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"이동 전 인덱스: {beforeIndex}, 이동 후 인덱스: {afterIndex}");
#endif

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
            Configs.IndexingMoveLocation(beforeIndex, afterIndex);
            RefreshList(false);
            EnsureMoveItem(afterIndex > beforeIndex ? afterIndex - 1 : afterIndex);
        }

        _dragSourcePath = null;
        _dragSourceIndex = null;
    }

    private void MoveAddMenuItem_Click(object? sender, EventArgs e)
    {
        if (!OpenDirectoryChooserDialog(_destText.Text, out var folder))
            return;

        if (EnsureMoveItem(folder))
            return;

        var di = new DirectoryInfo(folder);
        Configs.AddMoveLocation(folder, di.Name);

        RefreshList();
        EnsureMoveItem(folder);

        _save_settings = true;
    }

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
        Configs.SetMoveLocation(index, folder, di.Name);

        _moveListStore.SetValue(iter, 1, di.Name);
        _moveListStore.SetValue(iter, 2, folder);

        EnsureMoveItem(folder);

        _save_settings = true;
    }

    private void MoveAliasMenuItem_Click(object? sender, EventArgs e)
    {
        if (!_moveList.Selection.GetSelected(out var iter))
            return;

        var path = _moveListStore.GetPath(iter);
        var aliasColumn = _moveList.GetColumn(1);

        _child_focus = true;
        _moveList.SetCursor(path, aliasColumn, true);
    }

    private void MoveDeleteMenuItem_Click(object? sender, EventArgs e)
    {
        if (!_moveList.Selection.GetSelected(out var iter))
            return;

        var index = Alter.ToInt(_moveListStore.GetValue(iter, 0) as string) - 1;

        Configs.DeleteMoveLocation(index);
        RefreshList();

        _save_settings = true;
    }

    private bool EnsureMoveItem(string loc)
    {
        if (string.IsNullOrEmpty(loc))
            return false;

        if (_moveListStore.GetIterFirst(out var iter))
        {
            do
            {
                if (!loc.Equals((string)_moveListStore.GetValue(iter, 2)))
                    continue;

                EnsureMoveItem(iter);
                return true;
            } while (_moveListStore.IterNext(ref iter));
        }

        return false;
    }

    private void EnsureMoveItem(TreeIter iter)
    {
        var path = _moveListStore.GetPath(iter);
        _moveList.Selection.SelectIter(iter);
        _moveList.ScrollToCell(path, null, true, 0.5f, 0.0f);
        _moveList.GrabFocus();
        _destText.Text = (string)_moveListStore.GetValue(iter, 2);
    }

    private void EnsureMoveItem(int index)
    {
        if (_moveListStore.IterNthChild(out var iter, index))
            EnsureMoveItem(iter);
    }

    private void RefreshList(bool ensure = true)
    {
        _moveListStore.Clear();
        var lst = Configs.GetMoveLocations();
        for (var i = 0; i < lst.Length; i++)
            _moveListStore.AppendValues((i + 1).ToString(), lst[i].Value, lst[i].Key);

        if (ensure)
        {
            var found = false;
            if (!string.IsNullOrEmpty(s_last_location))
                found = EnsureMoveItem(s_last_location);
            if (!found && lst.Length > 0)
                EnsureMoveItem(0);
        }
        _moveList.GrabFocus();
    }

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

    public bool Run(string? filename)
    {
        if (!string.IsNullOrEmpty(filename))
            Filename = filename;
        return Run() == (int)ResponseType.Ok && !string.IsNullOrEmpty(Filename);
    }
}

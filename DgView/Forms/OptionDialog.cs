// ReSharper disable MissingXmlDoc

namespace DgView.Forms;

public class OptionDialog : Dialog
{
    // �Ϲ� ��
    private readonly CheckButton _runOnceCheck;
    private readonly CheckButton _escExitCheck;
    private readonly CheckButton _useMagneticCheck;
    private readonly CheckButton _confirmDeleteCheck;
    private readonly CheckButton _alwaysTopCheck;
    private readonly CheckButton _updateNotifyCheck;
    private readonly SpinButton _cacheSizeValue;
    private readonly Entry _externalRunText;
    private readonly Button _externalRunButton;
    private readonly CheckButton _reloadExternalExitCheck;

    // Ű����/���콺 ��
    private readonly CheckButton _useClickToPageCheck;
    private readonly CheckButton _useDoubleClickStateCheck;

    // ���� ��
    private readonly CheckButton _usePasswordCheck;
    private readonly Entry _passwordText;
    private readonly TreeView _passwordUsageList;
    private readonly ListStore _passwordUsageStore;

    //
    private static readonly string[] s_password_usage_items = [
        "�α׺� ����",
        "�ɼ� ����",
        "������ å ����",
        "å �ű��",
        "å �̸� �ٲٱ�",
    ];

    public OptionDialog(Window? parent) : base("����", parent, DialogFlags.Modal)
    {
        #region ������
        SetDefaultSize(640, 600);
        Resizable = false;
        BorderWidth = 8;

        var mainBox = new Box(Orientation.Vertical, 0);

        // ��
        var optionTab = new Notebook();
        mainBox.PackStart(optionTab, true, true, 4);

        // --- �Ϲ� �� ---
        var generalBox = new Box(Orientation.Vertical, 6);
        _runOnceCheck = new CheckButton("���α׷� �ϳ��� ����");
        _escExitCheck = new CheckButton("ESC Ű�� ���α׷� ����");
        _useMagneticCheck = new CheckButton("�ڼ� ������ ���");
        _confirmDeleteCheck = new CheckButton("������ ���ﶧ Ȯ��");
        _alwaysTopCheck = new CheckButton("���α׷��� �� ���� ���̱�");
        _updateNotifyCheck = new CheckButton("������Ʈ�� ������ �˷��ֱ�");
        _updateNotifyCheck.Sensitive = false; // ������Ʈ �˸��� ��Ȱ��ȭ

        // ĳ�� ũ��
        var cacheBox = new Box(Orientation.Horizontal, 4);
        _cacheSizeValue = new SpinButton(0, 1024, 1) { Value = Configs.MaxPageCache };
        var cacheLabel = new Label("MB");
        cacheBox.PackStart(_cacheSizeValue, false, false, 0);
        cacheBox.PackStart(cacheLabel, false, false, 0);

        // �ܺ� ����
        var extRunBox = new Box(Orientation.Horizontal, 4);
        _externalRunText = new Entry { WidthChars = 30 };
        _externalRunButton = new Button("ã�ƺ���");
        extRunBox.PackStart(_externalRunText, true, true, 0);
        extRunBox.PackStart(_externalRunButton, false, false, 0);
        _reloadExternalExitCheck = new CheckButton("�ܺ� ���� ������ å �ٽ� ����");

        // �׷���
        generalBox.PackStart(_runOnceCheck, false, false, 0);
        generalBox.PackStart(_escExitCheck, false, false, 0);
        generalBox.PackStart(_useMagneticCheck, false, false, 0);
        generalBox.PackStart(_confirmDeleteCheck, false, false, 0);
        generalBox.PackStart(_alwaysTopCheck, false, false, 0);
        generalBox.PackStart(_updateNotifyCheck, false, false, 0);
        generalBox.PackStart(new Label("ĳ�� ũ��"), false, false, 0);
        generalBox.PackStart(cacheBox, false, false, 0);
        generalBox.PackStart(new Label("�ܺ� ����"), false, false, 0);
        generalBox.PackStart(extRunBox, false, false, 0);
        generalBox.PackStart(_reloadExternalExitCheck, false, false, 0);
        optionTab.AppendPage(generalBox, new Label("�Ϲ�"));

        // --- ���� �� (��Ȱ��ȭ) ---
        var viewBox = new Box(Orientation.Vertical, 6);
        viewBox.Sensitive = false;
        viewBox.PackStart(new Label("�׸��� �����"), false, false, 0);
        optionTab.AppendPage(viewBox, new Label("����"));

        // --- Ű����/���콺 �� ---
        var kmBox = new Box(Orientation.Vertical, 6);
        _useDoubleClickStateCheck = new CheckButton("�ι� ���� �ִ�ȭ/�ּ�ȭ ��� ���");
        _useClickToPageCheck = new CheckButton("���콺 ��ư���� ����/���� ������ �̵�");
        kmBox.PackStart(_useDoubleClickStateCheck, false, false, 0);
        kmBox.PackStart(_useClickToPageCheck, false, false, 0);
        optionTab.AppendPage(kmBox, new Label("Ű����/���콺"));

        // --- Pad �� (��Ȱ��ȭ) ---
        var padBox = new Box(Orientation.Vertical, 6);
        padBox.Sensitive = false;
        padBox.PackStart(new Label("�׸��� �����"), false, false, 0);
        optionTab.AppendPage(padBox, new Label("��Ʈ�ѷ�"));

        // --- ���� �� ---
        var secBox = new Box(Orientation.Vertical, 6);
        _usePasswordCheck = new CheckButton("��� ��ȣ ���");
        _passwordText = new Entry { Visibility = false, MaxLength = 12 };

        _passwordUsageList = new TreeView();
        _passwordUsageStore = new ListStore(typeof(string));
        _passwordUsageList.Model = _passwordUsageStore;
        _passwordUsageList.HeadersVisible = false;
        _passwordUsageList.Selection.Mode = SelectionMode.Multiple;

        var renderer = new CellRendererText();
        var col = new TreeViewColumn("Usage", renderer, "text", 0);
        _passwordUsageList.AppendColumn(col);

        foreach (var code in s_password_usage_items)
            _passwordUsageStore.AppendValues(code);

        secBox.PackStart(_usePasswordCheck, false, false, 0);
        secBox.PackStart(_passwordText, false, false, 0);
        secBox.PackStart(_passwordUsageList, true, true, 0);
        optionTab.AppendPage(secBox, new Label("����"));

        // �̺�Ʈ ����
        _runOnceCheck.Toggled += (_, _) => Configs.GeneralRunOnce = _runOnceCheck.Active;
        _escExitCheck.Toggled += (_, _) => Configs.GeneralEscExit = _escExitCheck.Active;
        _useMagneticCheck.Toggled += (_, _) => Configs.GeneralUseMagnetic = _useMagneticCheck.Active;
        _confirmDeleteCheck.Toggled += (_, _) => Configs.GeneralConfirmDelete = _confirmDeleteCheck.Active;
        _alwaysTopCheck.Toggled += (_, _) => Configs.GeneralAlwaysTop = _alwaysTopCheck.Active;
        _updateNotifyCheck.Toggled += (_, _) => Configs.GeneralUpdateNotify = _updateNotifyCheck.Active;
        _cacheSizeValue.ValueChanged += (_, _) => Configs.MaxPageCache = (int)_cacheSizeValue.Value;
        _useDoubleClickStateCheck.Toggled += (_, _) => Configs.MouseUseDoubleClickState = _useDoubleClickStateCheck.Active;
        _useClickToPageCheck.Toggled += (_, _) => Configs.MouseUseClickPage = _useClickToPageCheck.Active;
        _usePasswordCheck.Toggled += (_, _) =>
        {
            Configs.UsePassCode = _usePasswordCheck.Active;
            _passwordText.Sensitive = _usePasswordCheck.Active;
            _passwordUsageList.Sensitive = _usePasswordCheck.Active;
        };
        _passwordText.Changed += (_, _) => Configs.PassCode = _passwordText.Text;

        // �ܺ� ����/������ ��ư(���� ����)
        _externalRunButton.Clicked += ExternalRunButton_Clicked;
        _reloadExternalExitCheck.Toggled += (_, _) => Configs.ReloadAfterExternal = _reloadExternalExitCheck.Active;

        DeleteEvent += OptionDialog_DeleteEvent;
        Response += OptionDialog_Response;

        //
        ContentArea.PackStart(mainBox, true, true, 0);
        AddButton(Stock.Close, ResponseType.Ok);
        #endregion

        // �ʱⰪ ����
        LoadSettings();

        ShowAll();
    }

    private void LoadSettings()
    {
        _runOnceCheck.Active = Configs.GeneralRunOnce;
        _escExitCheck.Active = Configs.GeneralEscExit;
        _useMagneticCheck.Active = Configs.GeneralUseMagnetic;
        _confirmDeleteCheck.Active = Configs.GeneralConfirmDelete;
        _alwaysTopCheck.Active = Configs.GeneralAlwaysTop;
        _cacheSizeValue.Value = Configs.MaxPageCache;
        _externalRunText.Text = Configs.ExternalRun;
        _reloadExternalExitCheck.Active = Configs.ReloadAfterExternal;

        _useClickToPageCheck.Active = Configs.MouseUseClickPage;
        _useDoubleClickStateCheck.Active = Configs.MouseUseDoubleClickState;

        _usePasswordCheck.Active = Configs.UsePassCode;
        _passwordText.Text = Configs.PassCode;
        _passwordText.Sensitive = Configs.UsePassCode;
        _passwordUsageList.Sensitive = Configs.UsePassCode;

        var selectedUsages = Configs.GetPassUsageArray();
        if (selectedUsages.Length > 0 && _passwordUsageStore.GetIterFirst(out var iter))
        {
            var idx = 0;
            do
            {
                if (selectedUsages.Any(u => (int)u == idx))
                    _passwordUsageList.Selection.SelectIter(iter);
                idx++;
            } while (_passwordUsageStore.IterNext(ref iter));
        }
    }

    private void OptionDialog_DeleteEvent(object o, DeleteEventArgs args)
    {
        SaveConfigs();
    }

    private void OptionDialog_Response(object o, ResponseArgs args)
    {
        SaveConfigs();
        Destroy();
    }

    private bool _save_configs;

    private void SaveConfigs()
    {
        if (_save_configs)
            return;
        _save_configs = true; // �ߺ� ���� ���� 

        Configs.MaxPageCache = (int)_cacheSizeValue.Value;
        if (_externalRunText.Text.Length > 0)
            Configs.ExternalRun = _externalRunText.Text;

        Configs.PassCode = _passwordText.Text;

        var rows = _passwordUsageList.Selection.GetSelectedRows();
        var usages = rows.Select(path => (PassCodeUsage)path.Indices[0]).ToArray();
        Configs.CommitPassUsage(usages);

        Configs.SaveSettings();
    }

    private void ExternalRunButton_Clicked(object? sender, EventArgs e)
    {
        using var dialog = new FileChooserDialog("�ܺ� ���� ���� ����", this, FileChooserAction.Open);
        dialog.AddButton(Stock.Cancel, ResponseType.Cancel);
        dialog.AddButton(Stock.Open, ResponseType.Accept);

        var flt = new FileFilter();
        flt.Name = "���� ����";
#if WINDOWS
        flt.AddPattern("*.exe");
#else
        flt.AddPattern("*");
#endif
        dialog.AddFilter(flt);

        if (dialog.Run() != (int)ResponseType.Accept)
            return;

        var filename = dialog.Filename;

        if (OperatingSystem.IsLinux())
        {
            var fi = new FileInfo(filename);
            if (!fi.Exists && (fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "���� ������ ���� �����Դϴ�.");
                md.Run();
                md.Destroy();
                return;
            }
        }
        _externalRunText.Text = filename;
    }
}
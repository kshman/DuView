namespace DgView.Forms;

/// <summary>
/// 설정을 위한 옵션 다이얼로그를 제공하는 클래스입니다.
/// 일반, 보기, 키보드/마우스, 보안 등 다양한 설정 탭을 포함합니다.
/// </summary>
public class OptionDialog : Dialog
{
	// 일반 탭
	private readonly CheckButton _runOnceCheck;
	private readonly CheckButton _escExitCheck;
	private readonly CheckButton _confirmDeleteCheck;
	private readonly CheckButton _updateNotifyCheck;
	private readonly SpinButton _cacheSizeValue;
	private readonly Entry _externalRunText;
	private readonly Button _externalRunButton;
	private readonly CheckButton _reloadExternalExitCheck;

	// 키보드/마우스 탭
	private readonly CheckButton _useClickToPageCheck;
	private readonly CheckButton _useDoubleClickStateCheck;

	// 보안 탭
	private readonly CheckButton _usePasswordCheck;
	private readonly Entry _passwordText;
	private readonly TreeView _passwordUsageList;
	private readonly ListStore _passwordUsageStore;

	//
	private static readonly string[] s_password_usage_items = [
		"두그뷰 실행",
		"옵션 설정",
		"마지막 책 열기",
		"책 옮기기",
		"책 이름 바꾸기",
	];

	/// <summary>
	/// OptionDialog의 인스턴스를 생성합니다.
	/// </summary>
	/// <param name="parent">부모 윈도우입니다.</param>
	public OptionDialog(Window? parent) :
		base("설정", parent, DialogFlags.Modal)
	{
		#region 디자인
		// 다이얼로그
		SetDefaultSize(640, 600);
		BorderWidth = 8;

		// 메인 박스
		var mainBox = new Box(Orientation.Vertical, 0);

		// 탭
		var optionTab = new Notebook();
		mainBox.PackStart(optionTab, true, true, 4);

		// --- 일반 탭 ---
		var generalBox = new Box(Orientation.Vertical, 6);
		_runOnceCheck = new CheckButton("프로그램 하나만 실행");
		_escExitCheck = new CheckButton("ESC 키로 프로그램 종료");
		_confirmDeleteCheck = new CheckButton("파일을 지울때 확인");
		_updateNotifyCheck = new CheckButton("업데이트가 있으면 알려주기");
		_updateNotifyCheck.Sensitive = false; // 업데이트 알림은 비활성화

		// 캐시 크기
		var cacheLabel = new Label("캐시 크기");
		cacheLabel.Halign = Align.Start;

		var cacheBox = new Box(Orientation.Horizontal, 4);
		_cacheSizeValue = new SpinButton(0, 1024, 1) { Value = Configs.GeneralMaxPageCache };
		cacheBox.PackStart(_cacheSizeValue, false, false, 0);
		cacheBox.PackStart(new Label("MB"), false, false, 0);

		// 외부 실행
		var externalLabel = new Label("외부 실행");
		externalLabel.Halign = Align.Start;

		var extRunBox = new Box(Orientation.Horizontal, 4);
		_externalRunText = new Entry { WidthChars = 30 };
		_externalRunButton = new Button("찾아보기");
		extRunBox.PackStart(_externalRunText, true, true, 0);
		extRunBox.PackStart(_externalRunButton, false, false, 0);
		_reloadExternalExitCheck = new CheckButton("외부 실행 끝나면 책 다시 열기");

		// 패킹
		generalBox.PackStart(_runOnceCheck, false, false, 0);
		generalBox.PackStart(_escExitCheck, false, false, 0);
		generalBox.PackStart(_confirmDeleteCheck, false, false, 0);
		generalBox.PackStart(_updateNotifyCheck, false, false, 0);
		generalBox.PackStart(cacheLabel, false, false, 0);
		generalBox.PackStart(cacheBox, false, false, 0);
		generalBox.PackStart(externalLabel, false, false, 0);
		generalBox.PackStart(extRunBox, false, false, 0);
		generalBox.PackStart(_reloadExternalExitCheck, false, false, 0);
		optionTab.AppendPage(generalBox, new Label("일반"));

		// --- 보기 탭 ---
		var viewBox = new Box(Orientation.Vertical, 6);
		viewBox.Sensitive = false;
		viewBox.PackStart(new Label("항목이 없어요"), false, false, 0);
		optionTab.AppendPage(viewBox, new Label("보기"));

		// --- 조작 탭 ---
		var kmBox = new Box(Orientation.Vertical, 6);
		_useDoubleClickStateCheck = new CheckButton("두번 눌러 최대화/최소화 기능 사용");
		_useClickToPageCheck = new CheckButton("마우스 버튼으로 이전/다음 페이지 이동");
		kmBox.PackStart(_useDoubleClickStateCheck, false, false, 0);
		kmBox.PackStart(_useClickToPageCheck, false, false, 0);
		optionTab.AppendPage(kmBox, new Label("조작"));

		// --- 컨트롤러 탭 ---
		var padBox = new Box(Orientation.Vertical, 6);
		padBox.Sensitive = false;
		padBox.PackStart(new Label("항목이 없어요"), false, false, 0);
		optionTab.AppendPage(padBox, new Label("컨트롤러"));

		// --- 보안 탭 ---
		var secBox = new Box(Orientation.Vertical, 6);
		_usePasswordCheck = new CheckButton("비밀 번호 사용");
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
		optionTab.AppendPage(secBox, new Label("보안"));

		// 이벤트 연결
		_runOnceCheck.Toggled += (_, _) => Configs.GeneralRunOnce = _runOnceCheck.Active;
		_escExitCheck.Toggled += (_, _) => Configs.GeneralEscExit = _escExitCheck.Active;
		_confirmDeleteCheck.Toggled += (_, _) => Configs.GeneralConfirmDelete = _confirmDeleteCheck.Active;
		_updateNotifyCheck.Toggled += (_, _) => { };
		_cacheSizeValue.ValueChanged += (_, _) => Configs.GeneralMaxPageCache = (int)_cacheSizeValue.Value;
		_useDoubleClickStateCheck.Toggled += (_, _) => Configs.MouseDoubleClickFullScreen = _useDoubleClickStateCheck.Active;
		_useClickToPageCheck.Toggled += (_, _) => Configs.MouseUseClickPaging = _useClickToPageCheck.Active;
		_usePasswordCheck.Toggled += (_, _) =>
		{
			Configs.SecurityUsePass = _usePasswordCheck.Active;
			_passwordText.Sensitive = _usePasswordCheck.Active;
			_passwordUsageList.Sensitive = _usePasswordCheck.Active;
		};
		_passwordText.Changed += (_, _) => Configs.SecurityPassCode = _passwordText.Text;

		// 외부 실행/브라우저 버튼(파일 선택)
		_externalRunButton.Clicked += ExternalRunButton_Clicked;
		_reloadExternalExitCheck.Toggled += (_, _) => Configs.GeneralReloadAfterExternalExit = _reloadExternalExitCheck.Active;

		DeleteEvent += OptionDialog_DeleteEvent;
		Response += OptionDialog_Response;

		//
		ContentArea.PackStart(mainBox, true, true, 0);
		AddButton(Stock.Close, ResponseType.Ok);
		#endregion

		// 초기값 세팅
		LoadSettings();

		ShowAll();
	}

	/// <summary>
	/// 설정값을 UI에 로드합니다.
	/// </summary>
	private void LoadSettings()
	{
		_runOnceCheck.Active = Configs.GeneralRunOnce;
		_escExitCheck.Active = Configs.GeneralEscExit;
		_confirmDeleteCheck.Active = Configs.GeneralConfirmDelete;
		_cacheSizeValue.Value = Configs.GeneralMaxPageCache;
		_externalRunText.Text = Configs.GeneralExternalRun;
		_reloadExternalExitCheck.Active = Configs.GeneralReloadAfterExternalExit;

		_useClickToPageCheck.Active = Configs.MouseUseClickPaging;
		_useDoubleClickStateCheck.Active = Configs.MouseDoubleClickFullScreen;

		_usePasswordCheck.Active = Configs.SecurityUsePass;
		_passwordText.Text = Configs.SecurityPassCode;
		_passwordText.Sensitive = Configs.SecurityUsePass;
		_passwordUsageList.Sensitive = Configs.SecurityUsePass;

		var selectedUsages = Configs.PassUsageGetArray();
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

	/// <summary>
	/// 다이얼로그가 삭제될 때 설정을 저장합니다.
	/// </summary>
	/// <param name="o">이벤트 소스 객체</param>
	/// <param name="args">이벤트 인자</param>
	private void OptionDialog_DeleteEvent(object o, DeleteEventArgs args)
	{
		SaveConfigs();
	}

	/// <summary>
	/// 다이얼로그의 응답 이벤트를 처리합니다.
	/// </summary>
	/// <param name="o">이벤트 소스 객체</param>
	/// <param name="args">응답 인자</param>
	private void OptionDialog_Response(object o, ResponseArgs args)
	{
		SaveConfigs();
		Destroy();
	}

	private bool _save_configs;

	/// <summary>
	/// 현재 UI 상태를 Configs에 저장하고, 중복 저장을 방지합니다.
	/// </summary>
	private void SaveConfigs()
	{
		if (_save_configs)
			return;
		_save_configs = true; // 중복 저장 방지 

		Configs.GeneralMaxPageCache = (int)_cacheSizeValue.Value;
		if (_externalRunText.Text.Length > 0)
			Configs.GeneralExternalRun = _externalRunText.Text;

		Configs.SecurityPassCode = _passwordText.Text;

		var rows = _passwordUsageList.Selection.GetSelectedRows();
		var usages = rows.Select(path => (PassCodeUsage)path.Indices[0]).ToArray();
		Configs.PassUsageCommit(usages);
	}

	/// <summary>
	/// 외부 실행 파일을 선택하는 파일 선택 다이얼로그를 표시합니다.
	/// </summary>
	/// <param name="sender">이벤트 소스 객체</param>
	/// <param name="e">이벤트 인자</param>
	private void ExternalRunButton_Clicked(object? sender, EventArgs e)
	{
		using var dialog = new FileChooserDialog("외부 실행 파일 선택", this, FileChooserAction.Open);
		dialog.AddButton(Stock.Cancel, ResponseType.Cancel);
		dialog.AddButton(Stock.Open, ResponseType.Accept);

		var flt = new FileFilter();
		flt.Name = "실행 파일";
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
				var md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "실행 권한이 없는 파일입니다.");
				md.Run();
				md.Destroy();
				return;
			}
		}
		_externalRunText.Text = filename;
	}
}

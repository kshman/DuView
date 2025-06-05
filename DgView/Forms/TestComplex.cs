#pragma warning disable CS0612 // 형식 또는 멤버는 사용되지 않습니다.

namespace DgView.Forms;

internal class TestComplex : Window
{
    public TestComplex() : base("GTK#3 복잡한 레이아웃 예제")
    {
        SetDefaultSize(600, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        var vbox = new VBox(false, 0);

        // 메뉴 바
        var menuBar = new MenuBar();
        var fileMenu = new Menu();
        var fileMenuItem = new MenuItem("파일");
        fileMenuItem.Submenu = fileMenu;

        var exitMenuItem = new MenuItem("종료");
        exitMenuItem.Activated += (o, args) => Application.Quit();
        fileMenu.Append(exitMenuItem);
        menuBar.Append(fileMenuItem);

        vbox.PackStart(menuBar, false, false, 0);

        // 툴바
        var toolbar = new Toolbar();
        var saveButton = new ToolButton(Stock.Save);
        var openButton = new ToolButton(Stock.Open);
        toolbar.Insert(openButton, 0);
        toolbar.Insert(saveButton, 1);

        vbox.PackStart(toolbar, false, false, 0);

        // 중앙 영역
        var hpaned = new HPaned();

        var treeView = new TreeView();
        var scroll1 = new ScrolledWindow();
        scroll1.Add(treeView);

        var textView = new TextView();
        var scroll2 = new ScrolledWindow();
        scroll2.Add(textView);

        hpaned.Pack1(scroll1, true, false);
        hpaned.Pack2(scroll2, true, false);

        vbox.PackStart(hpaned, true, true, 0);

        // 상태바
        var statusbar = new Statusbar();
        statusbar.Push(1, "준비 상태");

        vbox.PackStart(statusbar, false, false, 0);

        Add(vbox);
        ShowAll();
    }
}
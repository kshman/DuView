#pragma warning disable CS0612 // 형식 또는 멤버는 사용되지 않습니다.

namespace DgView.Forms;

internal class TestComplex : Window
{
    public TestComplex() : base("GTK# 복잡한 레이아웃 예제")
    {
        // 창 기본 설정
        SetDefaultSize(600, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (_, _) => Application.Quit();
        
        // 메인 컨테이너
        var vbox = new VBox();
        
        // 메뉴 바 생성
        var menuBar = new MenuBar();
        var fileMenu = new Menu();
        var fileMenuItem = new MenuItem("파일");
        fileMenuItem.Submenu = fileMenu;
        
        var exitMenuItem = new MenuItem("종료");
        exitMenuItem.Activated += (_, _) => Application.Quit();
        fileMenu.Append(exitMenuItem);
        menuBar.Append(fileMenuItem);
        
        vbox.PackStart(menuBar, false, false, 0);
        
        // 상단 툴바
        var toolbar = new Toolbar();
        var saveButton = new ToolButton(Stock.Save);
        var openButton = new ToolButton(Stock.Open);
        toolbar.Add(openButton);
        toolbar.Add(saveButton);
        
        vbox.PackStart(toolbar, false, false, 0);
        
        // 중앙 영역 (패널 + 텍스트뷰)
        var hpaned = new HPaned();
        
        // 왼쪽 트리뷰
        var treeView = new TreeView();
        var scroll1 = new ScrolledWindow();
        scroll1.Add(treeView);
        
        // 오른쪽 텍스트뷰
        var textView = new TextView();
        var scroll2 = new ScrolledWindow();
        scroll2.Add(textView);
        
        hpaned.Pack1(scroll1, true, false);
        hpaned.Pack2(scroll2, true, false);
        
        vbox.PackStart(hpaned, true, true, 0);
        
        // 하단 상태바
        var statusbar = new Statusbar();
        statusbar.Push(1, "준비 상태");
        
        vbox.PackStart(statusbar, false, false, 0);
        
        Add(vbox);
    }
}
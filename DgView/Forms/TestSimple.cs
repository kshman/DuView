#pragma warning disable CS0612 // 형식 또는 멤버는 사용되지 않습니다.

namespace DgView.Forms;

internal class TestSimple : Window
{
    [Obsolete("Obsolete")]
    public TestSimple() : base("GTK# 기본 창 예제")
    {
        // 창 기본 설정
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        
        // 창 닫기 이벤트 처리
        DeleteEvent += OnWindowDelete;
        
        // 간단한 컨트롤 추가
        var label = new Label("안녕하세요! GTK#으로 만든 창입니다.");
        var button = new Button("클릭하세요");
        button.Clicked += OnButtonClicked;
        
        // 레이아웃 구성
        var vbox = new VBox();
        vbox.PackStart(label, true, true, 5);
        vbox.PackStart(button, false, false, 5);
        
        Add(vbox);
    }
    
    private void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        Application.Quit();
        args.RetVal = true;
    }
    
    private void OnButtonClicked(object? sender, EventArgs args)
    {
        MessageDialog md = new MessageDialog(this, 
            DialogFlags.DestroyWithParent,
            MessageType.Info, 
            ButtonsType.Close,
            "버튼이 클릭되었습니다!");
        md.Run();
        md.Destroy();
    }
}

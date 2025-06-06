using System.Text;
using Gdk;
using Window = Gtk.Window;

namespace DgView.Forms;

/// <summary>
/// 책 파일의 이름을 사용자가 원하는 형식으로 변경할 수 있도록 지원하는 대화상자입니다.
/// 작가, 책 이름, 번호, 추가 정보 등 세부 항목을 입력받아 새로운 파일명을 생성합니다.
/// </summary>
public class RenexDialog : Dialog
{
    private string? _extension;

    /// <summary>완성된 파일 이름입니다.</summary>
    public string Filename { get; private set; }
    /// <summary>다시 열기 여부를 나타냅니다.</summary>
    public bool Reopen { get; private set; }

    // 컨트롤 선언
    private readonly Label _bookNameLabel;
    private readonly Entry _authorText;
    private readonly Entry _titleText;
    private readonly Entry _indexText;
    private readonly Entry _extraText;

    /// <summary>
    /// 책 이름을 변경할 수 있는 대화상자를 생성합니다.
    /// 이 대화상자는 부모 창(옵션)과 기존 파일명을 받아, 사용자가 작가, 책 이름, 번호, 추가 정보를 입력하여 새로운 파일명을 만들 수 있도록 합니다.
    /// </summary>
    /// <param name="parent">이 대화상자의 부모 윈도우입니다. 부모가 없으면 <see langword="null"/>을 전달할 수 있습니다.</param>
    /// <param name="filename">원래 책 파일명입니다. 대화상자에 표시되며, 이름 분석 및 초기화에 사용됩니다.</param>
    public RenexDialog(Window? parent, string filename) : base("책 이름 바꾸기", parent, DialogFlags.Modal)
    {
        #region 디자인

        SetDefaultSize(472, 236);
        //Modal = true;
        //WindowPosition = parent != null ? WindowPosition.CenterOnParent : WindowPosition.Center;
        Resizable = false;
        SkipTaskbarHint = true;
        
        Icon = Doumi.GetResourcePixmap("DgView.Resources.icon_rename.png");
        
        // 컨트롤 생성
        var renameLabel = new Label("작가") { Halign = Align.End, Valign = Align.Center };
        var originalLabel = new Label("--") { Halign = Align.Start, Valign = Align.Center };
        _bookNameLabel = new Label("--") { Halign = Align.Start, Valign = Align.Center };

        _authorText = new Entry();
        _titleText = new Entry();
        _indexText = new Entry();
        _extraText = new Entry();

        var label1 = new Label("책 이름") { Halign = Align.End, Valign = Align.Center };
        var label2 = new Label("번호") { Halign = Align.End, Valign = Align.Center };
        var label3 = new Label("추가 정보") { Halign = Align.End, Valign = Align.Center };
        var label4 = new Label("바꿀 이름") { Halign = Align.End, Valign = Align.Center };
        var label5 = new Label("원래 이름") { Halign = Align.End, Valign = Align.Center };

        originalLabel.StyleContext.AddClass("originalLabel");
        _bookNameLabel.StyleContext.AddClass("bookNameLabel");

        _authorText.KeyPressEvent += Entries_KeyPressEvent;
        _titleText.KeyPressEvent += Entries_KeyPressEvent;
        _indexText.KeyPressEvent += Entries_KeyPressEvent;
        _extraText.KeyPressEvent += Entries_KeyPressEvent;
        _authorText.Changed += Entries_Changed;
        _titleText.Changed += Entries_Changed;
        _indexText.Changed += Entries_Changed;
        _extraText.Changed += Entries_Changed;
        
        // 레이아웃
        var grid = new Grid
        {
            RowSpacing = 8,
            ColumnSpacing = 8,
            Margin = 16
        };

        // 첫 행: 타이틀 -> 안씀
        //grid.Attach(titleLabel, 0, 0, 4, 1);

        // 두 번째 행: 원래 이름
        grid.Attach(label5, 0, 1, 1, 1);
        grid.Attach(originalLabel, 1, 1, 3, 1);

        // 세 번째 행: 바꿀 이름
        grid.Attach(label4, 0, 2, 1, 1);
        grid.Attach(_bookNameLabel, 1, 2, 3, 1);

        // 네 번째 행: 책 이름
        grid.Attach(label1, 0, 3, 1, 1);
        grid.Attach(_titleText, 1, 3, 3, 1);

        // 다섯 번째 행: 작가, 번호
        grid.Attach(renameLabel, 0, 4, 1, 1);
        grid.Attach(_authorText, 1, 4, 1, 1);
        grid.Attach(label2, 2, 4, 1, 1);
        grid.Attach(_indexText, 3, 4, 1, 1);

        // 여섯 번째 행: 추가 정보
        grid.Attach(label3, 0, 5, 1, 1);
        grid.Attach(_extraText, 1, 5, 3, 1);

        // 전체 박스
        ContentArea.Add(grid);
        AddButton(Stock.Cancel, ResponseType.Cancel);
        AddButton(Stock.Ok, ResponseType.Ok);
        #endregion

        Filename = (string)filename.Clone();
        originalLabel.Text = filename;
        ParseFilename(filename);

        _titleText.GrabFocus();
        ShowAll();
    }

    /// <summary>
    /// 엔트리(입력란)에서 키 입력 이벤트를 처리합니다.
    /// 엔터 키를 누르면 다음 입력란으로 이동하거나, 마지막 입력란에서는 확인(OK) 동작을 수행합니다.
    /// Ctrl+Enter를 누르면 바로 확인(OK) 동작과 함께 Reopen을 true로 설정합니다.
    /// ESC 키를 누르면 취소(Cancel) 동작을 수행합니다.
    /// </summary>
    /// <param name="o">이벤트가 발생한 입력 컨트롤</param>
    /// <param name="args">키 입력 이벤트 인자</param>
    [GLib.ConnectBefore]
    private void Entries_KeyPressEvent(object o, KeyPressEventArgs args)
    {
        if (args.Event.Key is Gdk.Key.Return or Gdk.Key.KP_Enter)
        {
            // 엔터 키가 눌렸을 때
            if (o is not Entry entry)
                return;
            if ((args.Event.State & ModifierType.ControlMask) == ModifierType.ControlMask)
            {
                // 만약 Control 키가 눌렸다면, 엔트리의 포커스를 잡고 Ok 버튼을 클릭합니다.
                Reopen = true;
                entry.GrabFocus();
                Respond(ResponseType.Ok);
            }
            else
            {
                // 엔트리의 포커스를 다음 엔트리로 이동합니다.
                if (entry == _titleText)
                    _authorText.GrabFocus();
                else if (entry == _authorText)
                    _indexText.GrabFocus();
                else if (entry == _indexText)
                    _extraText.GrabFocus();
                else if (entry == _extraText)
                    Respond(ResponseType.Ok);
            }

            args.RetVal = true;
        }
        else if (args.Event.Key == Gdk.Key.Escape)
        {
            // ESC 키가 눌렸을 때
            Respond(ResponseType.Cancel);
            args.RetVal = true;
        }
    }

    /// <summary>
    /// 입력란(Entry) 값이 변경될 때마다 호출되어, 현재 입력값을 바탕으로 새 파일명을 만듭니다.
    /// </summary>
    /// <param name="sender">이벤트가 발생한 컨트롤</param>
    /// <param name="e">이벤트 인자</param>
    private void Entries_Changed(object? sender, EventArgs e)
    {
        MakeFilename();
    }

    /// <summary>
    /// 입력된 작가, 책 이름, 번호, 추가 정보, 확장자를 조합하여 새 파일명을 생성합니다.
    /// 생성된 파일명은 Filename 속성과 미리보기 라벨(_bookNameLabel)에 반영됩니다.
    /// </summary>
    private void MakeFilename()
    {
        var sb = new StringBuilder();

        // 작가
        if (_authorText.TextLength > 0)
            sb.Append($"[{_authorText.Text}] ");

        // 책이름
        if (_titleText.TextLength > 0)
            sb.Append(_titleText.Text);

        // 순번
        if (_indexText.TextLength > 0)
            sb.Append($" {_indexText.Text}");

        // 추가 정보
        if (_extraText.TextLength > 0)
            sb.Append($" ({_extraText.Text})");

        // 확장자
        if (_extension?.Length > 0)
            sb.Append(_extension);

        Filename = sb.ToString();
        _bookNameLabel.Text = Filename;
    }

    /// <summary>
    /// 기존 파일명을 분석하여 작가, 책 이름, 번호, 추가 정보, 확장자 등 각 입력란에 자동으로 분리하여 채웁니다.
    /// 파일명 형식이 맞지 않거나 분석에 실패하면 전체를 책 이름으로 처리합니다.
    /// </summary>
    /// <param name="filename">분석할 파일명</param>
    private void ParseFilename(string filename)
    {
        string ws;

        // 확장자
        var e = filename.LastIndexOf('.');
        if (e < 0)
            ws = filename;
        else
        {
            _extension = filename[e..];
            ws = filename[..e];
        }

        try
        {
            // 작가
            var n = ws.IndexOf('[');
            int l;
            if (n >= 0)
            {
                l = ws.IndexOf(']');
                if (l > n)
                {
                    _authorText.Text = ws.Substring(n + 1, l - n - 1).Trim();
                    ws = ws[(l + 1)..].TrimStart();
                }
            }

            // 추가
            n = ws.LastIndexOf('(');
            if (n >= 0)
            {
                l = ws.LastIndexOf(')');
                if (l > n)
                {
                    _extraText.Text = ws.Substring(n + 1, l - n - 1).Trim();
                    ws = ws[..(n - 1)].Trim();
                }
            }

            // 순번
            n = ws.LastIndexOf(' ');
            if (n >= 0)
            {
                var s = ws[(n + 1)..];
                if (int.TryParse(s, out l))
                {
                    _indexText.Text = ws[(n + 1)..].Trim();
                    ws = ws[..n].Trim();
                }
            }

            // 책이름
            _titleText.Text = ws.Trim();
        }
        catch
        {
            // 오류나면 그냥 통짜로
            _authorText.Text = string.Empty;
            _titleText.Text = string.Empty;
            _indexText.Text = string.Empty;
            _extraText.Text = string.Empty;

            _titleText.Text = e < 0 ? filename : filename[..e];
        }
    }
}

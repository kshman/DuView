using System.ComponentModel;
using System.Text;
using Gdk;
using Window = Gtk.Window;

namespace DgView.Forms;

public class RenexDialog : Dialog
{
    private string? _extension;

    public string Filename { get; private set; }
    public bool Reopen { get; private set; } = false;

    // 컨트롤 선언
    private readonly Label _bookNameLabel;
    private readonly Entry _authorText;
    private readonly Entry _titleText;
    private readonly Entry _indexText;
    private readonly Entry _extraText;

    public RenexDialog(Window? parent, string filename) : base("책 이름 바꾸기", parent, DialogFlags.Modal)
    {
        #region 디자인

        SetDefaultSize(472, 236);
        //Modal = true;
        //WindowPosition = parent != null ? WindowPosition.CenterOnParent : WindowPosition.Center;
        Resizable = false;
        
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

        _authorText.KeyPressEvent += Entries_OnKeyPressEvent;
        _titleText.KeyPressEvent += Entries_OnKeyPressEvent;
        _indexText.KeyPressEvent += Entries_OnKeyPressEvent;
        _extraText.KeyPressEvent += Entries_OnKeyPressEvent;
        _authorText.Changed += Entries_OnChanged;
        _titleText.Changed += Entries_OnChanged;
        _indexText.Changed += Entries_OnChanged;
        _extraText.Changed += Entries_OnChanged;

        // CSS
        var css = new CssProvider();
        css.LoadFromData(
            """
            .originalLabel {
                border: 1px solid #cccccc; border-radius: 4px;
            }
            .bookNameLabel {
                border: 1px solid #ff8080; border-radius: 4px;
            }
            """);
        StyleContext.AddProviderForScreen(Screen.Default, css, StyleProviderPriority.Application);
        
        originalLabel.StyleContext.AddClass("originalLabel");
        _bookNameLabel.StyleContext.AddClass("bookNameLabel");
        
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

    [GLib.ConnectBefore]
    private void Entries_OnKeyPressEvent(object o, KeyPressEventArgs args)
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

    private void Entries_OnChanged(object? sender, EventArgs e)
    {
        MakeFilename();
    }

    // 파일이름 만들기
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

    // 파일 이름 분석
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

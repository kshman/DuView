// ReSharper disable MissingXmlDoc

using System.ComponentModel;
using System.Text;

namespace DuView.Forms;

public partial class RenexForm : Form
{
    private string? _extension;

    [DefaultValue("")] public string Filename { get; private set; } = string.Empty;

    [DefaultValue(false)] public bool Reopen { get; private set; }

    public RenexForm()
    {
        InitializeComponent();
    }

    private void RenexForm_Load(object sender, EventArgs e)
    {
        ActiveControl = TitleText;
    }

    private void RenexForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
            MakeFilename();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        DuForm.EffectAppear(this);
    }

    private void RenexForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e is { Control: true, KeyCode: Keys.Enter })
        {
            // 바꾸고 다시 열기
            Reopen = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void Texts_TextChanged(object sender, EventArgs e)
    {
        MakeFilename();
    }

    // 파일이름 만들기
    private void MakeFilename()
    {
        var sb = new StringBuilder();

        // 작가
        if (AuthorText.TextLength > 0)
            sb.Append($"[{AuthorText.Text}] ");

        // 책이름
        if (TitleText.TextLength > 0)
            sb.Append(TitleText.Text);

        // 순번
        if (IndexText.TextLength > 0)
            sb.Append($" {IndexText.Text}");

        // 추가 정보
        if (ExtraText.TextLength > 0)
            sb.Append($" ({ExtraText.Text})");

        // 확장자
        if (_extension?.Length > 0)
            sb.Append(_extension);

        Filename = sb.ToString();
        BookNameLabel.Text = Filename;
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
                    AuthorText.Text = ws.Substring(n + 1, l - n - 1).Trim();
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
                    ExtraText.Text = ws.Substring(n + 1, l - n - 1).Trim();
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
                    IndexText.Text = ws[(n + 1)..].Trim();
                    ws = ws[..n].Trim();
                }
            }

            // 책이름
            TitleText.Text = ws.Trim();
        }
        catch
        {
            // 오류나면 그냥 통짜로
            AuthorText.Text = string.Empty;
            TitleText.Text = string.Empty;
            IndexText.Text = string.Empty;
            ExtraText.Text = string.Empty;

            TitleText.Text = e < 0 ? filename : filename[..e];
        }
    }

    // 다이얼로그
    public DialogResult ShowDialog(IWin32Window owner, string filename)
    {
        Opacity = 0;

        Filename = (string)filename.Clone();

        OriginalLabel.Text = filename;
        ParseFilename(filename);

        return ShowDialog(owner);
    }
}

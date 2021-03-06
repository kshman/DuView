using System;
using System.Text;
using System.Windows.Forms;
using Du.WinForms;

namespace DuView
{
	public partial class RenameExForm : Form
	{
		public string Filename { get; private set; } = string.Empty;

		private readonly BadakFormWorker _bfw;

		private string _extension;

		public RenameExForm()
		{
			InitializeComponent();

			SystemButton.Form = this;
			_bfw = new BadakFormWorker(this, SystemButton)
			{
				MoveTopToMaximize = false,
				BodyAsTitle = true,
			};
		}

		private void RenameForm_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Settings.FirefoxRun))
				SearchButton.Enabled = false;

			ActiveControl = TitleText;
		}

		private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void RenameForm_MouseDown(object sender, MouseEventArgs e)
		{
			_bfw.DragOnDown(e);
		}

		private void RenameForm_MouseUp(object sender, MouseEventArgs e)
		{
			_bfw.DragOnUp(e);
		}

		private void RenameForm_MouseMove(object sender, MouseEventArgs e)
		{
			_bfw.DragOnMove(e);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			FormDu.EffectAppear(this);
		}

		protected override void WndProc(ref Message m)
		{
			if (!_bfw.WndProc(ref m))
				base.WndProc(ref m);
		}

		private void DoOkButton_Click(object sender, EventArgs e)
		{
			Settings.RenameOpenNext = true;
			DialogResult = DialogResult.OK;
		}

		private void DoOkReopenButton_Click(object sender, EventArgs e)
		{
			Settings.RenameOpenNext = false;
			DialogResult = DialogResult.OK;
		}

		public DialogResult ShowDialog(IWin32Window owner, string filename)
		{
			Opacity = 0;

			Filename = (string)filename.Clone();

			OriginalNameLabel.Text = filename;
			ParseFilename(filename);

			return ShowDialog(owner);
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(OriginalNameLabel.Text))
				return;

			var keyword = OriginalNameLabel.Text.Replace('"', '\'');

			var ps = new System.Diagnostics.Process();
			ps.StartInfo.FileName = Settings.FirefoxRun;
			ps.StartInfo.Arguments = $"-search \"{keyword}\"";
			ps.StartInfo.UseShellExecute = false;
			ps.StartInfo.CreateNoWindow = true;
			ps.EnableRaisingEvents = false;
			ps.Start();
		}

		private void BuildRename_TextChanged(object sender, EventArgs e)
		{
			MakeFilename();
		}

		//
		private void MakeFilename()
		{
			var sb = new StringBuilder();

			// 작가
			if (AuthorText.TextLength > 0)
				sb.AppendFormat("[{0}] ", AuthorText.Text);

			// 책이름
			if (TitleText.TextLength > 0)
				sb.Append(TitleText.Text);

			// 순번
			if (IndexText.TextLength > 0)
				sb.AppendFormat(" {0}", IndexText.Text);

			// 추가 정보
			if (AdditionalText.TextLength > 0)
				sb.AppendFormat(" ({0})", AdditionalText.Text);

			// 확장자
			if (_extension.Length > 0)
				sb.Append(_extension);

			Filename = sb.ToString();
			BookNameLabel.Text = Filename;
		}

		//
		private void ParseFilename(string filename)
		{
			string ws;
			int e, n, l;

			// 확장자
			e = filename.LastIndexOf('.');
			if (e < 0)
				ws = filename;
			else
			{
				_extension = filename.Substring(e);
				ws = filename.Substring(0, e);
			}

			try
			{
				// 작가
				n = ws.IndexOf('[');
				if (n >= 0)
				{
					l = ws.IndexOf(']');
					if (l > n)
					{
						AuthorText.Text = ws.Substring(n + 1, l - n - 1).Trim();
						ws = ws.Substring(l + 1).TrimStart();
					}
				}

				// 추가
				n = ws.LastIndexOf('(');
				if (n >= 0)
				{
					l = ws.LastIndexOf(')');
					if (l > n)
					{
						AdditionalText.Text = ws.Substring(n + 1, l - n - 1).Trim();
						ws = ws.Substring(0, n - 1).Trim();
					}
				}

				// 순번
				n = ws.LastIndexOf(' ');
				if (n >= 0)
				{
					var s = ws.Substring(n + 1);
					if (int.TryParse(s, out l))
					{
						IndexText.Text = ws.Substring(n + 1).Trim();
						ws = ws.Substring(0, n).Trim();
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
				AdditionalText.Text = string.Empty;

				if (e < 0)
					TitleText.Text = filename;
				else
					TitleText.Text = filename.Substring(0, e);
			}
		}
	}
}

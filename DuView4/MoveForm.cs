using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Du.WinForms;

namespace DuView
{
	public partial class MoveForm : Form
	{
		public string Filename { get; private set; } = string.Empty;

		private readonly BadakFormWorker _bfw;

		public MoveForm()
		{
			InitializeComponent();

			SystemButton.Form = this;
			_bfw = new BadakFormWorker(this, SystemButton)
			{
				MoveTopToMaximize = false,
				BodyAsTitle = true,
			};

			DestinationLabel.Text = string.Empty;
		}

		private void MoveForm_Load(object sender, EventArgs e)
		{
			RefreshFasts();
		}

		private void MoveForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void MoveForm_MouseDown(object sender, MouseEventArgs e)
		{
			_bfw.DragOnDown(e);
		}

		private void MoveForm_MouseUp(object sender, MouseEventArgs e)
		{
			_bfw.DragOnUp(e);
		}

		private void MoveForm_MouseMove(object sender, MouseEventArgs e)
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
			var s = DestinationLabel.Text;
			if (Directory.Exists(s))
				Filename = Path.Combine(s, FilenameLabel.Text);

			DialogResult = DialogResult.OK;
		}

		public DialogResult ShowDialog(IWin32Window owner, string filename)
		{
			Opacity = 0;

			FilenameLabel.Text = filename;

			return ShowDialog(owner);
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			var dir = DestinationLabel.Text;
			if (string.IsNullOrEmpty(dir))
				dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			FolderBrowserDialog dlg = new FolderBrowserDialog()
			{
				ShowNewFolderButton = false,
				SelectedPath = dir,
			};

			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			DestinationLabel.Text = dlg.SelectedPath;
		}

		private void Fast1Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(0);
		}

		private void Fast2Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(1);
		}

		private void Fast3Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(2);
		}

		private void Fast4Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(3);
		}

		private void Fast5Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(4);
		}

		private void Fast6Browse_Click(object sender, EventArgs e)
		{
			FastBrowseFolder(5);
		}

		private void Fast1Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(0);
		}

		private void Fast2Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(1);
		}

		private void Fast3Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(2);
		}

		private void Fast4Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(3);
		}

		private void Fast5Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(4);
		}

		private void Fast6Button_Click(object sender, EventArgs e)
		{
			ClickFastButton(5);
		}

		//
		private bool FastBrowseFolder(int index)
		{
			var dir = Settings.GetMoveKeep(index);
			if (string.IsNullOrEmpty(dir))
				dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			FolderBrowserDialog dlg = new FolderBrowserDialog()
			{
				ShowNewFolderButton = false,
				SelectedPath = dir,
			};

			if (dlg.ShowDialog() != DialogResult.OK)
				return false;

			Settings.SetMoveKeep(index, dlg.SelectedPath);
			RefreshFasts();

			return true;
		}

		//
		private void ClickFastButton(int index)
		{
			var s = Settings.GetMoveKeep(index);

			if (!string.IsNullOrEmpty(s))
				DestinationLabel.Text = s;
		}

		//
		private void RefreshFasts()
		{
			var btns = new Button[6]
			{
				Fast1Button, Fast2Button, Fast3Button,
				Fast4Button, Fast5Button, Fast6Button,
			};

			for (var i = 0; i < 6; i++)
			{
				var s = Settings.GetMoveKeep(i);
				if (!string.IsNullOrEmpty(s))
				{
					var di = new DirectoryInfo(s);
					if (di.Exists)
						btns[i].Text = di.Name;
				}
			}
		}
	}
}

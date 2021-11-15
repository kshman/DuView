using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuView
{
	internal abstract class BookBase : IDisposable
	{
		public string FileName { get; set; }
		public string OnlyFileName { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPage => _entries.Count;
		public Image ImagePage1 => _ip1;
		public Image ImagePage2 => _ip2;

		protected List<object> _entries = new List<object>();
		protected Image _ip1 = null;
		protected Image _ip2 = null;

		protected BookBase()
		{
		}

		protected BookBase(string filename, string onlyfilename)
		{
			FileName = filename;
			OnlyFileName = onlyfilename;
			CurrentPage = 0;
		}

		protected void SetFileInfo(FileInfo fi)
		{
			FileName = fi.FullName;
			OnlyFileName = fi.Name;
		}

		protected void SetDirectoryInfo(DirectoryInfo di)
		{
			FileName = di.FullName;
			OnlyFileName = di.Name;
		}

		public void Dispose()
		{
			Close();
		}

		public virtual void Close()
		{

		}

		public void ActivateSetting()
		{
			Settings.LastFileName = FileName;
			Settings.LastFilePage = CurrentPage;
		}

		protected abstract Stream OpenStream(object entry);

		public bool ReadPage(out Image image, ref int pageno)
		{
			image = null;

			int npage = pageno;

			for (var i = npage; i < TotalPage; i++)
			{
				Stream stream = OpenStream(_entries[i]);

				if (stream != null)
				{
					image = Image.FromStream(stream);
					stream.Close();

					npage = i;
					break;
				}
			}

			if (npage < TotalPage)
			{
				pageno = npage;
				return true;
			}
			else
			{
				// 후... 더 못읽나
				return false;
			}
		}

		public bool ReadNextPage(out Image image)
		{
			int pageno = CurrentPage + 1;

			if (!ReadPage(out image, ref pageno))
				return false;
			else
			{
				CurrentPage = pageno;
				return true;
			}
		}
		
		public bool PrepareCurrent(Settings.ViewerMode mode)
		{
			int pageno;

			if (mode == Settings.ViewerMode.FitWidth || mode == Settings.ViewerMode.FitHeight)
			{
				pageno = CurrentPage;
				if (!ReadPage(out _ip1, ref pageno))
					return false;

				CurrentPage = pageno;
			}
			else if (mode == Settings.ViewerMode.LeftToRight || mode == Settings.ViewerMode.RightToLeft)
			{
				pageno = CurrentPage;
				if (!ReadPage(out _ip1, ref pageno))
					return false;

				pageno++;
				if (!ReadPage(out _ip2, ref pageno))
					_ip2 = null;

				CurrentPage = pageno;
			}
			else
			{
				// 뭠미
				return false;
			}

			return true;
		}

		public bool PrepareNext(Settings.ViewerMode mode)
		{
			int pageno;

			if (mode == Settings.ViewerMode.FitWidth || mode == Settings.ViewerMode.FitHeight)
			{
				pageno = CurrentPage + 1;
				if (!ReadPage(out _ip1, ref pageno))
					return false;

				CurrentPage = pageno;
			}
			else if (mode == Settings.ViewerMode.LeftToRight || mode == Settings.ViewerMode.RightToLeft)
			{
				pageno = CurrentPage + 1;
				if (!ReadPage(out _ip1, ref pageno))
					return false;

				pageno++;
				if (!ReadPage(out _ip2, ref pageno))
					_ip2 = null;

				CurrentPage = pageno;
			}
			else
			{
				// 뭠미
				return false;
			}

			return true;
		}

		public static bool IsValidImageFile(string extension)
		{
			switch (extension)
			{
				case ".png":
				case ".jpg":
				case ".jpeg":
				case ".bmp":
				case ".tga":
					return true;
				default:
					return false;
			}
		}
	}
}

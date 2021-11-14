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

		protected List<object> _entries = new List<object>();

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

		public virtual void Dispose()
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

			int npage = pageno + 1;

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
			int pageno = CurrentPage;

			if (!ReadPage(out image, ref pageno))
				return false;
			else
			{
				CurrentPage = pageno;
				return true;
			}
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

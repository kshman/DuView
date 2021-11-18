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

		public Image PageLeft { get; set; }
		public Image PageRight { get; set; }

		public long CacheSize => _csize;

		//
		protected List<object> _entries = new List<object>();

		private Dictionary<int, Image> _cache = new Dictionary<int, Image>();
		private long _csize = 0;

		//
		protected BookBase()
		{
		}

		//
		protected BookBase(string filename, string onlyfilename)
		{
			FileName = filename;
			OnlyFileName = onlyfilename;
			CurrentPage = 0;
		}

		//
		protected void SetFileInfo(FileInfo fi)
		{
			FileName = fi.FullName;
			OnlyFileName = fi.Name;
		}

		//
		protected void SetDirectoryInfo(DirectoryInfo di)
		{
			FileName = di.FullName;
			OnlyFileName = di.Name;
		}

		//
		public void Dispose()
		{
			Close();
		}

		//
		public virtual void Close()
		{
			_cache.Clear();
		}

		//
		public void ActivateSetting()
		{
			Settings.LastFileName = FileName;
			Settings.LastFilePage = CurrentPage;
		}

		//
		private void CacheImage(int page, Image img)
		{
			if (_cache.ContainsKey(page))
				return;

			var size = img.Width * img.Height * 4;

			if ((_csize + size) > Settings.MaxCacheSize && _cache.Count > 0)
			{
				var first = _cache.ElementAt(0);
				var fsize = first.Value.Width * first.Value.Height * 4;

				_cache.Remove(first.Key);
				_csize -= fsize;
			}

			_cache.Add(page, img);
			_csize += size;
		}

		//
		private bool TryCache(int page, out Image img)
		{
			return _cache.TryGetValue(page, out img);
		}

		//
		protected abstract Stream OpenStream(object entry);

		//
		public Image ReadPage(int pageno)
		{
			Image img = null;

			if (pageno >= 0 && pageno < TotalPage)
			{
				if (TryCache(pageno, out img))
					return img;

				var en = _entries[pageno];
				using (var st = OpenStream(en))
				{
					if (st != null)
					{
						img = Image.FromStream(st);
						CacheImage(pageno, img);
					}
				}
			}

			if (img == null)
			{
				// 기본 이미지
				img = Properties.Resources.ouch_noimg;
			}

			return img;
		}

		//
		public void PrepareCurrent(Types.ViewMode mode)
		{
			if (mode == Types.ViewMode.FitWidth || mode == Types.ViewMode.FitHeight)
			{
				PageLeft = ReadPage(CurrentPage);
				PageRight = null;
			}
			else if (mode == Types.ViewMode.LeftToRight || mode == Types.ViewMode.RightToLeft)
			{
				Image left = ReadPage(CurrentPage);
				Image right = null;

				if (left.Width > left.Height)
				{
					// 폭이 넓으면 1장만
				}
				else
				{
					if (CurrentPage < TotalPage)
						right = ReadPage(CurrentPage + 1);
				}

				if (mode == Types.ViewMode.LeftToRight)
				{
					PageLeft = right;
					PageRight = left;
				}
				else
				{
					PageLeft = left;
					PageRight = right;
				}
			}
			else
			{
				// 멍미
				PageLeft = null;
				PageRight = null;
			}
		}

		// 
		public void MoveNext(Types.ViewMode mode)
		{
			if (mode == Types.ViewMode.FitWidth || mode != Types.ViewMode.FitHeight)
			{
				if (CurrentPage + 1 < TotalPage)
					CurrentPage++;
			}
			else if (mode == Types.ViewMode.LeftToRight || mode == Types.ViewMode.RightToLeft)
			{
				if (CurrentPage + 2 < TotalPage)
					CurrentPage += 2;
			}
		}

		// 
		public void MovePrev(Types.ViewMode mode)
		{
			if (mode == Types.ViewMode.FitWidth || mode != Types.ViewMode.FitHeight)
				CurrentPage--;
			else if (mode == Types.ViewMode.LeftToRight || mode == Types.ViewMode.RightToLeft)
				CurrentPage -= 2;

			if (CurrentPage < 0)
				CurrentPage = 0;
		}

		//
		public void MovePage(Types.ViewMode mode, int page)
		{
			// 하... Math.Clamp 는 Net6 전용이네
			if (page < 0)
				CurrentPage = 0;
			else if (page >= TotalPage)
				CurrentPage = TotalPage - 1;
			else
				CurrentPage = page;
		}
	}
}

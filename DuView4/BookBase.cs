﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Du.Data;

namespace DuView
{
	public abstract class BookBase : IDisposable
	{
		public string FileName { get; private set; }
		public string OnlyFileName { get; private set; }

		public int CurrentPage { get; set; }
		public int TotalPage => _entries.Count;

		public Image PageLeft { get; private set; }
		public Image PageRight { get; private set; }

		public long CacheSize { get; private set; }

		//
		protected readonly List<object> _entries = new List<object>();
		private readonly Dictionary<int, Image> _cache = new Dictionary<int, Image>();

		//
		protected BookBase()
		{
			FileName = string.Empty;
			OnlyFileName = string.Empty;
		}

		//
		protected abstract Stream OpenStream(object entry);
		protected abstract string GetEntryName(object entry);
		public abstract IEnumerable<Types.BookEntryInfo> GetEntriesInfo();

		public virtual bool CanDeleteFile(out string reason)
		{
			reason = string.Empty;
			return true;
		}

		public abstract bool DeleteFile(out bool closebook);

		//
		protected void SetFileName(FileInfo fi)
		{
			FileName = fi.FullName;
			OnlyFileName = fi.Name;
		}

		//
		protected void SetFileName(DirectoryInfo di)
		{
			FileName = di.FullName;
			OnlyFileName = di.Name;
		}

		//
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		//
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_entries.Clear();
				_cache.Clear();
			}
		}

		//
		private void CacheImage(int page, Image img)
		{
			const int bpp = 4; // byte per pixel

			if (_cache.ContainsKey(page))
				return;

			var size = img.Width * img.Height * bpp;

			if ((CacheSize + size) > Settings.MaxCacheSize && _cache.Count > 0)
			{
				var first = _cache.ElementAt(0);
				var fsize = first.Value.Width * first.Value.Height * bpp;

				_cache.Remove(first.Key);
				CacheSize -= fsize;
			}

			_cache.Add(page, img);
			CacheSize += size;
		}

		//
		private bool TryCache(int page, out Image img)
		{
			return _cache.TryGetValue(page, out img);
		}

		//
		private Image ReadPage(int pageno)
		{
			Image img = null;

			if (pageno >= 0 && pageno < TotalPage)
			{
				if (TryCache(pageno, out img))
					return img;

				var en = _entries[pageno];
				using (var st = OpenStream(en))
					if (st != null)
					{
						img = Image.FromStream(st);
						img.Tag = GetEntryName(en);
						CacheImage(pageno, img);
					}
			}

			return img ?? Properties.Resources.ouch_noimg;
		}

		//
		public void PrepareImages()
		{
			var mode = Settings.ViewMode;

			if (mode == Types.ViewMode.FitWidth || mode == Types.ViewMode.FitHeight)
			{
				PageLeft = ReadPage(CurrentPage);
				PageRight = null;
			}
			else if (mode == Types.ViewMode.LeftToRight || mode == Types.ViewMode.RightToLeft)
			{
				var left = ReadPage(CurrentPage);

				if (left == null)
				{
					// 머여
				}
				else
				{
					Image right = null;

					if (left.Width > left.Height)
					{
						// 폭이 넓으면 1장만
					}
					else
					{
						if (CurrentPage + 1 < TotalPage)
						{
							right = ReadPage(CurrentPage + 1);
							if (right != null && right.Width > right.Height)
							{
								// 다른쪽도 넓으면 1장만 나오게 함
								right = null;
							}
						}
					}

					if (mode == Types.ViewMode.LeftToRight)
					{
						PageLeft = left;
						PageRight = right;
					}
					else
					{
						PageLeft = right;
						PageRight = left;
					}
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
		public bool MoveNext()
		{
			var mode = Settings.ViewMode;
			var prev = CurrentPage;

			if (PageLeft == null || PageRight == null)
			{
				// 이건 위험하지만 일단 쓰자
				mode = Types.ViewMode.FitWidth;
			}

			switch (mode)
			{
				case Types.ViewMode.FitWidth:
				case Types.ViewMode.FitHeight:
					if (CurrentPage + 1 < TotalPage)
						CurrentPage++;
					break;

				case Types.ViewMode.LeftToRight:
				case Types.ViewMode.RightToLeft:
					if (CurrentPage + 2 < TotalPage)
						CurrentPage += 2;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			return prev != CurrentPage;
		}

		// 
		public bool MovePrev()
		{
			var mode = Settings.ViewMode;
			var prev = CurrentPage;

			if (PageLeft == null || PageRight == null)
			{
				// 이건 위험하지만 일단 쓰자
				mode = Types.ViewMode.FitWidth;
			}

			switch (mode)
			{
				case Types.ViewMode.FitWidth:
				case Types.ViewMode.FitHeight:
					CurrentPage--;
					break;

				case Types.ViewMode.LeftToRight:
				case Types.ViewMode.RightToLeft:
					CurrentPage -= 2;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			if (CurrentPage < 0)
				CurrentPage = 0;

			return prev != CurrentPage;
		}

		//
		public bool MovePage(int page)
		{
			var prev = CurrentPage;

#if false
			// 하... Math.Clamp 는 Net6 전용이네
			CurrentPage = Math.Clamp(page, 0, TotalPage - 1);
#else
			if (page < 0)
				CurrentPage = 0;
			else if (page >= TotalPage)
				CurrentPage = TotalPage - 1;
			else
				CurrentPage = page;
#endif

			return prev != CurrentPage;
		}

		//
		public virtual string FindNextFile(bool no_i_want_prev_file)
		{
			return null;
		}

		//
		protected class BookEntryInfoComparer : IComparer<Types.BookEntryInfo>
		{
			public int Compare(Types.BookEntryInfo x, Types.BookEntryInfo y)
			{
				return StringAsNumericComparer.StringAsNumericCompare(x.Name, y.Name);
			}
		}
	}
}
namespace DuView;

public abstract class BookBase : IDisposable
{
	public string FileName { get; private set; }
	public string OnlyFileName { get; private set; }

	public int CurrentPage { get; set; }
	public int TotalPage => _entries.Count;

	public Image? PageLeft { get; private set; }
	public Image? PageRight { get; private set; }

	public long CacheSize { get; private set; }

	//
	protected readonly List<object> _entries = new();
	private readonly Dictionary<int, Image> _cache = new();

	//
	protected BookBase()
	{
		FileName = string.Empty;
		OnlyFileName = string.Empty;
	}

	//
	protected abstract Stream? OpenStream(object entry);
	protected abstract string? GetEntryName(object entry);
	public abstract IEnumerable<Types.BookEntryInfo> GetEntriesInfo();
	public virtual bool CanDeleteFile(out string? reason) { reason = string.Empty; return true; }
	public abstract bool DeleteFile(out bool closebook);
	public abstract bool RenameFile(string newfilename, out string fullpath);

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
		const int bpp = 4;  // byte per pixel

		if (_cache.ContainsKey(page))
			return;

		var size = img.Width * img.Height * bpp;

		if ((CacheSize + size) > Settings.MaxActualPageCache && _cache.Count > 0)
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
	private bool TryCache(int page, out Image? img)
	{
		return _cache.TryGetValue(page, out img);
	}

	//
	private Image? ReadPage(int pageno)
	{
		Image? img = null;

		if (pageno >= 0 && pageno < TotalPage)
		{
			if (TryCache(pageno, out img))
				return img;

			var en = _entries[pageno];
			using var st = OpenStream(en);

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

		if (mode is Types.ViewMode.FitWidth or Types.ViewMode.FitHeight)
		{
			PageLeft = ReadPage(CurrentPage);
			PageRight = null;
		}
		else if (mode is Types.ViewMode.LeftToRight or Types.ViewMode.RightToLeft)
		{
			var left = ReadPage(CurrentPage);

			if (left == null)
			{
				// 머여
			}
			else
			{
				Image? right = null;

				var entryname = left.Tag as string;
				if (entryname == null || !ToolBox.IsAnimatedImageFile(entryname, false))
				{
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
				}   // 엔트리 애니메이션 체크

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

#if true
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
	public virtual string? FindNextFile(Types.BookDirection direction)
	{
		return null;
	}

	//
	public string? FindNextFileAny(Types.BookDirection first_direction)
	{
		string? filename;

		if (first_direction == Types.BookDirection.Next)
		{
			filename =
			  FindNextFile(Types.BookDirection.Next) ??
			  FindNextFile(Types.BookDirection.Previous) ??
			  null;
		}
		else if (first_direction == Types.BookDirection.Previous)
		{
			filename =
			  FindNextFile(Types.BookDirection.Previous) ??
			  FindNextFile(Types.BookDirection.Next) ??
			  null;
		}
		else
			filename = null;

		return filename;
	}

	//
	protected class BookEntryInfoComparer : IComparer<Types.BookEntryInfo>
	{
		public int Compare(Types.BookEntryInfo? x, Types.BookEntryInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.Name, y?.Name);
		}
	}
}

// 하면 좋은거
// 메타데이터: https://docs.microsoft.com/ko-kr/dotnet/desktop/winforms/advanced/how-to-read-image-metadata?view=netframeworkdesktop-4.8

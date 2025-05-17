using System.Drawing.Imaging;
using DuView.Types;
using DuView.WebPWrapper;

namespace DuView;

public abstract class BookBase : IDisposable
{
	public string FileName { get; private set; } = string.Empty;
	public string OnlyFileName { get; private set; } = string.Empty;

	public int CurrentPage { get; set; }
	public int TotalPage => _entries.Count;

	public PageImage? PageLeft { get; private set; }
	public PageImage? PageRight { get; private set; }

	public long CacheSize { get; private set; }

	public ViewMode ViewMode { get; set; } = ViewMode.Follow;
	private ViewMode ActualViewMode => ViewMode == ViewMode.Follow ? Settings.ViewMode : ViewMode;

	//
	protected readonly List<object> _entries = new();
	private readonly Dictionary<int, MemoryStream> _cache = new();

	//
	protected abstract MemoryStream? ReadEntry(object entry);
	public abstract string? GetEntryName(object entry);
	public abstract IEnumerable<BookEntryInfo> GetEntriesInfo();
	public virtual bool CanDeleteFile(out string? reason) { reason = string.Empty; return true; }
	public abstract bool DeleteFile(out bool close_book);
	public abstract bool RenameFile(string new_filename, out string full_path);
	public abstract bool MoveFile(string new_filename);
	public virtual bool DisplayEntryTitle => false;

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
		if (!disposing)
			return;

		_entries.Clear();
		_cache.Clear();
		PageLeft?.Dispose();
		PageRight?.Dispose();
	}

	//
	private void CacheStream(int page, MemoryStream ms)
	{
		if (_cache.ContainsKey(page))
			return;

		var size = ms.Length;

		if ((CacheSize + size) > Settings.MaxActualPageCache && _cache.Count > 0)
		{
			var (key, value) = _cache.ElementAt(0);

			_cache.Remove(key);
			CacheSize -= value.Length;

			value.Dispose();
		}

		_cache.Add(page, ms);
		CacheSize += size;
	}

	//
	private bool TryCache(int page, out MemoryStream? st)
	{
		return _cache.TryGetValue(page, out st);
	}

	//
	private PageImage ReadPage(int pageno)
	{
		if (pageno < 0 || pageno >= TotalPage)
			return new PageImage(Properties.Resources.ouch_noimg);

		var en = _entries[pageno];
		var storecache = false;

		if (!TryCache(pageno, out var ms))
		{
			// 캐시에 없으면 파일 처리
			ms = ReadEntry(en);
			storecache = true;
		}

		if (ms == null)
			return new PageImage(Properties.Resources.ouch_noimg);

		Image? img = null;
		List<AnimatedFrame>? frames = null;
		var isgif = false;

		try
		{
			// 일단 이미지로 읽어보자
			img = Image.FromStream(ms);

			// 애니메이션 처리
			if (Equals(img.RawFormat, ImageFormat.Gif) /*|| Equals(img.RawFormat, ImageFormat.Webp)*/)
			{
				if (Settings.UseGdipGif)
					isgif = true;
				else
				{
					var cnt = img.GetFrameCount(FrameDimension.Time);
					if (cnt > 1)
					{
						var times = img.GetPropertyItem(0x5100)?.Value;
						if (times != null)
						{
							frames = new List<AnimatedFrame>();
							for (var i = 0; i < cnt; i++)
							{
								var dur = BitConverter.ToInt32(times, 4 * i);
								frames.Add(new AnimatedFrame(new Bitmap(img), dur * 10));
								img.SelectActiveFrame(FrameDimension.Time, i);
							}

							img.Dispose();
							img = null;
						}
					}
				}
			}
		}
		catch (Exception e)
		{
			System.Diagnostics.Debug.WriteLine($"Not .NET image: {e.Message}");

			// 지원하는 형식인지 확인하자
			ms.Position = 0;
			var raw = ms.ToArray();

			// WEBP?
			if (WebP.IsWebP(raw))
			{
				WebP.GetInfo(raw, out var width, out var height, out var alpha, out var animation, out _);
				if (!animation)
					img = WebP.Decode(raw, width, height, alpha);
				else
				{
					// 애니메이션...
					frames = WebP.AnimDecode(raw) as List<AnimatedFrame>;
				}
			}
		}

		if (storecache && img != null)
			CacheStream(pageno, ms);

		if (frames != null)
			return new PageImage(frames);
		if (img != null)
			return new PageImage(img, isgif);
		return new PageImage(Properties.Resources.ouch_noimg);
	}

	//
	public void PrepareImages()
	{
		PageLeft?.Dispose();
		PageRight?.Dispose();

		switch (ActualViewMode)
		{
			case ViewMode.FitWidth or ViewMode.FitHeight:
				PageLeft = ReadPage(CurrentPage);
				PageRight = null;
				break;

			case ViewMode.LeftToRight or ViewMode.RightToLeft:
			{
				var left = ReadPage(CurrentPage);

				PageImage? right = null;

				if (!left.HasAnimation)
				{
					if (left.Image.Width > left.Image.Height)
					{
						// 폭이 넓으면 1장만
					}
					else
					{
						if (CurrentPage + 1 < TotalPage)
						{
							right = ReadPage(CurrentPage + 1);
							if (right.Image.Width > right.Image.Height)
							{
								// 다른쪽도 넓으면 1장만 나오게 함
								right = null;
							}
						}
					}
				}   // 엔트리 애니메이션 체크

				if (ActualViewMode == ViewMode.LeftToRight)
				{
					PageLeft = left;
					PageRight = right;
				}
				else
				{
					PageLeft = right;
					PageRight = left;
				}

				break;
			}

			default:
				// 멍미
				PageLeft = null;
				PageRight = null;
				break;
		}
	}

	// 
	public bool MoveNext()
	{
		var mode = ActualViewMode;
		var prev = CurrentPage;

		if (PageLeft == null || PageRight == null)
		{
			// 이건 위험하지만 일단 쓰자
			mode = ViewMode.FitWidth;
		}

		switch (mode)
		{
			case ViewMode.FitWidth:
			case ViewMode.FitHeight:
				if (CurrentPage + 1 < TotalPage)
					CurrentPage++;
				break;

			case ViewMode.LeftToRight:
			case ViewMode.RightToLeft:
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
		var mode = ActualViewMode;
		var prev = CurrentPage;

		if (PageLeft == null || PageRight == null)
		{
			// 이건 위험하지만 일단 쓰자
			mode = ViewMode.FitWidth;
		}

		switch (mode)
		{
			case ViewMode.FitWidth:
			case ViewMode.FitHeight:
				CurrentPage--;
				break;

			case ViewMode.LeftToRight:
			case ViewMode.RightToLeft:
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

		CurrentPage = Math.Clamp(page, 0, TotalPage - 1);

		return prev != CurrentPage;
	}

	//
	public virtual string? FindNextFile(BookDirection direction)
	{
		return null;
	}

	//
	public string? FindNextFileAny(BookDirection first_direction) => first_direction switch
	{
		BookDirection.Next => FindNextFile(BookDirection.Next) ??
									FindNextFile(BookDirection.Previous) ?? null,
		BookDirection.Previous => FindNextFile(BookDirection.Previous) ??
										FindNextFile(BookDirection.Next) ?? null,
		_ => null
	};

	//
	protected class BookEntryInfoComparer : IComparer<BookEntryInfo>
	{
		public int Compare(BookEntryInfo? x, BookEntryInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.Name, y?.Name);
		}
	}

	//
	public string? GetEntryName(int pageno)
	{
		if (pageno < 0 || pageno >= TotalPage)
			return null;

		var entry = _entries[pageno];
		return GetEntryName(entry);
	}
}

// 하면 좋은거
// 메타데이터: https://docs.microsoft.com/ko-kr/dotnet/desktop/winforms/advanced/how-to-read-image-metadata?view=netframeworkdesktop-4.8

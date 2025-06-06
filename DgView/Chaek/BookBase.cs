using System.Collections.Generic;
using Cairo;
#if WINDOWS
using DgView.WebPWrapper;
#endif
using FileInfo = System.IO.FileInfo;

namespace DgView.Chaek;

/// <summary>
/// 책(이미지 모음) 기본 기능을 제공하는 추상 클래스입니다.
/// </summary>
public abstract class BookBase : IDisposable
{
    /// <summary>
    /// 전체 경로를 포함한 파일 이름입니다.
    /// </summary>
    public string FileName { get; private set; } = string.Empty;

    /// <summary>
    /// 경로를 제외한 파일 이름입니다.
    /// </summary>
    public string OnlyFileName { get; private set; } = string.Empty;

    /// <summary>
    /// 현재 페이지 번호입니다.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// 전체 페이지 수입니다.
    /// </summary>
    public int TotalPage => Entries.Count;

    /// <summary>
    /// 왼쪽 페이지 이미지입니다.
    /// </summary>
    public PageImage? PageLeft { get; private set; }

    /// <summary>
    /// 오른쪽 페이지 이미지입니다.
    /// </summary>
    public PageImage? PageRight { get; private set; }

    /// <summary>
    /// 현재 캐시된 이미지의 총 크기(바이트)입니다.
    /// </summary>
    public long CacheSize { get; private set; }

    /// <summary>
    /// 현재 보기 모드입니다.
    /// </summary>
    public ViewMode ViewMode { get; set; } = ViewMode.Follow;

    private ViewMode ActualViewMode => ViewMode == ViewMode.Follow ? Configs.ViewMode : ViewMode;

    /// <summary>엔트리 저장소</summary>
    protected readonly List<object> Entries = [];

    // 캐시
    private readonly Dictionary<int, MemoryStream> _cache = new();

    /// <summary>
    /// 엔트리에서 스트림을 읽어오는 추상 메서드입니다.
    /// </summary>
    /// <param name="entry">읽을 엔트리 객체입니다.</param>
    /// <returns>엔트리의 MemoryStream, 실패 시 null을 반환합니다.</returns>
    protected abstract MemoryStream? ReadEntry(object entry);

    /// <summary>
    /// 엔트리의 이름을 반환하는 추상 메서드입니다.
    /// </summary>
    /// <param name="entry">이름을 가져올 엔트리 객체입니다.</param>
    /// <returns>엔트리의 이름 문자열 또는 null을 반환합니다.</returns>
    public abstract string? GetEntryName(object entry);

    /// <summary>
    /// 모든 엔트리의 정보를 반환하는 추상 메서드입니다.
    /// </summary>
    /// <returns>BookEntryInfo의 열거형을 반환합니다.</returns>
    public abstract IEnumerable<BookEntryInfo> GetEntriesInfo();

    /// <summary>
    /// 파일 삭제 가능 여부를 반환합니다.
    /// </summary>
    /// <param name="reason">삭제 불가 사유(삭제 가능 시 빈 문자열)</param>
    /// <returns>삭제 가능하면 true, 아니면 false를 반환합니다.</returns>
    public virtual bool CanDeleteFile(out string? reason)
    {
        reason = string.Empty;
        return true;
    }

    /// <summary>
    /// 파일을 삭제합니다.
    /// </summary>
    /// <param name="closeBook">삭제 후 책을 닫아야 하는지 여부를 반환합니다.</param>
    /// <returns>삭제 성공 여부를 반환합니다.</returns>
    public abstract bool DeleteFile(out bool closeBook);

    /// <summary>
    /// 파일 이름을 변경합니다.
    /// </summary>
    /// <param name="newFilename">새 파일 이름입니다.</param>
    /// <param name="fullPath">변경된 전체 경로를 반환합니다.</param>
    /// <returns>이름 변경 성공 여부를 반환합니다.</returns>
    public abstract bool RenameFile(string newFilename, out string fullPath);

    /// <summary>
    /// 파일을 이동합니다.
    /// </summary>
    /// <param name="newFilename">이동할 새 파일 이름 또는 경로입니다.</param>
    /// <returns>이동 성공 여부를 반환합니다.</returns>
    public abstract bool MoveFile(string newFilename);

    /// <summary>
    /// 엔트리 제목 표시 여부입니다.
    /// </summary>
    public virtual bool DisplayEntryTitle => false;

    /// <summary>
    /// 파일 정보를 기반으로 파일명을 설정합니다.
    /// </summary>
    /// <param name="fi">파일 정보 객체</param>
    protected void SetFileName(FileInfo fi)
    {
        FileName = fi.FullName;
        OnlyFileName = fi.Name;
    }

    /// <summary>
    /// 디렉터리 정보를 기반으로 파일명을 설정합니다.
    /// </summary>
    /// <param name="di">디렉터리 정보 객체</param>
    protected void SetFileName(DirectoryInfo di)
    {
        FileName = di.FullName;
        OnlyFileName = di.Name;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 리소스를 해제합니다.
    /// </summary>
    /// <param name="disposing">관리 리소스 해제 여부입니다.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        Entries.Clear();
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

        if ((CacheSize + size) > Configs.MaxActualPageCache && _cache.Count > 0)
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
    private PageImage ReadPage(int pageNo)
    {
        if (pageNo < 0 || pageNo >= TotalPage)
            return new PageImage(Configs.OopsNoImage());

        var en = Entries[pageNo];
        var storeCache = false;

        if (!TryCache(pageNo, out var ms))
        {
            // 캐시에 없으면 파일 처리
            ms = ReadEntry(en);
            storeCache = true;
        }

        if (ms == null)
            return new PageImage(Configs.OopsNoImage());

        ImageSurface? img = null;
        List<AnimatedFrame>? frames = null;

        try
        {
            // 일단 이미지로 읽어보자
            ms.Position = 0;
            var pix = new PixBitmap(ms);
            img = new ImageSurface(Format.ARGB32, pix.Width, pix.Height);
            using (var cr = new Context(img))
            {
                Gdk.CairoHelper.SetSourcePixbuf(cr, pix, 0, 0);
                cr.Paint();
            }

            // 애니메이션 처리
            // GIF 어카지...
        }
        catch
        {
#if WINDOWS
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
#else
            // 윈도우 외에는 부디 WebP가 지원되기를...
#endif
        }

        if (storeCache && img != null)
            CacheStream(pageNo, ms);

        return frames != null ? new PageImage(frames) :
            img != null ? new PageImage(img) : new PageImage(Configs.OopsNoImage());
    }

    /// <summary>
    /// 현재 페이지에 맞는 이미지를 준비합니다.
    /// </summary>
    public void PrepareImages()
    {
        PageLeft?.Dispose();
        PageRight?.Dispose();

        switch (ActualViewMode)
        {
            case ViewMode.Fit:
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
                } // 엔트리 애니메이션 체크

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

    /// <summary>
    /// 다음 페이지로 이동합니다.
    /// </summary>
    /// <returns>페이지가 실제로 이동하면 true, 아니면 false</returns>
    public bool MoveNext()
    {
        var mode = ActualViewMode;
        var prev = CurrentPage;

        if (PageLeft == null || PageRight == null)
        {
            // 이건 위험하지만 일단 쓰자
            mode = ViewMode.Fit;
        }

        switch (mode)
        {
            case ViewMode.Fit:
                if (CurrentPage + 1 < TotalPage)
                    CurrentPage++;
                break;

            case ViewMode.LeftToRight:
            case ViewMode.RightToLeft:
                if (CurrentPage + 2 < TotalPage)
                    CurrentPage += 2;
                break;

            case ViewMode.Follow:
            default:
                throw new ArgumentOutOfRangeException();
        }

        return prev != CurrentPage;
    }

    /// <summary>
    /// 이전 페이지로 이동합니다.
    /// </summary>
    /// <returns>페이지가 실제로 이동하면 true, 아니면 false</returns>
    public bool MovePrev()
    {
        var mode = ActualViewMode;
        var prev = CurrentPage;

        if (PageLeft == null || PageRight == null)
        {
            // 이건 위험하지만 일단 쓰자
            mode = ViewMode.Fit;
        }

        switch (mode)
        {
            case ViewMode.Fit:
                CurrentPage--;
                break;

            case ViewMode.LeftToRight:
            case ViewMode.RightToLeft:
                CurrentPage -= 2;
                break;

            case ViewMode.Follow:
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (CurrentPage < 0)
            CurrentPage = 0;

        return prev != CurrentPage;
    }

    /// <summary>
    /// 지정한 페이지로 이동합니다.
    /// </summary>
    /// <param name="page">이동할 페이지 번호</param>
    /// <returns>페이지가 실제로 이동하면 true, 아니면 false</returns>
    public bool MovePage(int page)
    {
        var prev = CurrentPage;

        CurrentPage = Math.Clamp(page, 0, TotalPage - 1);

        return prev != CurrentPage;
    }

    /// <summary>
    /// 다음 또는 이전 파일을 찾는 가상 메서드입니다.
    /// </summary>
    /// <param name="direction">찾을 방향입니다.</param>
    /// <returns>찾은 파일 경로 또는 null을 반환합니다.</returns>
    public virtual string? FindNextFile(BookDirection direction)
    {
        return null;
    }

    /// <summary>
    /// Finds and returns the path of a randomly selected file.
    /// </summary>
    /// <returns>The path of the randomly selected file as a string, or <see langword="null"/> if no file is found.</returns>
    public virtual string? FindRandomFile()
    {
        return null;
    }

    /// <summary>
    /// 다음 또는 이전 파일을 찾습니다. 우선순위 방향을 먼저 시도합니다.
    /// </summary>
    /// <param name="firstDirection">우선적으로 시도할 방향</param>
    /// <returns>찾은 파일 경로 또는 null</returns>
    public string? FindAnyNextFile(BookDirection firstDirection) => firstDirection switch
    {
        BookDirection.Next => FindNextFile(BookDirection.Next) ??
                              FindNextFile(BookDirection.Previous) ?? null,
        BookDirection.Previous => FindNextFile(BookDirection.Previous) ??
                                  FindNextFile(BookDirection.Next) ?? null,
        _ => null
    };

    /// <summary>
    /// 책 엔트리 정보 비교
    /// </summary>
    protected class BookEntryInfoComparer : IComparer<BookEntryInfo>
    {
        /// <inheritdoc />
        public int Compare(BookEntryInfo? x, BookEntryInfo? y)
        {
            return Doumi.StringAsNumericCompare(x?.Name, y?.Name);
        }
    }

    /// <summary>
    /// 페이지 번호에 해당하는 엔트리의 이름을 반환합니다.
    /// </summary>
    /// <param name="pageNo">페이지 번호</param>
    /// <returns>엔트리 이름 또는 null</returns>
    public string? GetEntryName(int pageNo)
    {
        if (pageNo < 0 || pageNo >= TotalPage)
            return null;

        var entry = Entries[pageNo];
        return GetEntryName(entry);
    }
}
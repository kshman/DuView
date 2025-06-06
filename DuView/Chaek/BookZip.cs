using System.IO.Compression;

namespace DuView.Chaek;

internal class BookZip : BookBase
{
    private ZipArchive? _zip;

    //
    private BookZip()
    {
    }

    //
    public static BookZip? FromFile(string filename)
    {
        var b = new BookZip();
        if (!b.InternalOpenZip(filename))
            return null;

        b.SetFileName(new FileInfo(filename));

        return b;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!disposing)
            return;
        if (_zip == null)
            return;

        _zip.Dispose();
        _zip = null;
    }

    /// <inheritdoc />
    protected override MemoryStream? ReadEntry(object entry)
    {
        if (entry is not ZipArchiveEntry z)
            return null;

        using var st = z.Open();
        MemoryStream ms = new();
        st.CopyTo(ms);
        return ms;
    }

    /// <inheritdoc />
    public override string? GetEntryName(object entry)
    {
        return (entry is ZipArchiveEntry e) ? e.FullName : null;
    }

    /// <inheritdoc />
    public override IEnumerable<BookEntryInfo> GetEntriesInfo()
    {
        var r = new BookEntryInfo[Entries.Count];

        var n = 0;
        foreach (var e in Entries.Cast<ZipArchiveEntry>())
            r[n++] = new BookEntryInfo()
            {
                Name = e.FullName,
                DateTime = e.LastWriteTime.LocalDateTime,
                Size = e.Length,
            };

        return r;
    }

    //
    private bool InternalOpenZip(string filename)
    {
        if (_zip != null)
            return false;

        try
        {
            _zip = ZipFile.OpenRead(filename);
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
            _zip = null;
        }

        if (_zip == null)
            return false;

        var entries = new List<ZipArchiveEntry>();

        foreach (var e in _zip.Entries)
        {
            try
            {
                var fi = new FileInfo(e.FullName);
                if (fi.Extension.IsImageExtension())
                    entries.Add(e);
            }
            catch
            {
                // ignored
            }
        }

        entries.Sort(new ZipArchiveEntryComparer());

        foreach (var e in entries)
            Entries.Add(e);

        return true;
    }

    //
    private class ZipArchiveEntryComparer : IComparer<ZipArchiveEntry>
    {
        public int Compare(ZipArchiveEntry? x, ZipArchiveEntry? y) =>
            Doumi.StringAsNumericCompare(x?.FullName, y?.FullName);
    }

    /// <inheritdoc />
    public override string? FindNextFile(BookDirection direction)
    {
        var fi = new FileInfo(FileName);
        if (!fi.Exists)
            return null;

        var di = fi.Directory;
        if (di == null)
            return null;

        var ffs = di.GetFiles("*.zip");

        Array.Sort(ffs, new Doumi.FileInfoComparer());
        var at = Array.FindIndex(ffs, x => x.FullName == FileName);
        var want = direction == BookDirection.Previous ? at - 1 : at + 1;

        if (want < 0)
            return null;

        if (want >= ffs.Length)
            return null;

        return ffs[want].FullName;
    }

    /// <inheritdoc />
    public override bool CanDeleteFile(out string? reason)
    {
        reason = $"\"{OnlyFileName}\" {Resources.IsArchive}{Environment.NewLine}{Resources.DoYouWannaContinue}";
        return true;
    }

    /// <inheritdoc />
    public override bool DeleteFile(out bool closeBook)
    {
        closeBook = true;

        if (_zip != null)
        {
            _zip.Dispose();
            _zip = null;
        }

        try
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(FileName,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
            catch
            {
                File.Delete(FileName);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc />
    public override bool RenameFile(string newFilename, out string fullPath)
    {
        var fi = new FileInfo(FileName);
        if (fi.Directory == null)
        {
            fullPath = string.Empty;
            return false;
        }

        fullPath = Path.Combine(fi.Directory.FullName, newFilename);
        if (File.Exists(fullPath))
            return false;

        if (_zip != null)
        {
            _zip.Dispose();
            _zip = null;
        }

        try
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(FileName, newFilename);
            }
            catch
            {
                fi.MoveTo(fullPath);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc />
    public override bool MoveFile(string newFilename)
    {
        if (FileName.Equals(newFilename))
            return false;

        if (File.Exists(newFilename))
            return false;

        if (_zip != null)
        {
            _zip.Dispose();
            _zip = null;
        }

        try
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(FileName, newFilename);
            }
            catch
            {
                var fi = new FileInfo(FileName);
                fi.MoveTo(newFilename);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    // end of class
}
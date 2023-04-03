using System.IO.Compression;

namespace DuView;

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

	//
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		if (disposing)
		{
			if (_zip != null)
			{
				_zip.Dispose();
				_zip = null;
			}
		}
	}

	//
	protected override MemoryStream? ReadEntry(object entry)
	{
		if (entry is not ZipArchiveEntry z)
			return null;

		using var st = z.Open();
		MemoryStream ms = new();
		st.CopyTo(ms);
		return ms;
	}

	protected override string? GetEntryName(object entry)
	{
		return (entry is ZipArchiveEntry e) ? e.FullName : null;
	}

	public override IEnumerable<Types.BookEntryInfo> GetEntriesInfo()
	{
		var r = new Types.BookEntryInfo[_entries.Count];

		var n = 0;
		foreach (var e in _entries.Cast<ZipArchiveEntry>())
			r[n++] = new Types.BookEntryInfo()
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
				if (fi.Extension.IsValidImageFile())
					entries.Add(e);
			}
			catch
			{
				// ignored
			}
		}

		entries.Sort(new ZipArchiveEntryComparer());

		foreach (var e in entries)
			_entries.Add(e);

		return true;
	}

	//
	private class ZipArchiveEntryComparer : IComparer<ZipArchiveEntry>
	{
		public int Compare(ZipArchiveEntry? x, ZipArchiveEntry? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
		}
	}

	//
	public override string? FindNextFile(Types.BookDirection direction)
	{
		var fi = new FileInfo(FileName);
		if (!fi.Exists)
			return null;

		var di = fi.Directory;
		if (di == null)
			return null;

		var ffs = di.GetFiles("*.zip");

		Array.Sort(ffs, new ToolBox.FileInfoComparer());
		var at = Array.FindIndex(ffs, x => x.FullName == FileName);
		var want = direction == Types.BookDirection.Previous ? at - 1 : at + 1;

		if (want < 0)
			return null;

		if (want >= ffs.Length)
			return null;

		return ffs[want].FullName;
	}

	//
	public override bool CanDeleteFile(out string? reason)
	{
		reason = $"\"{OnlyFileName}\" {Locale.Text(113)}{Environment.NewLine}{Locale.Text(96)}";
		return true;
	}

	//
	public override bool DeleteFile(out bool close_book)
	{
		close_book = true;

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

	//
	public override bool RenameFile(string new_filename, out string full_path)
	{
		var fi = new FileInfo(FileName);
		if (fi.Directory == null)
		{
			full_path = string.Empty;
			return false;
		}

		full_path = Path.Combine(fi.Directory.FullName, new_filename);
		if (File.Exists(full_path))
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
				Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(FileName, new_filename);
			}
			catch
			{
				fi.MoveTo(full_path);
			}

			return true;
		}
		catch
		{
			return false;
		}
	}

	public override bool MoveFile(string new_filename)
	{
		if (FileName.Equals(new_filename))
			return false;

		if (File.Exists(new_filename))
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
				Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(FileName, new_filename);
			}
			catch
			{
				var fi = new FileInfo(FileName);
				fi.MoveTo(new_filename);
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


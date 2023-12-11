using DuView.Types;

namespace DuView;

internal class BookFolder : BookBase
{
	private BookFolder()
	{
	}

	//
	public static BookFolder? FromFolder(DirectoryInfo di)
	{
		var b = new BookFolder();
		if (!b.InternalOpenFolder(di))
			return null;

		b.SetFileName(di);

		return b;
	}

	//
	private bool InternalOpenFolder(DirectoryInfo di)
	{
		try
		{
			var entries =
				(from fi in di.EnumerateFiles()
					where fi.Exists
					where fi.Extension.IsValidImageFile()
					select new BookEntryInfo()
					{
						Name = fi.FullName,
						DateTime = fi.CreationTime,
						Size = fi.Length,
					}).ToArray();
			Array.Sort(entries, new BookEntryInfoComparer());

			foreach (var e in entries)
				_entries.Add(e);

			return true;
		}
		catch (Exception)
		{
			// 시스템 파일이나 이런거 읽을 경우 오류
			return false;
		}
	}

	//
	public int GetPageNumber(string filename)
	{
		for (var i = 0; i < _entries.Count; i++)
		{
			var entry = _entries[i] as BookEntryInfo;
			if (entry?.Name == filename)
				return i;
		}

		return -1;
	}

	//
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		if (disposing)
		{
		}
	}

	public override bool CanDeleteFile(out string? reason)
	{
		reason = $"\"{OnlyFileName}\" {Locale.Text(116)}{Environment.NewLine}{Locale.Text(96)}";
		return true;
	}

	public override bool DeleteFile(out bool close_book)
	{
		close_book = true;

		try
		{
			try
			{
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(FileName,
				  Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
				  Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
			}
			catch
			{
				Directory.Delete(FileName, true);
			}
			return true;
		}
		catch
		{
			return false;
		}
	}

	public override bool RenameFile(string new_filename, out string full_path)
	{
		var di = new DirectoryInfo(FileName);
		if (di.Parent == null)
		{
			full_path = string.Empty;
			return false;
		}

		full_path = Path.Combine(di.Parent.FullName, new_filename);
		if (Directory.Exists(full_path))
			return false;

		try
		{
			try
			{
				Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(FileName, new_filename);
			}
			catch
			{
				di.MoveTo(full_path);
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
		// 안만드러쓰요
		return false;
	}

	public override IEnumerable<BookEntryInfo> GetEntriesInfo()
	{
		var r = new BookEntryInfo[_entries.Count];

		var n = 0;
		foreach (var e in _entries.Cast<BookEntryInfo>())
			r[n++] = e;

		return r;
	}

	public override string? GetEntryName(object entry)
	{
		var e = (BookEntryInfo)entry;
		return e.Name;
	}

	protected override MemoryStream? ReadEntry(object entry)
	{
		var e = (BookEntryInfo)entry;
		if (e.Name == null)
			return null;
		else
		{
			using var st = new FileStream(e.Name, FileMode.Open, FileAccess.Read);
			MemoryStream ms = new();
			st.CopyTo(ms);
			return ms;
		}
	}

	public override string? FindNextFile(BookDirection direction)
	{
		var si = new DirectoryInfo(FileName);
		if (!si.Exists)
			return null;

		var di = si.Parent;
		if (di == null)
			return null;

		var drs = di.GetDirectories();
		Array.Sort(drs, new ToolBox.DirectoryInfoComparer());

		var at = Array.FindIndex(drs, x => x.FullName == FileName);
		var want = direction == BookDirection.Previous ? at - 1 : at + 1;

		return want < 0 ? null : want >= drs.Length ? null : drs[want].FullName;
	}

	/// <inheritdoc />
	public override bool DisplayEntryTitle => true;
}


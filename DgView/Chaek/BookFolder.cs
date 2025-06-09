using System.Collections.Generic;

namespace DgView.Chaek;

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
					where fi.Extension.IsImageExtension()
					select new BookEntryInfo()
					{
						Name = fi.FullName,
						DateTime = fi.CreationTime,
						Size = fi.Length,
					}).ToArray();
			Array.Sort(entries, new BookEntryInfoComparer());

			foreach (var e in entries)
				Entries.Add(e);

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
		for (var i = 0; i < Entries.Count; i++)
		{
			var entry = Entries[i] as BookEntryInfo;
			if (entry?.Name == filename)
				return i;
		}

		return -1;
	}

	/// <inheritdoc />
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		if (disposing)
		{
		}
	}

	/// <inheritdoc />
	public override bool CanDeleteFile(out string? reason)
	{
		reason = $"\"{OnlyFileName}\"은 디렉토리입니다.{Environment.NewLine}계속 하시겠습니까?";
		return true;
	}

	/// <inheritdoc />
	public override bool DeleteFile(out bool closeBook)
	{
		closeBook = true;

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

	/// <inheritdoc />
	public override bool RenameFile(string newFilename, out string fullPath)
	{
		var di = new DirectoryInfo(FileName);
		if (di.Parent == null)
		{
			fullPath = string.Empty;
			return false;
		}

		fullPath = Path.Combine(di.Parent.FullName, newFilename);
		if (Directory.Exists(fullPath))
			return false;

		try
		{
			try
			{
				Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(FileName, newFilename);
			}
			catch
			{
				di.MoveTo(fullPath);
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
		// 안만드러쓰요
		return false;
	}

	/// <inheritdoc />
	public override IEnumerable<BookEntryInfo> GetEntriesInfo()
	{
		var r = new BookEntryInfo[Entries.Count];

		var n = 0;
		foreach (var e in Entries.Cast<BookEntryInfo>())
			r[n++] = e;

		return r;
	}

	/// <inheritdoc />
	public override string? GetEntryName(object entry)
	{
		var e = (BookEntryInfo)entry;
		return e.Name;
	}

	/// <inheritdoc />
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

	/// <inheritdoc />
	public override string? FindNextFile(BookDirection direction)
	{
		var si = new DirectoryInfo(FileName);
		if (!si.Exists)
			return null;

		var di = si.Parent;
		if (di == null)
			return null;

		if (!Configs.NearIsPathSame(di.FullName))
		{
			var drs = di.GetDirectories();
			Array.Sort(drs, new Doumi.DirectoryInfoComparer());
			Configs.NearSetFiles(di.FullName, drs.Select(x => x.FullName));
		}

		return Configs.NearGetFile(FileName, direction);
	}

	/// <inheritdoc />
	public override bool DisplayEntryTitle => true;
}


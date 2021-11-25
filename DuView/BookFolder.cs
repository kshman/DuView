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
		var entries = new List<Types.BookEntryInfo>();

		foreach (var fi in di.EnumerateFiles())
		{
			if (!fi.Exists)
				continue;

			if (!ToolBox.IsValidImageFile(fi.Extension.ToLower()))
				continue;

			var entry = new Types.BookEntryInfo()
			{
				Name = fi.FullName,
				DateTime = fi.CreationTime,
				Size = fi.Length,
			};

			entries.Add(entry);
		}

		entries.Sort(new BookEntryInfoComparer());

		foreach (var e in entries)
			_entries.Add(e);

		return true;
	}

	//
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		if (disposing)
		{
		}
	}

	public override bool DeleteFile()
	{
		throw new NotImplementedException();
	}

	public override Types.BookEntryInfo[] GetEntriesInfo()
	{
		var r = new Types.BookEntryInfo[_entries.Count];

		int n = 0;
		foreach (var i in _entries)
		{
			var e = (Types.BookEntryInfo)i;
			r[n++] = e;
		}

		return r;
	}

	protected override string? GetEntryName(object entry)
	{
		var e = (Types.BookEntryInfo)entry;
		return e.Name;
	}

	protected override Stream? OpenStream(object entry)
	{
		var e = (Types.BookEntryInfo)entry;
		if (e.Name == null)
			return null;
		else
		{
			var st = new FileStream(e.Name, FileMode.Open, FileAccess.Read);
			return st;
		}
	}

	public override string? FindNextFile(bool no_i_want_prev_file)
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
		var want = no_i_want_prev_file ? at - 1 : at + 1;

		if (want < 0)
			return null;

		if (want >= drs.Length)
			return null;

		return drs[want].FullName;
	}
}


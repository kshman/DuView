using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DuView
{
	internal class BookZip : BookBase
	{
		private ZipArchive _zip;

		//
		private BookZip()
		{
		}

		//
		public static BookZip FromFile(string filename)
		{
			var b = new BookZip();
			if (!b.InternalOpenZip(filename))
				return null;

			b.SetFileInfo(new FileInfo(filename));

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
		protected override Stream OpenStream(object entry)
		{
			var e = entry as ZipArchiveEntry;
			if (e == null)
				return null;

			return e.Open();
		}

		protected override string GetEntryName(object entry)
		{
			var e = entry as ZipArchiveEntry;
			if (e == null)
				return null;

			return e.FullName;
		}

		public override Types.BookEntryInfo[] GetEntryInfos()
		{
			var r = new Types.BookEntryInfo[_entries.Count];

			int n = 0;
			foreach (var i in _entries)
			{
				var e = i as ZipArchiveEntry;

				r[n++] = new Types.BookEntryInfo()
				{
					Name = e.FullName,
					DateTime = e.LastWriteTime.LocalDateTime,
					Size = e.Length,
				};
			}

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

			List<ZipArchiveEntry> entries = new List<ZipArchiveEntry>();

			foreach (var e in _zip.Entries)
			{
				try
				{
					FileInfo fi = new FileInfo(e.FullName);
					if (ToolBox.IsValidImageFile(fi.Extension.ToLower()))
						entries.Add(e);
				}
				catch { }
			}

			entries.Sort(new ZipArchiveEntryComparer());

			foreach (var e in entries)
				_entries.Add(e);

			return true;
		}

		//
		private class ZipArchiveEntryComparer : IComparer<ZipArchiveEntry>
		{
			public ZipArchiveEntryComparer()
			{ }

			public int Compare(object x, object y)
			{
				return (x is ZipArchiveEntry ex) && (y is ZipArchiveEntry ey) ?
					Du.Data.StringAsNumericComparer.Comparer.Compare(ex.FullName, ey.FullName) : -1;
			}

			public int Compare(ZipArchiveEntry x, ZipArchiveEntry y)
			{
				return Du.Data.StringAsNumericComparer.Comparer.Compare(x.FullName, y.FullName);
			}
		}

		//
		public override string FindNextFile(bool no_i_want_prev_file)
		{
			FileInfo fi = new FileInfo(FileName);
			if (!fi.Exists)
				return null;

			var di = fi.Directory;
			var ffs = di.GetFiles("*.zip");

			Array.Sort(ffs, new ToolBox.FileInfoComparer());
			var at = Array.FindIndex(ffs, x => x.FullName == FileName);
			var want = no_i_want_prev_file ? at - 1 : at + 1;

			if (want < 0)
				return null;

			if (want >= ffs.Length)
				return null;

			return ffs[want].FullName;
		}
	}
}

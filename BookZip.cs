﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuView
{
	internal class BookZip : BookBase
	{
		private ZipArchive _zip;

		private BookZip()
		{
		}

		public static BookZip FromFile(string filename)
		{
			var b = new BookZip();
			if (!b.InternalOpenZip(filename))
				return null;

			b.SetFileInfo(new FileInfo(filename));

			return b;
		}

		public override void Close()
		{
			if (_zip != null)
				_zip.Dispose();
		}

		protected override Stream OpenStream(object entry)
		{
			var e = entry as ZipArchiveEntry;
			if (e == null)
				return null;

			return e.Open();
		}

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
				FileInfo fi = new FileInfo(e.FullName);
				if (IsValidImageFile(fi.Extension.ToLower()))
					entries.Add(e);
			}

			entries.Sort(new ZipArchiveEntryComparer());

			foreach (var e in entries)
				_entries.Add(e);

			return true;
		}

		private class ZipArchiveEntryComparer : IComparer<ZipArchiveEntry>
		{
			public ZipArchiveEntryComparer()
			{ }

			public int Compare(object x, object y)
			{
				return (x is ZipArchiveEntry ex) && (y is ZipArchiveEntry ey) ?
					DuLib.Data.StringAsNumericComparer.Comparer.Compare(ex.FullName, ey.FullName) : -1;
			}

			public int Compare(ZipArchiveEntry x, ZipArchiveEntry y)
			{
				return DuLib.Data.StringAsNumericComparer.Comparer.Compare(x.FullName, y.FullName);
			}
		}
	}
}

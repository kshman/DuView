using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Du.Globalization;

namespace DuView
{
	internal class BookFolder : BookBase
	{
		private BookFolder()
		{
		}

		//
		public static BookFolder FromFolder(DirectoryInfo di)
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
			var entries =
				(from fi in di.EnumerateFiles()
					where fi.Exists
					where ToolBox.IsValidImageFile(fi.Extension.ToLower())
					select new Types.BookEntryInfo()
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

		//
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
			}
		}

		public override bool CanDeleteFile(out string reason)
		{
			reason = $"\"{OnlyFileName}\" {Locale.Text(116)}{Environment.NewLine}{Locale.Text(96)}";
			return true;
		}

		public override bool DeleteFile(out bool closebook)
		{
			closebook = true;

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

		public override bool RenameFile(string newfilename, out string fullpath)
		{
			var di = new DirectoryInfo(FileName);
			if (di.Parent == null)
			{
				fullpath = string.Empty;
				return false;
			}

			fullpath = Path.Combine(di.Parent.FullName, newfilename);
			if (Directory.Exists(fullpath))
				return false;

			try
			{
				try
				{
					Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(FileName, newfilename);
				}
				catch
				{
					di.MoveTo(fullpath);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override bool MoveFile(string newfilename)
		{
			// 아직 지원 안함
			return false;
		}

		public override IEnumerable<Types.BookEntryInfo> GetEntriesInfo()
		{
			var r = new Types.BookEntryInfo[_entries.Count];

			var n = 0;
			foreach (var e in _entries.Cast<Types.BookEntryInfo>())
				r[n++] = e;

			return r;
		}

		protected override string GetEntryName(object entry)
		{
			var e = (Types.BookEntryInfo) entry;
			return e.Name;
		}

		protected override Stream OpenStream(object entry)
		{
			var e = (Types.BookEntryInfo) entry;
			if (e.Name == null)
				return null;
			else
			{
				var st = new FileStream(e.Name, FileMode.Open, FileAccess.Read);
				return st;
			}
		}

		public override string FindNextFile(Types.BookDirection direction)
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
			var want = direction == Types.BookDirection.Previous ? at - 1 : at + 1;

			if (want < 0)
				return null;

			if (want >= drs.Length)
				return null;

			return drs[want].FullName;
		}
	}
}

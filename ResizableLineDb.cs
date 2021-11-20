using Du.Data;
using System.Collections.Generic;
using System.Text;

namespace DuView
{
	internal class ResizableLineDb : LineDb
	{
		private ResizableLineDb()
		{
		}

		public static ResizableLineDb New()
		{
			return new ResizableLineDb();
		}

		public static ResizableLineDb FromFile(string filename)
		{
			var l = new ResizableLineDb();
			l.AddFromFile(filename, Encoding.UTF8, false);
			return l;
		}

		public void ResizeCutBeginSlowly(int count)
		{
			if (count < StringDb.Count)
			{
				var ns = new List<KeyValuePair<string, string>>();

				var n = StringDb.Count - count;
				foreach (var i in StringDb)
				{
					if (n > 0)
						n--;
					else
						ns.Add(i);
				}

				StringDb.Clear();

				foreach (var i in ns)
					StringDb[i.Key] = i.Value;
			}
		}
	}
}

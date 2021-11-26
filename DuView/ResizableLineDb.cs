using System.Text;

namespace DuView;

internal class ResizableLineDb : LineStringDb<int>
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
		l.AddFromFile(filename, Encoding.UTF8, new IntValueConverter());
		return l;
	}

	public void ResizeCutFrontSlowly(int count)
	{
		if (count < Db.Count)
		{
			var ns = new List<KeyValuePair<string, int>>();

			var n = Db.Count - count;
			foreach (var i in Db)
			{
				if (n > 0)
					n--;
				else
					ns.Add(i);
			}

			Db.Clear();

			foreach (var i in ns)
				Db[i.Key] = i.Value;
		}
	}

	private class IntValueConverter : Du.Data.Generic.IStringConverter<int>
	{
		public int StringConvert(string? s)
		{
			return int.TryParse(s, out var n) ? n : 0;
		}
	}
}


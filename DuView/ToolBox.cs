using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using InterpolationMode = System.Drawing.Drawing2D.InterpolationMode;

namespace DuView;

public static class ToolBox
{
	// 알 수 있는 로캘 얻기
	public static string GetKnownCultureLocale(CultureInfo culture)
	{
		var name = culture.Name;
		string locale;

		if (name.StartsWith("ko"))
			locale = "ko";
		else if (name.StartsWith("sh"))         // 세인트 헬러나
			locale = "sh";
		else if (name.StartsWith("kim"))        // 이런 나라 없다
			locale = "kim";
		else
			locale = "en";

		return locale;
	}

	// 열 수 있는 이미지
	public static bool IsValidImageFile(string extension)
	{
		return extension switch
		{
			".png" or ".jpg" or ".jpeg" or ".bmp" or ".tga" or ".webp" or ".gif" => true,
			_ => false,
		};
	}

	// 움직이나?
	public static bool IsAnimatedImageFile(string filename, bool isextension = true)
	{
		string extension;

		if (isextension)
			extension = filename;
		else
		{
			var n = filename.LastIndexOf('.');
			extension = filename[n..];
		}

		return extension switch
		{
			".gif" => true,
			_ => false,
		};
	}

	// 아카이브?
	public static bool IsArchiveType(string extension)
	{
		return extension switch
		{
			".zip" => true,
			_ => false,
		};
	}

	// 퀄리티 변환
	public static InterpolationMode QualityToInterpolationMode(Types.ViewQuality q)
	{
		return q switch
		{
			Types.ViewQuality.Invalid => InterpolationMode.Invalid,
			Types.ViewQuality.Default => InterpolationMode.Default,
			Types.ViewQuality.Low => InterpolationMode.Low,
			Types.ViewQuality.High => InterpolationMode.High,
			Types.ViewQuality.Bilinear => InterpolationMode.Bilinear,
			Types.ViewQuality.Bicubic => InterpolationMode.Bicubic,
			Types.ViewQuality.NearestNeighbor => InterpolationMode.NearestNeighbor,
			Types.ViewQuality.HqBilinear => InterpolationMode.HighQualityBilinear,
			Types.ViewQuality.HqBicubic => InterpolationMode.HighQualityBicubic,
			_ => InterpolationMode.Default,
		};
	}

	// 크기를 문자열로 표시
	public static string SizeToString(long size)
	{
		const long giga = 1024 * 1024 * 1024;
		const long mega = 1024 * 1024;
		const long kilo = 1024;

		double v;
		switch (size)
		{
			// 0.5 기가
			case > giga:
				v = size / (double)giga;
				return $"{v:0.0}GB";

			// 0.5 메가
			case > mega:
				v = size / (double)mega;
				return $"{v:0.0}MB";

			// 0.5 킬로
			case > kilo:
				v = size / (double)kilo;
				return $"{v:0.0}KB";

			default:
				return $"{size}B";
		}
	}

	//
	public static Rectangle CalcDestRect(int tw, int th, int dw, int dh, HorizontalAlignment align)
	{
		var rt = new Rectangle(0, 0, dw, dh);

		if (align == HorizontalAlignment.Left)
		{
			// 오우
		}
		else
		{
			// 오른쪽
			if (dw < tw)
			{
				rt.X = tw - dw;

				// 가운데
				if (align == HorizontalAlignment.Center)
					rt.X /= 2;
			}
		}

		if (dh < th)
			rt.Y = (th - dh) / 2;

		return rt;
	}

	//
	public static (int w, int h) CalcDestSize(bool zoom, int dw, int dh, int sw, int sh)
	{
		var dstaspect = dw / (double)dh;
		var srcaspect = sw / (double)sh;
		int nw = dw, nh = dh;

		if (zoom)
		{
			if (srcaspect > 1)
			{
				// 세로로 긴 그림
				if (dstaspect < srcaspect)
					nh = (int)(dw / srcaspect);
				else
					nw = (int)(dh * srcaspect);
			}
			else
			{
				// 가로로 긴 그림
				if (dstaspect > srcaspect)
					nw = (int)(dh * srcaspect);
				else
					nh = (int)(dw / srcaspect);
			}
		}
		else
		{
			// 가로로 맞춘다... 스크롤은 쌩깜
			nh = (int)(dw / srcaspect);
		}

		return (nw, nh);
	}

	//
	public class FileInfoComparer : IComparer<FileInfo>
	{
		public int Compare(FileInfo? x, FileInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
		}
	}

	//
	public class DirectoryInfoComparer : IComparer<DirectoryInfo>
	{
		public int Compare(DirectoryInfo? x, DirectoryInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
		}
	}

	//
	public static void GlobalizationLocaleText(Control container)
	{
		foreach (Control c in container.Controls)
		{
			Debug.WriteLine($"{c.Name}");

			if (c is Label l)
				l.Text = Locale.TextAsInt(l.Text);
			else if (c is ButtonBase b)
				b.Text = Locale.TextAsInt(b.Text);
			else if (c is TextBoxBase t)
				t.Text = Locale.TextAsInt(t.Text);
			else if (c is TabPage p)
				p.Text = Locale.TextAsInt(p.Text);
			else if (c is ToolStrip i)
			{
				i.Text = Locale.TextAsInt(i.Text);
				GlobalizationLocaleText(i.Items);
			}
			else if (c is MenuStrip m)
			{
				m.Text = Locale.TextAsInt(m.Text);
				GlobalizationLocaleText(m.Items);
			}
			else if (c is ListView v)
			{
				foreach (ColumnHeader h in v.Columns)
					h.Text = Locale.TextAsInt(h.Text);
			}

			if (c.ContextMenuStrip != null)
			{
				GlobalizationLocaleText(c.ContextMenuStrip);
				GlobalizationLocaleText(c.ContextMenuStrip.Items);
			}

			if (c.HasChildren)
				GlobalizationLocaleText(c);
		}
	}

	//
	public static void GlobalizationLocaleText(ToolStripItemCollection collection)
	{
		foreach (var c in collection)
		{
			if (c is ToolStripDropDownItem s)
			{
				s.Text = Locale.TextAsInt(s.Text);
				if (s.DropDownItems.Count > 0)
					GlobalizationLocaleText(s.DropDownItems);
			}
		}
	}
}

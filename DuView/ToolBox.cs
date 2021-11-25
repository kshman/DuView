namespace DuView;

public static class ToolBox
{
	// 열 수 있는 이미지
	public static bool IsValidImageFile(string extension)
	{
		return extension switch
		{
			".png" or ".jpg" or ".jpeg" or ".bmp" or ".tga" or ".gif" => true,
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
	public static System.Drawing.Drawing2D.InterpolationMode QualityToInterpolationMode(Types.ViewQuality q)
	{
		return q switch
		{
			Types.ViewQuality.Low => System.Drawing.Drawing2D.InterpolationMode.Low,
			Types.ViewQuality.Default => System.Drawing.Drawing2D.InterpolationMode.Default,
			Types.ViewQuality.Bilinear => System.Drawing.Drawing2D.InterpolationMode.Bilinear,
			Types.ViewQuality.Bicubic => System.Drawing.Drawing2D.InterpolationMode.Bicubic,
			Types.ViewQuality.High => System.Drawing.Drawing2D.InterpolationMode.High,
			Types.ViewQuality.HqBilinear => System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear,
			Types.ViewQuality.HqBicubic => System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic,
			_ => System.Drawing.Drawing2D.InterpolationMode.Default,
		};
	}

	// 파일이름 정보를 파일이름과 줄로
	public static (string filename, int line) StringToFileLine(string s)
	{
		var n = s.IndexOf('|');
		if (n < 0)
			return (string.Empty, 0);

		var line = Converter.ToInt(s[..n]);
		var filename = s[(n + 1)..];

		return (filename, line);
	}

	// 파일이름과 줄을 파일 이름 정보로
	public static string FileLineToString(string filename, int line)
	{
		return string.IsNullOrEmpty(filename) ? string.Empty : $"{line}|{filename}";
	}

	// 크기를 문자열로 표시
	public static string SizeToString(long size)
	{
		const long giga = 1024 * 1024 * 1024;
		const long mega = 1024 * 1024;
		const long kilo = 1024;

		double v;
		if (size > giga)          // 0.5 기가
		{
			v = size / (double)giga;
			return $"{v:0.0}GB";
		}
		else if (size > mega)     // 0.5 메가
		{
			v = size / (double)mega;
			return $"{v:0.0}MB";
		}
		else if (size > kilo)     // 0.5 킬로
		{
			v = size / (double)kilo;
			return $"{v:0.0}KB";
		}
		else
		{
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
		public FileInfoComparer()
		{ }

		public int Compare(FileInfo? x, FileInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
		}
	}

	//
	public class DirectoryInfoComparer : IComparer<DirectoryInfo>
	{
		public DirectoryInfoComparer()
		{ }

		public int Compare(DirectoryInfo? x, DirectoryInfo? y)
		{
			return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
		}
	}
}


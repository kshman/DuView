using Du;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuView
{
	public static class ToolBox
	{
		// 열 수 있는 이미지
		public static bool IsValidImageFile(string extension)
		{
			switch (extension)
			{
				case ".png":
				case ".jpg":
				case ".jpeg":
				case ".bmp":
				case ".tga":
				case ".gif":
					return true;
				default:
					return false;
			}
		}

		// 아카이브?
		public static bool IsArchiveType(string extension)
		{
			switch (extension)
			{
				case ".zip":
					return true;
				default:
					return false;
			}
		}

		// 퀄리티 변환
		public static System.Drawing.Drawing2D.InterpolationMode QualityToInterpolationMode(Types.ViewQuality q)
		{
			switch (q)
			{
				case Types.ViewQuality.Low:
					return System.Drawing.Drawing2D.InterpolationMode.Low;
				case Types.ViewQuality.Default:
					return System.Drawing.Drawing2D.InterpolationMode.Default;
				case Types.ViewQuality.Bilinear:
					return System.Drawing.Drawing2D.InterpolationMode.Bilinear;
				case Types.ViewQuality.Bicubic:
					return System.Drawing.Drawing2D.InterpolationMode.Bicubic;
				case Types.ViewQuality.High:
					return System.Drawing.Drawing2D.InterpolationMode.High;
				case Types.ViewQuality.HqBilinear:
					return System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
				case Types.ViewQuality.HqBicubic:
					return System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				default:
					return System.Drawing.Drawing2D.InterpolationMode.Default;
			}
		}

		// 파일이름 정보를 파일이름과 줄로
		public static (string filename, int line) StringToFileLine(string s)
		{
			var n = s.IndexOf('|');
			if (n < 0)
				return (string.Empty, 0);

			var line = Converter.ToInt(s.Substring(0, n));
			var filename = s.Substring(n + 1);

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
			Rectangle rt = new Rectangle(0, 0, dw, dh);

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

			public int Compare(FileInfo x, FileInfo y)
			{
				return Du.Data.StringAsNumericComparer.Comparer.Compare(x.FullName, y.FullName);
			}
		}

		//
		public class DirectoryInfoComparer : IComparer<DirectoryInfo>
		{
			public DirectoryInfoComparer()
			{ }

			public int Compare(DirectoryInfo x, DirectoryInfo y)
			{
				return Du.Data.StringAsNumericComparer.Comparer.Compare(x.FullName, y.FullName);
			}
		}
	}
}

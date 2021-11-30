using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Du.Data;
using InterpolationMode = System.Drawing.Drawing2D.InterpolationMode;

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
		public static InterpolationMode QualityToInterpolationMode(Types.ViewQuality q)
		{
			switch (q)
			{
				case Types.ViewQuality.Invalid:
					return InterpolationMode.Invalid;
				case Types.ViewQuality.Default:
					return InterpolationMode.Default;
				case Types.ViewQuality.Low:
					return InterpolationMode.Low;
				case Types.ViewQuality.High:
					return InterpolationMode.High;
				case Types.ViewQuality.Bilinear:
					return InterpolationMode.Bilinear;
				case Types.ViewQuality.Bicubic:
					return InterpolationMode.Bicubic;
				case Types.ViewQuality.NearestNeighbor:
					return InterpolationMode.NearestNeighbor;
				case Types.ViewQuality.HqBilinear:
					return InterpolationMode.HighQualityBilinear;
				case Types.ViewQuality.HqBicubic:
					return InterpolationMode.HighQualityBicubic;
				default:
					return InterpolationMode.Default;
			}
		}

		// 크기를 문자열로 표시
		public static string SizeToString(long size)
		{
			const long giga = 1024 * 1024 * 1024;
			const long mega = 1024 * 1024;
			const long kilo = 1024;

			double v;
			if (size > giga)
			{
				v = size / (double) giga;
				return $"{v:0.0}GB";
			}

			if (size > mega)
			{
				v = size / (double) mega;
				return $"{v:0.0}MB";
			}

			if (size > kilo)
			{
				v = size / (double) kilo;
				return $"{v:0.0}KB";
			}
			
			return $"{size}B";
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
			var dstaspect = dw / (double) dh;
			var srcaspect = sw / (double) sh;
			int nw = dw, nh = dh;

			if (zoom)
			{
				if (srcaspect > 1)
				{
					// 세로로 긴 그림
					if (dstaspect < srcaspect)
						nh = (int) (dw / srcaspect);
					else
						nw = (int) (dh * srcaspect);
				}
				else
				{
					// 가로로 긴 그림
					if (dstaspect > srcaspect)
						nw = (int) (dh * srcaspect);
					else
						nh = (int) (dw / srcaspect);
				}
			}
			else
			{
				// 가로로 맞춘다... 스크롤은 쌩깜
				nh = (int) (dw / srcaspect);
			}

			return (nw, nh);
		}

		//
		public class FileInfoComparer : IComparer<FileInfo>
		{
			public int Compare(FileInfo x, FileInfo y)
			{
				return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
			}
		}

		//
		public class DirectoryInfoComparer : IComparer<DirectoryInfo>
		{
			public int Compare(DirectoryInfo x, DirectoryInfo y)
			{
				return StringAsNumericComparer.StringAsNumericCompare(x?.FullName, y?.FullName);
			}
		}
	}
}

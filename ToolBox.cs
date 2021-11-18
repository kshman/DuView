using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuView
{
	internal static class ToolBox
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
	}
}

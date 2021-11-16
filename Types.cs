using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuView
{
	internal static class Types
	{
		// 보기 모드
		public enum ViewMode : int
		{
			FitWidth,
			FitHeight,
			LeftToRight,
			RightToLeft
		}

		// 계산
		public static class Calc
		{
			public static Rectangle DestRect(int tw, int th, int dw, int dh, HorizontalAlignment align)
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

			public static (int w, int h) DestSize(bool zoom, int dw, int dh, int sw, int sh)
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
}


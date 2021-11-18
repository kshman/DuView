﻿using System;
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

		// 보기 품질
		public enum ViewQuality : int
		{
			Low,
			Default,
			Bilinear,
			Bicubic,
			High,
			HqBilinear,
			HqBicubic,
		}
	}
}


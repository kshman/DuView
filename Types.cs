using System;

namespace DuView
{
	public static class Types
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

		//
		public struct BookEntryInfo
		{
			public string Name;
			public DateTime DateTime;
			public long Size;
		}
	}
}


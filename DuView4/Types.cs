using System;

namespace DuView
{
	public static class Types
	{
		// 보기 모드
		public enum ViewMode : int
		{
			FitWidth = 0,
			FitHeight = 1,
			LeftToRight = 2,
			RightToLeft = 3
		}

		// 보기 품질
		public enum ViewQuality : int
		{
			Invalid,
			Default,
			Low,
			High,
			Bilinear,
			Bicubic,
			NearestNeighbor,
			HqBilinear,
			HqBicubic,
		}

		// 조작
		public enum Controls
		{
			Previous,
			Next,

			First,
			Last,

			SeekPrevious10,
			SeekNext10,

			SeekMinusOne,
			SeekPlusOne,

			ScanPrevious,
			ScanNext,

			Select,
		}

		//
		public enum BookDirection
		{
			Previous,
			Next,
		}

		//
		public struct BookEntryInfo
		{
			public string Name { get; set; }
			public DateTime DateTime { get; set; }
			public long Size { get; set; }

			//public BookEntryInfo(string name, DateTime dateTime, long size)
			//=> (Name, DateTime, Size) = (name, dateTime, size);

			public override string ToString() => $"{Name} ({ToolBox.SizeToString(Size)})";
		}
	}
}

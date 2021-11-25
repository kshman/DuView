namespace DuView;

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
	public record BookEntryInfo
	{
		public string? Name { get; init; }
		public DateTime DateTime { get; init; }
		public long Size { get; init; }

		//public BookEntryInfo(string name, DateTime dateTime, long size)
		//=> (Name, DateTime, Size) = (name, dateTime, size);

		public override string ToString() => $"{Name} ({ToolBox.SizeToString(Size)})";
	}	
}


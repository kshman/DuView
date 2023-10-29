namespace DuView.Types;

// 보기 모드
public enum ViewMode
{
	FitWidth = 0,
	FitHeight = 1,
	LeftToRight = 2,
	RightToLeft = 3,
	Follow = 255,
}

// 보기 품질
public enum ViewQuality
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
public enum BookControl
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
public enum PassCodeUsage
{
	Run,
	Option,
	LastBook,
	MoveBook,
	RenameBook,
}

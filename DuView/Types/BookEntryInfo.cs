namespace DuView.Types;

public record BookEntryInfo
{
	public string? Name { get; init; }
	public DateTime DateTime { get; init; }
	public long Size { get; init; }

	//public BookEntryInfo(string name, DateTime dateTime, long size)
	//=> (Name, DateTime, Size) = (name, dateTime, size);

	public override string ToString() => $"{Name} ({ToolBox.SizeToString(Size)})";
}

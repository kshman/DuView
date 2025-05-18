namespace DuView.Chaek;

/// <summary>
/// 책 엔트리 정보
/// </summary>
public record BookEntryInfo
{
	/// <summary>
	/// 책 이름
	/// </summary>
	public string? Name { get; init; }
	/// <summary>
	/// 날짜와 시간
	/// </summary>
	public DateTime DateTime { get; init; }
	/// <summary>
	/// 크기
	/// </summary>
	public long Size { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({Doumi.SizeToString(Size)})";
}

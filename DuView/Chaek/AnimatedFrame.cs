namespace DuView.Chaek;

/// <summary>
/// 애니메이션 프레임
/// </summary>
/// <param name="bitmap"></param>
/// <param name="duration"></param>
public class AnimatedFrame(Bitmap bitmap, int duration)
{
	private const int MinFrameRate = 100;    // 60Hz

	/// <summary>
	/// 비트맵 데이터
	/// </summary>
	public Bitmap Bitmap { get; init; } = bitmap;

    /// <summary>
    /// 애니메이션 프레임 지속 시간 (ms)
    /// </summary>
    public int Duration { get; init; } = duration == 0 ? MinFrameRate : duration;

	/// <inheritdoc />
	public override string ToString() => $"{Duration}: {Bitmap.Width}x{Bitmap.Height}";
}

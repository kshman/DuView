namespace DuView.Types;

public class AnimatedFrame
{
	private const int MinFrameRate = 100;    // 60Hz

	public Bitmap Bitmap { get; init; }
	public int Duration { get; init; }

	public AnimatedFrame(Bitmap bitmap, int duration)
	{
		Bitmap = bitmap;
		Duration = duration == 0 ? MinFrameRate : duration;
	}

	/// <inheritdoc />
	public override string ToString() => $"{Duration}: {Bitmap.Width}x{Bitmap.Height}";
}

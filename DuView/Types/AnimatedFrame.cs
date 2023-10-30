namespace DuView.Types;

public class AnimatedFrame
{
	public Bitmap Bitmap { get; init; }
	public int Duration { get; init; }

	public AnimatedFrame(Bitmap bitmap, int duration)
	{
		Bitmap = bitmap;
		Duration = duration;
	}

	/// <inheritdoc />
	public override string ToString() => $"{Duration}: {Bitmap.Width}x{Bitmap.Height}";
}

namespace DuView.Types;

public class PageImage : IDisposable
{
	private const int MinimumDuration = 5;

	public Image Image { get; }
	public List<AnimatedFrame>? Frames { get; }

	public int CurrentFrame { get; private set; }
	public int LastDuration { get; private set; }

	public bool HasAnimation => Frames != null;

	public override string ToString()
	{
		return Frames != null ?
			$"애니메이션: {Frames.Count} 프레임" :
			$"이미지: {Image.RawFormat}";
	}

	public PageImage(Image image)
	{
		Image = image;
		Frames = null;
	}

	public PageImage(List<AnimatedFrame> frames)
	{
		Image = new Bitmap(frames[0].Bitmap);
		Frames = frames;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		Image.Dispose();
	}

	//
	public int Animate()
	{
		if (Frames == null)
			return -1;

		var frame = Frames[CurrentFrame++];
		if (CurrentFrame >= Frames.Count)
			CurrentFrame = 0;

		LastDuration = frame.Duration < MinimumDuration ? MinimumDuration: frame.Duration;

		return LastDuration;
	}

	//
	public void InitAnimation()
	{
		CurrentFrame = 0;
		LastDuration = 0;
	}

	//
	public Image GetImage() => Frames?[CurrentFrame].Bitmap ?? Image;
}

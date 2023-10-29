using DuView.WebPWrapper;

namespace DuView.Types;

public class PageImage : IDisposable
{
	public Image Image { get; }
	public bool IsAnimate { get; }
	public List<WebP.FrameData>? Frames { get; }

	public override string ToString()
	{
		return Frames != null ? 
			$"WEBP {Frames.Count()} 프레임" : 
			$"GDI이미지 {Image.RawFormat}";
	}

	public PageImage(Image image, bool isAnimate = false)
	{
		Image = image;
		IsAnimate = isAnimate;
		Frames = null;
	}

	public PageImage(IEnumerable<WebP.FrameData>? frames)
	{
		Image = new Bitmap(10, 10);
		IsAnimate = true;
		Frames = frames as List<WebP.FrameData>;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		Image.Dispose();
	}
}

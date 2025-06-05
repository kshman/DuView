using System.Collections.Generic;
using Cairo;

namespace DgView.Chaek;

/// <summary>
/// 이미지 또는 애니메이션 프레임을 나타내는 페이지 이미지 클래스입니다.
/// </summary>
public class PageImage
{
	/// <summary>
	/// 표시할 이미지입니다. 애니메이션이 아닌 경우 원본 이미지, 애니메이션인 경우 첫 프레임의 이미지입니다.
	/// </summary>
	public ImageSurface Image { get; }

	/// <summary>
	/// 애니메이션 프레임 목록입니다. 애니메이션이 아닌 경우 null입니다.
	/// </summary>
	public List<AnimatedFrame>? Frames { get; }

	/// <summary>
	/// 현재 애니메이션 프레임의 인덱스입니다.
	/// </summary>
	public int CurrentFrame { get; private set; }

	/// <summary>
	/// 마지막 프레임의 표시 시간(밀리초)입니다.
	/// </summary>
	public int LastDuration { get; private set; }

	/// <summary>
	/// 애니메이션 이미지 여부를 나타냅니다.
	/// </summary>
	public bool HasAnimation => Frames != null;

	/// <summary>
	/// 이미지 또는 애니메이션 정보를 문자열로 반환합니다.
	/// </summary>
	/// <returns>이미지 포맷 또는 프레임 수에 대한 설명 문자열입니다.</returns>
	public override string ToString()
	{
		return Frames != null ?
			$"애니메이션: {Frames.Count} 프레임" :
			$"이미지: {Image.Format}";
	}

	/// <summary>
	/// 정적 이미지를 사용하여 PageImage 인스턴스를 생성합니다.
	/// </summary>
	/// <param name="image">이미지 객체입니다.</param>
	public PageImage(ImageSurface image)
	{
		Image = image;
		Frames = null;
	}

	/// <summary>
	/// 애니메이션 프레임 목록을 사용하여 PageImage 인스턴스를 생성합니다.
	/// </summary>
	/// <param name="frames">애니메이션 프레임 목록입니다.</param>
	public PageImage(List<AnimatedFrame> frames)
	{
		Image = frames[0].Bitmap;
		Frames = frames;
	}

	/// <summary>
	/// 다음 애니메이션 프레임으로 이동하고, 해당 프레임의 표시 시간을 반환합니다.
	/// </summary>
	/// <returns>현재 프레임의 표시 시간(밀리초), 애니메이션이 없으면 -1을 반환합니다.</returns>
	public int Animate()
	{
		if (Frames == null)
			return -1;

		var frame = Frames[CurrentFrame++];
		if (CurrentFrame >= Frames.Count)
			CurrentFrame = 0;

		return LastDuration = frame.Duration;
	}

	/// <summary>
	/// 애니메이션 프레임 인덱스와 지속 시간을 초기화합니다.
	/// </summary>
	public void InitAnimation()
	{
		CurrentFrame = 0;
		LastDuration = 0;
	}

	/// <summary>
	/// 현재 프레임의 이미지를 반환합니다. 애니메이션이 없으면 기본 이미지를 반환합니다.
	/// </summary>
	/// <returns>현재 표시할 Image 객체입니다.</returns>
	public ImageSurface GetImage() => Frames?[CurrentFrame].Bitmap ?? Image;
}

using System.Collections.Generic;

namespace DgView;

public static class ResL
{
	public const string DuViewIcon = "DuView";
	public const string Housebari = "pix-housebari";
	public const string NoImage = "pix-no-img";
	public const string IconBook = "icon-book";
	public const string IconDirectory = "icon-directory";
	public const string IconMove = "icon-move";
	public const string IconPurutu = "icon-purutu";
	public const string IconRename = "icon-rename";
	public const string IconFit = "icon-view-mode-fit";
	public const string IconLeftToRight = "icon-view-mode-l2r";
	public const string IconRightToLeft = "icon-view-mode-r2l";

	private static readonly Dictionary<string, PixBitmap> s_bitmaps = [];

#pragma warning disable CS8618
	public static Cairo.ImageSurface NoImageSurface { get; private set; }
#pragma warning restore CS8618

	public static void Dispose()
	{
		NoImageSurface.Dispose();

		foreach (var bitmap in s_bitmaps.Values)
			bitmap.Dispose();
		s_bitmaps.Clear();
	}

	public static void LoadResources()
	{
		var assembly = typeof(ResL).Assembly;
		var resources = assembly.GetManifestResourceNames().Where(name => name.StartsWith("DgView.Resources.")).ToArray();
		foreach (var resource in resources)
		{
			var name = resource["DgView.Resources.".Length..];
			if (name.Length < 5)
				continue;
			var ext = name[^4..].ToLowerInvariant();
			switch (ext)
			{
				case ".png" or ".jpg" or ".ico":
					name = name[..^4];
					using (var stream = assembly.GetManifestResourceStream(resource))
					{
						if (stream == null)
							continue;
						var bitmap = new PixBitmap(stream);
						s_bitmaps[name] = bitmap;
					}
					break;

				case ".css":
					using (var stream = assembly.GetManifestResourceStream(resource))
					{
						if (stream == null)
							continue;
						var css = new CssProvider();
						using var reader = new StreamReader(stream);
						css.LoadFromData(reader.ReadToEnd());
						StyleContext.AddProviderForScreen(Gdk.Screen.Default, css, StyleProviderPriority.Application);
					}
					break;
			}
		}

		var noImage = GetBitmap(NoImage);
		if (noImage != null)
		{
			NoImageSurface = new Cairo.ImageSurface(Cairo.Format.ARGB32, noImage.Width, noImage.Height);
			using var cr = new Cairo.Context(NoImageSurface);
			Gdk.CairoHelper.SetSourcePixbuf(cr, noImage, 0, 0);
			cr.Paint();
		}
		else
		{
			NoImageSurface = new Cairo.ImageSurface(Cairo.Format.ARGB32, 300, 300);
			using var cr = new Cairo.Context(NoImageSurface);

			cr.SetSourceRGB(0, 0, 0);
			cr.Paint();

			// 빨간색 X 모양 그리기
			cr.SetSourceRGB(1, 0, 0); // 빨간색 (RGB 값)
			cr.LineWidth = 10; // 선 두께

			// 첫 번째 대각선 (왼쪽 위 -> 오른쪽 아래)
			cr.MoveTo(300 * 0.1, 300 * 0.1); // 시작점
			cr.LineTo(300 * 0.9, 300 * 0.9); // 끝점

			// 두 번째 대각선 (오른쪽 위 -> 왼쪽 아래)
			cr.MoveTo(300 * 0.9, 300 * 0.1); // 시작점
			cr.LineTo(300 * 0.1, 300 * 0.9); // 끝점
			cr.Stroke(); // 선 그리기 실행

			cr.Paint();
		}
	}

	public static PixBitmap? GetBitmap(string name) =>
		s_bitmaps.GetValueOrDefault(name);
}

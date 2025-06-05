using Cairo;

namespace DgView.Forms;

internal class ReadWindow : Window
{
    private ImageSurface? _bmp;
    private readonly BoundRect[] _paging_bound = new BoundRect[2];

    public ReadWindow() : base("DgView")
    {

    }

    public ReadWindow(string filename) : this()
    {
    }
}

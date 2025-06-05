using Cairo;

namespace DgView.Forms;

internal class ReadWindow : Window
{
    private ImageSurface? _bmp;
    private readonly BoundRect[] _paging_bound = new BoundRect[2];

    private readonly string _init_filename;

    private readonly System.Timers.Timer _notify_timer;
    private readonly DrawingArea _draw;

    public ReadWindow(string filename) : base("DgView")
    {
        _init_filename = filename;

        _notify_timer = new System.Timers.Timer() { Interval = 5000 };
        _notify_timer.Elapsed += (s, e) =>
        {
            _notify_timer.Stop();
            // 라벨 안보이게
        };

        DeleteEvent += Window_DeleteEvent;
        
        Settings.OnWindowInit(this);
        _draw = new DrawingArea();
        _draw.SetSizeRequest(WidthRequest, HeightRequest);
        _draw.Drawn += DrawOnDrawn;
        
        ShowAll();
    }

    private void Window_DeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
    }

    private void DrawOnDrawn(object o, DrawnArgs args)
    {
        Context cr = args.Cr;
        cr.SetSourceRGB(0.2, 0.4, 0.6);
        cr.Arc(200, 150, 100, 0, 2 * Math.PI);
        cr.Fill();
    }
}

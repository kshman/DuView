// ReSharper disable MissingXmlDoc
namespace DgView.Dowa;

public class BoundRect
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    public int Left => X;
    public int Top => Y;
    public int Right => X + Width;
    public int Bottom => Y + Height;

    public BoundRect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    
    public BoundRect() : this(0, 0, 0, 0)
    {
    }
    
    public BoundRect(BoundRect rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }

    public BoundRect(string[] ss)
    {
        if (ss.Length != 4)
            throw new ArgumentException("BoundRect requires exactly 4 string parameters: X, Y, Width, Height");

        if (!int.TryParse(ss[0], out var x) ||
            !int.TryParse(ss[1], out var y) ||
            !int.TryParse(ss[2], out var width) ||
            !int.TryParse(ss[3], out var height))
            throw new ArgumentException("BoundRect parameters must be integers");

        Set(x, y, width, height);
    }
    
    public void Set(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public void Set(BoundRect rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }
    
    public override string ToString()
    {
        return $"(X={X}, Y={Y}, Width={Width}, Height={Height})";
    }
}

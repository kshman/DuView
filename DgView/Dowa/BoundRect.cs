namespace DgView.Dowa;

/// <summary>
/// 2차원 공간에서 사각형의 위치와 크기를 표현하는 클래스입니다.
/// X, Y 좌표와 Width, Height를 속성으로 가지며, 포함 여부 검사, 복사, 문자열 변환 등의 기능을 제공합니다.
/// </summary>
public class BoundRect
{
    /// <summary>
    /// 사각형의 X 좌표(왼쪽 상단)를 가져오거나 설정합니다.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// 사각형의 Y 좌표(왼쪽 상단)를 가져오거나 설정합니다.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// 사각형의 너비를 가져오거나 설정합니다.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 사각형의 높이를 가져오거나 설정합니다.
    /// </summary>
    public int Height { get; set; }
    
    /// <summary>
    /// 사각형의 왼쪽(X) 좌표를 반환합니다.
    /// </summary>
    public int Left => X;

    /// <summary>
    /// 사각형의 위(Y) 좌표를 반환합니다.
    /// </summary>
    public int Top => Y;

    /// <summary>
    /// 사각형의 오른쪽(X + Width) 좌표를 반환합니다.
    /// </summary>
    public int Right => X + Width;

    /// <summary>
    /// 사각형의 아래(Y + Height) 좌표를 반환합니다.
    /// </summary>
    public int Bottom => Y + Height;

    /// <summary>
    /// X, Y, Width, Height 값을 지정하여 BoundRect를 생성합니다.
    /// </summary>
    public BoundRect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// (0,0,0,0)으로 초기화된 BoundRect를 생성합니다.
    /// </summary>
    public BoundRect() : this(0, 0, 0, 0)
    {
    }
    
    /// <summary>
    /// 다른 BoundRect의 값을 복사하여 생성합니다.
    /// </summary>
    public BoundRect(BoundRect rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }

    /// <summary>
    /// 문자열 배열로부터 BoundRect를 생성합니다. (X, Y, Width, Height 순)
    /// </summary>
    /// <param name="ss">X, Y, Width, Height 순서의 문자열 배열</param>
    /// <exception cref="ArgumentException">배열 길이가 4가 아니거나, 정수 변환에 실패할 경우</exception>
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
    
    /// <summary>
    /// X, Y, Width, Height 값을 한 번에 설정합니다.
    /// </summary>
    public void Set(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// 다른 BoundRect의 값을 복사하여 설정합니다.
    /// </summary>
    public void Set(BoundRect rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }

    /// <summary>
    /// 지정한 정수 좌표가 사각형 내부에 포함되는지 확인합니다.
    /// </summary>
    public bool Contains(int x, int y)
    {
        return x >= Left && x < Right && y >= Top && y < Bottom;
    }

    /// <summary>
    /// 지정한 실수 좌표가 사각형 내부에 포함되는지 확인합니다.
    /// </summary>
    public bool Contains(double dx, double dy)
    {
        var x = (int)dx;
        var y = (int)dy;
        return x >= Left && x < Right && y >= Top && y < Bottom;
    }

    /// <summary>
    /// X, Y 좌표가 0 이상이면 유효한 위치로 간주합니다.
    /// </summary>
    public bool IsValidLocation =>
        X >= 0 && Y >= 0;

    /// <summary>
    /// 사각형의 정보를 문자열로 반환합니다.
    /// </summary>
    public override string ToString()
    {
        return $"(X={X}, Y={Y}, Width={Width}, Height={Height})";
    }
}

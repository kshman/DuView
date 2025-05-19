using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace DuView.Dowa;

/// <summary>컨트롤(폼) 크기와 이동에 관한 충돌 테스트!</summary>
public class SizeMoveHitTest
{
    /// <summary>
    /// 눌림 굵기
    /// </summary>
    [Category("SizeMoveHitTest"), Description("눌림 굵기")]
    public int Thickness { get; set; } = 10;

    /// <summary>
    /// 각 모서리 크기
    /// </summary>
    [Category("SizeMoveHitTest"), Description("각 모서리 크기")]
    public int RoundLength { get; set; } = 8;

    /// <summary>
    /// 타이틀 크기
    /// </summary>
    [Category("SizeMoveHitTest"), Description("타이틀 크기")]
    public int TitleLength { get; set; } = 24;

    /// <summary>
    /// 몸뚱아리도 타이틀로 취급
    /// </summary>
    [Category("SizeMoveHitTest"), Description("몸뚱아리도 타이틀로 취급")]
    public bool BodyAsTitle { get; set; }

    /// <summary>마우스 위치와 폼의 크기로 눌림 위치를 판정합니다.</summary>
    /// <param name="pt">마우스 위치</param>
    /// <param name="clientRectangle">대상 폼의 클라이언트 크기</param>
    /// <returns>눌림 위치에 해당하는 HitTestResult 값</returns>
    public HitTestResult HitTest(Point pt, Rectangle clientRectangle)
    {
        var w = clientRectangle.Width;
        var h = clientRectangle.Height;

        var t_b = pt.X > RoundLength && pt.X < w - RoundLength;
        var l_r = pt.Y > RoundLength && pt.Y < h - RoundLength;

        var q_t = pt.Y <= Thickness;
        var q_b = pt.Y >= h - Thickness;
        var q_l = pt.X <= Thickness;
        var q_r = pt.X >= w - Thickness;

        if (q_t && t_b) return HitTestResult.Top;
        if (q_b && t_b) return HitTestResult.Bottom;
        if (q_l && l_r) return HitTestResult.Left;
        if (q_r && l_r) return HitTestResult.Right;

        if ((q_l && !l_r) && (q_t && !t_b)) return HitTestResult.TopLeft;
        if ((q_r && !l_r) && (q_t && !t_b)) return HitTestResult.TopRight;
        if ((q_l && !l_r) && (q_b && !t_b)) return HitTestResult.BottomLeft;
        if ((q_r && !l_r) && (q_b && !t_b)) return HitTestResult.BottomRight;

        if (BodyAsTitle || pt.Y < TitleLength)
            return HitTestResult.Title;
        return HitTestResult.Body;
    }
}

/// <summary>충돌 테스트 결과 (WIN32의 HT_와 같음)</summary>
public enum HitTestResult
{
    /// <summary>몸뚱아리</summary>
    Body = 1,
    /// <summary>타이틀바</summary>
    Title = 2,
    /// <summary>왼쪽</summary>
    Left = 10,
    /// <summary>오른쪽</summary>
    Right = 11,
    /// <summary>윗쪽</summary>
    Top = 12,
    /// <summary>왼쪽위</summary>
    TopLeft = 13,
    /// <summary>오른쪽 위</summary>
    TopRight = 14,
    /// <summary>아래쪽</summary>
    Bottom = 15,
    /// <summary>왼쪽 아래</summary>
    BottomLeft = 16,
    /// <summary>오른쪽 아래</summary>
    BottomRight = 17,
}

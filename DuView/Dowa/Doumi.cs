using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using InterpolationMode = System.Drawing.Drawing2D.InterpolationMode;

namespace DuView.Dowa;

/// <summary>
/// 도우미 클래스
/// </summary>
public static class Doumi
{
    private static readonly char[] s_separates = ['\n', '\r'];
    private static readonly string[] s_image_extensions = [".png", ".jpg", ".jpeg", ".bmp", ".tga", ".webp", ".gif"];
    private static readonly string[] s_anim_extensions = [".gif", ".webp"];
    private static readonly string[] s_archive_extensions = [".zip"];

    // 문자열을 줄 단위로 나누기
    internal static string[] SplitLines(string context) =>
        context.Split(s_separates, StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// 빈 문자열인지 검사
    /// </summary>
    /// <param name="v">평가할 문자열입니다. null일 수 있습니다.</param>
    /// <returns>문자열이 null이거나, 비어 있거나, 공백 문자로만 이루어져 있으면 <see langword="true"/>이고, 그렇지 않으면 <see langword="false"/>입니다.</returns>
    public static bool EmptyString(this string? v) =>
        string.IsNullOrEmpty(v);

    /// <summary>
    /// 지정한 문자열이 null이거나, 비어 있거나, 공백 문자로만 이루어져 있는지 확인합니다.
    /// </summary>
    /// <param name="v">평가할 문자열입니다. null일 수 있습니다.</param>
    /// <returns>문자열이 null이거나, 비어 있거나, 공백 문자로만 이루어져 있으면 <see langword="true"/>이고, 그렇지 않으면 <see langword="false"/>입니다.</returns>
    public static bool WhiteString(this string? v) =>
        string.IsNullOrWhiteSpace(v);

    /// <summary>
    /// 문자열이 null이 아니고, 비어 있지 않은지 또는 공백이 아닌지 검사합니다.
    /// </summary>
    /// <param name="v">검사할 문자열입니다.</param>
    /// <param name="testWhites">공백까지 검사할지 여부입니다.</param>
    /// <returns>조건에 따라 문자열이 존재하면 true, 아니면 false를 반환합니다.</returns>
    public static bool TestHave([NotNullWhen(true)] this string? v, bool testWhites = false) =>
        testWhites ? !string.IsNullOrWhiteSpace(v) : !string.IsNullOrEmpty(v);

    /// <summary>
    /// CultureInfo에서 알려진 로캘 문자열을 반환합니다.
    /// </summary>
    /// <param name="culture">CultureInfo 객체입니다.</param>
    /// <returns>알려진 로캘 문자열(ko, sh, kim, en) 중 하나를 반환합니다.</returns>
    public static string GetKnownLocale(this CultureInfo culture)
    {
        var name = culture.Name;
        string locale;

        if (name.StartsWith("ko"))
            locale = "ko"; // 대한민국
        else if (name.StartsWith("sh"))
            locale = "sh"; // 세인트 헬러나
        else if (name.StartsWith("kim"))
            locale = "kim"; // 이런 나라 없다
        else
            locale = "en"; // 기본은 영어

        return locale;
    }

    /// <summary>
    /// 확장자가 이미지 파일 확장자인지 확인합니다.
    /// </summary>
    /// <param name="extension">확장자 문자열입니다.</param>
    /// <returns>이미지 확장자이면 true, 아니면 false를 반환합니다.</returns>
    public static bool IsImageExtension(this string extension) =>
        s_image_extensions.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// 확장자가 애니메이션 이미지 확장자인지 확인합니다.
    /// </summary>
    /// <param name="extension">확장자 문자열입니다.</param>
    /// <returns>애니메이션 확장자이면 true, 아니면 false를 반환합니다.</returns>
    public static bool IsAnimationExtension(this string extension) =>
        s_anim_extensions.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// 파일 이름이 애니메이션 이미지 파일인지 확인합니다.
    /// </summary>
    /// <param name="filename">파일 이름입니다.</param>
    /// <returns>애니메이션 이미지 파일이면 true, 아니면 false를 반환합니다.</returns>
    public static bool IsAnimationFileName(string filename)
    {
        var n = filename.LastIndexOf('.');
        var extension = filename[n..];
        return IsAnimationExtension(extension);
    }

    /// <summary>
    /// 확장자가 아카이브 파일 타입인지 확인합니다.
    /// </summary>
    /// <param name="extension">확장자 문자열입니다.</param>
    /// <returns>아카이브 타입이면 true, 아니면 false를 반환합니다.</returns>
    public static bool IsArchiveExtension(this string extension) =>
        s_archive_extensions.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// ViewQuality 값을 InterpolationMode로 변환합니다.
    /// </summary>
    /// <param name="q">ViewQuality 값입니다.</param>
    /// <returns>해당하는 InterpolationMode를 반환합니다.</returns>
    public static InterpolationMode QualityToInterpolationMode(ViewQuality q)
    {
        return q switch
        {
            ViewQuality.Invalid => InterpolationMode.Invalid,
            ViewQuality.Default => InterpolationMode.Default,
            ViewQuality.Low => InterpolationMode.Low,
            ViewQuality.High => InterpolationMode.High,
            ViewQuality.Bilinear => InterpolationMode.Bilinear,
            ViewQuality.Bicubic => InterpolationMode.Bicubic,
            ViewQuality.NearestNeighbor => InterpolationMode.NearestNeighbor,
            ViewQuality.HqBilinear => InterpolationMode.HighQualityBilinear,
            ViewQuality.HqBicubic => InterpolationMode.HighQualityBicubic,
            _ => InterpolationMode.Default,
        };
    }

    /// <summary>
    /// 바이트 크기를 사람이 읽기 쉬운 문자열로 변환합니다.
    /// </summary>
    /// <param name="size">바이트 단위의 크기입니다.</param>
    /// <returns>GB, MB, KB, B 단위의 문자열을 반환합니다.</returns>
    public static string SizeToString(long size)
    {
        const long giga = 1024 * 1024 * 1024;
        const long mega = 1024 * 1024;
        const long kilo = 1024;

        double v;
        switch (size)
        {
            // 0.5 기가
            case > giga:
                v = size / (double)giga;
                return $"{v:0.0}GB";

            // 0.5 메가
            case > mega:
                v = size / (double)mega;
                return $"{v:0.0}MB";

            // 0.5 킬로
            case > kilo:
                v = size / (double)kilo;
                return $"{v:0.0}KB";

            default:
                return $"{size}B";
        }
    }

    /// <summary>
    /// 대상 사각형을 계산합니다.
    /// </summary>
    /// <param name="tw">원본 너비</param>
    /// <param name="th">원본 높이</param>
    /// <param name="dw">대상 너비</param>
    /// <param name="dh">대상 높이</param>
    /// <param name="align">정렬 방식</param>
    /// <returns>계산된 Rectangle을 반환합니다.</returns>
    public static Rectangle CalcDestRect(int tw, int th, int dw, int dh, HorizontalAlignment align)
    {
        var rt = new Rectangle(0, 0, dw, dh);

        if (align == HorizontalAlignment.Left)
        {
            // 오우
        }
        else
        {
            // 오른쪽
            if (dw < tw)
            {
                rt.X = tw - dw;

                // 가운데
                if (align == HorizontalAlignment.Center)
                    rt.X /= 2;
            }
        }

        if (dh < th)
            rt.Y = (th - dh) / 2;

        return rt;
    }

    /// <summary>
    /// 원본과 대상 크기 및 확대 여부에 따라 최적의 크기를 계산합니다.
    /// </summary>
    /// <param name="zoom">확대 여부</param>
    /// <param name="dw">대상 너비</param>
    /// <param name="dh">대상 높이</param>
    /// <param name="sw">원본 너비</param>
    /// <param name="sh">원본 높이</param>
    /// <returns>계산된 (너비, 높이) 튜플을 반환합니다.</returns>
    public static (int w, int h) CalcDestSize(bool zoom, int dw, int dh, int sw, int sh)
    {
        var dstAspect = dw / (double)dh;
        var srcAspect = sw / (double)sh;
        int nw = dw, nh = dh;

        if (zoom)
        {
            if (srcAspect > 1)
            {
                // 세로로 긴 그림
                if (dstAspect < srcAspect)
                    nh = (int)(dw / srcAspect);
                else
                    nw = (int)(dh * srcAspect);
            }
            else
            {
                // 가로로 긴 그림
                if (dstAspect > srcAspect)
                    nw = (int)(dh * srcAspect);
                else
                    nh = (int)(dw / srcAspect);
            }
        }
        else
        {
            // 가로로 맞춘다... 스크롤은 쌩깜
            nh = (int)(dw / srcAspect);
        }

        return (nw, nh);
    }

    /// <summary>
    /// 문자열 비교
    /// </summary>
    /// <param name="s1">비교할 첫 번째 문자열입니다.</param>
    /// <param name="s2">비교할 두 번째 문자열입니다.</param>
    /// <returns>숫자 및 문자 순서에 따라 비교 결과를 반환합니다. s1이 s2보다 작으면 음수, 같으면 0, 크면 양수를 반환합니다.</returns>
    public static int StringAsNumericCompare(string? s1, string? s2)
    {
        //get rid of special cases
        if (s1 == null) return s2 == null ? 0 : -1;
        if (s2 == null) return 1;

        if ((s1.Equals(string.Empty) && (s2.Equals(string.Empty)))) return 0;
        if (s1.Equals(string.Empty)) return -1;
        if (s2.Equals(string.Empty)) return -1;

        //WE style, special case
        var sp1 = char.IsLetterOrDigit(s1, 0);
        var sp2 = char.IsLetterOrDigit(s2, 0);
        switch (sp1)
        {
            case true when !sp2:
                return 1;
            case false when sp2:
                return -1;
        }

        int i1 = 0, i2 = 0; //current index
        while (true)
        {
            var c1 = char.IsDigit(s1, i1);
            var c2 = char.IsDigit(s2, i2);
            int r;
            switch (c1)
            {
                case false when !c2:
                {
                    var letter1 = char.IsLetter(s1, i1);
                    var letter2 = char.IsLetter(s2, i2);
                    switch (letter1)
                    {
                        case true when letter2:
                        case false when !letter2:
                            r = letter1 && letter2
                                ? char.ToLower(s1[i1]).CompareTo(char.ToLower(s2[i2]))
                                : s1[i1].CompareTo(s2[i2]);
                            if (r != 0) return r;
                            break;
                        case false when letter2:
                            return -1;
                        case true when !letter2:
                            return 1;
                    }
                }
                    break;
                case true when c2:
                    r = InternalNumberCompare(s1, ref i1, s2, ref i2);
                    if (r != 0) return r;
                    break;
                case true:
                    return -1;
                default:
                    if (c2) return 1;
                    break;
            }

            i1++;
            i2++;
            if ((i1 >= s1.Length) && (i2 >= s2.Length))
                return 0;
            if (i1 >= s1.Length)
                return -1;
            if (i2 >= s2.Length)
                return -1;
        }
    }

    private static int InternalNumberCompare(string s1, ref int i1, string s2, ref int i2)
    {
        var (start1, end1) = InternalNumberScanEnd(s1, i1);
        var (start2, end2) = InternalNumberScanEnd(s2, i2);
        var pos1 = i1;
        i1 = end1 - 1;
        var pos2 = i2;
        i2 = end2 - 1;

        var nzLength1 = end1 - start1;
        var nzLength2 = end2 - start2;

        if (nzLength1 < nzLength2) return -1;
        if (nzLength1 > nzLength2) return 1;

        for (int j1 = start1, j2 = start2; j1 <= i1; j1++, j2++)
        {
            var r = s1[j1].CompareTo(s2[j2]);
            if (r != 0) return r;
        }

        // the nz parts are equal
        var length1 = end1 - pos1;
        var length2 = end2 - pos2;
        if (length1 == length2) return 0;
        if (length1 > length2) return -1;
        return 1;
    }

    private static (int start, int end) InternalNumberScanEnd(string s, int startPosition)
    {
        var start = startPosition;
        var end = startPosition;
        var zero = true;
        while (char.IsDigit(s, end))
        {
            if (zero && s[end].Equals('0'))
                start++;
            else zero = false;
            end++;
            if (end >= s.Length) break;
        }

        return (start, end);
    }

    /// <summary>
    /// 파일 정보 이름으로 비교
    /// </summary>
    public class FileInfoComparer : IComparer<FileInfo>
    {
        /// <summary>
        /// 비교 메소드
        /// </summary>
        /// <param name="x">비교할 첫 번째 FileInfo 객체입니다.</param>
        /// <param name="y">비교할 두 번째 FileInfo 객체입니다.</param>
        /// <returns>비교 결과를 반환합니다. x가 y보다 작으면 음수, 같으면 0, 크면 양수를 반환합니다.</returns>
        public int Compare(FileInfo? x, FileInfo? y) =>
            StringAsNumericCompare(x?.FullName, y?.FullName);
    }

    /// <summary>
    /// 디렉토리 정보 이름으로 비교
    /// </summary>
    public class DirectoryInfoComparer : IComparer<DirectoryInfo>
    {
        /// <summary>
        /// 비교 메소드
        /// </summary>
        /// <param name="x">비교할 첫 번째 DirectoryInfo 객체입니다.</param>
        /// <param name="y">비교할 두 번째 DirectoryInfo 객체입니다.</param>
        /// <returns>비교 결과를 반환합니다. x가 y보다 작으면 음수, 같으면 0, 크면 양수를 반환합니다.</returns>
        public int Compare(DirectoryInfo? x, DirectoryInfo? y) =>
            StringAsNumericCompare(x?.FullName, y?.FullName);
    }
}
﻿namespace DgView.Chaek;

/// <summary>
/// 수평 정렬 방식을 지정합니다.
/// </summary>
public enum HorizAlign
{
    /// <summary>왼쪽 정렬</summary>
    Left,

    /// <summary>가운데 정렬</summary>
    Center,

    /// <summary>오른쪽 정렬</summary>
    Right
}

/// <summary>
/// 보기 모드
/// </summary>
public enum ViewMode
{
    /// <summary>화면에 맞춤</summary>
    Fit = 0,

    /// <summary>왼쪽에서 오른쪽으로</summary>
    LeftToRight = 2,

    /// <summary>오른쪽에서 왼쪽으로</summary>
    RightToLeft = 3,

    /// <summary>따라감</summary>
    Follow = 255,
}

/// <summary>
/// 보기 품질
/// </summary>
public enum ViewQuality
{
    /// <summary>잘못된 품질</summary>
    Invalid,

    /// <summary>낮음</summary>
    Fast,

    /// <summary>기본</summary>
    Default,

    /// <summary>높음</summary>
    High,

    /// <summary>픽셀 유지</summary>
    Nearest,

    /// <summary>양선형 보간</summary>
    Bilinear,
}

/// <summary>
/// 책 조작
/// </summary>
public enum BookControl
{
    /// <summary>이전</summary>
    Previous,

    /// <summary>다음</summary>
    Next,

    /// <summary>첫째 장</summary>
    First,

    /// <summary>마지막 장</summary>
    Last,

    /// <summary>이전 10쪽</summary>
    SeekPrevious10,

    /// <summary>다음 10쪽</summary>
    SeekNext10,

    /// <summary>이전 1쪽</summary>
    SeekMinusOne,

    /// <summary>다음 1쪽</summary>
    SeekPlusOne,

    /// <summary>이전 1장으로</summary>
    ScanPrevious,

    /// <summary>다음 1장으로</summary>
    ScanNext,

    /// <summary>쪽 선택</summary>
    Select,
}

/// <summary>
/// 책 읽기 방향
/// </summary>
public enum BookDirection
{
    /// <summary>앞쪽으로</summary>
    Previous,

    /// <summary>뒤쪽으로</summary>
    Next,
}

/// <summary>
/// 패스코드 사용
/// </summary>
public enum PassCodeUsage
{
    /// <summary>실행 할 때</summary>
    Run,

    /// <summary>설정을 열 때</summary>
    Option,

    /// <summary>마지막 책을 고를 때</summary>
    LastBook,

    /// <summary>책을 옮길 때</summary>
    MoveBook,

    /// <summary>책 이름을 바꿀 때</summary>
    RenameBook,
}
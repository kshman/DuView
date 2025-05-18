using System.Collections;
using System.Text;

namespace DuView.Dowa;

/// <summary>
/// StringConvert 함수 구현
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IStringConverter<out T>
{
    /// <summary>
    /// 문자열 변환
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    T StringConvert(string? s);
}

/// <summary>
/// 키/값 변환
/// </summary>
public interface IKeyValueStringConverter<out TKey, out TValue>
{
    /// <summary>
    /// 키 문자열 변환
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    TKey KeyStringConvert(string? key);

    /// <summary>
    /// 값 문자열 변환
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    TValue ValueStringConvert(string? value);
}

/// <summary>
/// 해시인데 입출력용
/// </summary>
/// <typeparam name="TKey">키형식</typeparam>
/// <typeparam name="TValue">값형식</typeparam>
public class LineHash<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>> where TKey : notnull
{
    /// <summary>
    /// 데이터 사전
    /// </summary>
    protected readonly Dictionary<TKey, TValue> Db = new();

    /// <summary>
    /// 생성자
    /// </summary>
    protected LineHash()
    {
    }

    /// <summary>
    /// 빈거 만들기
    /// </summary>
    /// <returns></returns>
    public static LineHash<TKey, TValue> Empty()
    {
        return new LineHash<TKey, TValue>();
    }

    /// <summary>
    /// 문자열에서 만들기
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineHash<TKey, TValue> FromContext(string context, IKeyValueStringConverter<TKey, TValue> converter)
    {
        var l = new LineHash<TKey, TValue>();
        l.AddFromContext(context, converter);
        return l;
    }

    /// <summary>
    /// 파일에서 만들기
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineHash<TKey, TValue>? FromFile(string filename, IKeyValueStringConverter<TKey, TValue> converter)
    {
        var l = new LineHash<TKey, TValue>();
        return l.AddFromFile(filename, converter) ? l : null;
    }

    /// <summary>
    /// 항목 갯수
    /// </summary>
    public int Count => Db.Count;

    /// <summary>
    /// 키로 얻기
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TValue this[TKey key]
    {
        get => Db[key];
        set => Db[key] = value;
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
        return Db.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Db.GetEnumerator();
    }

    /// <summary>
    /// 값 쓰기
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Set(TKey key, TValue value)
    {
        Db[key] = value;
    }

    /// <summary>
    /// 값 얻기
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public TValue Get(TKey key, TValue defaultValue)
    {
        return Db.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// 값 얻기
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TValue? Get(TKey key)
    {
        return Db.GetValueOrDefault(key);
    }

    /// <summary>
    /// 값 있나 확인하고 있으면 얻기
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Try(TKey key, out TValue? value)
    {
        return Db.TryGetValue(key, out value);
    }

    /// <summary>
    /// 문자열 값이 있나 확인
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryParse(TKey key, out string? value)
    {
        if (!Db.TryGetValue(key, out var v))
        {
            value = null;
            return false;
        }
        else
        {
            value = v?.ToString();
            return true;
        }
    }

    /// <summary>
    /// 정수 값이 있나 확인
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryParse(TKey key, out int value)
    {
        if (Db.TryGetValue(key, out var v))
            return int.TryParse(v?.ToString(), out value);

        value = 0;
        return false;
    }

    /// <summary>
    /// 짧은정수 값이 있나 확인
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryParse(TKey key, out ushort value)
    {
        if (Db.TryGetValue(key, out var v))
            return ushort.TryParse(v?.ToString(), out value);

        value = 0;
        return false;
    }

    /// <summary>
    /// 키 지우기
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Remove(TKey key)
    {
        return Db.Remove(key);
    }

    /// <summary>
    /// 한 줄에서 키와 값을 얻는다
    /// </summary>
    /// <param name="l"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected bool ParseLineString(string l, out string key, out string value)
    {
        key = string.Empty;
        value = string.Empty;

        if (l[0] == '#' || l.StartsWith("//"))
            return false;

        if (l[0] == '"')
        {
            var qt = l.IndexOf('"', 1);
            if (qt < 0)
                return false; // no end quote. probably

            var t = l[(qt + 1)..].TrimStart();
            if (t.Length == 0 || t[0] != '=')
                return false; // no value

            key = l[1..qt].Trim();
            value = t[1..].Trim();
        }
        else
        {
            var div = l.IndexOf('=');
            if (div <= 0)
                return false; // not valid line

            key = l[..div].Trim();
            value = l[(div + 1)..].Trim();
        }

        return true;
    }

    private void InternalParseLines(string ctx, IKeyValueStringConverter<TKey, TValue> converter)
    {
        var ss = Doumi.SplitLines(ctx);
        foreach (var v in ss)
        {
            var l = v.TrimStart();
            if (ParseLineString(l, out var key, out var value))
                Db[converter.KeyStringConvert(key)] = converter.ValueStringConvert(value);
        }
    }

    /// <summary>
    /// 문자열에서 항목을 추가
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    public void AddFromContext(string context, IKeyValueStringConverter<TKey, TValue> converter)
    {
        InternalParseLines(context, converter);
    }

    /// <summary>
    /// 파일에서 항목을 추가
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public bool AddFromFile(string filename, IKeyValueStringConverter<TKey, TValue> converter)
    {
        try
        {
            if (File.Exists(filename))
            {
                var context = File.ReadAllText(filename, Encoding.UTF8);
                InternalParseLines(context, converter);
                return true;
            }
        }
        catch
        {
            // 무시
        }

        return false;
    }

    /// <summary>
    /// 파일로 저장합니다
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="headers"></param>
    public void Save(string filename, string[]? headers = null)
    {
        using var sw = new StreamWriter(filename, false, Encoding.UTF8);
        Save(sw, headers);
    }

    /// <summary>
    /// 쓰개를 써서 저장
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="headers"></param>
    public void Save(TextWriter stream, string[]? headers = null)
    {
        if (headers != null)
        {
            foreach (var l in headers)
                stream.WriteLine($"# {l}");
            stream.WriteLine();
        }

        foreach (var l in Db)
        {
            var t = l.Key.ToString();
            stream.WriteLine(t?.IndexOf('=') != -1 ? $"\"{t}\"={l.Value}" : $"{t}={l.Value}");
        }
    }
}

/// <summary>
/// 문자열 키를 가진 줄 디비
/// </summary>
/// <typeparam name="T"></typeparam>
public class LineStringHash<T> : LineHash<string, T>
{
    /// <summary>
    /// 생성자
    /// </summary>
    protected LineStringHash()
    {
    }

    /// <summary>
    /// 빈거 만들기
    /// </summary>
    /// <returns></returns>
    public new static LineStringHash<T> Empty()
    {
        return new LineStringHash<T>();
    }

    /// <summary>
    /// 문자열에서 만들기
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineStringHash<T> FromContext(string context, IStringConverter<T> converter)
    {
        var l = new LineStringHash<T>();
        l.AddFromContext(context, converter);
        return l;
    }

    /// <summary>
    /// 파일에서 만들기
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineStringHash<T>? FromFile(string filename, IStringConverter<T> converter)
    {
        var l = new LineStringHash<T>();
        return l.AddFromFile(filename, converter) ? l : null;
    }

    private void InternalParseLines(string ctx, IStringConverter<T> converter)
    {
        var ss = Doumi.SplitLines(ctx);
        foreach (var v in ss)
        {
            var l = v.TrimStart();
            if (ParseLineString(l, out var key, out var value))
                Db[key] = converter.StringConvert(value);
        }
    }

    /// <summary>
    /// 문자열에서 추가하기
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    public void AddFromContext(string context, IStringConverter<T> converter)
    {
        InternalParseLines(context, converter);
    }

    /// <summary>
    /// 파일에서 추가하기
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public bool AddFromFile(string filename, IStringConverter<T> converter)
    {
        try
        {
            if (File.Exists(filename))
            {
                var context = File.ReadAllText(filename, Encoding.UTF8);
                InternalParseLines(context, converter);
                return true;
            }
        }
        catch
        {
            // 무시
        }

        return false;
    }
}

/// <summary>
/// 정수 키를 가진 줄 디비
/// </summary>
/// <typeparam name="T"></typeparam>
public class LineIntHash<T> : LineHash<int, T>
{
    /// <summary>
    /// 생성자
    /// </summary>
    protected LineIntHash()
    {
    }

    /// <summary>
    /// 빈거 만들기
    /// </summary>
    /// <returns></returns>
    public new static LineIntHash<T> Empty()
    {
        return new LineIntHash<T>();
    }

    /// <summary>
    /// 문자열에서 만들기
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineIntHash<T> FromContext(string context, IStringConverter<T> converter)
    {
        var l = new LineIntHash<T>();
        l.AddFromContext(context, converter);
        return l;
    }

    /// <summary>
    /// 파일에서 만들기
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="encoding"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static LineIntHash<T>? FromFile(string filename, Encoding encoding, IStringConverter<T> converter)
    {
        var l = new LineIntHash<T>();
        return l.AddFromFile(filename, encoding, converter) ? l : null;
    }

    private void InternalParseLines(string ctx, IStringConverter<T> converter)
    {
        var ss = Doumi.SplitLines(ctx);
        foreach (var v in ss)
        {
            var l = v.TrimStart();
            if (ParseLineString(l, out var key, out var value) && int.TryParse(key, out var intKey))
                Db[intKey] = converter.StringConvert(value);
        }
    }

    /// <summary>
    /// 문자열에서 추가하기
    /// </summary>
    /// <param name="context"></param>
    /// <param name="converter"></param>
    public void AddFromContext(string context, IStringConverter<T> converter)
    {
        InternalParseLines(context, converter);
    }

    /// <summary>
    /// 파일에서 추가하기
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="encoding"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public bool AddFromFile(string filename, Encoding encoding, IStringConverter<T> converter)
    {
        try
        {
            if (File.Exists(filename))
            {
                var context = File.ReadAllText(filename, encoding);
                InternalParseLines(context, converter);
                return true;
            }
        }
        catch
        {
            // 무시
        }

        return false;
    }
}

/// <summary>
/// 크기를 바꿀 수 있는 문자열 해시
/// </summary>
internal class ResizableHash : LineStringHash<int>
{
    private ResizableHash()
    {
    }

    public static ResizableHash New()
    {
        return new ResizableHash();
    }

    public static ResizableHash FromFile(string filename)
    {
        var l = new ResizableHash();
        l.AddFromFile(filename, new IntValueConverter());
        return l;
    }

    public void ResizeCutFrontSlowly(int count)
    {
        if (count >= Db.Count)
            return;

        var ns = new List<KeyValuePair<string, int>>();

        var n = Db.Count - count;
        foreach (var i in Db)
        {
            if (n > 0)
                n--;
            else
                ns.Add(i);
        }

        Db.Clear();

        foreach (var i in ns)
            Db[i.Key] = i.Value;
    }

    private class IntValueConverter : IStringConverter<int>
    {
        public int StringConvert(string? s) =>
            int.TryParse(s, out var n) ? n : 0;
    }
}

/// <summary>
/// 설정용 라인 해시
/// </summary>
public class SettingsHash : LineStringHash<string>
{
    private SettingsHash()
    {
    }

    /// <summary>
    /// 파일에서 만들기
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static SettingsHash FromFile(string filename)
    {
        var l = new SettingsHash();
        l.AddFromFile(filename, new StringToStringConverter());
        return l;
    }

    /// <summary>
    /// 문자열 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public string? GetString(string name, string? defaultValue = null) =>
        Try(name, out var value) ? value : defaultValue;

    /// <summary>
    /// 정수 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public int GetInt(string name, int defaultValue = 0) =>
        TryParse(name, out int value) ? value : defaultValue;

    /// <summary>
    /// 불린 값 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public bool GetBool(string name, bool defaultValue = false)
    {
        var s = GetString(name);
        return Alter.ToBool(s, defaultValue);
    }

    /// <summary>
    /// 디코딩된 문자열 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public string? GetDecodedString(string name, string? defaultValue = null)
    {
        var s = Try(name, out var ret) ? ret : defaultValue;
        return string.IsNullOrEmpty(s) ? null : Alter.DecodingString(s);
    }

    /// <summary>
    /// 문자열 입력하기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetString(string name, string? value)
    {
        if (value == null)
            Remove(name);
        else
            Set(name, value);
    }

    /// <summary>
    /// 정수 입력하기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetInt(string name, int value) =>
        Set(name, value.ToString());

    /// <summary>
    /// 불린 값 입력하기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetBool(string name, bool value) =>
        Set(name, value ? "true" : "false");

    /// <summary>
    /// 인코딩한 문자열 입력하기
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetEncodedString(string name, string? value)
    {
        var s = value == null ? null : Alter.EncodingString(value);
        SetString(name, s);
    }

    private class StringToStringConverter : IStringConverter<string>
    {
        public string StringConvert(string? s) =>
            s ?? string.Empty;
    }
}
using System.Text;

namespace DuView;

public class SettingsLineDb : LineStringDb<string>
{
	private SettingsLineDb()
	{ }

	public static SettingsLineDb FromFile(string filename)
	{
		var l = new SettingsLineDb();
		l.AddFromFile(filename, Encoding.UTF8, new StringToStringConverter());
		return l;
	}

	public string? GetString(string name, string? defaultValue = null) =>
		Try(name, out var value) ? value : defaultValue;

	public int GetInt(string name, int defaultValue = 0) =>
		TryParse(name, out int value) ? value : defaultValue;

	public bool GetBool(string name, bool defaultValue = false)
	{
		var s = GetString(name);
		return string.IsNullOrWhiteSpace(s)
			? defaultValue
			: s.ToLowerInvariant() switch
			{
				"true" or "1" or "cham" or "yes" => true,
				"false" or "0" or "anio" or "no" => false,
				_ => defaultValue,
			};
	}

	public string? GetDecodedString(string name, string? defaultValue = null)
	{
		var s = Try(name, out var ret) ? ret : defaultValue;
		return string.IsNullOrWhiteSpace(s) ? null : Converter.DecodingString(s);
	}

	public void SetString(string name, string? value)
	{
		if (value == null)
			Remove(name);
		else
			Set(name, value);
	}

	public void SetInt(string name, int value) =>
		Set(name, value.ToString());

	public void SetBool(string name, bool value) =>
		Set(name, value ? "true" : "false");

	public void SetEncodedString(string name, string? value)
	{
		var s = value == null ? null : Converter.EncodingString(value);
		SetString(name, s);
	}
}

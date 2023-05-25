using System.Text;

namespace maxim_tasks;

public static class Extensions
{
    public static string Reverse(this string text)
    {
        var chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public static void RequireEnglishLowercase(this string text)
    {
        var sb = new StringBuilder();
        foreach (var ch in text.AsSpan())
        {
            if (ch is < 'a' or > 'z')
            {
                sb.Append(ch);
            }
        }

        if (sb.Length > 0)
        {
            throw new RequireEnglishLowercaseException($"'{text}' has not allowed symbols: '{sb}'");
        }
    }
}

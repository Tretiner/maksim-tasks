namespace maxim_tasks;

public static class Utils
{
    public static string EvenOddReverseText(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return "";
        }

        text.RequireEnglishLowercase();

        if (text.Length % 2 != 0)
        {
            return text.Reverse() + text;
        }
        else
        {
            var halfTextLen = (text.Length + 1) / 2;
            return text[..halfTextLen].Reverse() + text[halfTextLen..].Reverse();
        }
    }
}

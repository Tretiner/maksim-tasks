namespace maxim_tasks;

public record EvenOddTextReverserResult(
    string Text,
    Dictionary<char, int> CharsOccurenceInfo
)
{
    public string FormattedCharsOccurenceInfo =>
        string.Join(
            "\n",
            CharsOccurenceInfo
                .OrderByDescending(kv => kv.Value)
                .Select(kv => $"{kv.Key}: {kv.Value}")
        );

    public override string ToString() => $"text: {Text}\nchars occurences:\n{FormattedCharsOccurenceInfo}";
}

public static class EvenOddTextReverser
{
    public static EvenOddTextReverserResult ReverseText(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new EvenOddTextReverserResult
            (
                Text: "",
                CharsOccurenceInfo: new Dictionary<char, int>()
            );
        }

        text.RequireEnglishLowercase();

        string resultText;
        if (text.Length % 2 != 0)
        {
            resultText = text.Reverse() + text;
        }
        else
        {
            var halfTextLen = (text.Length + 1) / 2;
            resultText = text[..halfTextLen].Reverse() + text[halfTextLen..].Reverse();
        }

        return new EvenOddTextReverserResult(
            Text: resultText,
            CharsOccurenceInfo: resultText
                .GroupBy(ch => ch)
                .ToDictionary(kv => kv.Key, kv => kv.Count())
            );
    }
}

using maxim_tasks.Utils.Sorters;

namespace maxim_tasks;

public record EvenOddTextReverserResult(
	string Text,
	Dictionary<char, int> CharsOccurenceInfo,
	string MaxSubstringWithVerbsOnBothSides,
	string SortedResultText
)
{
	public static EvenOddTextReverserResult Empty => new (
		Text: "",
		CharsOccurenceInfo: new Dictionary<char, int>(),
		MaxSubstringWithVerbsOnBothSides: "",
		SortedResultText: ""
	);


	public string FormattedCharsOccurenceInfo =>
		string.Join(
			"\n",
			CharsOccurenceInfo
				.OrderByDescending(kv => kv.Value)
				.Select(kv => $"{kv.Key}: {kv.Value}")
		);

	public override string ToString() =>
$@"Text: {Text}
Chars occurences:
{FormattedCharsOccurenceInfo}
Max substring with verbs on both sides: {MaxSubstringWithVerbsOnBothSides}
SortedResultText: {SortedResultText}";
}

public static class EvenOddTextReverser
{
	public static EvenOddTextReverserResult ReverseText(string? text, IStringSorter sorter)
	{
		if (string.IsNullOrEmpty(text))
		{
			return EvenOddTextReverserResult.Empty;
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

		var charOccurenceInfo = new Dictionary<char, int>();

		var englishVerbs = "aeiouy";
		int startIndex = -1, endIndex = -1;
		for (int i = 0; i < resultText.Length; i++)
		{
			var ch = resultText[i];

			charOccurenceInfo.TryGetValue(ch, out var charCount);
			charOccurenceInfo[ch] = charCount + 1;

			if (!englishVerbs.Contains(ch)) continue;

			if (startIndex == -1)
			{
				startIndex = i;
			}
			else
			{
				endIndex = i;
			}
		}

		var maxSubstringWithVerbsOnBothSides = endIndex != -1 ? resultText[startIndex..(endIndex + 1)] : "";
		var sortedResultText = sorter.SortString(resultText);

		return new EvenOddTextReverserResult(
			Text: resultText,
			CharsOccurenceInfo: charOccurenceInfo,
			MaxSubstringWithVerbsOnBothSides: maxSubstringWithVerbsOnBothSides,
			SortedResultText: sortedResultText
		);
	}
}

using maxim_tasks.Models;
using maxim_tasks.Services.RandomNumberGeneratorService;
using maxim_tasks.Utils.Sorters;
using System.Collections.Immutable;

namespace maxim_tasks.Services.EvenOddTextReverserService;

public class EvenOddTextReverserService: IEvenOddTextReverserService
{
    private readonly IRandomNumberGeneratorService _randomNumberGenerator;

    public EvenOddTextReverserService(IRandomNumberGeneratorService randomNumberGenerator)
    {
        _randomNumberGenerator = randomNumberGenerator;
    }

    public async Task<EvenOddTextReverserResult> ReverseText(string? text, IStringSorter? sorter = null)
    {
        if (string.IsNullOrEmpty(text))
        {
            return EvenOddTextReverserResult.Empty;
        }

        text.RequireEnglishLowercase();

        sorter ??= new QuickSorter();

		var resultText = GetReversedText(text);

		var charOccurenceInfo = GetCharOccurenceInfo(resultText);

        var maxSubstringWithVerbsOnBothSides = GetMaxSubstringWithVerbsOnBothSides(resultText);

        var sortedResultText = sorter.SortString(resultText);

        var randNum = await _randomNumberGenerator.GetRandomNumber(0, resultText.Length - 1);
        var cutString = resultText.RemoveCharAt(randNum);

        return new EvenOddTextReverserResult(
            Text: resultText,
            CharsOccurenceInfo: charOccurenceInfo,
            MaxSubstringWithVerbsOnBothSides: maxSubstringWithVerbsOnBothSides,
            SortedResultText: sortedResultText,
            CutText: cutString
        );
    }

	private static string GetReversedText(string text)
	{
		if (text.Length % 2 != 0)
		{
			return text.Reverse() + text;
		}
		var halfTextLen = (text.Length + 1) / 2;
		return text[..halfTextLen].Reverse() + text[halfTextLen..].Reverse();
	}

	private static ImmutableDictionary<char, int> GetCharOccurenceInfo(string text) =>
		text.GroupBy(ch => ch)
			.ToImmutableDictionary(kv => kv.Key, kv => kv.Count());

	private static string GetMaxSubstringWithVerbsOnBothSides(string text)
	{
		var englishVerbs = "aeiouy";
		int startIndex = -1, endIndex = -1;
		for (int i = 0; i < text.Length; i++)
		{
			var ch = text[i];

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

		return endIndex != -1 ? text[startIndex..(endIndex + 1)] : "";
	}
}
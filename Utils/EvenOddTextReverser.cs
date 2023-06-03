using maxim_tasks.Models;
using maxim_tasks.Services.RandomNumberGeneratorService;
using maxim_tasks.Utils.Sorters;

namespace maxim_tasks;

public class EvenOddTextReverser
{
	private readonly IRandomNumberGeneratorService _randomNumberGenerator;

	public EvenOddTextReverser(IRandomNumberGeneratorService randomNumberGenerator)
	{
		_randomNumberGenerator = randomNumberGenerator;
	}

	public async Task<EvenOddTextReverserResult> ReverseText(string? text, IStringSorter sorter)
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
}

using System.Text.Json.Serialization;

namespace maxim_tasks.Models;

public record EvenOddTextReverserResult(
	string Text,
	Dictionary<char, int> CharsOccurenceInfo,
	string MaxSubstringWithVerbsOnBothSides,
	string SortedResultText,
	string CutText
)
{
	[JsonIgnore]
	public static EvenOddTextReverserResult Empty => new(
		Text: "",
		CharsOccurenceInfo: new Dictionary<char, int>(),
		MaxSubstringWithVerbsOnBothSides: "",
		SortedResultText: "",
		CutText: ""
	);

	[JsonIgnore]
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
Sorted result text: {SortedResultText}
Cut text: {CutText}";
}

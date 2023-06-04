using maxim_tasks;
using maxim_tasks.Models;
using maxim_tasks.Services.EvenOddTextReverserService;
using maxim_tasks.Services.RandomNumberGeneratorService;
using maxim_tasks.Utils.Sorters;
using Moq;


namespace maxim_tasks_tests.Utils;

[TestFixture]
public class EvenOddTextReverserTests
{
	private Mock<IRandomNumberGeneratorService> _randomNumberGeneratorMock;
	private Mock<IStringSorter> _stringSorterMock;
	private IEvenOddTextReverserService _evenOddTextReverser;

	[SetUp]
	public void Setup()
	{
		_randomNumberGeneratorMock = new Mock<IRandomNumberGeneratorService>();

		_stringSorterMock = new Mock<IStringSorter>();
		_evenOddTextReverser = new EvenOddTextReverserService(_randomNumberGeneratorMock.Object);
	}

	[Test]
	[TestCase(null)]
	[TestCase("")]
	public async Task should_return_empty_result_if_text_is_null_or_empty(string text)
	{
		var result = await _evenOddTextReverser.ReverseText(text, _stringSorterMock.Object);

		Console.WriteLine(result);
		Console.WriteLine(EvenOddTextReverserResult.Empty);

		Assert.That(result, Is.EqualTo(EvenOddTextReverserResult.Empty));
	}

	[Test]
	public async Task should_work_if_text_length_is_odd()
	{
		var text = "abcdefg";
		var resultText = "gfedcbaabcdefg";

		var expectedCharOccurenceInfo = new Dictionary<char, int>
		{
			{'a', 2}, {'b', 2}, {'c', 2}, {'d', 2}, {'e', 2}, {'f', 2}, {'g', 2}
		};

		_randomNumberGeneratorMock
			.Setup(x => x.GetRandomNumber(0, 13))
			.ReturnsAsync(() => 2);

		_stringSorterMock
			.Setup(x => x.SortString(resultText))
			.Returns(() => "aabbccddeeffgg");

		var result = await _evenOddTextReverser.ReverseText(text, _stringSorterMock.Object);

		Assert.Multiple(() =>
		{
			Assert.That(result.Text, Is.EqualTo(resultText));
			Assert.That(result.CharsOccurenceInfo, Is.EqualTo(expectedCharOccurenceInfo));
			Assert.That(result.MaxSubstringWithVerbsOnBothSides, Is.EqualTo("edcbaabcde"));
			Assert.That(result.SortedResultText, Is.EqualTo("aabbccddeeffgg"));
			Assert.That(result.CutText, Is.EqualTo("gfdcbaabcdefg"));
		});
	}

	[Test]
	public async Task should_work_if_text_length_is_even()
	{
		var text = "abcdef";
		var resultText = "cbafed";

		var expectedCharOccurenceInfo = new Dictionary<char, int>
		{
			{'a', 1}, {'b', 1}, {'c', 1}, {'d', 1}, {'e', 1}, {'f', 1}
		};

		_randomNumberGeneratorMock
			.Setup(x => x.GetRandomNumber(0, 5))
			.ReturnsAsync(() => 2);

		_stringSorterMock
			.Setup(x => x.SortString(resultText))
			.Returns(() => "abcdef");

		var result = await _evenOddTextReverser.ReverseText(text, _stringSorterMock.Object);

		Assert.Multiple(() =>
		{
			Assert.That(result.Text, Is.EqualTo(resultText));
			Assert.That(result.CharsOccurenceInfo, Is.EqualTo(expectedCharOccurenceInfo));
			Assert.That(result.MaxSubstringWithVerbsOnBothSides, Is.EqualTo("afe"));
			Assert.That(result.SortedResultText, Is.EqualTo("abcdef"));
			Assert.That(result.CutText, Is.EqualTo("cbfed"));
		});
	}
}

using maxim_tasks.Utils.Sorters;

namespace maxim_tasks_tests.Utils.Sorters;

[TestFixtureSource(nameof(sorters))]
internal class TestSorters
{
	static readonly IStringSorter[] sorters = new IStringSorter[]
	{
		new QuickSorter(),
		new TreeSorter()
	};


	private readonly IStringSorter _sorter;

	public TestSorters(IStringSorter sorter)
	{
		_sorter = sorter;
	}

	[Test]
	[TestCase(null)]
	public void should_throw_error_on_null(string text)
	{
		Assert.Throws<ArgumentNullException>(() =>
		{
			_sorter.SortString(text);
		});
	}

	[Test]
	[TestCase("   ")]
	[TestCase("")]
	public void should_return_same_text_on_empty_or_whitespace(string text)
	{
		Assert.That(text, Is.SameAs(_sorter.SortString(text)));
	}

	[Test]
	[TestCase("abobus123TYU")]
	[TestCase("ab")]
	[TestCase("a")]
	public void should_return_sorted_text(string text)
	{
		var sortedText = _sorter.SortString(text);
		
		var trueSortedText = string.Join("", text.OrderBy(ch => ch));

		Assert.That(sortedText, Is.EqualTo(trueSortedText));
	}
}

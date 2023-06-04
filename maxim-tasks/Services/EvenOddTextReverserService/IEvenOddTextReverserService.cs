using maxim_tasks.Models;
using maxim_tasks.Utils.Sorters;

namespace maxim_tasks.Services.EvenOddTextReverserService;

public interface IEvenOddTextReverserService
{
	public Task<EvenOddTextReverserResult> ReverseText(string? text, IStringSorter? sorter);
}

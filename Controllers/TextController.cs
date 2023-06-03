using maxim_tasks.Models.Queries;
using maxim_tasks.Utils.Sorters;
using Microsoft.AspNetCore.Mvc;

namespace maxim_tasks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TextController : ControllerBase
{
	private readonly EvenOddTextReverser _evenOddTextReverser;

	public TextController(EvenOddTextReverser evenOddTextReverser)
	{
		_evenOddTextReverser = evenOddTextReverser;
	}

	[HttpGet("even_odd_reverse")]
	public async Task<IActionResult> ReverseText([FromQuery] ReverseTextQuery query)
	{
		IStringSorter sorter = query.Sorter switch
		{
			'q' => new QuickSorter(),
			't' => new TreeSorter(),
			_ => new QuickSorter()
		};

		try
		{
			var result = await _evenOddTextReverser.ReverseText(query.Text, sorter);
			return new JsonResult(result);
		}
		catch(RequireEnglishLowercaseException ex)
		{
			return BadRequest(ex.Message);
		}
		catch
		{
			return BadRequest();
		}
	}
}

using maxim_tasks.Models;
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

	/// <response code="200">OK</response>
	/// <response code="400">Text has non-english or non-lowered characters</response>
	/// <response code="500">Unknown error</response>
	[HttpGet("even_odd_reverse")]
	public async Task<ActionResult<EvenOddTextReverserResult>> EvenOddReverse([FromQuery] ReverseTextQuery query)
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
		catch (RequireEnglishLowercaseException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return StatusCode(500, "Unknown error");
		}
	}
}

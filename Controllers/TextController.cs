using maxim_tasks.Models;
using maxim_tasks.Models.Queries;
using maxim_tasks.Utils.Sorters;
using Microsoft.AspNetCore.Mvc;
namespace maxim_tasks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TextController : ControllerBase
{
	private readonly IConfiguration _config;
	private readonly EvenOddTextReverser _evenOddTextReverser;

	private readonly SemaphoreSlim _requestsLimitSemaphore;

	public TextController(IConfiguration config, EvenOddTextReverser evenOddTextReverser)
	{
		_config = config;
		_evenOddTextReverser = evenOddTextReverser;

		_requestsLimitSemaphore = new SemaphoreSlim(_config.GetValue<int>("AppConfig:Settings:ParallelLimit"));
	}

	/// <response code="200">OK</response>
	/// <response code="400">Text has non-english or non-lowered characters or blacklisted</response>
	/// <response code="500">Unknown error</response>
	/// <response code="503">Service is unavailable</response>
	[HttpGet("even_odd_reverse")]
	public async Task<ActionResult<EvenOddTextReverserResult>> EvenOddReverse([FromQuery] ReverseTextQuery query)
	{
		if (!_requestsLimitSemaphore.Wait(0))
		{
			return StatusCode(503, "Service is unavailable");
		}

		var blackList = _config.GetSection("AppConfig:Settings:BlackList").Get<List<string>>();
		if (blackList.Contains(query.Text) == true)
		{
			return BadRequest($"{query.Text} is blacklisted");
		}

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
		finally
		{
			_requestsLimitSemaphore.Release();
		}
	}
}

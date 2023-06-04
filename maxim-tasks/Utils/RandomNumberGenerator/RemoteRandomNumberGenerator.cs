using maxim_tasks.Models.Responses;
using System.Text;

namespace maxim_tasks.Utils.RandomNumberGenerator;

public static class RemoteRandomNumberGenerator
{
	private static readonly HttpClient _httpClient = new();

	public static async Task<int> GetAsync(IConfiguration config, int min, int max)
	{
		var json =
@$"
{{
    ""jsonrpc"": ""2.0"",
    ""method"": ""generateIntegers"",
    ""params"": {{
        ""apiKey"": ""{config["AppConfig:RandomOrgApiKey"]}"",
        ""n"": 1,
        ""min"": {min},
        ""max"": {max},
        ""replacement"": true
    }},
    ""id"": 9999
}}
".Trim();

		var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

		var baseAddress = config["AppConfig:RandomOrgLink"];

		var randomNumRequest = await _httpClient
			.PostAsync($"{baseAddress}/json-rpc/1/invoke", jsonContent);

		randomNumRequest.EnsureSuccessStatusCode();

		var randomNumParsedResponse = await randomNumRequest.Content.ReadFromJsonAsync<RandomNumberResponse>();

		var randomNum = randomNumParsedResponse.Result.Random.Data[0];

		return randomNum;
	}
}



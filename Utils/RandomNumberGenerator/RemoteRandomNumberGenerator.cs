using maxim_tasks.Models.Responses;
using System.Text;

namespace maxim_tasks.Utils.RandomNumberGenerator;

public static class RemoteRandomNumberGenerator
{
	private const string BASE_ADDRESS = "https://api.random.org/";
	private const string API_KEY = "f7bf36eb-dbac-47f3-b4fb-9ae1b2f452e7";


	private static readonly HttpClient _httpClient = new()
	{
		BaseAddress = new Uri(BASE_ADDRESS)
	};

	public static async ValueTask<int> GetAsync(int min, int max)
	{
		var json =
@$"
{{
    ""jsonrpc"": ""2.0"",
    ""method"": ""generateIntegers"",
    ""params"": {{
        ""apiKey"": ""{API_KEY}"",
        ""n"": 1,
        ""min"": {min},
        ""max"": {max},
        ""replacement"": true
    }},
    ""id"": 9999
}}
".Trim();

		var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

		var randomNumRequest = await _httpClient
			.PostAsync($"json-rpc/1/invoke", jsonContent);

		randomNumRequest.EnsureSuccessStatusCode();

		var randomNumParsedResponse = await randomNumRequest.Content.ReadFromJsonAsync<RandomNumberResponse>();

		var randomNum = randomNumParsedResponse.Result.Random.Data[0];

		return randomNum;
	}
}



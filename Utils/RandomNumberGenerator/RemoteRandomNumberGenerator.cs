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

		var randomNum = randomNumParsedResponse.result.random.data[0];

		return randomNum;
	}
}


// Classes for json parsing
internal sealed record RandomNumberResponse(
	ResultPartResponse result
);

internal sealed record ResultPartResponse(
	RandomPartResponse random
);

internal sealed record RandomPartResponse(
	int[] data
);
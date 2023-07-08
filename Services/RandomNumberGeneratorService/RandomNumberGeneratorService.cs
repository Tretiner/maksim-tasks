using maxim_tasks.Utils.RandomNumberGenerator;

namespace maxim_tasks.Services.RandomNumberGeneratorService;

public class RandomNumberGeneratorService: IRandomNumberGeneratorService
{
    private readonly IConfiguration _config;

	public RandomNumberGeneratorService(IConfiguration config)
	{
		_config = config;
	}

	public async ValueTask<int> GetRandomNumber(int min, int max)
    {
        try
        {
            return await RemoteRandomNumberGenerator.GetAsync(_config, min, max);
        } 
        catch
        {
            return LocalRandomNumberGenerator.Get(min, max);
        }
    }
}

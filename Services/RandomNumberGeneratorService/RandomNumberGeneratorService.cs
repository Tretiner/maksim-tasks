using maxim_tasks.Utils.RandomNumberGenerator;

namespace maxim_tasks.Services.RandomNumberGeneratorService;

public class RandomNumberGeneratorService: IRandomNumberGeneratorService
{
    public async ValueTask<int> GetRandomNumber(int min, int max)
    {
        try
        {
            return await RemoteRandomNumberGenerator.GetAsync(min, max);
        } 
        catch
        {
            return LocalRandomNumberGenerator.Get(min, max);
        }
    }
}

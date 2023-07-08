namespace maxim_tasks.Services.RandomNumberGeneratorService;

public interface IRandomNumberGeneratorService
{
	public ValueTask<int> GetRandomNumber(int min, int max);
}

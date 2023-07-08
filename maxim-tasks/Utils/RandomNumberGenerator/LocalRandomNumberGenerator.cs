namespace maxim_tasks.Utils.RandomNumberGenerator;

public static class LocalRandomNumberGenerator
{
    public static int Get(int min, int max)
	{
		return Random.Shared.Next(min, max);
	}
}
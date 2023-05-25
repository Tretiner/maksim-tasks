namespace maxim_tasks;

public static class Extensions
{
    public static string Reverse(this string text)
    {
        var chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

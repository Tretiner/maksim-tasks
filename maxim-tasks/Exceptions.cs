namespace maxim_tasks;

public sealed class RequireEnglishLowercaseException : Exception
{
    public RequireEnglishLowercaseException(string? message) : base(message){}
}

namespace FestivalTicketsApp.Shared;

public class RequiredDataNotFoundException : Exception
{
    public RequiredDataNotFoundException()
    {
    }

    public RequiredDataNotFoundException(string message)
        : base(message)
    {
    }

    public RequiredDataNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
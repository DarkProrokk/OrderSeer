namespace Domain.Exceptions;

public class WrongStatusException: ArgumentException
{
    public WrongStatusException(string message) : base(message)
    {
    }
}
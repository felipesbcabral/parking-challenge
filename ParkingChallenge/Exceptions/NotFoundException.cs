namespace ParkingChallenge.Exceptions;

public class NotFoundException(string? message) : ApplicationException(message)
{
    public static void ThrowIfNull(
        object? @object,
        string exceptionMessage)
    {
        if (@object == null)
        {
            throw new NotFoundException(exceptionMessage);
        }
    }
}
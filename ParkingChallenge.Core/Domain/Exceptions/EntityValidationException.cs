namespace ParkingChallenge.Core.Domain.Exceptions;
public class EntityValidationException(string message) : Exception(message)
{
}

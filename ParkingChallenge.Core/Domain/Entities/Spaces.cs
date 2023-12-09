namespace ParkingChallenge.Core.Domain.Entities;
public class Spaces
{
    public Spaces(int free = 0)
    {
        Free = free;
    }

    public int Occupied { get; set; }

    public int Free { get; set; }

    public int Total => Occupied + Free;

}

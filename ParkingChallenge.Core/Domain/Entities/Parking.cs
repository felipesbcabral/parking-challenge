namespace ParkingChallenge.Core.Domain.Entities;
public class Parking : Entity
{
    public Parking(Spaces carSpaces, Spaces vanSpaces, Spaces motorcyclesSpaces)
    {
        CarSpaces = carSpaces;
        VanSpaces = vanSpaces;
        MotorcyclesSpaces = motorcyclesSpaces;
        Initialize();

    }
    public bool CarsFull => CalculateCarSpace();
    public bool MotorcuclesFull => CalculateMotorcycleSpace();
    public bool VansFull => CalculateVanSpace();

    public int TotalSpaces => CalculateTotalSpaces();

    public int RemainingSpaces => CalculateFreeSpaces();

    public bool IsFull => RemainingSpaces == 0;

    public bool IsEmpty => TotalSpaces == RemainingSpaces;

    public Spaces CarSpaces { get; set; }
    public Spaces VanSpaces { get; set; }
    public Spaces MotorcyclesSpaces { get; set; }

    public int CalculateTotalSpaces()
        => CarSpaces.Total + VanSpaces.Total + MotorcyclesSpaces.Total;

    public int CalculateFreeSpaces()
        => CarSpaces.Free + VanSpaces.Free + MotorcyclesSpaces.Free;

    public bool CalculateCarSpace()
    => CarSpaces.Occupied == CarSpaces.Total;

    public bool CalculateMotorcycleSpace()
    => MotorcyclesSpaces.Occupied == CarSpaces.Total;

    public bool CalculateVanSpace()
    => VanSpaces.Occupied == CarSpaces.Total;
}


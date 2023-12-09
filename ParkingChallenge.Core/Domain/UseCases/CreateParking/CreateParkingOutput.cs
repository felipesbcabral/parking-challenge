using ParkingChallenge.Core.Domain.Entities;

namespace ParkingChallenge.Core.Domain.UseCases.CreateParking;
public class CreateParkingOutput
{
    public CreateParkingOutput(Spaces carSpaces, Spaces vanSpaces, Spaces motorcyclesSpaces)
    {
        CarSpaces = carSpaces;
        VanSpaces = vanSpaces;
        MotorcyclesSpaces = motorcyclesSpaces;

    }

    public string? Id { get; set; }

    public int TotalSpaces { get; set; }

    public int RemainingSpaces { get; set; }

    public bool IsFull;


    public bool IsEmpty;

    public bool CarsFull { get; set; }

    public bool MotorcyclesFull { get; set; }

    public bool VansFull { get; set; }

    public Spaces CarSpaces { get; set; }
    public Spaces VanSpaces { get; set; }
    public Spaces MotorcyclesSpaces { get; set; }
}

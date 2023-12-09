using ParkingChallenge.Core.Domain.Entities;

namespace ParkingChallenge.Core.Domain.UseCases.GetParking;
public class GetParkingOutput
{
    public GetParkingOutput(List<ParkingData> data)
    {
        Data = data;
    }
    public List<ParkingData> Data { get; set; }

    public class ParkingData
    {
        public string? Id { get; set; }

        public int TotalSpaces { get; set; }

        public int RemainingSpaces { get; set; }

        public bool IsFull { get; set; }

        public bool IsEmpty { get; set; }

        public bool CarsFull { get; set; }

        public bool MotorcyclesFull { get; set; }

        public bool VansFull { get; set; }

        public Spaces? CarSpaces { get; set; }
        public Spaces? VanSpaces { get; set; }
        public Spaces? MotorcyclesSpaces { get; set; }
    }
}

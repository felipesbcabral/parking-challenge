using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ParkingChallenge.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingChallenge.Models;

public class Parking
{
    private const int MaxTotalSpaces = 250;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [Range(0, MaxTotalSpaces, ErrorMessage = "The total number of vacancies must be between 0 and 250.")]
    public int TotalSpaces { get; set; }

    public int RemainingSpaces { get; set; }

    public bool IsFull => RemainingSpaces == 0;

    public bool IsEmpty => RemainingSpaces == TotalSpaces;

    public bool MotorcyclesFull => VehicleAvailability.MotorcycleSpaces == 0;

    public int VanOccupancy => VehicleAvailability.VanRegularSpaces + VehicleAvailability.VanLargeSpaces;

    public ParkingStatusEnum ParkingStatus { get; set; }

    public VehicleAvailability VehicleAvailability { get; set; } = new VehicleAvailability();

    public Parking(
        string id,
        int totalSpaces,
        int remainingSpaces,
        ParkingStatusEnum parkingStatus,
        VehicleAvailability vehicleAvailability)
    {
        Id = id;
        TotalSpaces = totalSpaces > MaxTotalSpaces ? MaxTotalSpaces : totalSpaces;
        RemainingSpaces = remainingSpaces;
        ParkingStatus = parkingStatus;
        VehicleAvailability = vehicleAvailability;
    }
}
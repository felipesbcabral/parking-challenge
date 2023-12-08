namespace ParkingChallenge.Configurations;

public class ParkingDatabaseConfiguration
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ParkingCollectionName { get; set; } = null!;
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkingChallenge.Configurations;
using ParkingChallenge.Models;

namespace ParkingChallenge.Services;

public class ParkingContext
{
    public IMongoCollection<Parking> Collection => _parkingCollection;

    private readonly IMongoCollection<Parking> _parkingCollection;

    public ParkingContext(IOptions<ParkingDatabaseConfiguration> parkingConfiguration)
    {
        var mongoClient = new MongoClient(parkingConfiguration.Value.ConnectionString);
        var database = mongoClient.GetDatabase(parkingConfiguration.Value.DatabaseName);
        _parkingCollection = database.GetCollection<Parking>(parkingConfiguration.Value.ParkingCollectionName);
    }
}
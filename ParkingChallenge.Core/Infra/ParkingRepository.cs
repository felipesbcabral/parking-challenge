using MongoDB.Driver;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;

namespace ParkingChallenge.Core.Infra;
public class ParkingRepository : Repository<Parking>, IParkingRepository
{
    private readonly IMongoCollection<Parking> _collection;

    public ParkingRepository(IMongoDatabase database, string collectionName) : base(database, "Parking")
    {
        _collection = database.GetCollection<Parking>(collectionName);
    }

    public async Task<IEnumerable<Parking>> GetParking()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Parking> GetParkingById(string id)
    {
        var filter = Builders<Parking>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateParking(Parking parking)
    {
        await _collection.InsertOneAsync(parking);
    }

    public async Task UpdateParking(Parking parking)
    {
        var filter = Builders<Parking>.Filter.Eq(x => x.Id, parking.Id);
        await _collection.ReplaceOneAsync(filter, parking);
    }

    public async Task DeleteParking(string id)
    {
        var filter = Builders<Parking>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter);
    }
}

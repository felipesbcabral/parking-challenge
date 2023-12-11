using MongoDB.Bson;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.UnitTests.Common;
using DomainEntity = ParkingChallenge.Core.Domain.Entities;
namespace ParkingChallenge.UnitTests.Domain.Entities.Parking;

[CollectionDefinition(nameof(ParkingTestFixture))]
public class ParkingTestFixtureCollection
    : ICollectionFixture<ParkingTestFixture>
{ }

public class ParkingTestFixture : BaseFixture
{
    public ParkingTestFixture() : base() { }

    public DomainEntity.Parking GetValidParking()
    {
        var carSpaces = GetValidSpaces();
        var vanSpaces = GetValidSpaces();
        var motorcycleSpaces = GetValidSpaces();

        return new DomainEntity.Parking(carSpaces, vanSpaces, motorcycleSpaces)
        {
            Id = ObjectId.GenerateNewId().ToString()
        };
    }

    public Spaces GetValidSpaces()
    {
        var total = Faker.Random.Int(10, 20);
        var occupied = Faker.Random.Int(0, total);
        var free = total - occupied;

        return new Spaces { Occupied = occupied, Free = free };
    }
}

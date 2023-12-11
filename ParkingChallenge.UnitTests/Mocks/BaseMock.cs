using ParkingChallenge.Core.Domain.Entities;

namespace ParkingChallenge.UnitTests.Mocks;
public class BaseMock
{
    public static Parking BuildParking(
        int carFree = 0, int carOccupied = 0,
        int vanFree = 0, int vanOccupied = 0,
        int motorcycleFree = 0, int motorcycleOccupied = 0)
    {
        var carSpaces = new Spaces { Free = carFree, Occupied = carOccupied };
        var vanSpaces = new Spaces { Free = vanFree, Occupied = vanOccupied };
        var motorcycleSpaces = new Spaces { Free = motorcycleFree, Occupied = motorcycleOccupied };

        return new Parking(carSpaces, vanSpaces, motorcycleSpaces);
    }
}


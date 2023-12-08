
using ParkingChallenge.Models;

namespace ParkingChallenge.Repositories;

public interface IParkingRepository
{
    Task<Parking> GetParking(string parkingId);

    Task CreateParking(Parking parking);
}

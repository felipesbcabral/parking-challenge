using ParkingChallenge.Core.Domain.Entities;

namespace ParkingChallenge.Core.Domain.Interfaces.Repositories;
public interface IParkingRepository
{
    Task<IEnumerable<Parking>> GetParking();
    Task<Parking> GetParkingById(string? parkingId);

    Task CreateParking(Parking parking);

    Task UpdateParking(Parking parking);

    Task DeleteParking(string parkingId);
}

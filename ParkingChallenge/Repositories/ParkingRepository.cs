using MongoDB.Driver;
using ParkingChallenge.Exceptions;
using ParkingChallenge.Models;
using ParkingChallenge.Services;

namespace ParkingChallenge.Repositories;

public class ParkingRepository : IParkingRepository
{
    private readonly ParkingContext _context;

    public ParkingRepository(ParkingContext context)
    {
        _context = context;
    }

    public async Task<Parking> GetParking(string parkingId)
    {
        try
        {
            var parking = await _context.Collection.Find(x => x.Id == parkingId)
                                                     .FirstOrDefaultAsync();

            NotFoundException.ThrowIfNull(parking, $"Parking '{parkingId}' not found.");
            return parking!;
        }
        catch (Exception ex)
        {
            // Adicionar lógica de tratamento de exceção, como logging
            throw new Exception("An error occurred while fetching parking.", ex);
        }
    }

    public async Task CreateParking(Parking parking)
    {
        try
        {
            await _context.Collection.InsertOneAsync(parking);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while inserting parking.", ex);
        }
    }
}

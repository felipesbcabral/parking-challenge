using Microsoft.AspNetCore.Mvc;
using ParkingChallenge.Entity.Enums;
using ParkingChallenge.Models;
using ParkingChallenge.Models.Response;
using ParkingChallenge.Repositories;

namespace ParkingChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkingController : ControllerBase
{
    private readonly IParkingRepository _parkingRepository;

    public ParkingController(IParkingRepository parkingRepository)
        => _parkingRepository = parkingRepository;

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Parking>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var output = await _parkingRepository.GetParking(id);

        return Ok(output);
    }

    [HttpPost]
    public async Task<IActionResult> CreateParking([FromBody] Parking request)
    {
        try
        {
            var parking = new Parking(
                id: string.Empty,
                totalSpaces: request.TotalSpaces,
                remainingSpaces: request.TotalSpaces,
                parkingStatus: ParkingStatusEnum.Empty,
                vehicleAvailability: new VehicleAvailability()
                {
                    CarRegularSpaces = request.VehicleAvailability.CarRegularSpaces,
                    CarLargeSpaces = request.VehicleAvailability.CarLargeSpaces,
                    MotorcycleSpaces = request.VehicleAvailability.MotorcycleSpaces,
                    VanRegularSpaces = request.VehicleAvailability.VanRegularSpaces,
                    VanLargeSpaces = request.VehicleAvailability.VanLargeSpaces
                }
            );

            await _parkingRepository.CreateParking(parking);

            return Created($"api/parkings/{parking.Id}", null);
        }
        catch (Exception ex)
        {
            // Log do erro
            return BadRequest($"Error creating parking: {ex.Message}");
        }
    }
}

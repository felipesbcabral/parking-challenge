using FluentValidation.Results;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.DeleteParking;
public class DeleteParkingUseCase : IRequestHandler<DeleteParkingInput, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;

    public DeleteParkingUseCase(IParkingRepository parkingRepository)
    {
        _parkingRepository = parkingRepository;
    }

    public async Task<ResponseUseCase> Handle(DeleteParkingInput request)
    {
        var existingParking = await _parkingRepository.GetParkingById(request.Id);

        if (existingParking == null)
        {
            return ResponseUseCase.NotFound("Parking not found");
        }

        try
        {
            await _parkingRepository.DeleteParking(existingParking.Id!);

            return ResponseUseCase.Ok("Parking deleted successfully");
        }
        catch (Exception ex)
        {
            var validationErrors = new List<ValidationFailure>
            {
                new("Parking", $"Error deleting the parking: {ex.Message}")
            };

            return ResponseUseCase.BadRequest(validationErrors);
        }
    }

}


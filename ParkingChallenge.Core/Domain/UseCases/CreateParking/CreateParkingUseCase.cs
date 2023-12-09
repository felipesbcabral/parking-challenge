using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.CreateParking;
public class CreateParkingUseCase : IRequestHandler<CreateParkingInput, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;


    public CreateParkingUseCase(IParkingRepository parkingRepository)
    {
        _parkingRepository = parkingRepository;
    }

    public async Task<ResponseUseCase> Handle(CreateParkingInput request)
    {
        var newParking = CreateParkingFromInput(request);
        await _parkingRepository.CreateParking(newParking);

        var output = CreateParkingOutput(newParking);
        return ResponseUseCase.Ok(output);
    }

    private Parking CreateParkingFromInput(CreateParkingInput request)
    {
        return new Parking(
            new(request.MotorcycleSpaces),
            new(request.CarSpaces),
            new(request.VanSpaces));
    }

    private CreateParkingOutput CreateParkingOutput(Parking newParking)
    {
        return new CreateParkingOutput(
            newParking.CarSpaces,
            newParking.VanSpaces,
            newParking.MotorcyclesSpaces)
        {
            Id = newParking.Id!,
            TotalSpaces = newParking.TotalSpaces,
            RemainingSpaces = newParking.RemainingSpaces,
            IsFull = newParking.IsFull,
            IsEmpty = newParking.IsEmpty,
            CarsFull = newParking.CarsFull,
            MotorcyclesFull = newParking.MotorcuclesFull,
            VansFull = newParking.VansFull
        };
    }
}


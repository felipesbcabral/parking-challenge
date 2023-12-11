using FluentValidation;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.CreateParking;
public class CreateParkingUseCase : IRequestHandler<CreateParkingInput, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;
    private readonly IValidator<CreateParkingInput> _validator;

    public CreateParkingUseCase(
        IParkingRepository parkingRepository,
        IValidator<CreateParkingInput> validator)
    {
        _parkingRepository = parkingRepository;
        _validator = validator;
    }

    public async Task<ResponseUseCase> Handle(CreateParkingInput request)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return ResponseUseCase.BadRequest(validationResult.Errors);
        }

        var newParking = CreateParkingFromInput(request);

        await _parkingRepository.CreateParking(newParking);

        var output = CreateParkingOutput(newParking);
        return ResponseUseCase.Created(output);
    }

    private Parking CreateParkingFromInput(CreateParkingInput request)
        => new(
            new(request.MotorcycleSpaces),
            new(request.CarSpaces),
            new(request.VanSpaces));

    private CreateParkingOutput CreateParkingOutput(Parking newParking)
        => new(
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
            MotorcyclesFull = newParking.MotorcyclesFull,
            VansFull = newParking.VansFull
        };
}

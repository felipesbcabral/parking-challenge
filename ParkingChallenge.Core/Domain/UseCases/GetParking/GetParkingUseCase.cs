using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.GetParking;
public class GetParkingUseCase : IRequestHandler<GetParkingInput, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;

    public GetParkingUseCase(IParkingRepository parkingRepository)
        => _parkingRepository = parkingRepository;

    public async Task<ResponseUseCase> Handle(GetParkingInput request)
    {
        var parking = await _parkingRepository.GetParking();

        var parkingData = parking.Select(x =>
            new GetParkingOutput.ParkingData
            {
                CarSpaces = x.CarSpaces,
                VanSpaces = x.VanSpaces,
                MotorcyclesSpaces = x.MotorcyclesSpaces,
                Id = x.Id!,
                TotalSpaces = x.TotalSpaces,
                RemainingSpaces = x.RemainingSpaces,
                IsFull = x.IsFull,
                IsEmpty = x.IsEmpty,
                CarsFull = x.CarsFull,
                MotorcyclesFull = x.MotorcuclesFull,
                VansFull = x.VansFull
            }).ToList();

        var output = new GetParkingOutput(parkingData);

        return ResponseUseCase.Ok(output);
    }
}


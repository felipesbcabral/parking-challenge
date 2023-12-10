using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;
using ParkingChallenge.Core.Domain.UseCases.GetParking;

namespace ParkingChallenge.Core.Domain.UseCases.GetParkingById;
public class GetParkingByIdUseCase : IRequestHandler<GetParkingInputById, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;

    public GetParkingByIdUseCase(IParkingRepository parkingRepository)
        => _parkingRepository = parkingRepository;

    public async Task<ResponseUseCase> Handle(GetParkingInputById request)
    {
        var parkingId = request.Id;
        var parking = await _parkingRepository.GetParkingById(parkingId);

        if (parking == null)
        {
            return ResponseUseCase.NotFound("Parking not found");
        }

        var parkingData = new GetParkingOutput.ParkingData
        {
            CarSpaces = parking.CarSpaces,
            VanSpaces = parking.VanSpaces,
            MotorcyclesSpaces = parking.MotorcyclesSpaces,
            Id = parking.Id!,
            TotalSpaces = parking.TotalSpaces,
            RemainingSpaces = parking.RemainingSpaces,
            IsFull = parking.IsFull,
            IsEmpty = parking.IsEmpty,
            CarsFull = parking.CarsFull,
            MotorcyclesFull = parking.MotorcyclesFull,
            VansFull = parking.VansFull
        };

        var output = new GetParkingOutput([parkingData]);

        return ResponseUseCase.Ok(output);
    }
}

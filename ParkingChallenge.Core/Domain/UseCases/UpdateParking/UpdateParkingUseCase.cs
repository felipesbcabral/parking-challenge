using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.UpdateParking;
public class UpdateParkingUseCase : IRequestHandler<UpdateParkingInput, ResponseUseCase>
{
    private readonly IParkingRepository _parkingRepository;

    public UpdateParkingUseCase(IParkingRepository parkingRepository)
    {
        _parkingRepository = parkingRepository;
    }

    public async Task<ResponseUseCase> Handle(UpdateParkingInput request)
    {
        var existingParking = await _parkingRepository.GetParkingById(request.ParkingId);

        if (existingParking == null)
        {
            return ResponseUseCase.NotFound("Estacionamento não encontrado");
        }

        existingParking.MotorcyclesSpaces = request.MotorcycleSpaces;
        existingParking.CarSpaces = request.CarSpaces;
        existingParking.VanSpaces = request.VanSpaces;

        try
        {
            await _parkingRepository.UpdateParking(existingParking);
            return ResponseUseCase.Ok("Estacionamento atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return ResponseUseCase.NotFound($"Erro ao atualizar o estacionamento: {ex.Message}");
        }
    }

}

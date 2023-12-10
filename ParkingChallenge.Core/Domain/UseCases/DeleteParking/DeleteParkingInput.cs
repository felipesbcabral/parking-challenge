using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.DeleteParking;
public class DeleteParkingInput : IRequest<ResponseUseCase>
{
    public string? Id { get; set; }
}

using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.GetParkingById;
public class GetParkingInputById : IRequest<ResponseUseCase>
{
    public string? Id { get; set; }

    public GetParkingInputById(string? id)
        => Id = id;
}

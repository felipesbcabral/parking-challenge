using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.UpdateParking;
public class UpdateParkingInput : IRequest<ResponseUseCase>
{
    public string? ParkingId { get; set; }
    public Spaces MotorcycleSpaces { get; set; }
    public Spaces CarSpaces { get; set; }
    public Spaces VanSpaces { get; set; }
}

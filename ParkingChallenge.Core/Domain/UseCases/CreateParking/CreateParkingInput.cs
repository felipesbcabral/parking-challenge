using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.CreateParking;
public class CreateParkingInput : IRequest<ResponseUseCase>
{
    public int MotorcycleSpaces { get; set; }

    public int CarSpaces { get; set; }

    public int VanSpaces { get; set; }
}

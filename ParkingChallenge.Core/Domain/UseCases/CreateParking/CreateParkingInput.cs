using FluentValidation;
using ParkingChallenge.Core.Domain.Interfaces.Requests;

namespace ParkingChallenge.Core.Domain.UseCases.CreateParking;
public class CreateParkingInput : IRequest<ResponseUseCase>
{
    public int MotorcycleSpaces { get; set; }

    public int CarSpaces { get; set; }

    public int VanSpaces { get; set; }

    public class CreateParkingValidation : AbstractValidator<CreateParkingInput>
    {
        public CreateParkingValidation()
        {
            RuleFor(x => x.MotorcycleSpaces)
                .GreaterThan(0).WithMessage("Motorcycle spaces must be greater than zero");

            RuleFor(x => x.CarSpaces)
                .GreaterThan(0).WithMessage("Car spaces must be greater than zero");

            RuleFor(x => x.VanSpaces)
                .GreaterThan(0).WithMessage("Van spaces must be greater than zero");
        }
    }
}

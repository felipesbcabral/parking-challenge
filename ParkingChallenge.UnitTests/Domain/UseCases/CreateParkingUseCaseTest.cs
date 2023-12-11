using FluentValidation;
using Moq;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.UseCases.CreateParking;
using ParkingChallenge.UnitTests.Mocks;
using System.Net;
using static ParkingChallenge.Core.Domain.UseCases.CreateParking.CreateParkingInput;

namespace ParkingChallenge.UnitTests.Domain.UseCases;
public class CreateParkingUseCaseTest
{
    private readonly Mock<IParkingRepository> _parkingRepository;
    private readonly IValidator<CreateParkingInput> _validator;

    public CreateParkingUseCaseTest()
    {
        _parkingRepository = new Mock<IParkingRepository>();
        _validator = new CreateParkingValidation();
    }

    [Fact]
    public async Task Given_ParkingInput_When_InputIsValid_Then_ExpectedCreated()
    {
        var createdParking = BaseMock.BuildParking(
            carFree: 20, carOccupied: 10,
            vanFree: 5, vanOccupied: 2,
            motorcycleFree: 10, motorcycleOccupied: 5);

        _parkingRepository.Setup(x => x.CreateParking(It.IsAny<Parking>()))
                         .Returns(Task.CompletedTask);

        var useCase = new CreateParkingUseCase(_parkingRepository.Object, _validator);

        var response = await useCase.Handle(BuildCreateParkingInput());

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        _parkingRepository.Verify(x => x.CreateParking(It.IsAny<Parking>()), Times.Once);
    }

    [Fact]
    public async void Given_Parking_When_InputIsInvalid_Then_Expected_BadRequest()
    {
        var useCase = new CreateParkingUseCase(_parkingRepository.Object, _validator);
        var response = await useCase.Handle(new CreateParkingInput());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _parkingRepository.Verify(x => x.GetParkingById(It.IsAny<string>()), Times.Never);
    }


    private CreateParkingInput BuildCreateParkingInput()
    {
        return new CreateParkingInput
        {
            MotorcycleSpaces = new Spaces { Free = 10, Occupied = 5 }.Total,
            CarSpaces = new Spaces { Free = 20, Occupied = 10 }.Total,
            VanSpaces = new Spaces { Free = 5, Occupied = 2 }.Total
        };
    }
}

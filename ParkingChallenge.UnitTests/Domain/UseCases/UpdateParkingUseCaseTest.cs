using Moq;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.UseCases.UpdateParking;
using ParkingChallenge.UnitTests.Mocks;
using System.Net;

namespace ParkingChallenge.UnitTests.Domain.UseCases;
public class UpdateParkingUseCaseTest
{
    private readonly Mock<IParkingRepository> _parkingRepository;

    public UpdateParkingUseCaseTest()
    {
        _parkingRepository = new Mock<IParkingRepository>();
    }

    [Fact]
    public async void Given_Parking_When_InputIsValid_Then_ExpectedOk()
    {
        var existingParking = BaseMock.BuildParking(carOccupied: 1, vanOccupied: 1, motorcycleOccupied: 1);
        _parkingRepository.Setup(x => x.GetParkingById(It.IsAny<string>()))
                         .Returns(Task.FromResult(existingParking));

        _parkingRepository.Setup(x => x.UpdateParking(It.IsAny<Parking>()))
                         .Returns(Task.CompletedTask);

        var useCase = new UpdateParkingUseCase(_parkingRepository.Object);
        var response = await useCase.Handle(BuildUpdateParkingInput());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        _parkingRepository.Verify(x => x.GetParkingById(It.IsAny<string>()), Times.Once);
        _parkingRepository.Verify(x => x.UpdateParking(It.IsAny<Parking>()), Times.Once);
    }

    [Fact]
    public async void Given_Parking_When_InputIsInvalid_Then_ExpectedNotFound()
    {
        _parkingRepository.Setup(x => x.GetParkingById(It.IsAny<string>()))
                         .Returns(Task.FromResult<Parking>(null));

        var useCase = new UpdateParkingUseCase(_parkingRepository.Object);
        var response = await useCase.Handle(BuildUpdateParkingInput());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        _parkingRepository.Verify(x => x.GetParkingById(It.IsAny<string>()), Times.Once);

        _parkingRepository.Verify(x => x.UpdateParking(It.IsAny<Parking>()), Times.Never);
    }

    private UpdateParkingInput BuildUpdateParkingInput()
    {
        return new UpdateParkingInput
        {
            ParkingId = "647a91808b643cfecf0b1f38",
            MotorcycleSpaces = new Spaces { Free = 8, Occupied = 3 },
            CarSpaces = new Spaces { Free = 15, Occupied = 8 },
            VanSpaces = new Spaces { Free = 4, Occupied = 2 }
        };
    }
}

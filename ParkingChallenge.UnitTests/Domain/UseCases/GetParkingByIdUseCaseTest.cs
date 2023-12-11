using Moq;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.UseCases.GetParkingById;
using ParkingChallenge.UnitTests.Mocks;
using System.Net;

namespace ParkingChallenge.UnitTests.Domain.UseCases;
public class GetParkingByIdUseCaseTest
{
    private readonly Mock<IParkingRepository> _parkingRepository;
    private GetParkingInputById _input;

    public GetParkingByIdUseCaseTest()
    {
        _parkingRepository = new Mock<IParkingRepository>();
        _input = new GetParkingInputById();
    }

    [Fact]
    public async void Given_Parking_When_InputIsValid_Then_ExpectedOk()
    {
        var parkingId = "647a91808b643cfecf0b1f38";
        var parking = BaseMock.BuildParking(carOccupied: 1, vanOccupied: 1, motorcycleOccupied: 1);

        _parkingRepository
            .Setup(x => x.GetParkingById(It.IsAny<string>()))
            .Returns((string id) => Task.FromResult(id == parkingId ? parking : null));

        _input.Id = parkingId;

        var useCase = new GetParkingByIdUseCase(_parkingRepository.Object);
        var response = await useCase.Handle(_input);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _parkingRepository.Verify(x => x.GetParkingById(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async void Given_Parking_When_InputIsInvalid_Then_ExpectedNotFound()
    {
        _parkingRepository
            .Setup(x => x.GetParkingById(It.IsAny<string>()))
            .Returns(() => Task.FromResult<Parking>(null));

        _input.Id = "647a91808b643cfecf0b1f38";

        var useCase = new GetParkingByIdUseCase(_parkingRepository.Object);
        var response = await useCase.Handle(_input);

        _parkingRepository.Verify(x => x.GetParkingById(It.Is<string>(id => id == _input.Id)), Times.Once);

        _parkingRepository.VerifyNoOtherCalls();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

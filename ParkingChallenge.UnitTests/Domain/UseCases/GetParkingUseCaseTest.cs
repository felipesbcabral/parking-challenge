using Moq;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.UseCases.GetParking;
using ParkingChallenge.UnitTests.Mocks;
using System.Net;

namespace ParkingChallenge.UnitTests.Domain.UseCases;
public class GetParkingUseCaseTest
{
    private readonly Mock<IParkingRepository> _parkingRepository;
    private GetParkingInput _input;

    public GetParkingUseCaseTest()
    {
        _parkingRepository = new Mock<IParkingRepository>();
        _input = new GetParkingInput();
    }

    [Fact]
    public async void Given_Parking_When_InputIsValid_Then_ExpectedOk()
    {
        IEnumerable<Parking> allParkings = new List<Parking>()
            {
                BaseMock.BuildParking(carOccupied: 1, vanOccupied: 1, motorcycleOccupied: 1)
            };

        _parkingRepository
            .Setup(x => x.GetParking())
            .Returns(() => Task.FromResult(allParkings));

        var useCase = new GetParkingUseCase(_parkingRepository.Object);
        var response = await useCase.Handle(_input);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(((GetParkingOutput)response.Result).Data);
        _parkingRepository.Verify(x => x.GetParking(), Times.Once);
    }
}

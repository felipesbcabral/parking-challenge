using FluentAssertions;
using ParkingChallenge.Core.Domain.Entities;
using ParkingChallenge.Core.Domain.Exceptions;
using DomainEntity = ParkingChallenge.Core.Domain.Entities;

namespace ParkingChallenge.UnitTests.Domain.Entities.Parking;

[Collection(nameof(ParkingTestFixture))]
public class ParkingTest
{
    private readonly ParkingTestFixture _parkingTestFixture;

    public ParkingTest(ParkingTestFixture parkingTestFixture)
        => _parkingTestFixture = parkingTestFixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Parking - Entity")]
    public void Instantiate()
    {
        //Arrange
        var validParking = _parkingTestFixture.GetValidParking();

        var dateTimeBefore = DateTime.Now;

        //Act
        var parking = new DomainEntity.Parking(
            validParking.CarSpaces,
            validParking.VanSpaces,
            validParking.MotorcyclesSpaces
       );

        Thread.Sleep(1);

        var dateTimeAfter = DateTime.Now;
        //Assert
        parking.Should().NotBeNull();
        parking.CarSpaces.Should().Be(validParking.CarSpaces);
        parking.VanSpaces.Should().Be(validParking.VanSpaces);
        parking.MotorcyclesSpaces.Should().Be(validParking.MotorcyclesSpaces);
        parking.Id.Should().NotBe(validParking.Id);
        parking.CreatedAt.Should().NotBeSameDateAs(default);
        parking.CreatedAt.Should().BeAfter(dateTimeBefore);
        parking.CreatedAt.Should().BeBefore(dateTimeAfter);
    }

    [Theory(DisplayName = nameof(Instantiate_Error_When_Any_Of_The_Vehicles_Is_Zero))]
    [Trait("Domain", "Parking - Entity")]
    [InlineData(0, 10, 15)]
    [InlineData(5, 0, 20)]
    [InlineData(8, 12, 0)]
    public void Instantiate_Error_When_Any_Of_The_Vehicles_Is_Zero(int carSpaces, int vanSpaces, int motorcycleSpaces)
    {
        Action action =
            () => new DomainEntity.Parking(
                new Spaces(carSpaces),
                new Spaces(vanSpaces),
                new Spaces(motorcycleSpaces)
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("All types of vehicle spaces must be greater than zero.");
    }

}

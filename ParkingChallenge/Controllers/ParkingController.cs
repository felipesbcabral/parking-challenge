using Microsoft.AspNetCore.Mvc;
using ParkingChallenge.Core.Domain.Interfaces.Requests;
using ParkingChallenge.Core.Domain.UseCases;
using ParkingChallenge.Core.Domain.UseCases.CreateParking;
using ParkingChallenge.Core.Domain.UseCases.GetParking;
using ParkingChallenge.Core.Domain.UseCases.UpdateParking;
using ParkingChallenge.Extensions;

namespace ParkingChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkingController : ControllerBase
{
    private readonly IRequestHandler<GetParkingInput, ResponseUseCase> _getParking;
    private readonly IRequestHandler<CreateParkingInput, ResponseUseCase> _createParking;
    private readonly IRequestHandler<UpdateParkingInput, ResponseUseCase> _updateParking;

    public ParkingController(
        IRequestHandler<GetParkingInput, ResponseUseCase> getParking,
        IRequestHandler<CreateParkingInput, ResponseUseCase> createParking,
        IRequestHandler<UpdateParkingInput, ResponseUseCase> updateParking)
    {
        _getParking = getParking;
        _createParking = createParking;
        _updateParking = updateParking;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var input = new GetParkingInput();
        var useCase = await _getParking.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetParkingById(string id)
    {
        var input = new GetParkingInput();
        var useCase = await _getParking.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseUseCase))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateParkingInput input) =>
            this.ResponseFromUseCase(await _createParking.Handle(input));

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(string id, UpdateParkingInput input)
    {
        input.ParkingId = id;
        var useCase = await _updateParking.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }
}

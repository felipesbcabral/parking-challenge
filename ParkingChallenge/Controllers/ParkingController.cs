using Microsoft.AspNetCore.Mvc;
using ParkingChallenge.Core.Domain.Interfaces.Requests;
using ParkingChallenge.Core.Domain.UseCases;
using ParkingChallenge.Core.Domain.UseCases.CreateParking;
using ParkingChallenge.Core.Domain.UseCases.DeleteParking;
using ParkingChallenge.Core.Domain.UseCases.GetParking;
using ParkingChallenge.Core.Domain.UseCases.GetParkingById;
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
    private readonly IRequestHandler<GetParkingInputById, ResponseUseCase> _getParkingById;
    private readonly IRequestHandler<DeleteParkingInput, ResponseUseCase> _deleteParking;

    public ParkingController(
        IRequestHandler<GetParkingInput, ResponseUseCase> getParking,
        IRequestHandler<CreateParkingInput, ResponseUseCase> createParking,
        IRequestHandler<UpdateParkingInput, ResponseUseCase> updateParking,
        IRequestHandler<GetParkingInputById, ResponseUseCase> getParkingById,
        IRequestHandler<DeleteParkingInput, ResponseUseCase> deleteParking)
    {
        _getParking = getParking;
        _createParking = createParking;
        _updateParking = updateParking;
        _getParkingById = getParkingById;
        _deleteParking = deleteParking;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllParkings()
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
    public async Task<IActionResult> GetParkingById([FromRoute] string id)
    {
        var input = new GetParkingInputById { Id = id };
        var useCase = await _getParkingById.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseUseCase))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateParking(CreateParkingInput input) =>
            this.ResponseFromUseCase(await _createParking.Handle(input));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateParking([FromRoute] string id, [FromBody] UpdateParkingInput input)
    {
        input.ParkingId = id;
        var useCase = await _updateParking.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseUseCase))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseUseCase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseUseCase))]
    public async Task<IActionResult> DeleteParking([FromRoute] string id)
    {
        var input = new DeleteParkingInput { Id = id };
        var useCase = await _deleteParking.Handle(input);
        return this.ResponseFromUseCase(useCase);
    }
}

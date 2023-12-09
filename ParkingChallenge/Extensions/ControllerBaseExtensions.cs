using Microsoft.AspNetCore.Mvc;
using ParkingChallenge.Core.Domain.UseCases;

namespace ParkingChallenge.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult ResponseFromUseCase(this ControllerBase controllerBase, ResponseUseCase responseUseCase)
    {
        object apiResult = responseUseCase.IsSuccess switch
        {
            true => responseUseCase.Result,
            false => new
            {
                responseUseCase.Errors
            }
        };

        return controllerBase.StatusCode((int)responseUseCase.StatusCode, apiResult);
    }
}

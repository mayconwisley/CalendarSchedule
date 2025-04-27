using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class LoginController(IGetTokenService _getTokenService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Result))]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(LoginDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var token = await _getTokenService.Token(login);
        if (token.IsFailure)
        {
            switch (token.Error.Code)
            {
                case "NotFound":
                    var notfound = Result.Failure(Error.NotFound(token.Error.Message));
                    return NotFound(notfound);
                case "Validation":
                    var validation = Result.Failure(Error.Validation(token.Error.Message));
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, validation);
                default:
                    break;
            }
        }
        return Ok(token.Value);
    }
    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

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
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Error))]
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
                    return NotFound(token.Error);
                case "Validation":
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, token.Error);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, token.Error);
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

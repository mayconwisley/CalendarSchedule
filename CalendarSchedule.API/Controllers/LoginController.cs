using System.Net;
using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class LoginController(IGetTokenService _getTokenService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(LoginDto)}): {errorMessage}"));
            return BadRequest(error.Error);
        }

        var token = await _getTokenService.Token(loginDto);
        if (token.IsFailure)
        {
            return token.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(token.Error),
                HttpStatusCode.UnprocessableEntity => StatusCode(StatusCodes.Status422UnprocessableEntity, token.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, token.Error),
            };
        }
        return Ok(token.Value);
    }
    private string ErrorMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

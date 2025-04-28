using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class UserContactController(IUserContactService _userContactService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var userContactsDto = await _userContactService.GetAll(page, size, search);
        if (userContactsDto.IsFailure)
            return NotFound(userContactsDto.Error);

        var userContacts = userContactsDto.Value;

        return Ok(userContacts);
    }

    [HttpGet]
    [Route("UserContactByUserId/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> UserContactByUserId([FromQuery] int page = 1, [FromQuery] int size = 10, int userId = 0)
    {
        var userContactsDto = await _userContactService.GetByUserId(page, size, userId);
        if (userContactsDto.IsFailure)
            return NotFound(userContactsDto.Error);

        var userContacts = userContactsDto.Value;
        return Ok(userContacts);
    }

    [HttpGet("{id:int}", Name = "GetUserContactId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        var userContactDto = await _userContactService.GetById(id);
        if (userContactDto.IsFailure)
            return NotFound(userContactDto.Error);

        return Ok(userContactDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] UserContactCreateDto userContactCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserContactCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var userContactDto = await _userContactService.Create(userContactCreateDto);
        if (userContactDto.IsFailure)
        {
            return userContactDto.Error.Code switch
            {
                "NotFound" => NotFound(userContactDto.Error),
                "BadRequest" => BadRequest(userContactDto.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, userContactDto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, userContactDto.Error) // fallback para erro desconhecido
            };
        }

        var userContact = userContactDto.Value;
        return new CreatedAtRouteResult("GetUserContactId", new { id = userContact.Id }, userContact);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] UserContactDto userContactDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserContactDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != userContactDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({userContactDto.Id}) objeto"));
            return BadRequest(error);
        }

        var userContact = await _userContactService.Update(userContactDto);
        if (userContact.IsFailure)
        {
            return userContact.Error.Code switch
            {
                "NotFound" => NotFound(userContact.Error),
                "BadRequest" => BadRequest(userContact.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, userContact.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, userContact.Error) // fallback para erro desconhecido
            };
        }

        return Ok(userContact.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var result = await _userContactService.Delete(id);
        if (result.IsFailure)
        {
            return result.Error.Code switch
            {
                "NotFound" => NotFound(result.Error),
                "BadRequest" => BadRequest(result.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, result.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error) // fallback para erro desconhecido
            };
        }

        return Ok(result.Value);
    }
    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

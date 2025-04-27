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
public class ClientContactController(IClientContactService _clientContactService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientContactList = await _clientContactService.GetAll(page, size, search);
        if (clientContactList.IsFailure)
            return NotFound(clientContactList.Error);

        var clientContacts = clientContactList.Value;

        return Ok(clientContacts);
    }

    [HttpGet("{id:int}", Name = "GetClientContactId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var clientContact = await _clientContactService.GetById(id);
        if (clientContact.IsFailure)
            return NotFound(clientContact.Error);

        return Ok(clientContact.Value);
    }

    [HttpGet("ContactByClientId/{clientId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetByClientId([FromQuery] int page = 1, [FromQuery] int size = 10, int clientId = 0)
    {
        if (clientId <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({clientId}) inválido"));
            return BadRequest(error);
        }

        var clientContactList = await _clientContactService.GetByClientId(page, size, clientId);
        if (clientContactList.IsFailure)
            return NotFound(clientContactList.Error);

        var clientContacts = clientContactList.Value;
        return Ok(clientContacts);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientContactCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var createdClientContact = await _clientContactService.Create(clientContactCreateDto);
        if (createdClientContact.IsFailure)
        {
            return createdClientContact.Error.Code switch
            {
                "NotFound" => NotFound(createdClientContact.Error),
                "BadRequest" => BadRequest(createdClientContact.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, createdClientContact.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, createdClientContact.Error) // fallback para erro desconhecido
            };
        }

        var createdContact = createdClientContact.Value;

        return new CreatedAtRouteResult(
            "GetClientContactId",
            new { id = createdContact.Id },
            createdContact
        );
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientContactDto clientContactDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientContactDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != clientContactDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientContactDto.Id}) objeto"));
            return BadRequest(error);
        }

        var updateClientContact = await _clientContactService.Update(clientContactDto);
        if (updateClientContact.IsFailure)
        {
            return updateClientContact.Error.Code switch
            {
                "NotFound" => NotFound(updateClientContact.Error),
                "BadRequest" => BadRequest(updateClientContact.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, updateClientContact.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, updateClientContact.Error) // fallback para erro desconhecido
            };
        }

        return Ok(updateClientContact.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        var deletedClientContact = await _clientContactService.Delete(id);
        if (deletedClientContact.IsFailure)
        {
            return deletedClientContact.Error.Code switch
            {
                "NotFound" => NotFound(deletedClientContact.Error),
                "BadRequest" => BadRequest(deletedClientContact.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, deletedClientContact.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, deletedClientContact.Error) // fallback para erro desconhecido
            };
        }

        return Ok(deletedClientContact.Value);
    }

    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                 .SelectMany(x => x.Errors)
                                 .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

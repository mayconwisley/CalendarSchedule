using System.Net;
using CalendarSchedule.Models.Abstractions;
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
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var dto = await _clientContactService.GetAll(page, size, search);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clientContacts = dto.Value;
        return Ok(clientContacts);
    }

    [HttpGet("{id:int}", Name = "GetClientContactId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var dto = await _clientContactService.GetById(id);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clientContact = dto.Value;
        return Ok(clientContact);
    }

    [HttpGet("ContactByClientId/{clientId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetByClientId([FromQuery] int page = 1, [FromQuery] int size = 10, int clientId = 0)
    {
        if (clientId <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({clientId}) inválido"));
            return BadRequest(error);
        }

        var dto = await _clientContactService.GetByClientId(page, size, clientId);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clientContacts = dto.Value;
        return Ok(clientContacts);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientContactCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var dto = await _clientContactService.Create(clientContactCreateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        var clientContact = dto.Value;
        return CreatedAtRoute("GetClientContactId", new { id = clientContact.Id }, clientContact);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientContactUpdateDto clientContactUpdateDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientContactDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != clientContactUpdateDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientContactUpdateDto.Id}) objeto"));
            return BadRequest(error);
        }

        var dto = await _clientContactService.Update(clientContactUpdateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var clientContatct = dto.Value;
        return Ok(clientContatct);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        var dto = await _clientContactService.Delete(id);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var clientContact = dto.Value;
        return Ok(clientContact);
    }

    private string ErrorMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                 .SelectMany(x => x.Errors)
                                 .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

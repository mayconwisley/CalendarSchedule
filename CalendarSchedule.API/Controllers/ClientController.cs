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
public class ClientController(IClientService _clientService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var dto = await _clientService.GetAll(page, size, search);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clients = dto.Value;
        return Ok(clients);
    }

    [HttpGet("{id:int}", Name = "GetClient")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var errorResult = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(errorResult);
        }

        var dto = await _clientService.GetById(id);
        if (dto.IsFailure)
            return NotFound(dto.Error);
        var client = dto.Value;
        return Ok(client);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] ClientCreateDto clientCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var dto = await _clientService.Create(clientCreateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var client = dto.Value;

        return CreatedAtRoute("GetClient", new { id = client.Id }, client);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientDto clientDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != clientDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientDto.Id}) do objeto"));
            return BadRequest(error);
        }

        var dto = await _clientService.Update(clientDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var client = dto.Value;
        return Ok(client);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }
        var dto = await _clientService.Delete(id);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var client = dto.Value;
        return Ok(client);
    }

    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                             .SelectMany(x => x.Errors)
                                             .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

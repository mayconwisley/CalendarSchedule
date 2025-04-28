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
public class ClientResponsibleController(IClientResponsibleService _clientResponsibleService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientResponsibleDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsResponsibleDto = await _clientResponsibleService.GetAll(page, size, search);
        if (clientsResponsibleDto.IsFailure)
            return NotFound(clientsResponsibleDto.Error);

        var clientsResponsible = clientsResponsibleDto.Value;

        return Ok(clientsResponsible);
    }

    [HttpGet("{id:int}", Name = "GetClientResponsibleId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }
        var clientResponsibleDto = await _clientResponsibleService.GetById(id);
        if (clientResponsibleDto.IsFailure)
            return NotFound(clientResponsibleDto.Error);

        return Ok(clientResponsibleDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var clientResponsibleDto = await _clientResponsibleService.Create(clientResponsibleCreateDto);
        if (clientResponsibleDto.IsFailure)
        {
            return clientResponsibleDto.Error.Code switch
            {
                "NotFound" => NotFound(clientResponsibleDto.Error),
                "BadRequest" => BadRequest(clientResponsibleDto.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, clientResponsibleDto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, clientResponsibleDto.Error) // fallback para erro desconhecido
            };
        }

        var createResult = clientResponsibleDto.Value;

        return new CreatedAtRouteResult("GetClientResponsibleId", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientResponsibleDto clientResponsibleDto)
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

        if (id != clientResponsibleDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientResponsibleDto.Id}) objeto"));
            return BadRequest(error);
        }
        var clientResponsible = await _clientResponsibleService.Update(clientResponsibleDto);
        if (clientResponsible.IsFailure)
        {
            return clientResponsible.Error.Code switch
            {
                "NotFound" => NotFound(clientResponsible.Error),
                "BadRequest" => BadRequest(clientResponsible.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, clientResponsible.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, clientResponsible.Error) // fallback para erro desconhecido
            };
        }

        var result = clientResponsible.Value;

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var result = await _clientResponsibleService.Delete(id);
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

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
public class ClientResponsibleController(IClientResponsibleService _clientResponsibleService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientResponsibleDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var dto = await _clientResponsibleService.GetAll(page, size, search);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clientsResponsibles = dto.Value;
        return Ok(clientsResponsibles);
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
        var dto = await _clientResponsibleService.GetById(id);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var clientResponsible = dto.Value;
        return Ok(clientResponsible);
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
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var dto = await _clientResponsibleService.Create(clientResponsibleCreateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        var clientResponsible = dto.Value;
        return CreatedAtRoute("GetClientResponsibleId", new { id = clientResponsible.Id }, clientResponsible);
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
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != clientResponsibleDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientResponsibleDto.Id}) do objeto"));
            return BadRequest(error);
        }

        var dto = await _clientResponsibleService.Update(clientResponsibleDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        var clientResponsible = dto.Value;
        return Ok(clientResponsible);
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

        var dto = await _clientResponsibleService.Delete(id);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }
        var clientResponsible = dto.Value;
        return Ok(clientResponsible);
    }
    private string ErrorMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

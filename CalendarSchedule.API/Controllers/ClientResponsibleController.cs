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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsResponsibleDto = await _clientResponsibleService.GetAll(page, size, search);
        if (clientsResponsibleDto.IsFailure)
            return NotFound(clientsResponsibleDto.Error);

        var totalClienteResposible = await _clientResponsibleService.TotalClientResponsible(search);
        if (totalClienteResposible.IsFailure)
            return NotFound(totalClienteResposible.Error);

        decimal totalData = totalClienteResposible.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var clientsResponsible = clientsResponsibleDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientsResponsible
        });

    }

    [HttpGet("{id:int}", Name = "GetClientResponsibleId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var clientResponsibleDto = await _clientResponsibleService.GetById(id);
        if (clientResponsibleDto.IsFailure)
            return NotFound(clientResponsibleDto.Error);

        return Ok(clientResponsibleDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        var clientResponsibleDto = await _clientResponsibleService.Create(clientResponsibleCreateDto);
        if (clientResponsibleDto.IsFailure)
            return NotFound(clientResponsibleDto.Error);

        var createResult = clientResponsibleDto.Value;

        return new CreatedAtRouteResult("GetClientResponsibleId", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        if (id != clientResponsibleCreateDto.Id)
            return BadRequest("Dados inválidos");

        var clientResponsibleDto = await _clientResponsibleService.Update(clientResponsibleCreateDto);
        if (clientResponsibleDto.IsFailure)
            return NotFound(clientResponsibleDto.Error);

        var result = clientResponsibleDto.Value;

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponsibleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido");

        var result = await _clientResponsibleService.Delete(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        var clientResponsibleDto = await _clientResponsibleService.GetById(id);
        if (clientResponsibleDto.IsFailure)
            return NotFound(clientResponsibleDto.Error);

        return Ok(clientResponsibleDto);
    }
}

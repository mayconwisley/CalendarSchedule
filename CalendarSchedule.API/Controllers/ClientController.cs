using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService _clientService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsDto = await _clientService.GetAll(page, size, search);
        if (clientsDto.IsFailure)
            return NotFound(clientsDto.Error);

        var totalClient = await _clientService.TotalClients(search);
        if (totalClient.IsFailure)
            return NotFound(totalClient.Error);

        decimal totalData = totalClient.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        var clients = clientsDto.Value;

        if (size == 1)
            totalPage = totalData;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clients
        });
    }

    [HttpGet("{id:int}", Name = "GetClient")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var clientDto = await _clientService.GetById(id);
        if (clientDto.IsFailure)
            return NotFound(clientDto.Error);

        return Ok(clientDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ClientDto clientDto)
    {
        var clientCreate = await _clientService.Create(clientDto);
        if (clientCreate.IsFailure)
            return BadRequest(clientCreate.Error);

        var client = clientCreate.Value;

        return new CreatedAtRouteResult("GetClient", new { id = client }, client);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientDto clientDto)
    {
        if (id != clientDto.Id)
            return BadRequest("Id inválido");

        var client = await _clientService.Update(clientDto);
        if (client.IsFailure)
            return BadRequest(client.Error);

        return Ok(client.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido");

        var clientDeleted = await _clientService.Delete(id);
        if (clientDeleted.IsFailure)
            if (clientDeleted.Error.Code == "NotFound")
                return NotFound(clientDeleted.Error);
            else
                return BadRequest(clientDeleted.Error);

        var clientDto = await _clientService.GetById(id);
        if (clientDto.IsFailure)
            if (clientDto.Error.Code == "NotFound")
                return NotFound(clientDto.Error);
            else
                return BadRequest(clientDto.Error);

        return Ok(clientDto);
    }
}

using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientContactController(IClientContactService _clientContactService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientContactsDto = await _clientContactService.GetAll(page, size, search);
        if (clientContactsDto.IsFailure)
            return NotFound(clientContactsDto.Error);

        var totalClientContact = await _clientContactService.TotalClientContact(search);
        if (totalClientContact.IsFailure)
            return NotFound(totalClientContact.Error);

        decimal totalData = totalClientContact.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        var clientContacts = clientContactsDto.Value;

        if (size == 1)
            totalPage = totalData;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientContacts
        });

    }

    [HttpGet("{id:int}", Name = "GetClientContactId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var clientContactDto = await _clientContactService.GetById(id);
        if (clientContactDto.IsFailure)
            return NotFound(clientContactDto.Error);

        return Ok(clientContactDto.Value);
    }

    [HttpGet("ContactByClientId/{clientId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetByClientId([FromQuery] int page = 1, [FromQuery] int size = 10, int clientId = 0)
    {
        var clientContactsDto = await _clientContactService.GetByClientId(page, size, clientId);
        var totalClientContact = await _clientContactService.TotalClientContact(clientId.ToString());
        if (totalClientContact.IsFailure)
            return NotFound(totalClientContact.Error);

        decimal totalData = totalClientContact.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (clientContactsDto.IsFailure)
            return NotFound(clientContactsDto.Error);


        if (size == 1)
            totalPage = totalData;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientContactsDto
        });
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        var createResult = await _clientContactService.Create(clientContactCreateDto);
        if (createResult.IsFailure)
            return BadRequest(createResult.Error);
        var createdContact = createResult.Value;

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
    public async Task<IActionResult> Put(int id, [FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        if (id != clientContactCreateDto.Id)
            return BadRequest("Dados inválidos");

        if (clientContactCreateDto is null)
            return NotFound($"Objeto {nameof(ClientContactCreateDto)} nulo");

        var clientContactDto = await _clientContactService.Update(clientContactCreateDto);
        if (clientContactDto.IsFailure)
            return BadRequest(clientContactDto.Error);

        return Ok(clientContactDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido");

        var result = await _clientContactService.Delete(id);
        if (result.IsFailure)
            return BadRequest(result.Error);

        var clientContactDto = await _clientContactService.GetById(id);
        if (clientContactDto.IsFailure)
            if (clientContactDto.Error.Code == "NotFound")
                return NotFound(clientContactDto.Error);
            else
                return BadRequest(clientContactDto.Error);



        return Ok(clientContactDto);
    }
}

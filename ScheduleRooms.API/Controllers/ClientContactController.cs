using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientContactController(IClientContactService clientContactService) : ControllerBase
{
    readonly IClientContactService _clientContactService = clientContactService;
    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ClientContactDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientContactDtos = await _clientContactService.GetAll(page, size, search);
        decimal totalData = (decimal)await _clientContactService.TotalClientContact(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!clientContactDtos.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientContactDtos
        });

    }
    [HttpGet("{id:int}", Name = "GetClientContactId")]
    public async Task<ActionResult<ClientContactDto>> GetById(int id)
    {
        var clientContactDto = await _clientContactService.GetById(id);
        if (clientContactDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (clientContactDto is not null)
        {
            return Ok(clientContactDto);
        }
        return NotFound("Sem dados");

    }

    [HttpGet("ContactByClientId/{clientId:int}")]
    public async Task<ActionResult<ClientContactDto>> GetByClientId([FromQuery] int page = 1, [FromQuery] int size = 10, int clientId = 0)
    {
        {
            var clientContactDto = await _clientContactService.GetByClientId(page, size, clientId);

            if (clientContactDto is not null)
            {
                return Ok(clientContactDto);
            }
            return NotFound("Sem dados");

        }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ClientContactDto>> Post([FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        if (clientContactCreateDto is not null)
        {
            var clientContactDto = await _clientContactService.Create(clientContactCreateDto);
            return new CreatedAtRouteResult("GetClientContactId", new { id = clientContactDto.Id }, clientContactDto);
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClientContactDto>> Put(int id, [FromBody] ClientContactCreateDto clientContactCreateDto)
    {
        if (id != clientContactCreateDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (clientContactCreateDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        var clientContactDto = await _clientContactService.Update(clientContactCreateDto);
        return Ok(clientContactDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ClientContactDto>> Delete(int id)
    {
        var clientContactDto = await _clientContactService.GetById(id);
        if (clientContactDto is null)
        {
            return NotFound("Sem dados");
        }
        if (clientContactDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _clientContactService.Delete(id);
        return Ok(clientContactDto);
    }
}

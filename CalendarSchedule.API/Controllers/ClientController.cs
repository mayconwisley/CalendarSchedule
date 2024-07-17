using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    readonly IClientService _clientService = clientService;

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsDto = await _clientService.GetAll(page, size, search);
        decimal totalData = (decimal)await _clientService.TotalClients(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!clientsDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientsDto
        });

    }
    [HttpGet("{id:int}", Name = "GetClient")]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var clientDto = await _clientService.GetById(id);
        if (clientDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (clientDto is not null)
        {
            return Ok(clientDto);
        }
        return NotFound("Sem dados");

    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ClientDto>> Post([FromBody] ClientDto clientDto)
    {
        if (clientDto is not null)
        {
            await _clientService.Create(clientDto);
            return new CreatedAtRouteResult("GetClient", new { id = clientDto.Id }, clientDto);
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClientDto>> Put(int id, [FromBody] ClientDto clientDto)
    {
        if (id != clientDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (clientDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _clientService.Update(clientDto);
        return Ok(clientDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ClientDto>> Delete(int id)
    {
        var clientDto = await _clientService.GetById(id);
        if (clientDto is null)
        {
            return NotFound("Sem dados");
        }
        if (clientDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _clientService.Delete(id);
        return Ok(clientDto);
    }
}

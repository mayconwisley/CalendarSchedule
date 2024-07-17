using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientResponsibleController(IClientResponsibleService clientResponsibleService) : ControllerBase
{
    readonly IClientResponsibleService _clientResponsibleService = clientResponsibleService;
    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ClientResponsibleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsResponsibleDto = await _clientResponsibleService.GetAll(page, size, search);
        decimal totalData = (decimal)await _clientResponsibleService.TotalClientResponsible(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!clientsResponsibleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            clientsResponsibleDto
        });

    }
    [HttpGet("{id:int}", Name = "GetClientResponsibleId")]
    public async Task<ActionResult<ClientResponsibleDto>> GetById(int id)
    {
        var clientResponsibleDto = await _clientResponsibleService.GetById(id);
        if (clientResponsibleDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (clientResponsibleDto is not null)
        {
            return Ok(clientResponsibleDto);
        }
        return NotFound("Sem dados");

    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ClientResponsibleDto>> Post([FromBody] ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        if (clientResponsibleCreateDto is not null)
        {
            var clientResponsibleDto = await _clientResponsibleService.Create(clientResponsibleCreateDto);
            return new CreatedAtRouteResult("GetClientResponsibleId", new { id = clientResponsibleDto.Id }, clientResponsibleDto);
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClientResponsibleDto>> Put(int id, [FromBody] ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        if (id != clientResponsibleCreateDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (clientResponsibleCreateDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        var clientResponsibleDto = await _clientResponsibleService.Update(clientResponsibleCreateDto);
        return Ok(clientResponsibleDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ClientResponsibleDto>> Delete(int id)
    {
        var clientResponsibleDto = await _clientResponsibleService.GetById(id);
        if (clientResponsibleDto is null)
        {
            return NotFound("Sem dados");
        }
        if (clientResponsibleDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _clientResponsibleService.Delete(id);
        return Ok(clientResponsibleDto);
    }
}

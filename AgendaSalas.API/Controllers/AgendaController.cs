using AgendaSalas.API.Service.Interface;
using AgendaSalas.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaSalas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendaController : ControllerBase
{
    readonly IAgendaService _agendaService;

    public AgendaController(IAgendaService agendaService)
    {
        _agendaService = agendaService;
    }

    [HttpGet]
    [Route("Todas")]
    public async Task<ActionResult<IEnumerable<AgendaDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string search = "")
    {
        var agendasDto = await _agendaService.GetAll(page, size, search);
        decimal totalDados = (decimal)await _agendaService.TotalAgendas(search);
        decimal totalPage = (totalDados / size) <= 0 ? 1 : Math.Ceiling((totalDados / size));

        if (size == 1)
        {
            totalPage = totalDados;
        }

        if (!agendasDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalDados,
            page,
            totalPage,
            size,
            agendasDto
        });

    }
    [HttpGet("Data")]
    public async Task<ActionResult<IEnumerable<AgendaDto>>> GetByDate()
    {
        var agendasDto = await _agendaService.GetByDate();

        if (!agendasDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(agendasDto);
    }
    [HttpGet("{id:int}", Name = "GetAgenda")]
    public async Task<ActionResult<AgendaDto>> GetById(int id)
    {
        var agendaDto = await _agendaService.GetById(id);
        if (agendaDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (agendaDto is not null)
        {
            return Ok(agendaDto);
        }
        return NotFound("Sem dados");

    }
    [HttpPost]
    public async Task<ActionResult<AgendaDto>> Post([FromBody] AgendaDto agendaDto)
    {
        if (agendaDto is not null)
        {
            await _agendaService.Create(agendaDto);
            return new CreatedAtRouteResult("GetAgenda", new { id = agendaDto.Id }, agendaDto);
        }
        return BadRequest("Dados inválidos");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AgendaDto>> Put(int id, [FromBody] AgendaDto agendaDto)
    {
        if (id != agendaDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (agendaDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _agendaService.Update(agendaDto);
        return Ok(agendaDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AgendaDto>> Delete(int id)
    {
        var agendaDto = await _agendaService.GetById(id);
        if (agendaDto is null)
        {
            return NotFound("Sem dados");
        }
        if (agendaDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _agendaService.Delete(id);
        return Ok(agendaDto);
    }
}

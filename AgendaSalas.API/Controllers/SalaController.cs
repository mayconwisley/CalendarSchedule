using AgendaSalas.API.Service.Interface;
using AgendaSalas.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSalas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaController : ControllerBase
{
    readonly ISalaService _salaService;

    public SalaController(ISalaService salaService)
    {
        _salaService = salaService;
    }

    [HttpGet]
    [Route("Todas")]
    public async Task<ActionResult<IEnumerable<SalaDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string search = "")
    {
        var salaDto = await _salaService.GetAll(page, size, search);
        decimal totalDados = (decimal)await _salaService.TotalSalas(search);
        decimal totalPage = (totalDados / size) <= 0 ? 1 : Math.Ceiling((totalDados / size));

        if (size == 1)
        {
            totalPage = totalDados;
        }

        if (!salaDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalDados,
            page,
            totalPage,
            size,
            salaDto
        });

    }
    [HttpGet("{id:int}", Name = "GetSala")]
    public async Task<ActionResult<SalaDto>> GetById(int id)
    {
        var salaDto = await _salaService.GetById(id);
        if (salaDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (salaDto is not null)
        {
            return Ok(salaDto);
        }
        return NotFound("Sem dados");

    }
    [HttpPost]
    public async Task<ActionResult<SalaDto>> Post([FromBody] SalaDto salaDto)
    {
        if (salaDto is not null)
        {
            await _salaService.Create(salaDto);
            return new CreatedAtRouteResult("GetSala", new { id = salaDto.Id }, salaDto);
        }
        return BadRequest("Dados inválidos");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<SalaDto>> Put(int id, [FromBody] SalaDto salaDto)
    {
        if (id != salaDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (salaDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _salaService.Update(salaDto);
        return Ok(salaDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<SalaDto>> Delete(int id)
    {
        var salaDto = await _salaService.GetById(id);
        if (salaDto is null)
        {
            return NotFound("Sem dados");
        }
        if (salaDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _salaService.Delete(id);
        return Ok(salaDto);
    }
}

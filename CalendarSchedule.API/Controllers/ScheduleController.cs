using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController(IScheduleService scheduleService) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleDto = await _scheduleService.GetAll(page, size, search);
        decimal totalData = (decimal)await _scheduleService.TotalSchedules(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            scheduleDto
        });

    }
    [HttpGet("{id:int}", Name = "GetScheduleId")]
    public async Task<ActionResult<ScheduleDto>> GetById(int id)
    {
        var scheduleDto = await _scheduleService.GetById(id);
        if (scheduleDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (scheduleDto is not null)
        {
            return Ok(scheduleDto);
        }
        return NotFound("Sem dados");

    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> Post([FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        if (scheduleCreateDto is not null)
        {
            try
            {
                var scheduleDto = await _scheduleService.Create(scheduleCreateDto);

                return new CreatedAtRouteResult("GetScheduleId", new { id = scheduleDto.Id }, scheduleDto);
            }
            catch (Exception ex)
            {
                if (ex.Message == "409")
                {
                    return Conflict("O cadastro da agenda resultaria em uma sobreposição de datas para este usuário.");
                }
                if (ex.Message == "400")
                {
                    return BadRequest("Datas iguais ou 'Data de Início' é maior que a 'Data Final'.");

                }
            }
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ScheduleDto>> Put(int id, [FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        if (id != scheduleCreateDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (scheduleCreateDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        var scheduleDto = await _scheduleService.Update(scheduleCreateDto);
        return Ok(scheduleDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ScheduleDto>> Delete(int id)
    {
        var scheduleDto = await _scheduleService.GetById(id);
        if (scheduleDto is null)
        {
            return NotFound("Sem dados");
        }
        if (scheduleDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _scheduleService.Delete(id);
        return Ok(scheduleDto);
    }
}

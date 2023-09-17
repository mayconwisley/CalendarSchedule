using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleRooms.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var schedulesDto = await _scheduleService.GetAll(page, size, search);
        decimal totalData = (decimal)await _scheduleService.TotalAgendas(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!schedulesDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            schedulesDto
        });

    }
    [HttpGet("ScheduleActive")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetByAgendaActive()
    {
        var scheduleDto = await _scheduleService.GetByAgendaActive();

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
    }
    [HttpGet("ScheduleActiveRoomId/{roomId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetByAgendaActiveSalaId(int roomId, string strDateSalected)
    {
        DateTime dateSalected = DateTime.Parse(strDateSalected.Replace("%2F", "/"));

        var scheduleDto = await _scheduleService.GetByAgendaActiveSalaId(roomId, dateSalected);

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
    }
    [HttpGet("{id:int}", Name = "GetSchedule")]
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
    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> Post([FromBody] ScheduleDto scheduleDto)
    {
        if (scheduleDto is not null)
        {

            try
            {
                await _scheduleService.Create(scheduleDto);
                return new CreatedAtRouteResult("GetSchedule", new { id = scheduleDto.Id }, scheduleDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        return BadRequest("Dados inválidos");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ScheduleDto>> Put(int id, [FromBody] ScheduleDto scheduleDto)
    {
        if (id != scheduleDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (scheduleDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _scheduleService.Update(scheduleDto);
        return Ok(scheduleDto);
    }

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

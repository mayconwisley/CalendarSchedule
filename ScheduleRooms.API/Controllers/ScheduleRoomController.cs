using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleRoomController : ControllerBase
{
    readonly IScheduleRoomService _scheduleService;

    public ScheduleRoomController(IScheduleRoomService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ScheduleRoomDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
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
    public async Task<ActionResult<IEnumerable<ScheduleRoomDto>>> GetByAgendaActive()
    {
        var scheduleDto = await _scheduleService.GetByAgendaActive();

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
    }
    [HttpGet("ScheduleActiveRoomId/{roomId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleRoomDto>>> GetByAgendaActiveSalaId(int roomId, string strDateSalected)
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
    public async Task<ActionResult<ScheduleRoomDto>> GetById(int id)
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
    public async Task<ActionResult<ScheduleRoomDto>> Post([FromBody] ScheduleRoomDto scheduleDto)
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
                if (ex.Message == "409")
                {
                    return Conflict("O cadastro da Agenda resultaria em uma sobreposição de datas para esta sala.");
                }
            }
        }
        return BadRequest("Dados inválidos");
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ScheduleRoomDto>> Put(int id, [FromBody] ScheduleRoomDto scheduleDto)
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
    public async Task<ActionResult<ScheduleRoomDto>> Delete(int id)
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

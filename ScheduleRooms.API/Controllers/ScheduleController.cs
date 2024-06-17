using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers;


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
    [HttpGet("Schedule")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetBySchedule()
    {
        var scheduleDto = await _scheduleService.GetBySchedule();

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
    }
    [HttpGet("ScheduleActive")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetByScheduleActive()
    {
        var scheduleDto = await _scheduleService.GetByScheduleActive();

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
    }
    [HttpGet("ScheduleByClientDateFinal/{clientId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetByScheduleByClientDateFinal(int clientId, string strDateSalected)
    {
        DateTime dateSalected = DateTime.Parse(strDateSalected.Replace("%2F", "/"));

        var scheduleDto = await _scheduleService.GetByScheduleActiveClientId(clientId, dateSalected);

        if (!scheduleDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleDto);
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
    public async Task<ActionResult<ScheduleDto>> Post([FromBody] ScheduleDto scheduleDto)
    {
        if (scheduleDto is not null)
        {
            try
            {
                scheduleDto = await _scheduleService.Create(scheduleDto);

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

        scheduleDto = await _scheduleService.Update(scheduleDto);
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

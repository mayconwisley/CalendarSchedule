using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleUserController(IScheduleUserService scheduleUserService) : ControllerBase
{
    private readonly IScheduleUserService _scheduleUserService = scheduleUserService;

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleUsersDto = await _scheduleUserService.GetAll(page, size, search);
        decimal totalData = (decimal)await _scheduleUserService.TotalSchedules(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!scheduleUsersDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            scheduleUsersDto
        });

    }
    [HttpGet("Schedule")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetBySchedule()
    {
        var scheduleUserDto = await _scheduleUserService.GetBySchedule();

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("ScheduleActive")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByScheduleActive()
    {
        var scheduleUserDto = await _scheduleUserService.GetByScheduleActive();

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("ScheduleUserId/{userId:int}")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByScheduleUserId(int userId)
    {
        var scheduleUserDto = await _scheduleUserService.GetByScheduleUserId(userId);

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("ScheduleDateUserId/{userId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByScheduleUserId(int userId, string strDateSalected)
    {
        DateTime dateSalected = DateTime.Parse(strDateSalected.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByScheduleDateUserId(userId, dateSalected);

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("ScheduleActiveClientId/{clientId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByScheduleClientId(int clientId, string strDateSalected)
    {
        DateTime dateSalected = DateTime.Parse(strDateSalected.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByScheduleActiveClientId(clientId, dateSalected);

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("ScheduleActiveClientIdUserId/{clientId:int}/{userId:int}/{strDateSalected}")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByScheduleClientId(int clientId, int userId, string strDateSalected)
    {
        DateTime dateSalected = DateTime.Parse(strDateSalected.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByScheduleActiveClientIdUserId(clientId, userId, dateSalected);

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [HttpGet("{id:int}", Name = "GetScheduleUser")]
    public async Task<ActionResult<ScheduleUserDto>> GetById(int id)
    {
        var scheduleUserDto = await _scheduleUserService.GetById(id);
        if (scheduleUserDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (scheduleUserDto is not null)
        {
            return Ok(scheduleUserDto);
        }
        return NotFound("Sem dados");

    }
    
    [HttpPost]
    public async Task<ActionResult<ScheduleUserDto>> Post([FromBody] ScheduleUserDto scheduleUserDto)
    {
        if (scheduleUserDto is not null)
        {
            try
            {
                await _scheduleUserService.Create(scheduleUserDto);

                return new CreatedAtRouteResult("GetScheduleUser", new { id = scheduleUserDto.Id }, scheduleUserDto);
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
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ScheduleUserDto>> Put(int id, [FromBody] ScheduleUserDto scheduleUserDto)
    {
        if (id != scheduleUserDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (scheduleUserDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _scheduleUserService.Update(scheduleUserDto);
        return Ok(scheduleUserDto);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ScheduleUserDto>> Delete(int id)
    {
        var scheduleUserDto = await _scheduleUserService.GetById(id);
        if (scheduleUserDto is null)
        {
            return NotFound("Sem dados");
        }
        if (scheduleUserDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _scheduleUserService.Delete(id);
        return Ok(scheduleUserDto);
    }
}

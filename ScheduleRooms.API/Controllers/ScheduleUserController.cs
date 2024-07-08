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
        decimal totalData = (decimal)await _scheduleUserService.TotalScheduleUser(search);
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

    [HttpGet("{scheduleId:int}", Name = "GetScheduleUserId")]
    public async Task<ActionResult<ScheduleUserDto>> GetByScheduleId(int scheduleId)
    {
        var scheduleUserDto = await _scheduleUserService.GetByScheduleId(scheduleId);
        if (scheduleUserDto is not null)
        {
            return Ok(scheduleUserDto);
        }
        return NotFound("Sem dados");

    }
    [HttpGet("{scheduleId:int}/{userId:int}", Name = "GetScheduleIdUserId")]
    public async Task<ActionResult<ScheduleUserDto>> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserService.GetById(scheduleId, userId);
        if (scheduleUserDto is not null)
        {
            return Ok(scheduleUserDto);
        }
        return NotFound("Sem dados");

    }
    [HttpGet("DateStart/{dateStart}")]
    public async Task<ActionResult<IEnumerable<ScheduleUserDto>>> GetByDateStart(string dateStart = "")
    {
        DateTime dateSalected = DateTime.Parse(dateStart.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByDateStart(dateSalected);

        if (!scheduleUserDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(scheduleUserDto);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ScheduleUserDto>> Post([FromBody] ScheduleUserCreateDto scheduleUserCreateDto)
    {
        if (scheduleUserCreateDto is not null)
        {
            try
            {
                var scheduleUser = await _scheduleUserService.Create(scheduleUserCreateDto);

                return new CreatedAtRouteResult("GetScheduleUserId", new { scheduleId = scheduleUser.ScheduleId }, scheduleUser);
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
    public async Task<ActionResult<ScheduleUserDto>> Put(int id, [FromBody] ScheduleUserCreateDto scheduleUserCreateDto)
    {
        if (id != scheduleUserCreateDto.ScheduleId)
        {
            return BadRequest("Dados inválidos");
        }
        if (scheduleUserCreateDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        var scheduleUserDto = await _scheduleUserService.Update(scheduleUserCreateDto);
        return Ok(scheduleUserDto);
    }
    [Authorize]
    [HttpDelete("{scheduleId:int}/{userId:int}")]
    public async Task<ActionResult<ScheduleUserDto>> Delete(int scheduleId, int userId)
    {
        var scheduleDto = await _scheduleUserService.GetById(scheduleId, userId);
        if (scheduleDto is null)
        {
            return NotFound("Sem dados");
        }
        await _scheduleUserService.Delete(scheduleId, userId);
        return Ok(scheduleDto);
    }
}

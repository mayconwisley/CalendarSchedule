using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class ScheduleUserController(IScheduleUserService _scheduleUserService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleUsersDto = await _scheduleUserService.GetAll(page, size, search);

        var scheduleUsers = scheduleUsersDto.Value;

        return Ok(scheduleUsers);
    }

    [HttpGet("{scheduleId:int}", Name = "GetScheduleUserId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetByScheduleId([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", int scheduleId = default)
    {
        var scheduleUserDto = await _scheduleUserService.GetByScheduleId(page, size, search, scheduleId);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);

    }

    [HttpGet("{scheduleId:int}/{userId:int}", Name = "GetScheduleIdUserId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserService.GetById(scheduleId, userId);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [HttpGet("DateStart/{dateStart}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetByDateStart([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", string dateStart = "")
    {
        DateTime dateSalected = DateTime.Parse(dateStart.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByDateStart(page, size, search, dateSalected);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [HttpGet("Period/{dateStart}/{dateEnd}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetByDatePeriod([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", string dateStart = "", string dateEnd = "")
    {
        DateTime dtDateStart = DateTime.Parse(dateStart.Replace("%2F", "/"));
        DateTime dtDateEnd = DateTime.Parse(dateEnd.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByDatePeriod(page, size, search, dtDateStart, dtDateEnd);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserService.Create(scheduleUserCreateDto);
        if (scheduleUser.IsFailure)
            return NotFound(scheduleUser.Error);

        var createResult = scheduleUser.Value;
        return new CreatedAtRouteResult("GetScheduleUserId", new { scheduleId = createResult.ScheduleId }, createResult);

    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] ScheduleUserDto scheduleUserDto)
    {
        if (id <= 0)
            return BadRequest("Id inválidos");

        var scheduleUser = await _scheduleUserService.Update(scheduleUserDto);
        if (scheduleUser.IsFailure)
            return NotFound(scheduleUser.Error);

        return Ok(scheduleUser.Value);
    }

    [Authorize]
    [HttpDelete("{scheduleId:int}/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    public async Task<IActionResult> Delete(int scheduleId, int userId)
    {
        var result = await _scheduleUserService.Delete(scheduleId, userId);
        if (result.IsFailure)
            return NotFound(result.Error);

        var scheduleDto = await _scheduleUserService.GetById(scheduleId, userId);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        return Ok(scheduleDto.Value);
    }
}

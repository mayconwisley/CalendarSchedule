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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleUsersDto = await _scheduleUserService.GetAll(page, size, search);
        if (scheduleUsersDto.IsFailure)
            return NotFound(scheduleUsersDto.Error);

        var totalScheduleUser = await _scheduleUserService.TotalScheduleUser(search);
        if (totalScheduleUser.IsFailure)
            return NotFound(totalScheduleUser.Error);

        decimal totalData = totalScheduleUser.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var scheduleUsers = scheduleUsersDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            scheduleUsers
        });

    }

    [HttpGet("{scheduleId:int}", Name = "GetScheduleUserId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetByScheduleId(int scheduleId)
    {
        var scheduleUserDto = await _scheduleUserService.GetByScheduleId(scheduleId);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);

    }

    [HttpGet("{scheduleId:int}/{userId:int}", Name = "GetScheduleIdUserId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserService.GetById(scheduleId, userId);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [HttpGet("DateStart/{dateStart}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetByDateStart(string dateStart = "")
    {
        DateTime dateSalected = DateTime.Parse(dateStart.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByDateStart(dateSalected);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [HttpGet("Period/{dateStart}/{dateEnd}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetByDatePeriod(string dateStart = "", string dateEnd = "")
    {
        DateTime dtDateStart = DateTime.Parse(dateStart.Replace("%2F", "/"));
        DateTime dtDateEnd = DateTime.Parse(dateEnd.Replace("%2F", "/"));

        var scheduleUserDto = await _scheduleUserService.GetByDatePeriod(dtDateStart, dtDateEnd);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
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
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ScheduleUserCreateDto scheduleUserCreateDto)
    {
        if (id <= 0)
            return BadRequest("Id inválidos");

        var scheduleUserDto = await _scheduleUserService.Update(scheduleUserCreateDto);
        if (scheduleUserDto.IsFailure)
            return NotFound(scheduleUserDto.Error);

        return Ok(scheduleUserDto.Value);
    }

    [Authorize]
    [HttpDelete("{scheduleId:int}/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
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

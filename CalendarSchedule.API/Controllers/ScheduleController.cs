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
public class ScheduleController(IScheduleService _scheduleService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleDto = await _scheduleService.GetAll(page, size, search);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        var totalSchedule = await _scheduleService.TotalSchedules(search);
        if (totalSchedule.IsFailure)
            return NotFound(totalSchedule.Error);

        decimal totalData = totalSchedule.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var schedule = scheduleDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            schedule
        });

    }

    [HttpGet("{id:int}", Name = "GetScheduleId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scheduleDto = await _scheduleService.GetById(id);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        return Ok(scheduleDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        var scheduleDto = await _scheduleService.Create(scheduleCreateDto);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        var createResult = scheduleDto.Value;

        return new CreatedAtRouteResult("GetScheduleId", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        if (id <= 0)
            return BadRequest("Dados inválidos");

        var scheduleDto = await _scheduleService.Update(scheduleCreateDto);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        return Ok(scheduleDto.Value);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]

    public async Task<ActionResult<ScheduleDto>> Delete(int id)
    {
        var result = await _scheduleService.Delete(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        var scheduleDto = await _scheduleService.GetById(id);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        return Ok(scheduleDto);
    }
}

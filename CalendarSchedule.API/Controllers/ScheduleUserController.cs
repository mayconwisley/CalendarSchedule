using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class ScheduleUserController(IScheduleUserService _scheduleUserService) : ControllerBase
{
	[HttpGet]
	[Route("All")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ScheduleUserDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
	{
		var scheduleUsersDto = await _scheduleUserService.GetAll(page, size, search);

		var scheduleUsers = scheduleUsersDto.Value;

		return Ok(scheduleUsers);
	}

	[HttpGet("{scheduleId:int}", Name = "GetScheduleUserId")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ScheduleUserDto>))]
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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleUserDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetById(int scheduleId, int userId)
	{
		var scheduleUserDto = await _scheduleUserService.GetById(scheduleId, userId);
		if (scheduleUserDto.IsFailure)
			return NotFound(scheduleUserDto.Error);

		return Ok(scheduleUserDto.Value);
	}

	[HttpGet("DateStart/{dateStart:datetime}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ScheduleUserDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetByDateStart([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", DateOnly dateStart = default)
	{
		var scheduleUserDto = await _scheduleUserService.GetByDateStart(page, size, search, dateStart);
		if (scheduleUserDto.IsFailure)
			return NotFound(scheduleUserDto.Error);

		return Ok(scheduleUserDto.Value);
	}

	[HttpGet("Period/{dateStart:datetime}/{dateEnd:datetime}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ScheduleUserDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetByDatePeriod([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", DateOnly dateStart = default, DateOnly dateEnd = default)
	{
		//DateTime dtDateStart = DateTime.Parse(dateStart.Replace("%2F", "/"));
		//DateTime dtDateEnd = DateTime.Parse(dateEnd.Replace("%2F", "/"));

		var scheduleUserDto = await _scheduleUserService.GetByDatePeriod(page, size, search, dateStart, dateEnd);
		if (scheduleUserDto.IsFailure)
			return NotFound(scheduleUserDto.Error);

		return Ok(scheduleUserDto.Value);
	}

	[Authorize]
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ScheduleUserDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
	public async Task<IActionResult> Post([FromBody] ScheduleUserCreateDto scheduleUserCreateDto)
	{
		if (!ModelState.IsValid)
		{
			var errorMessage = ErrorMoldeState();

			var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientCreateDto)}): {errorMessage}"));
			return BadRequest(error);
		}

		var scheduleUser = await _scheduleUserService.Create(scheduleUserCreateDto);
		if (scheduleUser.IsFailure)
		{
			return scheduleUser.Error.StatusCode switch
			{
				HttpStatusCode.NotFound => NotFound(scheduleUser.Error),
				HttpStatusCode.BadRequest => BadRequest(scheduleUser.Error),
				_ => StatusCode(StatusCodes.Status500InternalServerError, scheduleUser.Error) // fallback para erro desconhecido
			};
		}

		var createResult = scheduleUser.Value;
		return new CreatedAtRouteResult("GetScheduleUserId", new { scheduleId = createResult.ScheduleId }, createResult);

	}

	[Authorize]
	[HttpDelete("{scheduleId:int}/{userId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleUserDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
	public async Task<IActionResult> Delete(int scheduleId, int userId)
	{
		if (scheduleId <= 0 || userId <= 0)
		{
			var error = Result.Failure(Error.BadRequest($"ScheduleId ({scheduleId}) ou UserId ({userId}) inválido"));
			return BadRequest(error);
		}

		var result = await _scheduleUserService.Delete(scheduleId, userId);
		if (result.IsFailure)
		{
			return result.Error.StatusCode switch
			{
				HttpStatusCode.NotFound => NotFound(result.Error),
				HttpStatusCode.BadRequest => BadRequest(result.Error),
				_ => StatusCode(StatusCodes.Status500InternalServerError, result.Error)
			};
		}

		return Ok(result.Value);
	}
	private string ErrorMoldeState()
	{
		var errorMessage = string.Join("; ", ModelState.Values
											  .SelectMany(x => x.Errors)
											  .Select(x => x.ErrorMessage));
		return errorMessage;
	}
}

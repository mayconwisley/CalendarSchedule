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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ScheduleDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var scheduleDto = await _scheduleService.GetAll(page, size, search);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        var schedule = scheduleDto.Value;

        return Ok(schedule);
    }

    [HttpGet("{id:int}", Name = "GetScheduleId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        var scheduleDto = await _scheduleService.GetById(id);
        if (scheduleDto.IsFailure)
            return NotFound(scheduleDto.Error);

        return Ok(scheduleDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ScheduleCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var scheduleDto = await _scheduleService.Create(scheduleCreateDto);
        if (scheduleDto.IsFailure)
        {
            return scheduleDto.Error.Code switch
            {
                "NotFound" => NotFound(scheduleDto.Error),
                "BadRequest" => BadRequest(scheduleDto.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, scheduleDto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, scheduleDto.Error) // fallback para erro desconhecido
            };
        }

        var createResult = scheduleDto.Value;

        return new CreatedAtRouteResult("GetScheduleId", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] ScheduleCreateDto scheduleCreateDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ScheduleCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != scheduleCreateDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({scheduleCreateDto.Id}) objeto"));
            return BadRequest(error);
        }

        var scheduleDto = await _scheduleService.Update(scheduleCreateDto);
        if (scheduleDto.IsFailure)
        {
            return scheduleDto.Error.Code switch
            {
                "NotFound" => NotFound(scheduleDto.Error),
                "BadRequest" => BadRequest(scheduleDto.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, scheduleDto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, scheduleDto.Error) // fallback para erro desconhecido
            };
        }

        return Ok(scheduleDto.Value);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]

    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var result = await _scheduleService.Delete(id);
        if (result.IsFailure)
        {
            return result.Error.Code switch
            {
                "NotFound" => NotFound(result.Error),
                "BadRequest" => BadRequest(result.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, result.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error) // fallback para erro desconhecido
            };
        }

        return Ok(result);
    }

    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                             .SelectMany(x => x.Errors)
                                             .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

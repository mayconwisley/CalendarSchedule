using System.Net;
using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UserController(IUserService _userService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var dto = await _userService.GetAll(page, size, search);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var users = dto.Value;
        return Ok(users);
    }

    [HttpGet]
    [Route("ManagerAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetManagerAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var dto = await _userService.GetManagerAll(page, size, search);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var users = dto.Value;
        return Ok(users);
    }

    [HttpGet]
    [Route("ManagerAllByUserCurrent")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetManagerAllByUserCurrent([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", [FromQuery] string username = "")
    {
        var dto = await _userService.GetManagerAllByUserCurrent(page, size, search, username);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var users = dto.Value;
        return Ok(users);
    }

    [HttpGet("{id:int}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        var dto = await _userService.GetById(id);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var user = dto.Value;
        return Ok(user);
    }

    [HttpGet("Username/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetManagerUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            var error = Result.Failure(Error.BadRequest($"Username nulo"));
            return BadRequest(error);
        }

        var dto = await _userService.GetManagerUsername(username);
        if (dto.IsFailure)
            return NotFound(dto.Error);

        var user = dto.Value;
        return Ok(user);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] UserCreateDto userCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(UserDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var dto = await _userService.Create(userCreateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                HttpStatusCode.Conflict => Conflict(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        var user = dto.Value;

        return CreatedAtRoute("GetUser", new { id = user.Id }, user);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] UserUpdateDto userUpdateDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }
        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserDto)}): {errorMessage}"));
            return BadRequest(error);
        }
        if (userUpdateDto.Id != id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) diferente do Id ({userUpdateDto.Id}) do objeto"));
            return BadRequest(error);
        }
        var dto = await _userService.Update(userUpdateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                HttpStatusCode.Conflict => Conflict(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        return Ok(dto.Value);
    }
    [Authorize]
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Patch(int id, [FromBody] UserUpdateDto userUpdateDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }
        if (!ModelState.IsValid)
        {
            var errorMessage = ErrorMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserDto)}): {errorMessage}"));
            return BadRequest(error);
        }
        if (userUpdateDto.Id != id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) diferente do Id ({userUpdateDto.Id}) do objeto"));
            return BadRequest(error);
        }
        var dto = await _userService.Update(userUpdateDto);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                HttpStatusCode.Conflict => Conflict(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        return Ok(dto.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<ActionResult<UserDto>> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }

        var dto = await _userService.Delete(id);
        if (dto.IsFailure)
        {
            return dto.Error.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(dto.Error),
                HttpStatusCode.BadRequest => BadRequest(dto.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
            };
        }

        var user = dto.Value;
        return Ok(user);
    }
    private string ErrorMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

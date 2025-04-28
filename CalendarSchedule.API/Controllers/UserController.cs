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
public class UserController(IUserService _userService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetAll(page, size, search);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var users = usersDto.Value;

        return Ok(users);
    }

    [HttpGet]
    [Route("ManagerAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetManagerAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetManagerAll(page, size, search);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var users = usersDto.Value;

        return Ok(users);
    }

    [HttpGet]
    [Route("ManagerAllByUserCurrent")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> GetManagerAllByUserCurrent([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", [FromQuery] string username = "")
    {
        var usersDto = await _userService.GetManagerAllByUserCurrent(page, size, search, username);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var users = usersDto.Value;

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

        var userDto = await _userService.GetById(id);
        if (userDto.IsFailure)
            return NotFound(userDto.Error);
        return Ok(userDto.Value);
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

        var userDto = await _userService.GetManagerUsername(username);
        if (userDto.IsFailure)
            return NotFound(userDto.Error);

        return Ok(userDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Post([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(UserDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var user = await _userService.Create(userDto);
        if (user.IsFailure)
        {
            return user.Error.Code switch
            {
                "NotFound" => NotFound(user.Error),
                "BadRequest" => BadRequest(user.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, user.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, user.Error) // fallback para erro desconhecido
            };
        }

        var createResult = user.Value;

        return new CreatedAtRouteResult("GetUser", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserDto)}): {errorMessage}"));
            return BadRequest(error);
        }
        if (userDto.Id != id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }
        var user = await _userService.Update(userDto);
        if (user.IsFailure)
        {
            return user.Error.Code switch
            {
                "NotFound" => NotFound(user.Error),
                "BadRequest" => BadRequest(user.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, user.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, user.Error) // fallback para erro desconhecido
            };
        }

        return Ok(user.Value);
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

        var result = await _userService.Delete(id);
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

        return Ok(result.Value);
    }
    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

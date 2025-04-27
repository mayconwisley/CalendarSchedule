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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetAll(page, size, search);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var totalUsers = await _userService.TotalUsers(search);
        if (totalUsers.IsFailure)
            return NotFound(totalUsers.Error);

        decimal totalData = totalUsers.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var users = usersDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            users
        });

    }

    [HttpGet]
    [Route("ManagerAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetManagerAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetManagerAll(page, size, search);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var totalUsers = await _userService.TotalUsers(search);
        if (totalUsers.IsFailure)
            return NotFound(totalUsers.Error);

        decimal totalData = totalUsers.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var users = usersDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            users
        });

    }

    [HttpGet]
    [Route("ManagerAllByUserCurrent")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetManagerAllByUserCurrent([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", [FromQuery] string username = "")
    {
        var usersDto = await _userService.GetManagerAllByUserCurrent(page, size, search, username);
        if (usersDto.IsFailure)
            return NotFound(usersDto.Error);

        var totalUsers = await _userService.TotalUsers(search);
        if (totalUsers.IsFailure)
            return NotFound(totalUsers.Error);

        decimal totalData = totalUsers.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var users = usersDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            users
        });
    }

    [HttpGet("{id:int}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var userDto = await _userService.GetById(id);
        if (userDto.IsFailure)
            return NotFound(userDto.Error);
        return Ok(userDto.Value);
    }

    [HttpGet("Username/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetManagerUsername(string username)
    {
        var userDto = await _userService.GetManagerUsername(username);
        if (userDto.IsFailure)
            return NotFound(userDto.Error);

        return Ok(userDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] UserDto userDto)
    {
        var user = await _userService.Create(userDto);
        if (user.IsFailure)
            return NotFound(user.Error);

        var createResult = user.Value;

        return new CreatedAtRouteResult("GetUser", new { id = createResult.Id }, createResult);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
    {
        if (id <= 0)
            return BadRequest("Id inválidos");

        var user = await _userService.Update(userDto);
        if (user.IsFailure)
            return NotFound(user.Error);

        return Ok(user.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<ActionResult<UserDto>> Delete(int id)
    {
        var result = await _userService.Delete(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        var userDto = await _userService.GetById(id);
        if (userDto.IsFailure)
            return NotFound(userDto.Error);

        return Ok(userDto.Value);
    }
}

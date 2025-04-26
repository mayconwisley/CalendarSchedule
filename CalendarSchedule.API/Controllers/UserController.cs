﻿using CalendarSchedule.API.Service.Interface;
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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetAll(page, size, search);
        decimal totalData = (decimal)await _userService.TotalUsers(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!usersDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            usersDto
        });

    }
    [HttpGet]
    [Route("ManagerAll")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetManagerAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var usersDto = await _userService.GetManagerAll(page, size, search);
        decimal totalData = (decimal)await _userService.TotalUsers(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!usersDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            usersDto
        });

    }
    [HttpGet]
    [Route("ManagerAllByUserCurrent")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetManagerAllByUserCurrent([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "", [FromQuery] string username = "")
    {
        var usersDto = await _userService.GetManagerAllByUserCurrent(page, size, search, username);
        decimal totalData = (decimal)await _userService.TotalUsers(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!usersDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            usersDto
        });

    }
    [HttpGet("{id:int}", Name = "GetUser")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var userDto = await _userService.GetById(id);
        if (userDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (userDto is not null)
        {
            return Ok(userDto);
        }
        return NotFound("Sem dados");

    }
    [HttpGet("Username/{username}")]
    public async Task<ActionResult<UserDto>> GetManagerUsername(string username)
    {
        var userDto = await _userService.GetManagerUsername(username);
        if (userDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (userDto is not null)
        {
            return Ok(userDto);
        }
        return NotFound("Sem dados");

    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] UserDto userDto)
    {
        if (userDto is not null)
        {
            userDto = await _userService.Create(userDto);
            return new CreatedAtRouteResult("GetUser", new { id = userDto.Id }, userDto);
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UserDto userDto)
    {
        if (id != userDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (userDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _userService.Update(userDto);
        return Ok(userDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<UserDto>> Delete(int id)
    {
        var userDto = await _userService.GetById(id);
        if (userDto is null)
        {
            return NotFound("Sem dados");
        }
        if (userDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _userService.Delete(id);
        return Ok(userDto);
    }
}

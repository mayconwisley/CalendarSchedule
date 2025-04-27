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
public class UserContactController(IUserContactService _userContactService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var userContactsDto = await _userContactService.GetAll(page, size, search);
        if (userContactsDto.IsFailure)
            return NotFound(userContactsDto.Error);
        var totalUserContact = await _userContactService.TotalUserContact(search);
        if (totalUserContact.IsFailure)
            return NotFound(totalUserContact.Error);

        decimal totalData = totalUserContact.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var userContacts = userContactsDto.Value;

        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            userContacts
        });
    }

    [HttpGet]
    [Route("UserContactByUserId/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> UserContactByUserId([FromQuery] int page = 1, [FromQuery] int size = 10, int userId = 0)
    {
        var userContactsDto = await _userContactService.GetByUserId(page, size, userId);
        if (userContactsDto.IsFailure)
            return NotFound(userContactsDto.Error);
        var totalUserContact = await _userContactService.TotalUserContact(userId.ToString());
        if (totalUserContact.IsFailure)
            return NotFound(totalUserContact.Error);

        decimal totalData = totalUserContact.Value;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var userContacts = userContactsDto.Value;
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            userContacts
        });
    }

    [HttpGet("{id:int}", Name = "GetUserContactId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var userContactDto = await _userContactService.GetById(id);
        if (userContactDto.IsFailure)
            return NotFound(userContactDto.Error);

        return Ok(userContactDto);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] UserContactCreateDto userContactCreateDto)
    {
        var userContactDto = await _userContactService.Create(userContactCreateDto);
        if (userContactDto.IsFailure)
            return NotFound(userContactDto.Error);
        var userContact = userContactDto.Value;
        return new CreatedAtRouteResult("GetUserContactId", new { id = userContact.Id }, userContact);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] UserContactCreateDto userContactCreateDto)
    {
        if (id <= 0)
            return BadRequest("Id inválidos");

        var userContactDto = await _userContactService.Update(userContactCreateDto);
        if (userContactDto.IsFailure)
            return NotFound(userContactDto.Error);

        return Ok(userContactDto.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userContactService.Delete(id);
        if (result.IsFailure)
            return NotFound(result.Error);

        var userContactDto = await _userContactService.GetById(id);
        if (userContactDto.IsFailure)
            return NotFound(userContactDto.Error);

        return Ok(userContactDto.Value);
    }
}

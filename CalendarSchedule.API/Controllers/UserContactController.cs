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
    public async Task<ActionResult<IEnumerable<UserContactDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var userContactsDto = await _userContactService.GetAll(page, size, search);
        decimal totalData = (decimal)await _userContactService.TotalUserContact(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!userContactsDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            userContactsDto
        });

    }
    [HttpGet]
    [Route("UserContactByUserId/{userId:int}")]
    public async Task<ActionResult<IEnumerable<UserContactDto>>> UserContactByUserId([FromQuery] int page = 1, [FromQuery] int size = 10, int userId = 0)
    {
        var userContactsDto = await _userContactService.GetByUserId(page, size, userId);
        decimal totalData = (decimal)await _userContactService.TotalUserContact(userId.ToString());
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!userContactsDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            userContactsDto
        });

    }
    [HttpGet("{id:int}", Name = "GetUserContactId")]
    public async Task<ActionResult<UserContactDto>> GetById(int id)
    {
        var userContactDto = await _userContactService.GetById(id);
        if (userContactDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (userContactDto is not null)
        {
            return Ok(userContactDto);
        }
        return NotFound("Sem dados");

    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<UserContactDto>> Post([FromBody] UserContactCreateDto userContactCreateDto)
    {
        if (userContactCreateDto is not null)
        {
            var userContactDto = await _userContactService.Create(userContactCreateDto);
            return new CreatedAtRouteResult("GetUserContactId", new { id = userContactDto.Id }, userContactDto);
        }
        return BadRequest("Dados inválidos");
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserContactDto>> Put(int id, [FromBody] UserContactCreateDto userContactCreateDto)
    {
        if (id != userContactCreateDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (userContactCreateDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        var userContactDto = await _userContactService.Update(userContactCreateDto);
        return Ok(userContactDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<UserContactDto>> Delete(int id)
    {
        var userContactDto = await _userContactService.GetById(id);
        if (userContactDto is null)
        {
            return NotFound("Sem dados");
        }
        if (userContactDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _userContactService.Delete(id);
        return Ok(userContactDto);
    }
}

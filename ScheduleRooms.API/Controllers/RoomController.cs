using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var roomsDto = await _roomService.GetAll(page, size, search);
        decimal totalData = (decimal)await _roomService.TotalSalas(search);
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
        {
            totalPage = totalData;
        }

        if (!roomsDto.Any())
        {
            return NotFound("Sem dados");
        }
        return Ok(new
        {
            totalData,
            page,
            totalPage,
            size,
            roomsDto
        });

    }
    [HttpGet("{id:int}", Name = "GetRoom")]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        var roomDto = await _roomService.GetById(id);
        if (roomDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        if (roomDto is not null)
        {
            return Ok(roomDto);
        }
        return NotFound("Sem dados");

    }
    
    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post([FromBody] RoomDto roomDto)
    {
        if (roomDto is not null)
        {
            await _roomService.Create(roomDto);
            return new CreatedAtRouteResult("GetRoom", new { id = roomDto.Id }, roomDto);
        }
        return BadRequest("Dados inválidos");
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<RoomDto>> Put(int id, [FromBody] RoomDto roomDto)
    {
        if (id != roomDto.Id)
        {
            return BadRequest("Dados inválidos");
        }
        if (roomDto is null)
        {
            return BadRequest("Dados inválidos");
        }

        await _roomService.Update(roomDto);
        return Ok(roomDto);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<RoomDto>> Delete(int id)
    {
        var roomDto = await _roomService.GetById(id);
        if (roomDto is null)
        {
            return NotFound("Sem dados");
        }
        if (roomDto.Id <= 0)
        {
            return NotFound("Sem dados");
        }
        await _roomService.Delete(id);
        return Ok(roomDto);
    }
}

using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalendarSchedule.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class UserContactController(IUserContactService _userContactService) : ControllerBase
{
	[HttpGet]
	[Route("All")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserContactDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
	{
		var dto = await _userContactService.GetAll(page, size, search);
		if (dto.IsFailure)
			return NotFound(dto.Error);

		var userContacts = dto.Value;

		return Ok(userContacts);
	}

	[HttpGet]
	[Route("UserContactByUserId/{userId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserContactDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> UserContactByUserId([FromQuery] int page = 1, [FromQuery] int size = 10, int userId = 0)
	{
		var dto = await _userContactService.GetByUserId(page, size, userId);
		if (dto.IsFailure)
			return NotFound(dto.Error);

		var userContacts = dto.Value;
		return Ok(userContacts);
	}

	[HttpGet("{id:int}", Name = "GetUserContactId")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	public async Task<IActionResult> GetById(int id)
	{
		var dto = await _userContactService.GetById(id);
		if (dto.IsFailure)
			return NotFound(dto.Error);

		var userContact = dto.Value;
		return Ok(userContact);
	}

	[Authorize]
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
	public async Task<IActionResult> Post([FromBody] UserContactCreateDto userContactCreateDto)
	{
		if (!ModelState.IsValid)
		{
			var errorMessage = ErroMoldeState();
			var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserContactCreateDto)}): {errorMessage}"));
			return BadRequest(error);
		}

		var dto = await _userContactService.Create(userContactCreateDto);
		if (dto.IsFailure)
		{
			return dto.Error.StatusCode switch
			{
				HttpStatusCode.NotFound => NotFound(dto.Error),
				HttpStatusCode.BadRequest => BadRequest(dto.Error),
				_ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
			};
		}

		var userContact = dto.Value;
		return new CreatedAtRouteResult("GetUserContactId", new { id = userContact.Id }, userContact);
	}

	[Authorize]
	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
	public async Task<IActionResult> Put(int id, [FromBody] UserContactUpdateDto userContactUpdateDto)
	{
		if (id <= 0)
		{
			var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
			return BadRequest(error);
		}

		if (!ModelState.IsValid)
		{
			var errorMessage = ErroMoldeState();
			var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(UserContactUpdateDto)}): {errorMessage}"));
			return BadRequest(error);
		}

		if (id != userContactUpdateDto.Id)
		{
			var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({userContactUpdateDto.Id}) objeto"));
			return BadRequest(error);
		}

		var dto = await _userContactService.Update(userContactUpdateDto);
		if (dto.IsFailure)
		{
			return dto.Error.StatusCode switch
			{
				HttpStatusCode.NotFound => NotFound(dto.Error),
				HttpStatusCode.BadRequest => BadRequest(dto.Error),
				_ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
			};
		}

		var userContact = dto.Value;
		return Ok(userContact);
	}

	[Authorize]
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserContactDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
	public async Task<IActionResult> Delete(int id)
	{
		if (id <= 0)
		{
			var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
			return BadRequest(error);
		}

		var dto = await _userContactService.Delete(id);
		if (dto.IsFailure)
		{
			return dto.Error.StatusCode switch
			{
				HttpStatusCode.NotFound => NotFound(dto.Error),
				HttpStatusCode.BadRequest => BadRequest(dto.Error),
				_ => StatusCode(StatusCodes.Status500InternalServerError, dto.Error)
			};
		}
		var userContact = dto.Value;

		return Ok(userContact);
	}
	private string ErroMoldeState()
	{
		var errorMessage = string.Join("; ", ModelState.Values
											  .SelectMany(x => x.Errors)
											  .Select(x => x.ErrorMessage));
		return errorMessage;
	}
}

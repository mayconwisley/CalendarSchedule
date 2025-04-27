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
public class ClientController(IClientService _clientService) : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ClientDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = "")
    {
        var clientsDto = await _clientService.GetAll(page, size, search);
        if (clientsDto.IsFailure)
            return NotFound(clientsDto.Error);

        return Ok(clientsDto.Value);
    }

    [HttpGet("{id:int}", Name = "GetClient")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        var clientDto = await _clientService.GetById(id);
        if (clientDto.IsFailure)
            return NotFound(clientDto.Error);

        return Ok(clientDto.Value);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Post([FromBody] ClientCreateDto clientCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();

            var error = Result.Failure(Error.BadRequest($"Erro de validação no objeto ({nameof(ClientCreateDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        var create = await _clientService.Create(clientCreateDto);
        if (create.IsFailure)
        {
            return create.Error.Code switch
            {
                "NotFound" => NotFound(create.Error),
                "BadRequest" => BadRequest(create.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, create.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, create.Error) // fallback para erro desconhecido
            };
        }
        var client = create.Value;

        return new CreatedAtRouteResult("GetClient", new { id = client.Id }, client);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Put(int id, [FromBody] ClientDto clientDto)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) inválido"));
            return BadRequest(error);
        }

        if (!ModelState.IsValid)
        {
            var errorMessage = ErroMoldeState();
            var error = Result.Failure(Error.BadRequest($"Erro de validação do objeto ({nameof(ClientDto)}): {errorMessage}"));
            return BadRequest(error);
        }

        if (id != clientDto.Id)
        {
            var error = Result.Failure(Error.BadRequest($"Id ({id}) da rota diferente do Id ({clientDto.Id}) objeto"));
            return BadRequest(error);
        }

        var client = await _clientService.Update(clientDto);
        if (client.IsFailure)
        {
            return client.Error.Code switch
            {
                "NotFound" => NotFound(client.Error),
                "BadRequest" => BadRequest(client.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, client.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, client.Error) // fallback para erro desconhecido
            };
        }
        return Ok(client.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            var error = Result.Failure(Error.BadRequest($"Id inválido: {id}"));
            return BadRequest(error);
        }
        var clientDeleted = await _clientService.Delete(id);
        if (clientDeleted.IsFailure)
        {
            return clientDeleted.Error.Code switch
            {
                "NotFound" => NotFound(clientDeleted.Error),
                "BadRequest" => BadRequest(clientDeleted.Error),
                "Internal" => StatusCode(StatusCodes.Status500InternalServerError, clientDeleted.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, clientDeleted.Error) // fallback para erro desconhecido
            };
        }

        return Ok(clientDeleted.Value);
    }

    private string ErroMoldeState()
    {
        var errorMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
        return errorMessage;
    }
}

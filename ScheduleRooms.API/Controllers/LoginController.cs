using Microsoft.AspNetCore.Mvc;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IGetTokenService getTokenService) : ControllerBase
    {
        private readonly IGetTokenService _getTokenService = getTokenService;

        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto login)
        {
            if (login is not null)
            {
                var token = await _getTokenService.Token(login);
                if (token != null)
                {
                    return Ok(token);
                }
            }
            return BadRequest();
        }
    }
}

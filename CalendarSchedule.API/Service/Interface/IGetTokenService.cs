using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IGetTokenService
{
    public Task<TokenDto> Token(LoginDto login);
}

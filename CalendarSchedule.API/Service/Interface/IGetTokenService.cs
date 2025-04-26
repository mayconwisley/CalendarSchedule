using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IGetTokenService
{
    public Task<Result<TokenDto>> Token(LoginDto login);
}

using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IGetTokenService
{
    public Task<TokenDto> Token(LoginDto login);
}

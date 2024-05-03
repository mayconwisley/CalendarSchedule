using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Service.Interface;

public interface ITokenStorageService
{
    Task<TokenDto> GetToken();
    Task<TokenDto> GetToken(LoginDto login);
    Task RemoverToken();
    Task<UserDto> GetUserSession();
    Task<UserDto> GetUserSession(LoginDto loginDto);
    Task RemoveUserSession();
}

using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Service.Interface;

public interface IUserStorageService
{
    Task<UserDto> GetUserSession();
    Task<UserDto> GetUserSession(LoginDto loginDto);
    Task RemoveUserSession();
}

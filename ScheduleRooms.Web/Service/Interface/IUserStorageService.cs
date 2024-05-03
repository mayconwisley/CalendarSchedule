using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Service.Interface;

public interface IUserStorageService
{
    Task<UserSessionDto> GetUserSession();
    Task<UserSessionDto> GetUserSession(LoginDto loginDto);
    Task RemoveUserSession();
}

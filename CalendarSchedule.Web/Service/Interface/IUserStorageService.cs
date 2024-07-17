using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserStorageService
{
    Task<UserSessionDto> GetUserSession();
    Task<UserSessionDto> GetUserSession(LoginDto loginDto);
    Task RemoveUserSession();
}

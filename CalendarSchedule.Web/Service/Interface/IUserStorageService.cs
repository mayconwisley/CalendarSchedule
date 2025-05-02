using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserStorageService
{
	Task<Result<UserSessionDto>> GetUserSession();
	Task<Result<UserSessionDto>> GetUserSession(LoginDto loginDto);
	Task RemoveUserSession();
}

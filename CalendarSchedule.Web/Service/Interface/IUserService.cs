using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserService
{
	Task<PagedResultView<UserDto>> GetAll(int page = 1, int size = 10, string search = "");
	Task<PagedResultView<UserDto>> GetManagerAll(int page = 1, int size = 10, string search = "");
	Task<PagedResultView<UserDto>> GetManagerAllByUserCurrent(int page = 1, int size = 10, string search = "", string username = "");
	Task<UserDto> GetManagerUsername(string username);
	Task<UserDto> GetById(int id);
	Task<UserDto> Create(UserDto userDto);
	Task<UserDto> Update(UserDto userDto);
	Task<bool> Delete(int id);
}

using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserService
{
    Task<UserView> GetAll(int page = 1, int size = 10, string search = "");
    Task<UserView> GetManagerAll(int page = 1, int size = 10, string search = "");
    Task<UserView> GetManagerAllByUserCurrent(int page = 1, int size = 10, string search = "", string username = "");
    Task<UserDto> GetManagerUsername(string username);
    Task<UserDto> GetById(int id);
    Task<UserDto> Create(UserDto userDto);
    Task<UserDto> Update(UserDto userDto);
    Task<bool> Delete(int id);
}

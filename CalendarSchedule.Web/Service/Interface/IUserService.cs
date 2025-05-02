using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserService
{
    Task<Result<PagedResultView<UserDto>>> GetAll(int page = 1, int size = 10, string search = "");
    Task<Result<PagedResultView<UserDto>>> GetManagerAll(int page = 1, int size = 10, string search = "");
    Task<Result<PagedResultView<UserDto>>> GetManagerAllByUserCurrent(int page = 1, int size = 10, string search = "", string username = "");
    Task<Result<UserDto>> GetManagerUsername(string username);
    Task<Result<UserDto>> GetById(int id);
    Task<Result<UserDto>> Create(UserCreateDto userCreateDto);
    Task<Result<UserDto>> Update(UserUpdateDto userUpdateDto);
    Task<Result<bool>> Delete(int id);
}

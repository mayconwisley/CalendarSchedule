using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IUserService
{
    Task<Result<PagedResult<UserDto>>> GetAll(int page, int size, string search);
    Task<Result<PagedResult<UserDto>>> GetManagerAll(int page, int size, string search);
    Task<Result<PagedResult<UserDto>>> GetManagerAllByUserCurrent(int page, int size, string search, string username);
    Task<Result<UserDto>> GetById(int id);
    Task<Result<UserDto>> GetManagerUsername(string username);
    Task<Result<bool>> IsPassword(LoginDto login);
    Task<Result<bool>> IsUsername(string username);
    Task<Result<UserDto>> Create(UserDto userDto);
    Task<Result<UserDto>> Update(UserDto userDto);
    Task<Result<UserDto>> Delete(int id);
    Task<Result<int>> TotalUsers(string search);
}

using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAll(int page, int size, string search);
    Task<Result<IEnumerable<UserDto>>> GetManagerAll(int page, int size, string search);
    Task<Result<IEnumerable<UserDto>>> GetManagerAllByUserCurrent(int page, int size, string search, string username);
    Task<Result<UserDto>> GetById(int id);
    Task<Result<UserDto>> GetManagerUsername(string username);
    Task<Result<bool>> GetPassword(LoginDto login);
    Task<Result<UserDto>> Create(UserDto userDto);
    Task<Result<UserDto>> Update(UserDto userDto);
    Task<Result> Delete(int id);
    Task<Result<int>> TotalUsers(string search);
}

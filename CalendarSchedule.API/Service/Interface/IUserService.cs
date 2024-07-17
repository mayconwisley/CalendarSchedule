using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<UserDto>> GetManagerAll(int page, int size, string search);
    Task<IEnumerable<UserDto>> GetManagerAllByUserCurrent(int page, int size, string search, string username);
    Task<UserDto> GetById(int id);
    Task<UserDto> GetManagerUsername(string username);
    Task<bool> GetPassword(LoginDto login);
    Task<UserDto> Create(UserDto userDto);
    Task Update(UserDto userDto);
    Task Delete(int id);
    Task<int> TotalUsers(string search);
}

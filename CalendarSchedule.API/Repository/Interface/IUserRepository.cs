using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll(int page, int size, string search);
    Task<IEnumerable<User>> GetManagerAll(int page, int size, string search);
    Task<IEnumerable<User>> GetManagerAllByUserCurrent(int page, int size, string search, string username);
    Task<User?> GetById(int id);
    Task<User?> GetManagerUsername(string username);
    Task<string?> GetPassword(LoginApi login);
    Task<bool> IsUsername(string username);
    Task<User> Create(User user);
    Task<User?> Update(User user);
    Task<User?> Delete(int id);
    Task<int> TotalUser(string search);
}

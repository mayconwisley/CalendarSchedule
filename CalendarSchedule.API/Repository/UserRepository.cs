using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class UserRepository(ScheduleContext _scheduleContext) : IUserRepository
{
    public async Task<User> Create(User user)
    {
        _scheduleContext.Users.Add(user);
        await _scheduleContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Delete(int id)
    {
        var listUser = await GetAll(1, 15, "");
        var countManager = listUser
                        .Count(w => w.Manager == true);
        var countUser = listUser
                        .Count(w => w.Manager == false);

        if (countManager == 1 && countUser == 0)
        {
            throw new ArgumentException("Não é possivel excluir o ultimo usuário Gestor");
        }

        var user = await GetById(id);
        if (user is null)
            return null;

        _scheduleContext.Users.Remove(user);
        await _scheduleContext.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAll(int page, int size, string search)
    {
        var users =
        await _scheduleContext.Users
              .AsNoTracking()
              .OrderBy(o => o.Name)
              .Skip((page - 1) * size)
              .Take(size)
              .ToListAsync();
        return users;
    }

    public async Task<User?> GetById(int id)
    {
        var user =
        await _scheduleContext.Users
              .FirstOrDefaultAsync(w => w.Id == id);
        return user;
    }

    public async Task<IEnumerable<User>> GetManagerAll(int page, int size, string search)
    {
        var users =
        await _scheduleContext.Users
              .AsNoTracking()
              .Where(w => w.Manager == true)
              .Skip((page - 1) * size)
              .Take(size)
              .OrderBy(o => o.Name)
              .ToListAsync();
        return users;
    }

    public async Task<IEnumerable<User>> GetManagerAllByUserCurrent(int page, int size, string search, string username)
    {
        IEnumerable<User> users = await _scheduleContext.Users
              .Where(w => w.Username!.Equals(username, StringComparison.OrdinalIgnoreCase))
              .Skip((page - 1) * size)
              .Take(size)
              .ToListAsync();

        IEnumerable<User> usersManager = await _scheduleContext.Users
            .Where(w => w.Manager == true)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        users = users
                .Union(usersManager)
                .OrderBy(o => o.Name);

        return users;
    }

    public async Task<User?> GetManagerUsername(string username)
    {
        var user =
        await _scheduleContext.Users
              .FirstOrDefaultAsync(w => w.Username!.Equals(username, StringComparison.OrdinalIgnoreCase));

        return user;
    }

    public async Task<string> GetPassword(LoginApi login)
    {
        var password =
        await _scheduleContext.Users
              .Where(w => w.Username == login.Username)
              .Select(w => w.Password)
              .FirstOrDefaultAsync();

        if (password is not null)
        {
            return password;
        }
        return string.Empty;
    }

    public async Task<int> TotalUser(string search)
    {
        var totalUser =
        await _scheduleContext.Users
              .AsNoTracking()
              .CountAsync(w => w.Name!.Contains(search));
        return totalUser;
    }

    public async Task<User?> Update(User user)
    {
        var existingUser = await GetById(user.Id);
        if (existingUser is null)
            return null;

        _scheduleContext.Users.Entry(existingUser).CurrentValues.SetValues(user);
        await _scheduleContext.SaveChangesAsync();
        return user;
    }
}

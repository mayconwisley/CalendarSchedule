﻿using Microsoft.EntityFrameworkCore;
using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;

namespace CalendarSchedule.API.Repository;

public class UserRepository(ScheduleContext scheduleContext) : IUserRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<User> Create(User user)
    {
        try
        {
            if (user is not null)
            {
                _scheduleContext.Users.Add(user);
                await _scheduleContext.SaveChangesAsync();
                return user;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> Delete(int id)
    {
        try
        {
            var listUser = await GetAll(1, 15, "");
            var countManager = listUser
                            .Where(w => w.Manager == true)
                            .Count();
            var countUser = listUser
                            .Where(w => w.Manager == false)
                            .Count();

            if (countManager == 1 && countUser == 0)
            {
                throw new Exception("Não é possivel excluir o ultimo usuário Gestor");
            }


            var user = await GetById(id);
            if (user is not null)
            {
                _scheduleContext.Users.Remove(user);
                await _scheduleContext.SaveChangesAsync();
                return user;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAll(int page, int size, string search)
    {
        try
        {
            var users = await _scheduleContext.Users
                .OrderBy(o => o.Name)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> GetById(int id)
    {
        try
        {
            var user = await _scheduleContext.Users
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (user is not null)
            {
                return user;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetManagerAll(int page, int size, string search)
    {
        try
        {
            var users = await _scheduleContext.Users
                .Where(w => w.Manager == true)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Name)
                .ToListAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetManagerAllByUserCurrent(int page, int size, string search, string username)
    {
        try
        {
            IEnumerable<User> users = await _scheduleContext.Users
                  .Where(w => w.Username.ToUpper() == username.ToUpper())
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
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> GetManagerUsername(string username)
    {
        try
        {
            var user = await _scheduleContext.Users
                .Where(w => w.Username.ToUpper() == username.ToUpper())
                .FirstOrDefaultAsync();

            if (user is not null)
            {
                return user;
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> GetPassword(LoginApi login)
    {
        var password = await _scheduleContext.Users
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
        var totalUser = await _scheduleContext.Users
            .Where(w => w.Name!.Contains(search))
            .CountAsync();
        return totalUser;
    }

    public async Task<User> Update(User user)
    {
        try
        {
            if (user is not null)
            {
                _scheduleContext.Users.Entry(user).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return user;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

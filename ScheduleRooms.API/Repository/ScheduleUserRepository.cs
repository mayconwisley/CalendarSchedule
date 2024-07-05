using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class ScheduleUserRepository(ScheduleContext scheduleContext) : IScheduleUserRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<ScheduleUser> Create(ScheduleUser scheduleUser)
    {
        try
        {
            if (scheduleUser is not null)
            {
                _scheduleContext.ScheduleUsers.Add(scheduleUser);
                await _scheduleContext.SaveChangesAsync();

                scheduleUser = await GetById(scheduleUser.UserId, scheduleUser.ScheduleId);

                return scheduleUser;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ScheduleUser> Delete(int scheduleId, int userId)
    {
        try
        {
            var scheduleUsers = await GetById(scheduleId, userId);

            if (scheduleUsers is not null)
            {
                _scheduleContext.ScheduleUsers.RemoveRange(scheduleUsers);
                await _scheduleContext.SaveChangesAsync();
                return scheduleUsers;
            }

            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUser>> GetAll(int page, int size, string search)
    {
        try
        {
            var scheduleUsers = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                .Include(i => i.Schedule.Client)
                .OrderByDescending(o => o.ScheduleId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return scheduleUsers;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUser>> GetByDateStart(int page, int size, DateTime dateStart)
    {
        try
        {
            var scheduleUsers = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                  .Include(i => i.Schedule.Client)
                .OrderByDescending(o => o.Schedule.DateFinal)
                .Skip((page - 1) * size)
                .Take(size)
                .Where(w => w.Schedule.DateStart.Date == dateStart.Date)
                .ToListAsync();

            if (scheduleUsers is not null)
            {
                return scheduleUsers;
            }

            return [];
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ScheduleUser> GetById(int scheduleId, int userId)
    {
        try
        {
            var scheduleUser = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                .Include(i => i.Schedule.Client)
                .Where(w => w.ScheduleId == scheduleId &&
                            w.UserId == userId)
                .FirstOrDefaultAsync();

            if (scheduleUser is not null)
            {
                return scheduleUser;
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleId(int scheduleId)
    {
        try
        {
            var scheduleUsers = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                  .Include(i => i.Schedule.Client)
                .Where(w => w.ScheduleId == scheduleId)
                .ToListAsync();
            return scheduleUsers;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalScheduleUser(string search)
    {
        var totalScheduleUser = await _scheduleContext.ScheduleUsers
                .Where(w => w.Schedule.DateStart.Date.ToString().Contains(search))
                .CountAsync();
        return totalScheduleUser;
    }

    public async Task<ScheduleUser> Update(ScheduleUser scheduleUser)
    {
        try
        {
            if (scheduleUser is not null)
            {
                _scheduleContext.ScheduleUsers.Entry(scheduleUser).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return scheduleUser;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

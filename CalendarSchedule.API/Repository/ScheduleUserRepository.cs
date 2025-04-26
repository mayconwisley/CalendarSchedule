using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ScheduleUserRepository(ScheduleContext _scheduleContext) : IScheduleUserRepository
{
    public async Task<ScheduleUser> Create(ScheduleUser scheduleUser)
    {
        _scheduleContext.ScheduleUsers.Add(scheduleUser);
        await _scheduleContext.SaveChangesAsync();
        return scheduleUser;

    }
    public async Task<ScheduleUser?> Delete(int scheduleId, int userId)
    {
        var scheduleUsers = await GetById(scheduleId, userId);
        if (scheduleUsers is null)
            return null;

        _scheduleContext.ScheduleUsers.Remove(scheduleUsers);
        await _scheduleContext.SaveChangesAsync();
        return scheduleUsers;
    }
    public async Task<IEnumerable<ScheduleUser>?> GetAll(int page, int size, string search)
    {
        var scheduleUsers =
        await _scheduleContext.ScheduleUsers
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Schedule)
                    .ThenInclude(t => t!.Client)
                .OrderByDescending(o => o.ScheduleId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

        return scheduleUsers;

    }

    public async Task<IEnumerable<ScheduleUser>?> GetByDatePeriod(DateTime dateStart, DateTime dateEnd)
    {
        var scheduleUsers =
        await _scheduleContext.ScheduleUsers
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Schedule)
                    .ThenInclude(t => t!.Client)
                .Where(w => w.Schedule != null &&
                            w.Schedule.DateStart.Date >= dateStart.Date &&
                            w.Schedule.DateStart.Date <= dateEnd.Date)
                .OrderByDescending(o => o.Schedule!.DateFinal)
                .ToListAsync();

        return scheduleUsers;
    }

    public async Task<IEnumerable<ScheduleUser>?> GetByDateStart(DateTime dateStart)
    {
        var scheduleUsers =
        await _scheduleContext.ScheduleUsers
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Schedule)
                    .ThenInclude(t => t!.Client)
                .OrderByDescending(o => o.Schedule!.DateFinal)
                .Where(w => w.Schedule!.DateStart.Date == dateStart.Date)
                .ToListAsync();
        return scheduleUsers;
    }

    public async Task<ScheduleUser?> GetById(int scheduleId, int userId)
    {
        var scheduleUser =
        await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                    .ThenInclude(t => t!.Client)
                .Where(w => w.ScheduleId == scheduleId &&
                            w.UserId == userId)
                .FirstOrDefaultAsync();

        return scheduleUser;
    }
    public async Task<IEnumerable<ScheduleUser>?> GetByScheduleId(int scheduleId)
    {
        var scheduleUsers =
        await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Schedule)
                   .ThenInclude(t => t!.Client)
                .Where(w => w.ScheduleId == scheduleId)
                .ToListAsync();

        return scheduleUsers;
    }
    public async Task<int> TotalScheduleUser(string search)
    {
        if (DateTime.TryParse(search, out var searchDate))
        {
            return await _scheduleContext.ScheduleUsers
                .AsNoTracking()
                .CountAsync(w => w.Schedule!.DateStart.Date == searchDate.Date);
        }

        return 0;
    }
    public async Task<ScheduleUser?> Update(ScheduleUser scheduleUser)
    {
        var existingScheduleUser = await GetById(scheduleUser.ScheduleId, scheduleUser.UserId);
        if (existingScheduleUser is null)
            return null;

        _scheduleContext.ScheduleUsers.Entry(existingScheduleUser).CurrentValues.SetValues(scheduleUser);
        await _scheduleContext.SaveChangesAsync();
        return existingScheduleUser;
    }
}

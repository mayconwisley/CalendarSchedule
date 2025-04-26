using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ScheduleRepository(ScheduleContext _scheduleContext) : IScheduleRepository
{
    public async Task<Schedule> Create(Schedule schedule)
    {
        // Verifique se existe uma schedule que se sobrepõe no mesmo usuário
        var overlappingSchedule =
        await _scheduleContext.Schedules
                .Where(a => a.DateStart < schedule.DateFinal &&
                            a.DateFinal > schedule.DateStart &&
                            a.ClientId == schedule.ClientId &&
                            a.Particular == false)
                .AnyAsync();
        if (overlappingSchedule)
        {
            throw new InvalidOperationException("Horário conflitante.");
        }

        var minorDate = schedule.DateStart >= schedule.DateFinal;

        if (minorDate)
        {
            throw new InvalidOperationException("Horário conflitante.");
        }

        _scheduleContext.Schedules.Add(schedule);
        await _scheduleContext.SaveChangesAsync();

        return schedule;

    }
    public async Task<Schedule?> Delete(int id)
    {
        var schedule = await GetById(id);
        if (schedule is null)
            return null;

        _scheduleContext.Schedules.Remove(schedule);
        await _scheduleContext.SaveChangesAsync();
        return schedule;

    }
    public async Task<IEnumerable<Schedule>?> GetAll(int page, int size, string search)
    {
        var schedules =
        await _scheduleContext.Schedules
                .Include(i => i.Client)
                .Include(i => i.ScheduleUsers)
                .Where(w => w.Description!.Contains(search))
                .OrderByDescending(o => o.DateFinal)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

        return schedules;
    }
    public async Task<Schedule?> GetById(int id)
    {
        var schedule =
        await _scheduleContext.Schedules
                .Include(i => i.Client)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

        return schedule;
    }
    public async Task<int> TotalSchedules(string search)
    {
        var totalSchedule =
        await _scheduleContext.Schedules
                .Where(w => w.Description!.Contains(search))
                .CountAsync();

        return totalSchedule;
    }
    public async Task<Schedule?> Update(Schedule schedule)
    {
        var existingSchedule = await GetById(schedule.Id);
        if (existingSchedule is null)
            return null;

        _scheduleContext.Schedules.Entry(existingSchedule).CurrentValues.SetValues(schedule);
        await _scheduleContext.SaveChangesAsync();
        return existingSchedule;
    }
}


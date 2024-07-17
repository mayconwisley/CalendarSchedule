using Microsoft.EntityFrameworkCore;
using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;

namespace CalendarSchedule.API.Repository;

public class ScheduleRepository(ScheduleContext scheduleContext) : IScheduleRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<Schedule> Create(Schedule schedule)
    {
        try
        {
            if (schedule is not null)
            {
                // Verifique se existe uma schedule que se sobrepõe no mesmo usuário
                var overlappingSchedule = await _scheduleContext.Schedules
                    .Where(a => a.DateStart < schedule.DateFinal &&
                                a.DateFinal > schedule.DateStart &&
                                a.ClientId == schedule.ClientId &&
                                a.Particular == false)
                    .AnyAsync();
                if (overlappingSchedule)
                {
                    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                    throw new Exception("409");
                }

                var minorDate = schedule.DateStart >= schedule.DateFinal;

                if (minorDate)
                {
                    throw new Exception("400");
                }


                _scheduleContext.Schedules.Add(schedule);
                await _scheduleContext.SaveChangesAsync();

                return schedule;
            }
            return new();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Schedule> Delete(int id)
    {
        try
        {
            var schedule = await GetById(id);

            if (schedule is not null)
            {
                _scheduleContext.Schedules.Remove(schedule);
                await _scheduleContext.SaveChangesAsync();
                return schedule;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<IEnumerable<Schedule>> GetAll(int page, int size, string search)
    {
        try
        {
            var schedules = await _scheduleContext.Schedules
                .Include(i => i.Client)
                .Include(i => i.ScheduleUsers)
                .OrderByDescending(o => o.DateFinal)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return schedules;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Schedule> GetById(int id)
    {
        try
        {
            var schedule = await _scheduleContext.Schedules
                .Include(i => i.Client)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (schedule is not null)
            {
                return schedule;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<int> TotalSchedules(string search)
    {
        var totalSchedule = await _scheduleContext.Schedules
            .Where(w => w!.Description!.Contains(search))
            .CountAsync();

        return totalSchedule;
    }
    public async Task<Schedule> Update(Schedule schedule)
    {
        try
        {
            if (schedule is not null)
            {

                //// Verifique se existe uma schedule que se sobrepõe na mesma room
                //var overlappingSchedule = await _scheduleContext.Schedules
                //         .Where(a => a.UserId == schedule.UserId &&
                //                a.ClientId == schedule.ClientId &&
                //                a.DateStart < schedule.DateFinal &&
                //                a.DateFinal > schedule.DateStart)
                //    .ToListAsync();

                //if (overlappingSchedule.Count() > 0)
                //{
                //    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                //    throw new Exception("A atualização resultaria em uma sobreposição de datas para esta room.");
                //}

                _scheduleContext.Schedules.Entry(schedule).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return schedule;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

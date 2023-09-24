using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ScheduleRooms.API.Repository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ScheduleContext _scheduleContext;

    public ScheduleRepository(ScheduleContext scheduleContext)
    {
        _scheduleContext = scheduleContext;
    }

    public async Task<Schedule> Create(Schedule schedule)
    {
        try
        {
            if (schedule is not null)
            {

                // Verifique se existe uma schedule que se sobrepõe na mesma room
                var overlappingAgendas = await _scheduleContext.Schedules
                    .Where(a => a.RoomId == schedule.RoomId &&
                                a.DateStart < schedule.DateFinal &&
                                a.DateFinal > schedule.DateStart)
                    .ToListAsync();

                if (overlappingAgendas.Count > 0)
                {
                    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                    throw new Exception("409");
                }
                _scheduleContext.Schedules.Add(schedule);
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

    public async Task<Schedule> Delete(int id)
    {
        try
        {
            var schedule = await GetById(id);

            if (schedule is not null)
            {
                _scheduleContext.Remove(schedule);
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
                .Include(i => i.Room)
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

    public async Task<IEnumerable<Schedule>> GetByAgendaActive()
    {
        try
        {
            var schedules = await _scheduleContext.Schedules
                .Include(i => i.Room)
                .Where(w => w.DateFinal >= DateTime.Now)
                .OrderBy(o => o.DateFinal)
                .ToListAsync();

            return schedules;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Schedule>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.Schedules
                .Include(i => i.Room)
                .Where(w => w.DateFinal >= DateTime.Now &&
                            w.DateFinal.Date == dateSalected.Date &&
                            w.RoomId == roomId)
                .OrderBy(o => o.DateStart)
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
                .Include(i => i.Room)
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
        var totalAgendas = await _scheduleContext.Schedules
            .Where(w => w!.Description!.Contains(search))
            .CountAsync();

        return totalAgendas;

    }

    public async Task<Schedule> Update(Schedule schedule)
    {
        try
        {
            if (schedule is not null)
            {

                // Verifique se existe uma schedule que se sobrepõe na mesma room
                var overlappingAgendas = await _scheduleContext.Schedules
                    .Where(a => a.RoomId == schedule.RoomId &&
                                a.Id != schedule.Id && // Exclua a própria schedule da verificação
                                a.DateStart < schedule.DateFinal &&
                                a.DateFinal > schedule.DateStart)
                    .ToListAsync();

                if (overlappingAgendas.Count > 0)
                {
                    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                    throw new Exception("A atualização resultaria em uma sobreposição de datas para esta room.");
                }

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

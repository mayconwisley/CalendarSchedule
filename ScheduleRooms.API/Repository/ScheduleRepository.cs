using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

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

    public async Task<IEnumerable<Schedule>> GetBySchedule()
    {
        try
        {
            var schedules = await (
                        from s in _scheduleContext.Schedules
                        join c in _scheduleContext.Clients on s.ClientId equals c.Id into sc
                        from c in sc.DefaultIfEmpty()
                        select new Schedule
                        {
                            Id = s.Id,

                            ClientId = s.ClientId,
                            DateStart = s.DateStart,
                            DateFinal = s.DateFinal,
                            Description = s.Description,

                            MeetingType = s.MeetingType,
                            Particular = s.Particular,
                            StatusSchedule = s.StatusSchedule,

                            Client = c == null ? null : new Client
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Active = c.Active,
                                City = c.City,
                                Description = c.Description,

                                Telephone = c.Telephone,
                            }


                        })
                .OrderBy(o => o.DateFinal)
                .ToListAsync();

            return schedules;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Schedule>> GetByScheduleActive()
    {
        try
        {
            var schedules = await _scheduleContext.Schedules
                .Include(i => i.Client)

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

    public async Task<IEnumerable<Schedule>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.Schedules

                .Include(i => i.Client)
                .Where(w => w.DateFinal >= DateTime.Now &&
                            w.DateFinal.Date == dateSalected.Date &&
                            w.ClientId == clientId)
                .OrderBy(o => o.DateStart)
                .ToListAsync();

            return schedules;
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

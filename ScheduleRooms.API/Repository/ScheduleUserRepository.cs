using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class ScheduleUserRepository(ScheduleContext scheduleContext) : IScheduleUserRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<ScheduleUser> Create(ScheduleUser schedulesUser)
    {
        try
        {
            if (schedulesUser is not null)
            {
                // Verifique se existe uma schedulesUser que se sobrepõe no mesmo usuário
                var overlappingScheduleUser = await _scheduleContext.ScheduleUsers
                    .Where(a => a.UserId == schedulesUser.UserId &&
                                a.ClientId == schedulesUser.ClientId &&
                                a.DateStart < schedulesUser.DateFinal &&
                                a.DateFinal > schedulesUser.DateStart)
                    .ToListAsync();

                if (overlappingScheduleUser.Count() > 0)
                {
                    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                    throw new Exception("409");
                }

                var minorDate = schedulesUser.DateStart >= schedulesUser.DateFinal;

                if (minorDate)
                {
                    throw new Exception("400");
                }


                _scheduleContext.ScheduleUsers.Add(schedulesUser);
                await _scheduleContext.SaveChangesAsync();
                return schedulesUser;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ScheduleUser> Delete(int id)
    {
        try
        {
            var schedulesUser = await GetById(id);

            if (schedulesUser is not null)
            {
                _scheduleContext.ScheduleUsers.Remove(schedulesUser);
                await _scheduleContext.SaveChangesAsync();
                return schedulesUser;
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
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.Client)
                .Include(i => i.User)
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

    public async Task<ScheduleUser> GetById(int id)
    {
        try
        {
            var schedule = await _scheduleContext.ScheduleUsers
                .Include(i => i.Client)
                .Include(i => i.User)
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleActive()
    {
        try
        {
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.Client)
                .Include(i => i.User)
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleActiveClientIdUserId(int clientId, int userId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Client)
                .Where(w => w.DateFinal >= DateTime.Now &&
                            w.DateFinal.Date == dateSalected.Date &&
                            w.UserId == userId &&
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleActiveUserId(int userId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Client)
                .Where(w => w.DateFinal >= DateTime.Now &&
                            w.DateFinal.Date == dateSalected.Date &&
                            w.UserId == userId)
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
        var totalSchedule = await _scheduleContext.ScheduleUsers
            .Where(w => w!.Description!.Contains(search))
            .CountAsync();

        return totalSchedule;
    }

    public async Task<ScheduleUser> Update(ScheduleUser schedulesUser)
    {
        try
        {
            if (schedulesUser is not null)
            {

                // Verifique se existe uma schedule que se sobrepõe na mesma room
                var overlappingSchedule = await _scheduleContext.ScheduleUsers
                         .Where(a => a.UserId == schedulesUser.UserId &&
                                a.ClientId == schedulesUser.ClientId &&
                                a.DateStart < schedulesUser.DateFinal &&
                                a.DateFinal > schedulesUser.DateStart)
                    .ToListAsync();

                if (overlappingSchedule.Count() > 0)
                {
                    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                    throw new Exception("A atualização resultaria em uma sobreposição de datas para esta room.");
                }

                _scheduleContext.ScheduleUsers.Entry(schedulesUser).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return schedulesUser;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

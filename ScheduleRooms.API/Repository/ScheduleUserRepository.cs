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
                                a.DateStart < schedulesUser.DateFinal &&
                                a.DateFinal > schedulesUser.DateStart)
                    .AnyAsync();
                if (overlappingScheduleUser)
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

    public async Task<IEnumerable<ScheduleUser>> GetBySchedule()
    {
        try
        {
            var schedules = await (
                        from s in _scheduleContext.ScheduleUsers
                        join u in _scheduleContext.Users on s.UserId equals u.Id
                        join c in _scheduleContext.Clients on s.ClientId equals c.Id into sc
                        from c in sc.DefaultIfEmpty()
                        join m in _scheduleContext.Users on s.ManagerId equals m.Id into sm
                        from m in sm.DefaultIfEmpty()
                        select new ScheduleUser
                        {
                            Id = s.Id,
                            UserId = s.UserId,
                            ClientId = s.ClientId,
                            DateStart = s.DateStart,
                            DateFinal = s.DateFinal,
                            Description = s.Description,
                            ManagerId = s.ManagerId,
                            MeetingType = s.MeetingType,
                            Particular = s.Particular,
                            StatusSchedule = s.StatusSchedule,
                            Manager = m.Name,
                            Client = c == null ? null : new Client
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Active = c.Active,
                                City = c.City,
                                Description = c.Description,
                                Email = c.Email,
                                Position = c.Position,
                                Prospection = c.Prospection,
                                Responsible = c.Responsible,
                                Telephone = c.Telephone,
                            },
                            User = new User
                            {
                                Id = u.Id,
                                Name = u.Name,
                                Description = u.Description,
                                Cellphone = u.Cellphone,
                                Active = u.Active,
                                Manager = u.Manager,
                                Username = u.Username

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
                .Where(w => w.DateFinal.Date == dateSalected.Date &&
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleDateUserId(int userId, DateTime dateSalected)
    {
        try
        {
            var schedules = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Client)
                .Where(w => w.DateFinal.Date == dateSalected.Date &&
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

    public async Task<IEnumerable<ScheduleUser>> GetByScheduleUserId(int userId)
    {
        try
        {
            var schedule = await _scheduleContext.ScheduleUsers
                .Include(i => i.User)
                .Include(i => i.Client)
                .Where(w => w.UserId == userId)
                .OrderBy(o => o.User)
                .ToListAsync();

            return schedule;
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

                //// Verifique se existe uma schedule que se sobrepõe na mesma room
                //var overlappingSchedule = await _scheduleContext.ScheduleUsers
                //         .Where(a => a.UserId == schedulesUser.UserId &&
                //                a.ClientId == schedulesUser.ClientId &&
                //                a.DateStart < schedulesUser.DateFinal &&
                //                a.DateFinal > schedulesUser.DateStart)
                //    .ToListAsync();

                //if (overlappingSchedule.Count() > 0)
                //{
                //    // Existe sobreposição, faça algo aqui, como lançar uma exceção.
                //    throw new Exception("A atualização resultaria em uma sobreposição de datas para esta room.");
                //}

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

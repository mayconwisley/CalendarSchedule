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
				.Where(w => w.User.Name.Contains(search) ||
									 w.Schedule.Client.Name.Contains(search) ||
									 w.Schedule.Description.Contains(search))
				.OrderByDescending(o => o.ScheduleId)
				.Skip((page - 1) * size)
				.Take(size)
				.ToListAsync();

		return scheduleUsers;

	}

	public async Task<IEnumerable<ScheduleUser>?> GetByDatePeriod(int page, int size, string search, DateTime dateStart, DateTime dateEnd)
	{
		var scheduleUsers = await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.Schedule != null &&
							w.Schedule.DateStart.Date >= dateStart.Date &&
							w.Schedule.DateStart.Date <= dateEnd.Date &&
							(w.User.Name.Contains(search) ||
							 w.Schedule.Client.Name.Contains(search) ||
							 w.Schedule.Description.Contains(search)))
			.OrderByDescending(o => o.Schedule!.DateFinal)
			.Skip((page - 1) * size)
			.Take(size)
			.ToListAsync();

		return scheduleUsers;
	}

	public async Task<IEnumerable<ScheduleUser>?> GetByDateStart(int page, int size, string search, DateTime dateStart)
	{
		var scheduleUsers = await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.Schedule!.DateStart.Date == dateStart.Date &&
								(w.User.Name.Contains(search) ||
								w.Schedule.Client.Name.Contains(search) ||
								w.Schedule.Description.Contains(search)))
			.OrderByDescending(o => o.Schedule!.DateFinal)
			.Skip((page - 1) * size)
			.Take(size)
			.ToListAsync();
		return scheduleUsers;
	}

	public async Task<ScheduleUser?> GetById(int scheduleId, int userId)
	{
		var scheduleUser = await _scheduleContext.ScheduleUsers
			.AsSingleQuery()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.ScheduleId == scheduleId &&
								w.UserId == userId)
			.FirstOrDefaultAsync();

		return scheduleUser;
	}
	public async Task<IEnumerable<ScheduleUser>?> GetByScheduleId(int page, int size, string search, int scheduleId)
	{
		var scheduleUsers = await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
			   .ThenInclude(t => t!.Client)
			.Where(w => w.ScheduleId == scheduleId &&
								(w.Schedule.Client.Name.Contains(search) ||
								w.User.Name.Contains(search) ||
								w.Schedule.Description.Contains(search)))
			.ToListAsync();

		return scheduleUsers;
	}
	public async Task<int> TotalScheduleUser(string search)
	{
		return await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.User.Name.Contains(search) ||
								 w.Schedule.Client.Name.Contains(search))
			.CountAsync();
	}

	public async Task<int> TotalScheduleUser(int scheduleId, string search)
	{
		return await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.ScheduleId == scheduleId &&
								 (w.User.Name.Contains(search) ||
								 w.Schedule.Client.Name.Contains(search) ||
								 w.Schedule.Description.Contains(search)))
			.CountAsync();
	}

	public async Task<int> TotalScheduleUser(DateTime dateStart, DateTime dateEnd, string search)
	{
		return await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.Schedule != null &&
							w.Schedule.DateStart.Date >= dateStart.Date &&
							w.Schedule.DateStart.Date <= dateEnd.Date &&
							(w.User.Name.Contains(search) ||
							 w.Schedule.Client.Name.Contains(search) ||
							 w.Schedule.Description.Contains(search)))
			.CountAsync();
	}

	public async Task<int> TotalScheduleUser(DateTime dateStart, string search)
	{
		return await _scheduleContext.ScheduleUsers
			.AsNoTracking()
			.AsQueryable()
			.Include(i => i.User)
			.Include(i => i.Schedule)
				.ThenInclude(t => t!.Client)
			.Where(w => w.Schedule != null &&
							w.Schedule.DateStart.Date >= dateStart.Date &&
							(w.User.Name.Contains(search) ||
							 w.Schedule.Client.Name.Contains(search) ||
							 w.Schedule.Description.Contains(search)))
			.CountAsync();
	}
}

using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class UserContactRepository(ScheduleContext scheduleContext) : IUserContactRepository
{
	private readonly ScheduleContext _scheduleContext = scheduleContext;

	public async Task<UserContact> Create(UserContact userContact)
	{
		_scheduleContext.UserContacts.Add(userContact);
		await _scheduleContext.SaveChangesAsync();
		return userContact;
	}

	public async Task<UserContact?> Delete(int id)
	{
		var userContact = await GetById(id);
		if (userContact is null)
			return null;

		_scheduleContext.UserContacts.Remove(userContact);
		await _scheduleContext.SaveChangesAsync();
		return userContact;
	}

	public async Task<IEnumerable<UserContact>> GetAll(int page, int size, string search)
	{
		var userContacts =
		await _scheduleContext.UserContacts
			  .Include(i => i.User)
			  .Where(w => w.Type.Contains(search) ||
								 w.Number.Contains(search) ||
								 w.User.Name.Contains(search))
			  .OrderBy(o => o!.User!.Name)
			  .Skip((page - 1) * size)
			  .Take(size)
			  .ToListAsync();
		return userContacts;
	}

	public async Task<UserContact?> GetById(int id)
	{
		var userContact =
		await _scheduleContext.UserContacts
				.Include(i => i.User)
				.Where(o => o.Id == id)
				.FirstOrDefaultAsync();
		return userContact;
	}

	public async Task<IEnumerable<UserContact>> GetByUserId(int page, int size, int userId)
	{
		var userContacts =
		await _scheduleContext.UserContacts
				.Include(i => i.User)
				.Where(w => w.UserId.Equals(userId))
				.OrderBy(o => o!.User!.Name)
				.Skip((page - 1) * size)
				.Take(size)
				.ToListAsync();
		return userContacts;
	}

	public async Task<int> TotalUserContact(string search)
	{
		var totalUserContact =
		await _scheduleContext.UserContacts
			 .Include(i => i.User)
			 .CountAsync(w => w.Type.Contains(search) ||
								 w.Number.Contains(search) ||
								 w.User.Name.Contains(search));
		return totalUserContact;
	}

	public async Task<UserContact?> Update(UserContact userContact)
	{
		var existingUserContact = await GetById(userContact.Id);
		if (existingUserContact is null)
			return null;

		_scheduleContext.UserContacts.Entry(existingUserContact).CurrentValues.SetValues(userContact);
		await _scheduleContext.SaveChangesAsync();
		return existingUserContact;
	}
}

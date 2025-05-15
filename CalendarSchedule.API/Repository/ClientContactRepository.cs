using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ClientContactRepository(ScheduleContext _scheduleContext) : IClientContactRepository
{
	public async Task<ClientContact> Create(ClientContact clientContact)
	{
		_scheduleContext.ClientContacts.Add(clientContact);
		await _scheduleContext.SaveChangesAsync();
		return clientContact;
	}

	public async Task<ClientContact?> Delete(int id)
	{
		var clientContact = await GetById(id);

		if (clientContact == null)
			return null;

		_scheduleContext.ClientContacts.Remove(clientContact);
		await _scheduleContext.SaveChangesAsync();
		return clientContact;
	}

	public async Task<IEnumerable<ClientContact>?> GetAll(int page, int size, string search)
	{
		var clientContacts =
		await _scheduleContext.ClientContacts
				.AsNoTracking()
				.Include(i => i.Client)
				.Include(i => i.ClientResponsible)
				.Where(w => w.Client!.Name!.Contains(search) ||
							w.ClientResponsible!.Name!.Contains(search))
				.OrderBy(o => o.Client!.Name)
				.Skip((page - 1) * size)
				.Take(size)
				.ToListAsync();
		return clientContacts;
	}

	public async Task<IEnumerable<ClientContact>?> GetByClientId(int page, int size, int clientId)
	{
		var clientContacts =
		await _scheduleContext.ClientContacts
				.AsNoTracking()
				.Include(i => i.Client)
				.Include(i => i.ClientResponsible)
				.Where(o => o.ClientId == clientId)
				.ToListAsync();
		return clientContacts;
	}

	public async Task<ClientContact?> GetById(int id)
	{
		var clientContact =
		await _scheduleContext.ClientContacts
				.Include(i => i.Client)
				.Include(i => i.ClientResponsible)
				.Where(o => o.Id == id)
				.FirstOrDefaultAsync();
		return clientContact;
	}

	public async Task<int> TotalClientContact(string search)
	{
		var totalClientContact =
		await _scheduleContext.ClientContacts
			  .CountAsync(w => w.Client!.Name!.Contains(search) ||
							w.ClientResponsible!.Name!.Contains(search) ||
							w.ClientId.ToString().Contains(search));
		return totalClientContact;
	}

	public async Task<ClientContact?> Update(ClientContact clientContact)
	{
		var existingClientContact = await GetById(clientContact.Id);
		if (existingClientContact == null)
			return null;

		_scheduleContext.ClientContacts.Entry(existingClientContact).CurrentValues.SetValues(clientContact);
		await _scheduleContext.SaveChangesAsync();
		return existingClientContact;
	}
}

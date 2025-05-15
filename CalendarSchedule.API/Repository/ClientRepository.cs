using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ClientRepository(ScheduleContext _scheduleContext) : IClientRepository
{
	public async Task<Client> Create(Client client)
	{
		_scheduleContext.Clients.Add(client);
		await _scheduleContext.SaveChangesAsync();
		return client;
	}

	public async Task<Client?> Delete(int id)
	{
		var client = await GetById(id);
		if (client == null)
			return null;

		_scheduleContext.Clients.Remove(client);
		await _scheduleContext.SaveChangesAsync();
		return client;
	}

	public async Task<IEnumerable<Client>?> GetAll(int page, int size, string search)
	{
		var users =
		await _scheduleContext.Clients
				.AsNoTracking()
				.Where(w => w.Name!.Contains(search) ||
							w.Telephone!.Contains(search) ||
							w.State!.Contains(search) ||
							w.City!.Contains(search) ||
							w.Road!.Contains(search) ||
							w.Number!.Contains(search) ||
							w.Garden!.Contains(search) ||
							w.Description!.Contains(search))
				.OrderBy(o => o.Name)
				.Skip((page - 1) * size)
				.Take(size)
				.ToListAsync();
		return users;
	}

	public async Task<Client?> GetById(int id)
	{
		var client =
		await _scheduleContext.Clients
				.Where(w => w.Id == id)
				.FirstOrDefaultAsync();

		return client;
	}

	public async Task<int> TotalClients(string search)
	{
		var totalClient =
		await _scheduleContext.Clients
				.AsNoTracking()
				.Where(w => w.Name!.Contains(search) ||
							w.Telephone!.Contains(search) ||
							w.State!.Contains(search) ||
							w.City!.Contains(search) ||
							w.Road!.Contains(search) ||
							w.Number!.Contains(search) ||
							w.Garden!.Contains(search) ||
							w.Description!.Contains(search))
				.CountAsync();
		return totalClient;
	}

	public async Task<Client?> Update(Client client)
	{
		var exisingClient = await GetById(client.Id);
		if (exisingClient == null)
			return null;

		_scheduleContext.Clients.Entry(exisingClient).CurrentValues.SetValues(client);
		await _scheduleContext.SaveChangesAsync();
		return exisingClient;
	}
}

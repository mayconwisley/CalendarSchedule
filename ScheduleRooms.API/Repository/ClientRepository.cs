using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class ClientRepository(ScheduleContext scheduleContext) : IClientRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<Client> Create(Client client)
    {
        try
        {
            if (client is not null)
            {
                _scheduleContext.Clients.Add(client);
                await _scheduleContext.SaveChangesAsync();
                return client;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Client> Delete(int id)
    {
        try
        {
            var client = await GetById(id);
            if (client is not null)
            {
                _scheduleContext.Clients.Remove(client);
                await _scheduleContext.SaveChangesAsync();
                return client;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Client>> GetAll(int page, int size, string search)
    {
        try
        {
            var users = await _scheduleContext.Clients
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Name)
                .ToListAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Client> GetById(int id)
    {
        try
        {
            var client = await _scheduleContext.Clients
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (client is not null)
            {
                return client;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalClients(string search)
    {
        var totalClient = await _scheduleContext.Clients
            .Where(w => w.Name!.Contains(search))
            .CountAsync();
        return totalClient;
    }

    public async Task<Client> Update(Client client)
    {
        try
        {
            if (client is not null)
            {
                _scheduleContext.Clients.Entry(client).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return client;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class ClientContactRepository(ScheduleContext scheduleContext) : IClientContactRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<ClientContact> Create(ClientContact clientContact)
    {
        try
        {
            if (clientContact is not null)
            {
                _scheduleContext.ClientContacts.Add(clientContact);
                await _scheduleContext.SaveChangesAsync();

                clientContact = await GetById(clientContact.Id);

                return clientContact;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ClientContact> Delete(int id)
    {
        try
        {
            var clientContact = await GetById(id);
            if (clientContact is not null)
            {
                _scheduleContext.ClientContacts.Remove(clientContact);
                await _scheduleContext.SaveChangesAsync();
                return clientContact;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ClientContact>> GetAll(int page, int size, string search)
    {
        try
        {
            var clientContacts = await _scheduleContext.ClientContacts
                .Include(i => i.Client)
                .Include(i => i.ClientResponsible)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Client.Name)
                .ToListAsync();
            return clientContacts;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ClientContact>> GetByClientId(int page, int size, int clientId)
    {
        try
        {
            var clientContacts = await _scheduleContext.ClientContacts
                            .Include(i => i.Client)
                            .Include(i => i.ClientResponsible)
                            .Where(o => o.ClientId == clientId)
                            .ToListAsync();
            if (clientContacts is not null)
            {
                return clientContacts;
            }

            return [];
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ClientContact> GetById(int id)
    {
        try
        {
            var clientContact = await _scheduleContext.ClientContacts
                            .Include(i => i.Client)
                            .Include(i => i.ClientResponsible)
                            .Where(o => o.Id == id)
                            .FirstOrDefaultAsync();
            if (clientContact is not null)
            {
                return clientContact;
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalClientContact(string search)
    {
        var totalClientContact = await _scheduleContext.ClientContacts
             .Where(w => w.Client.Name.Contains(search) ||
                         w.ClientResponsible.Name.Contains(search))
             .CountAsync();
        return totalClientContact;
    }

    public async Task<ClientContact> Update(ClientContact clientContact)
    {
        try
        {
            if (clientContact is not null)
            {
                _scheduleContext.ClientContacts.Entry(clientContact).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();

                clientContact = await GetById(clientContact.Id);

                return clientContact;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

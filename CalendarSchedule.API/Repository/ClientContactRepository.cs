using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ClientContactRepository(ScheduleContext scheduleContext) : IClientContactRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<ClientContact> Create(ClientContact clientContact)
    {
        _scheduleContext.ClientContacts.Add(clientContact);
        await _scheduleContext.SaveChangesAsync();
        clientContact = await GetById(clientContact.Id);
        return clientContact;
    }

    public async Task<ClientContact> Delete(int id)
    {
        var clientContact = await GetById(id);
        _scheduleContext.ClientContacts.Remove(clientContact);
        await _scheduleContext.SaveChangesAsync();
        return clientContact;
    }

    public async Task<IEnumerable<ClientContact>> GetAll(int page, int size, string search)
    {
        var clientContacts = await _scheduleContext.ClientContacts
              .Include(i => i.Client)
              .Include(i => i.ClientResponsible)
              .Skip((page - 1) * size)
              .Take(size)
              .OrderBy(o => o.Client!.Name)
              .ToListAsync();
        return clientContacts;
    }

    public async Task<IEnumerable<ClientContact>> GetByClientId(int page, int size, int clientId)
    {
        var clientContacts = await _scheduleContext.ClientContacts
                        .Include(i => i.Client)
                        .Include(i => i.ClientResponsible)
                        .Where(o => o.ClientId == clientId)
                        .ToListAsync();
        return clientContacts;
    }

    public async Task<ClientContact> GetById(int id)
    {
        var clientContact = await _scheduleContext.ClientContacts
                        .Include(i => i.Client)
                        .Include(i => i.ClientResponsible)
                        .Where(o => o.Id == id)
                        .FirstOrDefaultAsync();
        return clientContact!;
    }

    public async Task<int> TotalClientContact(string search)
    {
        var totalClientContact = await _scheduleContext.ClientContacts
             .Where(w => w.Client!.Name!.Contains(search) ||
                         w.ClientResponsible!.Name!.Contains(search))
             .CountAsync();
        return totalClientContact;
    }

    public async Task<ClientContact> Update(ClientContact clientContact)
    {
        _scheduleContext.ClientContacts.Entry(clientContact).State = EntityState.Modified;
        await _scheduleContext.SaveChangesAsync();

        clientContact = await GetById(clientContact.Id);

        return clientContact;
    }
}

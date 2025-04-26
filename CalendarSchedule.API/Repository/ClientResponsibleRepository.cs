using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CalendarSchedule.API.Repository;

public class ClientResponsibleRepository(ScheduleContext _scheduleContext) : IClientResponsibleRepository
{
    public async Task<ClientResponsible> Create(ClientResponsible clientResponsible)
    {
        _scheduleContext.ClientResponsibles.Add(clientResponsible);
        await _scheduleContext.SaveChangesAsync();
        return clientResponsible;
    }

    public async Task<ClientResponsible?> Delete(int id)
    {
        var clientResponsible = await GetById(id);
        if (clientResponsible == null)
            return null;

        _scheduleContext.ClientResponsibles.Remove(clientResponsible);
        await _scheduleContext.SaveChangesAsync();
        return clientResponsible;
    }

    public async Task<IEnumerable<ClientResponsible>?> GetAll(int page, int size, string search)
    {
        var clientResponsibles =
        await _scheduleContext.ClientResponsibles
                .AsNoTracking()
                .Include(i => i.ClientContacts)
                .Where(w => w.Name!.Contains(search) ||
                            w.Email!.Contains(search) ||
                            w.Description!.Contains(search) ||
                            w.Position!.Contains(search))
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Name)
                .ToListAsync();
        return clientResponsibles;
    }

    public async Task<ClientResponsible?> GetById(int id)
    {
        var clientResponsible =
        await _scheduleContext.ClientResponsibles
                .Include(i => i.ClientContacts)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        return clientResponsible;
    }

    public async Task<int> TotalClientResponsible(string search)
    {
        var totalclientResponsible =
        await _scheduleContext.ClientResponsibles
                .AsNoTracking()
                .Where(w => w.Name!.Contains(search) ||
                            w.Email!.Contains(search) ||
                            w.Description!.Contains(search) ||
                            w.Position!.Contains(search))
                .CountAsync();
        return totalclientResponsible;
    }

    public async Task<ClientResponsible?> Update(ClientResponsible clientResponsible)
    {
        var existingClientResponsible = await GetById(clientResponsible.Id);
        if (existingClientResponsible == null)
            return null;

        _scheduleContext.ClientResponsibles.Entry(existingClientResponsible).CurrentValues.SetValues(clientResponsible);
        await _scheduleContext.SaveChangesAsync();
        return clientResponsible;
    }
}

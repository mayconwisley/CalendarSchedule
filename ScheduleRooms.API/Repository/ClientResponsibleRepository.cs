using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class ClientResponsibleRepository(ScheduleContext scheduleContext) : IClientResponsibleRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<ClientResponsible> Create(ClientResponsible clientResponsible)
    {
        try
        {
            if (clientResponsible is not null)
            {
                _scheduleContext.ClientResponsibles.Add(clientResponsible);
                await _scheduleContext.SaveChangesAsync();

                clientResponsible = await GetById(clientResponsible.Id);

                return clientResponsible;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ClientResponsible> Delete(int id)
    {
        try
        {
            var clientResponsible = await GetById(id);
            if (clientResponsible is not null)
            {
                _scheduleContext.ClientResponsibles.Remove(clientResponsible);
                await _scheduleContext.SaveChangesAsync();
                return clientResponsible;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ClientResponsible>> GetAll(int page, int size, string search)
    {
        try
        {
            var clientResponsibles = await _scheduleContext.ClientResponsibles
                .Include(i => i.ClientContacts)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Name)
                .ToListAsync();
            return clientResponsibles;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ClientResponsible> GetById(int id)
    {
        try
        {
            var clientResponsible = await _scheduleContext.ClientResponsibles
                            .Include(i => i.ClientContacts)
                            .Where(o => o.Id == id)
                            .FirstOrDefaultAsync();
            if (clientResponsible is not null)
            {
                return clientResponsible;
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalClientResponsible(string search)
    {
        var totalclientResponsible = await _scheduleContext.ClientResponsibles
            .Where(w => w.Name.Contains(search))
            .CountAsync();
        return totalclientResponsible;
    }

    public async Task<ClientResponsible> Update(ClientResponsible clientResponsible)
    {
        try
        {
            if (clientResponsible is not null)
            {
                _scheduleContext.ClientResponsibles.Entry(clientResponsible).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();

                clientResponsible = await GetById(clientResponsible.Id);

                return clientResponsible;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

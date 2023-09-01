using AgendaSalas.API.Data;
using AgendaSalas.API.Model;
using AgendaSalas.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AgendaSalas.API.Repository;

public class AgendaRepository : IAgendaRepository
{
    private readonly AgendaContext _agendaContext;

    public AgendaRepository(AgendaContext agendaContext)
    {
        _agendaContext = agendaContext;
    }

    public async Task<Agenda> Create(Agenda agenda)
    {
        try
        {
            if (agenda is not null)
            {
                _agendaContext.Agendas.Add(agenda);
                await _agendaContext.SaveChangesAsync();
                return agenda;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Agenda> Delete(int id)
    {
        try
        {
            var agenda = await GetById(id);

            if (agenda is not null)
            {
                _agendaContext.Remove(agenda);
                await _agendaContext.SaveChangesAsync();
                return agenda;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Agenda>> GetAll(int page, int size, string search)
    {
        try
        {
            var agendas = await _agendaContext.Agendas
                .Include(i => i.Sala)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.DataInicio)
                .ToListAsync();

            return agendas;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Agenda>> GetByDate(DateTime dateTime)
    {
        try
        {
            var agendas = await _agendaContext.Agendas
                .Include(i => i.Sala)
                .Where(w => w.DataFinal >= dateTime)
                .OrderBy(o => o.DataInicio)
                .ToListAsync();

            return agendas;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Agenda> GetById(int id)
    {
        try
        {
            var agenda = await _agendaContext.Agendas
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (agenda is not null)
            {
                return agenda;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalAgendas(string search)
    {
        var totalAgendas = await _agendaContext.Agendas
            .Where(w => w!.Descricao!.Contains(search))
            .CountAsync();

        return totalAgendas;

    }

    public async Task<Agenda> Update(Agenda agenda)
    {
        try
        {
            if (agenda is not null)
            {
                _agendaContext.Agendas.Entry(agenda).State = EntityState.Modified;
                await _agendaContext.SaveChangesAsync();
                return agenda;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

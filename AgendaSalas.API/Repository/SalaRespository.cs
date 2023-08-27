using AgendaSalas.API.Data;
using AgendaSalas.API.Model;
using AgendaSalas.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AgendaSalas.API.Repository;

public class SalaRespository : ISalaRepository
{
    private readonly AgendaContext _agendaContext;

    public SalaRespository(AgendaContext agendaContext)
    {
        _agendaContext = agendaContext;
    }

    public async Task<Sala> Create(Sala sala)
    {
        try
        {
            if (sala is not null)
            {
                _agendaContext.Salas.Add(sala);
                await _agendaContext.SaveChangesAsync();
                return sala;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Sala> Delete(int id)
    {
        try
        {
            var sala = await GetById(id);

            if (sala is not null)
            {
                _agendaContext.Remove(sala);
                await _agendaContext.SaveChangesAsync();
                return sala;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Sala>> GetAll(int page, int size, string search)
    {
        try
        {
            var salas = await _agendaContext.Salas
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Nome)
                .ToListAsync();

            return salas;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Sala> GetById(int id)
    {
        try
        {
            var sala = await _agendaContext.Salas
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (sala is not null)
            {
                return sala;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalSalas(string search)
    {
        var totalSala = await _agendaContext.Salas
            .Where(w => w.Nome!.Contains(search))
            .CountAsync();
        return totalSala;
    }

    public async Task<Sala> Update(Sala sala)
    {
        try
        {
            if (sala is not null)
            {
                _agendaContext.Salas.Entry(sala).State = EntityState.Modified;
                await _agendaContext.SaveChangesAsync();
                return sala;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

using ScheduleRooms.Data;
using ScheduleRooms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleRooms.Repositorio;

public class ReuniaoRepositorio
{
    readonly AgendaContext scheduleContext = new();

    public async Task<IEnumerable<Reuniao>> ListarTudo()
    {
        return await scheduleContext.Reunioes
            .OrderBy(or => or.Description)
            .ToListAsync();
    }
    public async Task<IEnumerable<Reuniao>> ListarPorSala(int roomId)
    {
        var listaPorSala = await scheduleContext.Reunioes
                .Include(i => i.Room)
                .Where(w => w.RoomId == roomId)
                .OrderBy(or => or.Description)
                .ToListAsync();
        return listaPorSala;
    }

    public async Task<IEnumerable<Reuniao>> ListarAgendadas(DateTime dataAtual)
    {
        var listaPorSala = await scheduleContext.Reunioes
                .Include(i => i.Room)
                .Where(w => w.DataFim >= dataAtual)
                .OrderBy(or => or.Description)
                .ToListAsync();
        return listaPorSala;
    }

    public async Task<Reuniao> BuscarPorId(int id)
    {
        var reuniao = await scheduleContext.Reunioes
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        if (reuniao is not null)
        {
            return reuniao;
        }
        return new Reuniao();
    }

    public async Task Adicionar(Reuniao reuniao)
    {
        try
        {
            scheduleContext.Reunioes.Add(reuniao);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Alterar(Reuniao reuniao)
    {
        try
        {
            Reuniao reuniao1 = await BuscarPorId(reuniao.Id);
            if (reuniao1 is not null)
            {
                scheduleContext.Reunioes.Entry(reuniao1).CurrentValues.SetValues(reuniao);
                await scheduleContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Excluir(int id)
    {
        Reuniao reuniao = await BuscarPorId(id);
        try
        {
            scheduleContext.Reunioes.Remove(reuniao);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

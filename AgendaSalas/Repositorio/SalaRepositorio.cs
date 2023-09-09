using ScheduleRooms.Data;
using ScheduleRooms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleRooms.Repositorio;

public class SalaRepositorio
{
    readonly AgendaContext scheduleContext = new();

    public async Task<IEnumerable<Room>> ListarTudo()
    {
        return await scheduleContext.Salas
            .OrderBy(or => or.SalaReuniao)
            .ToListAsync();
    }

    public async Task<Room> BuscarPorId(int id)
    {
        var room = await scheduleContext.Salas
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        if (room is not null)
        {
            return room;
        }
        return new Room();
    }

    public async Task Adicionar(Room room)
    {
        try
        {
            scheduleContext.Salas.Add(room);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Alterar(Room room)
    {
        try
        {
            Room sala1 = await BuscarPorId(room.Id);
            if (sala1 is not null)
            {
                scheduleContext.Salas.Entry(sala1).CurrentValues.SetValues(room);
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
        Room room = await BuscarPorId(id);
        try
        {
            scheduleContext.Remove(room);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

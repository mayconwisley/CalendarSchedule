using Microsoft.EntityFrameworkCore;
using ScheduleRooms.Data;
using CalendarSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleRooms.Repository;

public class RoomRepository
{
    readonly ScheduleContext scheduleContext = new();

    public async Task<IEnumerable<Room>> GetAll()
    {
        return await scheduleContext.Rooms
            .OrderBy(or => or.Name)
            .ToListAsync();
    }

    public async Task<Room> GetById(int id)
    {
        var room = await scheduleContext.Rooms
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        if (room is not null)
        {
            return room;
        }
        return new Room();
    }

    public async Task Create(Room room)
    {
        try
        {
            scheduleContext.Rooms.Add(room);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(Room room)
    {
        try
        {
            Room sala1 = await GetById(room.Id);
            if (sala1 is not null)
            {
                scheduleContext.Rooms.Entry(sala1).CurrentValues.SetValues(room);
                await scheduleContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(int id)
    {
        Room room = await GetById(id);
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

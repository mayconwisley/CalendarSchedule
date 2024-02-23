using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Data;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Repository.Interface;

namespace ScheduleRooms.API.Repository;

public class RoomRespository : IRoomRepository
{
    private readonly ScheduleContext _scheduleContext;

    public RoomRespository(ScheduleContext scheduleContext)
    {
        _scheduleContext = scheduleContext;
    }

    public async Task<Room> Create(Room room)
    {
        try
        {
            if (room is not null)
            {
                _scheduleContext.Rooms.Add(room);
                await _scheduleContext.SaveChangesAsync();
                return room;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Room> Delete(int id)
    {
        try
        {
            var room = await GetById(id);

            if (room is not null)
            {
                _scheduleContext.Remove(room);
                await _scheduleContext.SaveChangesAsync();
                return room;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Room>> GetAll(int page, int size, string search)
    {
        try
        {
            var rooms = await _scheduleContext.Rooms
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.Name)
                .ToListAsync();

            return rooms;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Room> GetById(int id)
    {
        try
        {
            var room = await _scheduleContext.Rooms
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (room is not null)
            {
                return room;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> TotalRooms(string search)
    {
        var totalSala = await _scheduleContext.Rooms
            .Where(w => w.Name!.Contains(search))
            .CountAsync();
        return totalSala;
    }

    public async Task<Room> Update(Room room)
    {
        try
        {
            if (room is not null)
            {
                _scheduleContext.Rooms.Entry(room).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();
                return room;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

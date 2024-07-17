using Microsoft.EntityFrameworkCore;
using ScheduleRooms.Data;
using CalendarSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleRooms.Repository;

public class ScheduleRepository
{
    readonly ScheduleContext scheduleContext = new();

    public async Task<IEnumerable<Schedule>> GetAll()
    {
        return await scheduleContext.Schedules
            .OrderBy(or => or.Description)
            .ToListAsync();
    }
    public async Task<IEnumerable<Schedule>> GetByRoomId(int roomId)
    {
        var listSchedule = await scheduleContext.Schedules
                .Include(i => i.Room)
                .Where(w => w.RoomId == roomId)
                .OrderBy(or => or.Description)
                .ToListAsync();
        return listSchedule;
    }

    public async Task<IEnumerable<Schedule>> GetScheduleDateCurrent(DateTime dateCurrent)
    {
        var listSchedule = await scheduleContext.Schedules
                .Include(i => i.Room)
                .Where(w => w.DataFim >= dateCurrent)
                .OrderBy(or => or.Description)
                .ToListAsync();
        return listSchedule;
    }

    public async Task<Schedule> GetById(int id)
    {
        var schedule = await scheduleContext.Schedules
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        if (schedule is not null)
        {
            return schedule;
        }
        return new Schedule();
    }

    public async Task Create(Schedule schedule)
    {
        try
        {
            scheduleContext.Schedules.Add(schedule);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(Schedule schedule)
    {
        try
        {
            Schedule scheduleCurrent = await GetById(schedule.Id);
            if (scheduleCurrent is not null)
            {
                scheduleContext.Schedules.Entry(scheduleCurrent).CurrentValues.SetValues(schedule);
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
        Schedule schedule = await GetById(id);
        try
        {
            scheduleContext.Schedules.Remove(schedule);
            await scheduleContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

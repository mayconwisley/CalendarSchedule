using ScheduleRooms.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ScheduleRooms.API.Data;

public class ScheduleContext : DbContext
{
    public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
}

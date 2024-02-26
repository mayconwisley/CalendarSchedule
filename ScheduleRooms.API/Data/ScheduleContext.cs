using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Model;
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
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ScheduleRoom> ScheduleRooms { get; set; }
    public DbSet<ScheduleUser> ScheduleUsers { get; set; }

}

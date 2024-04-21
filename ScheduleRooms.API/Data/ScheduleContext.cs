using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Utility.Interface;
using System.Reflection;

namespace ScheduleRooms.API.Data;

public class ScheduleContext(DbContextOptions<ScheduleContext> options, IEncryptionUtility encryptionUtility) : DbContext(options)
{
    private readonly IEncryptionUtility _encryptionUtility = encryptionUtility;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>().HasData(
          new User
          {
              Id = 1,
              Name = "Admin",
              Username = "Admin",
              Password = _encryptionUtility.Dado("admin"),
              Cellphone = "44111111111",
              Description = "Administrador",
              Active = true,
              Manager = false
          });
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ScheduleRoom> ScheduleRooms { get; set; }
    public DbSet<ScheduleUser> ScheduleUsers { get; set; }


}

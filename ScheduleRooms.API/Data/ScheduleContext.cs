using Microsoft.EntityFrameworkCore;
using ScheduleRooms.API.Model;
using ScheduleRooms.API.Utility.Interface;
using System.Reflection;

namespace ScheduleRooms.API.Data;

public class ScheduleContext(DbContextOptions<ScheduleContext> options, IEncryptionUtility encryptionUtility) : DbContext(options)
{
    private readonly IEncryptionUtility _encryptionUtility = encryptionUtility;

    public DbSet<User> Users { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientResponsible> ClientResponsibles { get; set; }
    public DbSet<ClientContact> ClientContacts { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ScheduleUser> ScheduleUsers { get; set; }

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
              Description = "Administrador",
              Active = true,
              Manager = true
          });

        modelBuilder.Entity<ScheduleUser>()
            .HasKey(at => new { at.ScheduleId, at.UserId });
    }
}

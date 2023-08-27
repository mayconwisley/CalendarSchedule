using AgendaSalas.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgendaSalas.API.Data;

public class AgendaContext : DbContext
{
    public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Sala> Salas { get; set; }
    public DbSet<Agenda> Agendas { get; set; }
}

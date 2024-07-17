using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarSchedule.API.Model.ConfigurationEntityType;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(250)")
            .IsRequired();
        builder.Property(p => p.Telephone)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
        builder.Property(p => p.State)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
        builder.Property(p => p.City)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.Property(p => p.Road)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.Property(p => p.Number)
            .HasColumnType("VARCHAR(10)")
            .IsRequired();
        builder.Property(p => p.Garden)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
    }
}

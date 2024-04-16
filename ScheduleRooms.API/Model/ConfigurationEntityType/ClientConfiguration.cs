using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(250)")
            .IsRequired();
        builder.Property(p => p.Responsible)
            .HasColumnType("VARCHAR(250)")
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
        builder.Property(p => p.Telephone)
            .HasColumnType("VARCHAR(20)");
        builder.Property(p => p.Email)
            .HasColumnType("VARCHAR(50)");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType;

public class SalaConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(15)")
            .IsRequired();
        builder.Property(p => p.Ramal)
            .HasColumnType("VARCHAR(5)");
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(255)");

        builder.HasIndex(p => p.Name);
    }
}

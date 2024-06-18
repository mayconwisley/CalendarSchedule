using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType;

public class ClientReposibleConfiguration : IEntityTypeConfiguration<ClientResponsible>
{
    public void Configure(EntityTypeBuilder<ClientResponsible> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(150)")
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
        builder.Property(p => p.Email)
            .HasColumnType("VARCHAR(150)")
            .IsRequired();
        builder.Property(p => p.Position)
            .HasColumnType("VARCHAR(120)")
            .IsRequired();
    }
}

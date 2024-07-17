using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarSchedule.API.Model.ConfigurationEntityType;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(250)")
            .IsRequired();
        builder.Property(p => p.Username)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.HasIndex(h => h.Username)
                .IsUnique();
        builder.Property(p => p.Password)
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
    }
}

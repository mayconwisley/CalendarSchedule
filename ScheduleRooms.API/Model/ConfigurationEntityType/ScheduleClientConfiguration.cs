using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType;

public class ScheduleClientConfiguration : IEntityTypeConfiguration<ScheduleUser>
{
    public void Configure(EntityTypeBuilder<ScheduleUser> builder)
    {
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
    }
}

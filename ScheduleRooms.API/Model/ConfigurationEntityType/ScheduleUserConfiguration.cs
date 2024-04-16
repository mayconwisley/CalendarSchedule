using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType;

public class ScheduleUserConfiguration : IEntityTypeConfiguration<ScheduleUser>
{
    public void Configure(EntityTypeBuilder<ScheduleUser> builder)
    {
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)");
        builder.Property(p => p.ManagerId)
            .IsRequired();
    }
}

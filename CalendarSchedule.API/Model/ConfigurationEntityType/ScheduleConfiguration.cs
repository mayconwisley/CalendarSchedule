using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarSchedule.API.Model.ConfigurationEntityType;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR(500)")
            .IsRequired();
        builder.Property(p => p.DateStart)
            .HasColumnType("DATETIME");
        builder.Property(p => p.DateFinal)
            .HasColumnType("DATETIME");
    }
}

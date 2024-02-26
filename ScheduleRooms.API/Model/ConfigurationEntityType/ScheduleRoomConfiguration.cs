using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType
{
    public class ScheduleRoomConfiguration : IEntityTypeConfiguration<ScheduleRoom>
    {
        public void Configure(EntityTypeBuilder<ScheduleRoom> builder)
        {
            builder.Property(p => p.Description)
                .HasColumnType("VARCHAR(500)")
                .IsRequired();
        }
    }
}

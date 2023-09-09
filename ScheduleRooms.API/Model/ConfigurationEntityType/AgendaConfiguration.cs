using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduleRooms.API.Model.ConfigurationEntityType
{
    public class AgendaConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.Property(p => p.Description)
                .HasColumnType("VARCHAR(500)")
                .IsRequired();
        }
    }
}

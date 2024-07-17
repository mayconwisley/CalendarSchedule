using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarSchedule.API.Model.ConfigurationEntityType;

public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
{
    public void Configure(EntityTypeBuilder<ClientContact> builder)
    {
        builder.Property(p => p.Number)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
        builder.Property(p => p.Type)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
    }
}

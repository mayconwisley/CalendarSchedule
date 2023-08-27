using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSalas.API.Model.ConfigurationEntityType
{
    public class AgendaConfiguration : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.Property(p => p.Descricao)
                .HasColumnType("VARCHAR(500)")
                .IsRequired();
        }
    }
}

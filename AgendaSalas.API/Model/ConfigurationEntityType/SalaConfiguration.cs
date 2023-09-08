using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSalas.API.Model.ConfigurationEntityType;

public class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.Property(p => p.Nome)
            .HasColumnType("VARCHAR(15)")

            .IsRequired();
        builder.Property(p => p.Ramal)
            .HasColumnType("VARCHAR(5)");
        builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR(255)");

        builder.HasIndex(p => p.Nome);
    }
}

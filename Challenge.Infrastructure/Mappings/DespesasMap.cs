using Challenge.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Mappings;

public class DespesasMap : IEntityTypeConfiguration<Despesas>
{
    public void Configure(EntityTypeBuilder<Despesas> builder)
    {
        builder.ToTable("Despesas");

        builder.HasKey(k => k.Id);

        builder.Property(pk => pk.Id)
            .UseMySqlIdentityColumn()
            .HasColumnType("BIGINT");

        builder.Property(d => d.Descricao)
            .IsRequired()
            .HasColumnName("Descrição")
            .HasColumnType("VARCHAR(100)")
            .HasMaxLength(100);

        builder.Property(v => v.Valor)
            .IsRequired()
            .HasColumnName("Valor")
            .HasColumnType("DECIMAL(7,3)")
            .HasMaxLength(1000000000)
            .HasDefaultValue(0);

        builder.Property(d => d.Data)
            .IsRequired()
            .HasColumnName("Despesa_Data")
            .HasColumnType("TIMESTAMP");
    }
}
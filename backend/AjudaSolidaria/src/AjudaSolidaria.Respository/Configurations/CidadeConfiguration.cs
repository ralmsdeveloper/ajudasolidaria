using AjudaSolidaria.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AjudaSolidaria.Respository.Configurations
{
    public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidade");
            builder.HasKey(p => p.Id);
            builder.Property(b => b.Nome).HasMaxLength(100);
            builder.Property(b => b.CodigoIBGE);
            builder.Property(b => b.UF).HasColumnType("bpchar(2)");
            builder.HasIndex(b => new { b.UF });
            builder.HasIndex(b => new { b.UF, b.Nome });
        }
    }
}

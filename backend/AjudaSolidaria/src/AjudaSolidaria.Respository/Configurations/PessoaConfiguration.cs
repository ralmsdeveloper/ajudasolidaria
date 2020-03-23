using AjudaSolidaria.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AjudaSolidaria.Respository.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(p => p.Key);

            builder.Property(b => b.Key).HasField("_key");
            builder.Property(b => b.CreatedAt).HasDefaultValueSql("current_timestamp");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("current_timestamp");
            builder.Property(b => b.Nome).HasMaxLength(100);
            builder.Property(b => b.CPF).HasColumnType("bpchar(11)");
            builder.Property(b => b.Telefone).HasColumnType("bpchar(11)");
            builder.Property(b => b.Estado).HasColumnType("bpchar(2)");
            builder.Property(b => b.Cidade).HasColumnType("text");
            builder.Property(b => b.Bairro).HasColumnType("text");
            builder.Property(b => b.Endereco).HasColumnType("text");
            builder.Property(b => b.Numero).HasColumnType("text");
            builder.Property(b => b.Coordenadas).HasColumnType("text");
        }
    }
}

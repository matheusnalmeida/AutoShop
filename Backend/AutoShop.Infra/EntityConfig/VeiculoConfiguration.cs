using AutoShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoShop.Infra.EntityConfig
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(40);

            builder.OwnsOne(x => x.Nome)
                .Property(x => x.Valor)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("Nome");

            builder.Property(x => x.Ano)
                .IsRequired();

            builder.Property(x => x.Modelo)
                .IsRequired()
                .HasMaxLength(120);

            builder.OwnsOne(x => x.Preco)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("Preco");

            builder.Property(x => x.ImagemURL)
                .IsRequired();

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Navigation(x => x.Nome).IsRequired();
            builder.Navigation(x => x.Preco).IsRequired();

            builder.HasMany(x => x.Operacoes).WithOne(x => x.Veiculo).HasForeignKey(x => x.IdVeiculo);
        }
    }
}

using AutoShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoShop.Infra.EntityConfig
{
    public class OperacaoConfiguration : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.ToTable("Operacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(40);

            builder.OwnsOne(x => x.ValorTotal)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("ValorTotal");

            builder.OwnsOne(x => x.ValorFinanciado)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("ValorFinanciado");

            builder.OwnsOne(x => x.ValorVeiculo)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("ValorVeiculo");

            builder.Property(x => x.QuantidadeDeParcelas)
                .IsRequired();

            builder.Property(x => x.Situacao)
                .IsRequired()
                .HasColumnName("Situacao");

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Navigation(x => x.ValorTotal).IsRequired();
            builder.Navigation(x => x.ValorFinanciado).IsRequired();
            builder.Navigation(x => x.ValorVeiculo).IsRequired();

            builder.Property(x => x.IdVeiculo).IsRequired().HasMaxLength(40);
            builder.Property(x => x.IdCliente).IsRequired().HasMaxLength(40);
            builder.Property(x => x.IdVendedor).HasMaxLength(40);

            builder.HasOne(x => x.Veiculo).WithMany(x => x.Operacoes).HasForeignKey(x => x.IdVeiculo);
            builder.HasOne(x => x.Cliente).WithMany(x => x.OperacoesCriadas).HasForeignKey(x => x.IdCliente).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Vendedor).WithMany(x => x.OperacoesAprovadas).HasForeignKey(x => x.IdVendedor).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ProdutoOperacoes).WithOne(x => x.Operacao).HasForeignKey(x => x.IdOperacao);
        }
    }
}

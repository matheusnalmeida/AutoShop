using AutoShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Infra.EntityConfig
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(40);

            builder.OwnsOne(x => x.Nome)
                .Property(x => x.Valor)
                .HasMaxLength(120)
                .HasColumnName("Nome")
                .IsRequired();

            builder.OwnsOne(x => x.Preco)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,4)")
                .IsRequired()
                .HasColumnName("Preco");

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Navigation(x => x.Nome).IsRequired();
            builder.Navigation(x => x.Preco).IsRequired();

            builder.HasMany(x => x.ProdutoOperacoes).WithOne(x => x.Produto).HasForeignKey(x => x.IdProduto);
        }
    }
}

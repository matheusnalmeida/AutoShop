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
    public class ProdutoOperacaoConfiguration : IEntityTypeConfiguration<ProdutoOperacao>
    {
        public void Configure(EntityTypeBuilder<ProdutoOperacao> builder)
        {
            builder.ToTable("ProdutoOperacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(40);

            builder.OwnsOne(x => x.Preco)
                .Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("Preco");

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Navigation(x => x.Preco).IsRequired();

            builder.Property(x => x.IdOperacao).IsRequired().HasMaxLength(40);
            builder.Property(x => x.IdProduto).IsRequired().HasMaxLength(40);

            builder.HasOne(x => x.Operacao).WithMany(x => x.ProdutoOperacoes).HasForeignKey(x => x.IdOperacao);
            builder.HasOne(x => x.Produto).WithMany(x => x.ProdutoOperacoes).HasForeignKey(x => x.IdProduto);
        }
    }
}

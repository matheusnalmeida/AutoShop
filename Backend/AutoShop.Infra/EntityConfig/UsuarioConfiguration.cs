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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(40);

            builder.OwnsOne(x => x.Cpf)
                .Property(x => x.Numero)
                .HasMaxLength(120)
                .HasColumnName("Cpf")
                .IsRequired();

            builder.Property(x => x.Idade)
                .IsRequired();

            builder.OwnsOne(x => x.Telefone)
                .Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Telefone");

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Endereco)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("Email");

            builder.OwnsOne(x => x.Senha)
                .Property(x => x.Valor)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("Senha");

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Navigation(x => x.Cpf).IsRequired();
            builder.Navigation(x => x.Telefone).IsRequired();
            builder.Navigation(x => x.Email).IsRequired();
            builder.Navigation(x => x.Senha).IsRequired();

            builder.HasMany(x => x.OperacoesCriadas).WithOne(x => x.Cliente).HasForeignKey(x => x.IdCliente);

            builder.HasMany(x => x.OperacoesAprovadas).WithOne(x => x.Vendedor).HasForeignKey(x => x.IdVendedor);
        }
    }
}

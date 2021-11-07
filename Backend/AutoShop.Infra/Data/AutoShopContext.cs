using AutoShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoShop.Infra.EntityConfig;
using Flunt.Notifications;
using AutoShop.Domain.ValueObjects;

namespace AutoShop.Infra.Data
{
    public class AutoShopContext : DbContext
    {
        public AutoShopContext(DbContextOptions<AutoShopContext> options) : base(options)
        {
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddModelIgnores(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new VeiculoConfiguration());
            modelBuilder.ApplyConfiguration(new OperacaoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoOperacaoConfiguration());
        }

        private void AddModelIgnores(ModelBuilder modelBuilder) {
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<CPF>();
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Senha>();
            modelBuilder.Ignore<Preco>();
            modelBuilder.Ignore<Telefone>();
        }
    }
}

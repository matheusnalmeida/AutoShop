using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;

namespace AutoShop.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AutoShopContext Context { get; set; }

        public UnitOfWork(AutoShopContext context)
        {
            Context = context;
        }

        public void PersistChanges()
        {
            Context.SaveChanges();
        }
    }
}

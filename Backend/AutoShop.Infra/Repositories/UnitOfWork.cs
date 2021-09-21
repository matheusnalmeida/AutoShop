using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

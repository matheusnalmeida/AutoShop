using AutoShop.Shared.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Infra.Repositories
{
    public abstract class BaseContext<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        protected DbSet<TEntity> DbSet;
        protected TContext DbContext;

        protected BaseContext(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }
    }
}

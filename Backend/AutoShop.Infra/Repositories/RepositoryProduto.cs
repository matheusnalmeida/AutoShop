using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AutoShop.Infra.Repositories
{
    public class RepositoryProduto : BaseContext<Produto, AutoShopContext>, IRepositoryProduto 
    {

        public RepositoryProduto(AutoShopContext dbContext) : base(dbContext){}

        public void Add(Produto produto)
        {
            if (produto.IsValid) 
            {
                DbSet.Add(produto);
            }
        }

        public IQueryable<Produto> GetAll(params Expression<Func<Produto, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet.AsQueryable();
        }

        public IQueryable<Produto> GetById(string[] ids, params Expression<Func<Produto, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet.Where(produto => ids.Contains(produto.Id)).AsQueryable();
        }

        public void Remove(Produto produto)
        {
            if (produto?.Id != null) 
            {
                produto.Ativo = false;
                DbSet.Update(produto);
            }
        }

        public void Update(Produto produto)
        {
            if (produto.IsValid) 
            {
                DbSet.Update(produto);
            }
        }
    }
}

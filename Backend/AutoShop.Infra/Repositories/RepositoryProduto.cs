using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IQueryable<Produto> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public Produto GetById(string id)
        {
            return DbSet.FirstOrDefault(produto => produto.Id == id);
        }

        public IQueryable<Produto> GetByIds(IEnumerable<string> ids)
        {
            return DbSet.Where(produto => ids.Contains(produto.Id));
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

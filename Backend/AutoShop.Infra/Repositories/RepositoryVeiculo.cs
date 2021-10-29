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
    public class RepositoryVeiculo : BaseContext<Veiculo, AutoShopContext>, IRepositoryVeiculo
    {
        public RepositoryVeiculo(AutoShopContext baseContext) : base(baseContext) { }

        public void Add(Veiculo veiculo)
        {
            if (veiculo.IsValid)
            {
                DbSet.Add(veiculo);
            }
        }

        public IQueryable<Veiculo> GetAll(params Expression<Func<Veiculo, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }
            return DbSet.AsQueryable();
        }

        public IQueryable<Veiculo> GetById(string[] ids, params Expression<Func<Veiculo, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }
            return DbSet.Where(veiculo => ids.Contains(veiculo.Id)).AsQueryable();
        }

        public void Remove(Veiculo veiculo)
        {
            if (veiculo?.Id != null)
            {
                veiculo.Ativo = false;
                DbSet.Update(veiculo);
            }
        }

        public void Update(Veiculo veiculo)
        {
            if (veiculo.IsValid)
            {
                DbSet.Update(veiculo);
            }
        }
    }
}

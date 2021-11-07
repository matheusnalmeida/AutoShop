using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using AutoShop.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query;
        }

        public IQueryable<Veiculo> GetById(string[] ids, params Expression<Func<Veiculo, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query.Where(veiculo => ids.Contains(veiculo.Id));
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

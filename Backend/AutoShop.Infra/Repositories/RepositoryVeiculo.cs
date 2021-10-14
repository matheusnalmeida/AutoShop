using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IQueryable<Veiculo> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public Veiculo GetById(string id)
        {
            return DbSet.FirstOrDefault(veiculo => veiculo.Id == id);
        }

        public IQueryable<Veiculo> GetByIds(IEnumerable<string> ids)
        {
            return DbSet.Where(veiculo => ids.Contains(veiculo.Id));
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

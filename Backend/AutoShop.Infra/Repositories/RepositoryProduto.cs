using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Infra.Repositories
{
    public class RepositoryProduto : IRepositoryProduto
    {
        public void Add(Produto obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Produto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Produto GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Produto> GetByIds(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public void Remove(Produto obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Produto obj)
        {
            throw new NotImplementedException();
        }
    }
}

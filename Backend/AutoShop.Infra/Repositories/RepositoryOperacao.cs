using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Infra.Repositories
{
    public class RepositoryOperacao : IRepositoryOperacao
    {
        public void Add(Operacao obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Operacao> GetAll()
        {
            throw new NotImplementedException();
        }

        public Operacao GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Operacao obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Operacao obj)
        {
            throw new NotImplementedException();
        }
    }
}

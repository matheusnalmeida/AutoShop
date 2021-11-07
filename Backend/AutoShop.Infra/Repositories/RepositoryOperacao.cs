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
    public class RepositoryOperacao : BaseContext<Operacao, AutoShopContext>, IRepositoryOperacao
    {
        public RepositoryOperacao(AutoShopContext baseContext) : base(baseContext) { }

        public void Add(Operacao operacao)
        {
            if (operacao.IsValid)
            {
                DbSet.Add(operacao);
            }
        }

        public IQueryable<Operacao> GetAll(params Expression<Func<Operacao, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query;
        }

        public IQueryable<Operacao> GetById(string[] ids, params Expression<Func<Operacao, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query.Where(operacao => ids.Contains(operacao.Id));
        }

        public IQueryable<Operacao> GetByIds(IEnumerable<string> ids)
        {
            return DbSet.Where(operacao => ids.Contains(operacao.Id));
        }

        public void Remove(Operacao operacao)
        {
            throw new InvalidOperationException("Não é possivel remover uma operação!");
        }

        public void Update(Operacao operacao)
        {
            throw new InvalidOperationException("Não é possivel atualizar uma operação!");
        }
    }
}

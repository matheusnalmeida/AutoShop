using AutoShop.Shared.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AutoShop.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);

        IQueryable<TEntity> GetById(string[] ids, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        void Update(TEntity obj);

        void Remove(TEntity obj);

    }
}

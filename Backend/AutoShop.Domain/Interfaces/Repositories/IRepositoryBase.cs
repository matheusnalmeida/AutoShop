using AutoShop.Shared.Entities;
using System.Linq;

namespace AutoShop.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);

        TEntity GetById(string id);

        IQueryable<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(TEntity obj);

    }
}

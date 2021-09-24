using AutoShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Domain.Interfaces.Repositories
{
    public interface IRepositoryProduto : IRepositoryBase<Produto>
    {
        public IQueryable<Produto> GetByIds(IEnumerable<string> ids);
    }
}

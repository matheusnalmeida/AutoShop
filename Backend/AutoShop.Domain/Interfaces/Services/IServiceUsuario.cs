using AutoShop.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AutoShop.Domain.Interfaces.Services
{
    public interface IServiceUsuario : IServiceBase<Usuario>
    {
        IQueryable<Usuario> GetAllLogin(params Expression<Func<Usuario, object>>[] includeProperties);

    }
}

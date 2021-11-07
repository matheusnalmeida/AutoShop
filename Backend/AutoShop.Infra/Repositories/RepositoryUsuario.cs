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
    public class RepositoryUsuario : BaseContext<Usuario, AutoShopContext>, IRepositoryUsuario
    {
        public RepositoryUsuario(AutoShopContext baseContext) : base(baseContext) { }

        public void Add(Usuario usuario)
        {
            if (usuario.IsValid)
            {
                DbSet.Add(usuario);
            }
        }

        public IQueryable<Usuario> GetAll(params Expression<Func<Usuario, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query;
        }

        public IQueryable<Usuario> GetById(string[] ids, params Expression<Func<Usuario, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            query = query.Include(includeProperties);

            return query.Where(usuario => ids.Contains(usuario.Id));
        }

        public void Remove(Usuario usuario)
        {
            if (usuario?.Id != null)
            {
                usuario.Ativo = false;
                DbSet.Remove(usuario);
            }
        }

        public void Update(Usuario usuario)
        {
            if (usuario.IsValid)
            {
                DbSet.Update(usuario);
            }
        }
    }
}

using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet.AsQueryable();
        }

        public IQueryable<Usuario> GetById(string[] ids, params Expression<Func<Usuario, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet.Where(usuario => ids.Contains(usuario.Id)).AsQueryable();
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

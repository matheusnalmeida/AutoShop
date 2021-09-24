using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IQueryable<Usuario> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public Usuario GetById(string id)
        {
            return DbSet.FirstOrDefault(usuario => usuario.Id == id);
        }

        public IQueryable<Usuario> GetByIds(IEnumerable<string> ids)
        {
            return DbSet.Where(usuario => ids.Contains(usuario.Id));
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

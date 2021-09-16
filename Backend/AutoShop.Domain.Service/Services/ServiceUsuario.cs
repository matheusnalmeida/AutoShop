using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;

        public ServiceUsuario(IRepositoryUsuario repository)
        {
            _repository = repository;
        }

        public Notifiable<Notification> Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _repository.GetAll();
        }

        public Usuario GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(Usuario usuario)
        {
            var usuarioAtual = GetById(usuario?.Id);
            ValidaUsuarioExiste(usuario, usuarioAtual);
            if (usuario.IsValid) 
            {
                return usuario;
            }
            _repository.Remove(usuarioAtual);
            return usuarioAtual;
        }

        public Notifiable<Notification> Update(Usuario usuario)
        {
            var usuarioAtual = GetById(usuario?.Id);
            ValidaUsuarioExiste(usuario, usuarioAtual);
            if (usuario.IsValid)
            {
                return usuario;
            }
            usuarioAtual.FillUpdate(usuario);
            _repository.Update(usuarioAtual);
            return usuarioAtual;
        }

        private void ValidaUsuarioExiste(Usuario usuario, Usuario usuarioAtual)
        {
            if (usuarioAtual == null)
            {
                usuario.AddNotification("Usuario", "Não existe usuario com o id informado");
            }
        }
    }
}

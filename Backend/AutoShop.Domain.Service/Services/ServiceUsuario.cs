using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.Notifications;
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
        private readonly IUnitOfWork _unitOfWork;

        public ServiceUsuario(IRepositoryUsuario repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Notifiable<Notification> Add(Usuario usuario)
        {
            if (usuario.IsValid)
            {
                _repository.Add(usuario);
                _unitOfWork.PersistChanges();
            }
            return usuario;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _repository.GetAll();
        }

        public Usuario GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(string id)
        {
            var usuarioAtual = GetById(id);
            if (usuarioAtual == null) 
            {
                var usuarioNaoExistenteResult = new ServiceNotification(new Notification("Usuario", "Não existe usuario com o id informado"));
                return usuarioNaoExistenteResult;
            }
            _repository.Remove(usuarioAtual);
            _unitOfWork.PersistChanges();
            return usuarioAtual;
        }

        public Notifiable<Notification> Update(Usuario usuario)
        {
            usuario.ValidateUpdate();
            ValidaUsuarioExiste(usuario);
            if (!usuario.IsValid)
            {
                return usuario;
            }
            _repository.Update(usuario);
            _unitOfWork.PersistChanges();
            return usuario;
        }

        private void ValidaUsuarioExiste(Usuario usuario)
        {
            var usuarioAtual = GetById(usuario?.Id);
            if (usuarioAtual == null)
            {
                usuario.AddNotification("Usuario", "Não existe usuario com o id informado");
            }
        }
    }
}

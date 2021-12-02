using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.Notifications;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IQueryable<Usuario> GetAll(params Expression<Func<Usuario, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties).Where(x => x.Ativo && x.Id != "1");
        }

        public IQueryable<Usuario> GetAllLogin(params Expression<Func<Usuario, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties).Where(x => x.Ativo);
        }

        public IQueryable<Usuario> GetById(string[] ids, params Expression<Func<Usuario, object>>[] includeProperties)
        {
            return _repository.GetById(ids, includeProperties).Where(x => x.Id != "1");
        }

        public Notifiable<Notification> Remove(string id)
        {
            var usuarioAtual = GetById(new string[] { id }).FirstOrDefault();
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
            var usuarioAtual = GetById(new string[] { usuario?.Id }).FirstOrDefault();
            if (usuarioAtual == null)
            {
                usuario.AddNotification("Usuario", "Não existe usuario com o id informado");
            }
            if (!usuario.IsValid)
            {
                return usuario;
            }
            _repository.Update(usuario);
            _unitOfWork.PersistChanges();
            return usuario;
        }
    }
}

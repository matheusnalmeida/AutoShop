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
    public class ServiceProduto : IServiceProduto 
    {
        private readonly IRepositoryProduto _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceProduto(IRepositoryProduto repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Notifiable<Notification> Add(Produto produto)
        {
            if (produto.IsValid) {
                _repository.Add(produto);
                _unitOfWork.PersistChanges();
            }
            return produto;
        }

        public IQueryable<Produto> GetAll(params Expression<Func<Produto, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties).Where(x => x.Ativo);
        }

        public IQueryable<Produto> GetById(string[] ids, params Expression<Func<Produto, object>>[] includeProperties)
        {
            return _repository.GetById(ids, includeProperties);
        }

        public Notifiable<Notification> Remove(string id)
        {
            var produtoAtual = GetById(new string[] { id }).FirstOrDefault();
            if (produtoAtual == null)
            {
                var produtoNaoExistenteResult = new ServiceNotification(new Notification("Produto", "Não existe produto com o id informado"));
                return produtoNaoExistenteResult;
            }
            _repository.Remove(produtoAtual);
            _unitOfWork.PersistChanges();
            return new ServiceNotification(produtoAtual);
        }

        public Notifiable<Notification> Update(Produto produto)
        {
            produto.ValidateUpdate();
            var produtoAtual = _repository.GetById(new string[] { produto.Id }).FirstOrDefault();
            if (produtoAtual == null)
            {
                produto.AddNotification("Produto", "Não existe produto com o id informado");
            }
            if (!produto.IsValid)
            {
                return produto;
            }
            _repository.Update(produto);
            _unitOfWork.PersistChanges();
            return produto;
        }
    }
}

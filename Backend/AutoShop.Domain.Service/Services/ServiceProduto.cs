using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System.Collections.Generic;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceProduto : IServiceProduto 
    {
        private readonly IRepositoryBase<Produto> _repository;
        private readonly IRepositoryBase<InstituicaoFinanceira> _repositoryInstituicaoFinanceira;

        public ServiceProduto(IRepositoryBase<Produto> repository, IRepositoryBase<InstituicaoFinanceira> repositoryInstituicaoFinanceira)
        {
            _repository = repository;
            _repositoryInstituicaoFinanceira = repositoryInstituicaoFinanceira;
        }

        public Notifiable<Notification> Add(Produto produto)
        {
            if (produto.IsValid) {
                _repository.Add(produto);
            }

            return produto;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _repository.GetAll();
        }

        public Produto GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(Produto produto)
        {
            if (GetById(produto?.Id) == null) 
            {
                //Limpando notificações e adicionando notificação informado que o produto não existe
                produto.Clear();
                produto.AddNotification("Produto", "Não existe produto com o id informado");
                return produto;
            }
            _repository.Remove(produto);
            return produto;
        }

        public Notifiable<Notification> Update(Produto produto)
        {
            if (_repository.GetById(produto?.Id) == null)
            {
                produto.AddNotification("Produto", "Não existe produto com o id informado");
            }
            if (_repositoryInstituicaoFinanceira.GetById(produto?.IdInstituicaoFinanceira) == null) 
            {
                produto.AddNotification("Produto", "Não existe instituicao financeira com o id informado para o produto");
            }
            if (produto.IsValid)
            {
                _repository.Update(produto);
            }
            return produto;
        }
    }
}

using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System.Collections.Generic;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceProduto : IServiceProduto 
    {
        private readonly IRepositoryProduto _repository;

        public ServiceProduto(IRepositoryProduto repository)
        {
            _repository = repository;
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
            var produtoAtual = GetById(produto?.Id);
            ValidaProdutoExiste(produto, produtoAtual);
            if (!produto.IsValid)
            {
                return produto;
            }
            _repository.Remove(produtoAtual);
            return produtoAtual;
        }

        public Notifiable<Notification> Update(Produto produto)
        {
            var produtoAtual = _repository.GetById(produto?.Id);
            ValidaProdutoExiste(produto, produtoAtual);
            if (!produto.IsValid)
            {
                return produto;
            }
            produtoAtual.FillUpdate(produto);
            _repository.Update(produtoAtual);
            return produtoAtual;
        }

        private void ValidaProdutoExiste(Produto produto, Produto produtoAtual)
        {
            if (produtoAtual == null)
            {
                produto.AddNotification("Produto", "Não existe produto com o id informado");
            }
        }
    }
}

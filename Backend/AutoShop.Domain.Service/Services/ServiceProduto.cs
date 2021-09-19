﻿using AutoShop.Domain.Entities;
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

        public Notifiable<Notification> Remove(string id)
        {
            var produtoAtual = GetById(id);
            if (produtoAtual == null)
            {
                produtoAtual.AddNotification("Produto", "Não existe produto com o id informado");
                return produtoAtual;
            }
            _repository.Remove(produtoAtual);
            return produtoAtual;
        }

        public Notifiable<Notification> Update(Produto produto)
        {
            produto.ValidateUpdate();
            ValidaProdutoExiste(produto);
            if (!produto.IsValid)
            {
                return produto;
            }
            _repository.Update(produto);
            return produto;
        }

        private void ValidaProdutoExiste(Produto produto)
        {
            var produtoAtual = _repository.GetById(produto?.Id);
            if (produtoAtual == null)
            {
                produto.AddNotification("Produto", "Não existe produto com o id informado");
            }
        }
    }
}

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
        private readonly IRepositoryInstituicaoFinanceira _repositoryInstituicaoFinanceira;

        public ServiceProduto(IRepositoryProduto repository, IRepositoryInstituicaoFinanceira repositoryInstituicaoFinanceira)
        {
            _repository = repository;
            _repositoryInstituicaoFinanceira = repositoryInstituicaoFinanceira;
        }

        public Notifiable<Notification> Add(Produto produto)
        {
            ValidateInstituicaoFinanceiraExiste(produto);
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
            ValidateInstituicaoFinanceiraExiste(produto);
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

        private void ValidateInstituicaoFinanceiraExiste(Produto produto)
        {
            if (_repositoryInstituicaoFinanceira.GetById(produto?.IdInstituicaoFinanceira) == null)
            {
                produto.AddNotification("Produto", "Não existe instituicao financeira com o id informado para o produto");
            }
        }
    }
}

using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceOperacao : IServiceOperacao
    {
        private readonly IRepositoryOperacao _repository;
        private readonly IRepositoryProduto _repositoryProduto;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceOperacao(IRepositoryOperacao repository, IRepositoryProduto repositoryProduto, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _repositoryProduto = repositoryProduto;
            _unitOfWork = unitOfWork;
        }

        public Notifiable<Notification> Add(Operacao operacao)
        {
            var produtosOperacaoAtual = operacao.ProdutoOperacoes ?? new List<ProdutoOperacao>();
            var produtosExistentes = _repositoryProduto.GetById(produtosOperacaoAtual.Select(obj => obj.IdProduto).ToArray()).ToDictionary(x => x.Id, y => y);
            if (produtosOperacaoAtual.Count() != produtosExistentes.Count) 
            {
                var idProdutosExistentes = produtosExistentes.Select(x => x.Key).ToHashSet();
                var idsProdutosNaoEncontrados = produtosOperacaoAtual.Where(produtoAtual => !idProdutosExistentes.Contains(produtoAtual.Id)) ;
                operacao.AddNotification(typeof(ProdutoOperacao), $"Existem ids de produtos não existentes informados para compra! Os seguintes ids não existem: " +
                                                                                                                    $"{string.Join(", ", idsProdutosNaoEncontrados)}");
            }
            decimal valorTotalProdutos = 0;
            //Atualizando o valor dos produtos na entidade do meio pois o valor do produto pode mudar, porém para aquela operação tem que salvar o valor no momento de cadastro da operação
            foreach (var produtoOperacao in operacao.ProdutoOperacoes ?? new List<ProdutoOperacao>())
            {
                produtoOperacao.Preco = produtosExistentes[produtoOperacao.IdProduto].Preco;
                valorTotalProdutos += produtoOperacao.Preco.Valor;
            }

            var valorTotal = CalcularValorTotal(operacao.ValorVeiculo.Valor, valorTotalProdutos);
            var valorFinanciado = CalcularValorFinanciado(valorTotal.Valor, operacao.QuantidadeDeParcelas);
            operacao.AtualizarValorTotal(valorTotal);
            operacao.AtualizarValorFinanciado(valorFinanciado);

            if (operacao.IsValid) 
            {
                _repository.Add(operacao);
                _unitOfWork.PersistChanges();
            }
            return operacao;
        }

        public IQueryable<Operacao> GetAll(params Expression<Func<Operacao, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties);
        }

        public IQueryable<Operacao> GetById(string[] ids, params Expression<Func<Operacao, object>>[] includeProperties)
        {
            return _repository.GetById(ids, includeProperties);
        }

        public Preco CalcularValorFinanciado(decimal valorTotal, int quantidadeDeParcelas)
        {
            var valorFinanciado = new Preco(valorTotal / quantidadeDeParcelas);
            return valorFinanciado;
        }

        public Preco CalcularValorTotal(decimal valorVeiculo, decimal valorProdutos)
        {
            var valorTotal = new Preco(valorVeiculo + valorProdutos);
            return valorTotal;
        }

        public Notifiable<Notification> Remove(string id)
        {
            throw new InvalidOperationException("Não é possivel remover uma operação!");
        }

        public Notifiable<Notification> Update(Operacao operacao)
        {
            throw new InvalidOperationException("Não é possivel atualizar uma operação!");
        }
    }
}

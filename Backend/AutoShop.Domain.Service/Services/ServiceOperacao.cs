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
    public class ServiceOperacao : IServiceOperacao
    {
        private readonly IRepositoryOperacao _repository;
        private readonly IRepositoryProduto _repositoryProduto;

        public ServiceOperacao(IRepositoryOperacao repository, IRepositoryProduto repositoryProduto)
        {
            _repository = repository;
            _repositoryProduto = repositoryProduto;
        }

        public Notifiable<Notification> Add(Operacao operacao)
        {
            var produtosOperacaoAtual = operacao.ProdutoOperacoes ?? new List<ProdutoOperacao>();
            var produtosExistentes = _repositoryProduto.GetByIds(produtosOperacaoAtual.Select(obj => obj.IdProduto)).ToDictionary(x => x.Id, y => y);
            if (produtosOperacaoAtual.Count() != produtosExistentes.Count()) 
            {
                var idProdutosExistentes = produtosExistentes.Select(x => x.Key).ToHashSet();
                var idsProdutosNaoEncontrados = produtosOperacaoAtual.Where(produtoAtual => !idProdutosExistentes.Contains(produtoAtual.Id)) ;
                operacao.AddNotification(typeof(ProdutoOperacao), $"Existem ids de produtos não existentes informados para compra! Os seguintes ids não existem: " +
                                                                                                                    $"{string.Join(", ", idsProdutosNaoEncontrados)}");
            }
            if (operacao.IsValid) {
                //Atualizando o valor dos produtos na entidade do meio pois o valor do produto pode mudar, porém para aquela operação tem que salvar o valor no momento de cadastro da operação
                foreach (var produtoOperacao in operacao.ProdutoOperacoes ?? new List<ProdutoOperacao>())
                {
                    produtoOperacao.Preco = produtosExistentes[produtoOperacao.IdProduto].Preco;
                }
                _repository.Add(operacao);
            }

            return operacao;
        }

        public IEnumerable<Operacao> GetAll()
        {
            return _repository.GetAll();
        }

        public Operacao GetById(string id)
        {
            return _repository.GetById(id);
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

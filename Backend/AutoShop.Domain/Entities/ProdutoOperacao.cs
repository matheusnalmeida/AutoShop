using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using Flunt.Validations;
using System;

namespace AutoShop.Domain.Entities
{
    public class ProdutoOperacao : Entity
    {
        public Preco Preco { get; set; }
        public DateTime DataDeCompra { get; set; }
        public string IdOperacao { get; set; }
        public Operacao Operacao { get; set; }
        public string IdProduto { get; set; }
        public Produto Produto { get; set; }

        private ProdutoOperacao(){}
        
        public ProdutoOperacao(string idOperacao, string idProduto)
        {
            IdOperacao = idOperacao;
            IdProduto = idProduto;
            DataDeCompra = DateTime.Now;

            AddNotifications(new Contract<ProdutoOperacao>()
                                .Requires()
                                .IsNotNullOrEmpty(IdOperacao, "ProdutoOperacao.IdOperacao", "O id da operação vinculado com o produto, não pode ser nulo")
                                .IsNotNullOrEmpty(IdProduto, "ProdutoOperacao.IdProduto", "O id do produto vinculado com a operação, não pode ser nulo"));
        }
    }
}

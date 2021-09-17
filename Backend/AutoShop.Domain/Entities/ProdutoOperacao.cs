using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoShop.Domain.Entities
{
    public class ProdutoOperacao : Entity
    {
        public Preco Preco { get; set; }
        public DateTime DataCriacao { get; set; }
        public string IdOperacao { get; set; }
        public Operacao Operacao { get; set; }
        public string IdProduto { get; set; }
        public Produto Produto { get; set; }

        public ProdutoOperacao(string idOperacao, string idProduto)
        {
            IdOperacao = idOperacao;
            IdProduto = idProduto;
            DataCriacao = DateTime.Now;

            AddNotifications(new Contract<ProdutoOperacao>()
                                .Requires()
                                .IsNotNullOrEmpty(IdProduto, "ProdutoOperacao.IdProduto", "O id do produto vinculado com a operação, não pode ser nulo"));
        }
    }
}

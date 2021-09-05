using AutoShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoShop.Domain.Entities
{
    public class ProdutoOperacao : Entity
    {
        public DateTime DataCriacao { get; set; }
        public string IdOperacao { get; set; }
        public Operacao Operacao { get; set; }
        public string IdProduto { get; set; }
        public Produto Produto { get; set; }

        public ProdutoOperacao(Operacao operacao, Produto produto)
        {
            Operacao = operacao;
            Produto = produto;
            DataCriacao = DateTime.Now;

            AddNotifications(operacao, produto);
        }
    }
}

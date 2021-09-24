using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using AutoShop.Shared.Enums;
using System.Collections.Generic;
namespace AutoShop.Domain.Entities
{
    public class Produto : Entity
    {
        public Nome Nome{ get; set; }
        public Preco Preco{ get; set; }
        public ProdutoTipoEnum Tipo { get; set; }
        public bool Ativo { get; set; }
        public IList<ProdutoOperacao> ProdutoOperacoes { get; set; }

        private Produto(){}

        public Produto(Nome nome, Preco preco, ProdutoTipoEnum tipo)
        {
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
            Ativo = true;
            ProdutoOperacoes = new List<ProdutoOperacao>();

            AddNotifications(Nome, Preco);
        }

        public void FillUpdate(Preco preco) {
            Preco = preco ?? Preco;
            AddNotifications(Preco);
        }

        public void ValidateUpdate() {
            if (string.IsNullOrEmpty(Id))
            {
                AddNotification("Produto.Id", "Para atualizar o produto é necessário informar o seu id");
            }
        }
    }
}

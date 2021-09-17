using AutoShop.Domain.Enums;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoShop.Domain.Entities
{
    public class Produto : Entity
    {
        public Nome Nome{ get; set; }
        public Preco Preco{ get; set; }
        public ProdutoTipoEnum Tipo { get; set; }
        public string IdInstituicaoFinanceira { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; }
        public bool Ativo { get; set; }
        public IList<ProdutoOperacao> ProdutoOperacoes { get; set; }

        public Produto(Nome nome, Preco preco, ProdutoTipoEnum tipo, InstituicaoFinanceira instituicaoFinanceira)
        {
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
            InstituicaoFinanceira = instituicaoFinanceira;
            Ativo = true;
            ProdutoOperacoes = new List<ProdutoOperacao>();

            AddNotifications(Nome, Preco, InstituicaoFinanceira);
        }

        //Somente pode ser atualizado o valor de um produto
        public void FillUpdate(Produto produto) {
            if (produto == null) return;
            Preco = produto.Preco;
            AddNotifications(Preco);
        }
    }
}

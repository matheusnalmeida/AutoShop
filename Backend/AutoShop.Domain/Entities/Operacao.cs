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
    public class Operacao : Entity, IEntityValidate<Operacao>
    {
        public Nome Nome { get; private set; } 
        public Preco ValorTotal { get; private set; }
        public Preco ValorFinanciado { get; private set; }
        public Preco ValorVeiculo { get; private set; }
        public IList<ProdutoOperacao> ProdutoOperacoes { get; private set; }
        public string IdVeiculo { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public string IdCliente { get; private set; }
        public Usuario Cliente { get; private set; }

        private Operacao(){}

        public Operacao(Nome nome, Preco valorTotal, Preco valorFinanciado, Preco valorVeiculo, Veiculo veiculo, Usuario cliente)
        {
            Nome = nome;
            ValorTotal = valorTotal;
            ValorFinanciado = valorFinanciado;
            ValorVeiculo = valorVeiculo;
            Veiculo = veiculo;
            Cliente = cliente;

            ProdutoOperacoes = new List<ProdutoOperacao>();

            AddNotifications(Nome, ValorTotal, ValorFinanciado, veiculo, cliente);
            AddEntityValidation();
        }

        public void AddEntityValidation()
        {
            var possuiValorFinanciadoMaiorOuIgualQueFinanciado = ValorTotal != null 
                                                                && ValorFinanciado != null
                                                                && ValorTotal.Valor >= ValorFinanciado.Valor ;
            var valorTotalMaiorOuIgualQueFinanciadoContract = new Contract<Operacao>()
                    .Requires()
                    .IsTrue(possuiValorFinanciadoMaiorOuIgualQueFinanciado,
                    "Operacao",
                    "O valor total da operação tem que ser maior ou igual ao valor financiado!");

            AddNotifications(valorTotalMaiorOuIgualQueFinanciadoContract);
        }
    }
}

using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using Flunt.Validations;
using System.Collections.Generic;
namespace AutoShop.Domain.Entities
{
    public class Operacao : Entity, IEntityValidate<Operacao>
    {
        public Preco ValorTotal { get; private set; }
        public Preco ValorFinanciado { get; private set; }
        public Preco ValorVeiculo { get; private set; }
        public int QuantidadeDeParcelas { get; set; }
        public IList<ProdutoOperacao> ProdutoOperacoes { get; private set; }
        public string IdVeiculo { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public string IdCliente { get; private set; }
        public Usuario Cliente { get; private set; }
        public string IdVendedor { get; private set; }
        public Usuario Vendedor { get; private set; }

        private Operacao(){}

        public Operacao(Preco valorVeiculo, int quantidadeDeParcelas, Veiculo veiculo, Usuario cliente)
        {
            ValorVeiculo = valorVeiculo;
            QuantidadeDeParcelas = quantidadeDeParcelas;
            Veiculo = veiculo;
            Cliente = cliente;

            ProdutoOperacoes = new List<ProdutoOperacao>();

            AddNotifications(ValorTotal, ValorFinanciado, veiculo, cliente);
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
            
            var quantidadeDeParcelasContract = new Contract<Operacao>()
                .Requires()
                .IsGreaterThan(QuantidadeDeParcelas,0,
                "Operacao",
                "A quantidade de parcelas não pode ser nula ou negativa!");

            AddNotifications(valorTotalMaiorOuIgualQueFinanciadoContract, quantidadeDeParcelasContract);
        }

        public void AtualizarValorTotal(Preco valorTotal)
        {
            ValorTotal = valorTotal;
            AddNotifications(ValorTotal);
        }

        public void AtualizarValorFinanciado(Preco valorFinanciado)
        {
            ValorFinanciado = valorFinanciado;
            AddNotifications(ValorFinanciado);
        }

        public void AdicionarProdutoOperacao(ProdutoOperacao produtoOperacao) {
            if (produtoOperacao == null || !produtoOperacao.IsValid) {
                AddNotification("Operacao.Produto", "Produto inválido na operação!");
                return;
            }
            ProdutoOperacoes.Add(produtoOperacao);
        }
    }
}

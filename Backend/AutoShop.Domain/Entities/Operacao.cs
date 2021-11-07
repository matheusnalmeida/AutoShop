using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using AutoShop.Shared.Enums;
using Flunt.Validations;
using System;
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
        public OperacaoSituacaoEnum Situacao { get; set; }
        public DateTime DataCriacao { get; set; }

        private Operacao(){}

        public Operacao(Preco valorVeiculo, int quantidadeDeParcelas, Veiculo veiculo, Usuario cliente)
        {
            ValorVeiculo = valorVeiculo;
            QuantidadeDeParcelas = quantidadeDeParcelas;
            Veiculo = veiculo;
            Cliente = cliente;
            Situacao = OperacaoSituacaoEnum.EmAnalise;
            DataCriacao = DateTime.Now;

            ProdutoOperacoes = new List<ProdutoOperacao>();

            AddNotifications(veiculo, cliente);
            AddEntityValidation();
        }

        public void AddEntityValidation()
        {            
            var quantidadeDeParcelasContract = new Contract<Operacao>()
                .Requires()
                .IsGreaterThan(QuantidadeDeParcelas,0,
                "Operacao",
                "A quantidade de parcelas não pode ser nula ou negativa!");

            AddNotifications(quantidadeDeParcelasContract);
        }

        public void ValidateValorTotal() {
            var possuiValorFinanciadoMaiorOuIgualQueFinanciado = ValorTotal != null
                                                        && ValorFinanciado != null
                                                        && ValorTotal.Valor >= ValorFinanciado.Valor;

            var valorTotalMaiorOuIgualQueFinanciadoContract = new Contract<Operacao>()
                    .Requires()
                    .IsTrue(possuiValorFinanciadoMaiorOuIgualQueFinanciado,
                    "Operacao",
                    "O valor total da operação tem que ser maior ou igual ao valor financiado!");
            
            AddNotifications(valorTotalMaiorOuIgualQueFinanciadoContract);
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

        public void FillUpdate(OperacaoSituacaoEnum situacao)
        {
            Situacao = situacao;
        }

        public void ValidateUpdate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                AddNotification("Operacao.Id", "Para atualizar a operacao é necessário informar o seu id");
            }
        }
    }
}

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
        public Nome Nome { get; set; }
        public CNPJ Cnpj { get; set; }
        public Preco ValorTotal { get; set; }
        public Preco ValorFinanciado { get; set; }
        public Preco ValorVeiculo { get; set; }

        public Operacao(Nome nome, CNPJ cnpj, Preco valorTotal, Preco valorFinanciado, Preco valorVeiculo)
        {
            Nome = nome;
            Cnpj = cnpj;
            ValorTotal = valorTotal;
            ValorFinanciado = valorFinanciado;
            ValorVeiculo = valorVeiculo;

            AddNotifications(Nome, Cnpj, ValorTotal, ValorFinanciado);
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

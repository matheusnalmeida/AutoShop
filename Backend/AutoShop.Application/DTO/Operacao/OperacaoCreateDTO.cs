using AutoShop.Shared.DTO;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Operacao
{
    public class OperacaoCreateDTO : Notifiable<Notification>, IDTO
    {
        public string Nome { get; set; }
        public decimal ValorVeiculo { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public IEnumerable<string> IdsProdutos { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<OperacaoCreateDTO>()
                                .Requires()
                                .IsNotNullOrEmpty(Nome, "Operacao.Nome", "Informe um nome válido para o veiculo!")
                                .IsNotNullOrWhiteSpace(Nome, "Operacao.Nome", "O nome não pode ser vazio!")
                                .IsGreaterThan(ValorVeiculo, 0, "Operacao.ValorVeiculo", "A operação não pode ter valor do veiculo nulo ou negativo!")
                                .IsGreaterThan(QuantidadeDeParcelas, 0, "Veiculo.QuantidadeDeParcelas", "A quantidade de parcelas não pode ser nula ou negativa!"));
        }
    }
}

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
        public int QuantidadeDeParcelas { get; set; }
        public string IdVeiculo { get; set; }
        public string IdCliente { get; set; }
        public IEnumerable<string> IdsProdutos { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<OperacaoCreateDTO>()
                                .Requires()
                                .IsFalse(string.IsNullOrEmpty(IdVeiculo) || string.IsNullOrWhiteSpace(IdVeiculo), "Operacao.Veiculo", "É necessário informar um veiculo para a operação!")
                                .IsFalse(string.IsNullOrEmpty(IdCliente) || string.IsNullOrWhiteSpace(IdCliente), "Operacao.Cliente", "É necessário informar um cliente para a operação!")
                                .IsGreaterThan(QuantidadeDeParcelas, 0, "Veiculo.QuantidadeDeParcelas", "A quantidade de parcelas não pode ser nula ou negativa!"));
        }
    }
}

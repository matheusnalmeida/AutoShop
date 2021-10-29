using AutoShop.Shared.DTO;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace AutoShop.Application.DTO.Operacao
{
    public class OperacaoUpdateDTO : Notifiable<Notification>, IDTO
    {
        public int Situacao { get; set; }

        public void Validate()
        {
            var tipoValido = Enum.IsDefined(typeof(OperacaoSituacaoEnum), Situacao);

            AddNotifications(new Contract<OperacaoUpdateDTO>()
                                .Requires()
                                .IsTrue(tipoValido, "Operacao.Tipo", "Codigo de situacao para a operação é inválido!"));
        }
    }
}

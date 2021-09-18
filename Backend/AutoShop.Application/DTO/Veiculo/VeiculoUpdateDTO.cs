using AutoShop.Shared.DTO;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Veiculo
{
    public class VeiculoUpdateDTO : Notifiable<Notification>, IDTO
    {
        public string ImageURL { get; set; }
        public decimal Valor { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<VeiculoUpdateDTO>()
                                .Requires()
                                .IsUrl(ImageURL, "Veiculo.ImageURL", "A url informada para a imagem é inválida!")
                                .IsGreaterThan(Valor, 0, "Veiculo.Valor", "O preço não pode ser nulo ou negativo!"));
        }
    }
}

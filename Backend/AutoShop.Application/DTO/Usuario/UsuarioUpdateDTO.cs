using AutoShop.Shared.DTO;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Usuario
{
    public class UsuarioUpdateDTO : Notifiable<Notification>, IDTO
    {
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<UsuarioUpdateDTO>()
                                .Requires()
                                .Matches(Telefone, @"^[1-9]{2}(?:[2-8]|9[1-9])[0-9]{3}[0-9]{4}$", "Usuario.Telefone", "Número de telefone inválido!")
                                .IsEmail(Email, "Usuario.Email", "O email informado é inválido!")
                                .IsNotNullOrEmpty(Senha, "Usuario.Senha", "A senha informada é inválida!"));
        }
    }
}

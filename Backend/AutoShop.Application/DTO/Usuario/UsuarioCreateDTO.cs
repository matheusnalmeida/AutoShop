using AutoShop.Shared.DTO;
using AutoShop.Shared.Enums;
using AutoShop.Shared.Util;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Usuario
{
    public class UsuarioCreateDTO : Notifiable<Notification>, IDTO
    {
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string Senha { get; set; }

        public void Validate()
        {
            var tipoValido = Enum.IsDefined(typeof(UsuarioTipoEnum), Tipo);
            AddNotifications(new Contract<UsuarioCreateDTO>()
                                .Requires()
                                .IsTrue(GenericValidators.IsCpfValid(Cpf), "Usuario.Cpf", "O cpf informado é inválido!")
                                .IsGreaterThan(Idade, 17, "Usuario.Idade", "A idade minima para o usuário é de 18 anos")
                                .Matches(Telefone, @"^[1-9]{2}(?:[2-8]|9[1-9])[0-9]{3}[0-9]{4}$", "Usuario.Telefone", "Número de telefone inválido!")
                                .IsEmail(Email, "Usuario.Email", "O email informado é inválido!")
                                .IsNotNullOrEmpty(Senha, "Usuario.Senha", "A senha informada é inválida!")
                                .IsTrue(tipoValido, "Usuario.Tipo", "Codigo de tipo para o usuario é inválido"));
        }
    }
}

using AutoShop.Shared.DTO;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Veiculo
{
    public class Veoculo : Notifiable<Notification>, IDTO
    {
        public string Nome { get; set; }
        public string ImageURL { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public decimal Valor { get; set; }
        public int Tipo { get; set; }

        public void Validate()
        {
            var tipoValido = Enum.IsDefined(typeof(VeiculoTipoEnum), Tipo);
            AddNotifications(new Contract<Veoculo>()
                                .Requires()
                                .IsNotNullOrEmpty(Nome, "Veiculo.Nome", "Informe um nome válido para o veiculo!")
                                .IsNotNullOrWhiteSpace(Nome, "Veiculo.Nome", "O nome não pode ser vazio!")
                                .IsUrl(ImageURL, "Veiculo.ImageURL", "A url informada para a imagem é inválida!")
                                .IsGreaterThan(Ano, 1884, "Veiculo.Ano", "O Ano do veiculo não pode ser menor que 1884")
                                .IsLowerThan(Ano, DateTime.Now.Year, "Veiculo.Ano", "O Ano do veiculo não pode ser maior que o ano atual")
                                .IsNotNullOrEmpty(Modelo, "Veiculo.Modelo", "Informe um valor para o modelo do veiculo!")
                                .IsGreaterThan(Valor, 0, "Veiculo.Valor", "O preço não pode ser nulo ou negativo!")
                                .IsTrue(tipoValido, "Veiculo.Tipo", "Codigo de tipo para o veiculo é inválido!"));
        }
    }
}

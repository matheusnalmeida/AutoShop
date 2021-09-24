using AutoShop.Shared.DTO;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace AutoShop.Application.DTO.Produto
{
    public class ProdutoCreateDTO : Notifiable<Notification>, IDTO 
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Tipo { get; set; }

        public void Validate()
        {
            var tipoValido = Enum.IsDefined(typeof(ProdutoTipoEnum), Tipo);
            AddNotifications(new Contract<ProdutoCreateDTO>()
                                .Requires()
                                .IsNotNullOrEmpty(Nome, "Produto.Nome", "Informe um nome válido para o produto!")
                                .IsNotNullOrWhiteSpace(Nome, "Produto.Nome", "O nome não pode ser vazio!")
                                .IsGreaterThan(Preco, 0, "Produto.Preco", "O produto não pode ter preço nulo ou negativo!")
                                .IsTrue(tipoValido, "Produto.Tipo", "Codigo de tipo para o produto é inválido"));
        }
    }
}

using AutoShop.Shared.DTO;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Produto
{
    public class ProdutoUpdateDTO : Notifiable<Notification>, IDTO
    {
        public decimal Preco { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<ProdutoUpdateDTO>()
                                .Requires()
                                .IsGreaterThan(Preco, 0, "Produto.Preco", "O produto não pode ter preço nulo ou negativo!"));
        }
    }
}

using AutoShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Produto
{
    public class ProdutoGetDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }

        public static ProdutoGetDTO MapEntityAsDTO(Domain.Entities.Produto produto) {
            return  new ProdutoGetDTO()
            {
                Nome = produto.Nome.Valor,
                Preco = produto.Preco.Valor,
                Tipo = produto.Tipo.ToString()
            };
        }
    }
}

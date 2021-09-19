using AutoShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Veiculo
{
    public class VeiculoGetDTO
    {
        public string Nome { get; set; }
        public string ImageURL { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }

        public static VeiculoGetDTO MapEntityAsDTO(Domain.Entities.Veiculo veiculo)
        {
            return veiculo == null ? null : new VeiculoGetDTO()
            {
                Nome = veiculo.Nome.Valor,
                Ano = veiculo.Ano,
                Modelo = veiculo.Modelo,
                Valor = veiculo.Preco.Valor,
                ImageURL = veiculo.ImagemURL,
                Tipo = veiculo.Tipo.ToString()
            };
        }
    }
}

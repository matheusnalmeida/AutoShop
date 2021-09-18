using AutoShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Veiculo
{
    public class VeiculoCreateDTO
    {
        public string Nome { get; set; }
        public string ImageURL { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        public VeiculoTipoEnum Tipo { get; set; }
    }
}

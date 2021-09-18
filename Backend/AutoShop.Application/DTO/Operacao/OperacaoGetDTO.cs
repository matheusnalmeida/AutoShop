using AutoShop.Application.DTO.Produto;
using AutoShop.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Operacao
{
    public class OperacaoGetDTO
    {
        public string Nome { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorFinanciado { get; set; }
        public decimal ValorVeiculo { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public IEnumerable<ProdutoGetDTO> Produtos { get; set; }
    }
}

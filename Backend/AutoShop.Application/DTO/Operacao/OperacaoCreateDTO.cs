using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Operacao
{
    public class OperacaoCreateDTO
    {
        public string Nome { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorFinanciado { get; set; }
        public decimal ValorVeiculo { get; set; }
        public IEnumerable<string> IdsProdutos { get; set; }
    }
}

using AutoShop.Application.DTO.Produto;
using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.DTO.Veiculo;
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
        public decimal ValorTotal { get; set; }
        public decimal ValorFinanciado { get; set; }
        public decimal ValorVeiculo { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public VeiculoGetDTO Veiculo { get; set; }
        public UsuarioGetDTO Cliente { get; set; }
        public UsuarioGetDTO Vendedor { get; set; }
        public IEnumerable<ProdutoGetDTO> Produtos { get; set; }

        public static OperacaoGetDTO MapEntityAsDTO(Domain.Entities.Operacao operacao)
        {
            return operacao == null ? null : new OperacaoGetDTO()
            {
                ValorTotal = operacao.ValorTotal.Valor,
                ValorFinanciado = operacao.ValorFinanciado.Valor,
                ValorVeiculo = operacao.ValorVeiculo.Valor,
                QuantidadeDeParcelas = operacao.QuantidadeDeParcelas,
                Veiculo = VeiculoGetDTO.MapEntityAsDTO(operacao.Veiculo),
                Cliente = UsuarioGetDTO.MapEntityAsDTO(operacao.Cliente),
                Vendedor = UsuarioGetDTO.MapEntityAsDTO(operacao.Vendedor),
                Produtos = operacao.ProdutoOperacoes.Select(produtoOperacao => ProdutoGetDTO.MapEntityAsDTO(produtoOperacao.Produto))
            };
        }
    }
}

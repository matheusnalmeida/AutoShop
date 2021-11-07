using AutoShop.Application.DTO.Produto;
using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.DTO.Veiculo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Application.DTO.Operacao
{
    public class OperacaoGetDTO
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorFinanciado { get; set; }
        public decimal ValorVeiculo { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public VeiculoGetDTO Veiculo { get; set; }
        public UsuarioGetDTO Cliente { get; set; }
        public UsuarioGetDTO Vendedor { get; set; }
        public IEnumerable<ProdutoGetDTO> Produtos { get; set; }
        public string Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public static OperacaoGetDTO MapEntityAsDTO(Domain.Entities.Operacao operacao, int number)
        {
            return operacao == null ? null : new OperacaoGetDTO()
            {
                Id = operacao.Id,
                Number = number,
                ValorTotal = operacao.ValorTotal.Valor,
                ValorFinanciado = operacao.ValorFinanciado.Valor,
                ValorVeiculo = operacao.ValorVeiculo.Valor,
                QuantidadeDeParcelas = operacao.QuantidadeDeParcelas,
                Veiculo = VeiculoGetDTO.MapEntityAsDTO(operacao.Veiculo),
                Cliente = UsuarioGetDTO.MapEntityAsDTO(operacao.Cliente),
                Vendedor = UsuarioGetDTO.MapEntityAsDTO(operacao.Vendedor),
                Produtos = operacao.ProdutoOperacoes?.Select(produtoOperacao => ProdutoGetDTO.MapEntityAsDTO(produtoOperacao.Produto)),
                Situacao = operacao.Situacao.ToString(),
                DataCriacao = operacao.DataCriacao
            };
        }
    }
}

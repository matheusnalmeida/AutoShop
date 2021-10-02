using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Application.Result.Mapper;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceOperacao : IApplicationServiceOperacao
    {
        private readonly IServiceOperacao _serviceOperacao;
        private readonly IServiceVeiculo _serviceVeiculo;
        private readonly IServiceUsuario _serviceUsuario;

        public ApplicationServiceOperacao(
            IServiceOperacao serviceOperacao,
            IServiceVeiculo serviceVeiculo,
            IServiceUsuario serviceUsuario)
        {
            _serviceOperacao = serviceOperacao;
            _serviceVeiculo = serviceVeiculo;
            _serviceUsuario = serviceUsuario;
        }

        public ApplicationCreateResult Add(OperacaoCreateDTO operacaoDTO)
        {
            var veiculo = _serviceVeiculo.GetById(operacaoDTO.IdVeiculo);
            var cliente = _serviceUsuario.GetById(operacaoDTO.IdCliente);
            if (veiculo == null)
            {
                operacaoDTO.AddNotification("Operacao.Veiculo", "Não existe veiculo com o id informado!");
            }
            if (cliente == null)
            {
                operacaoDTO.AddNotification("Operacao.Cliente", "Não existe cliente com o id informado!");
            }
            operacaoDTO.Validate();
            if (!operacaoDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(null, operacaoDTO);
            }
            //Entidade
            var operacao = new Operacao(veiculo.Preco, operacaoDTO.QuantidadeDeParcelas, veiculo, cliente);
            foreach (var idProduto in operacaoDTO.IdsProdutos)
            {
                operacao.AdicionarProdutoOperacao(new ProdutoOperacao(operacao.Id, idProduto));
            }
            var result = _serviceOperacao.Add(operacao);
            return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(operacao.Id, result);
        }

        public IEnumerable<OperacaoGetDTO> GetAll()
        {
            var veiculoDTO = _serviceOperacao.GetAll().Select(operacao => OperacaoGetDTO.MapEntityAsDTO(operacao));
            return veiculoDTO;
        }

        public OperacaoGetDTO GetById(string id)
        {
            var operacao = _serviceOperacao.GetById(id);
            var operacaoDTO = OperacaoGetDTO.MapEntityAsDTO(operacao);
            return operacaoDTO;
        }
    }
}

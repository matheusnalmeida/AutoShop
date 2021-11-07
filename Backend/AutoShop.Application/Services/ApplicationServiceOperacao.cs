using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Application.Result.Mapper;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

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
            var veiculo = _serviceVeiculo.GetById(new string[]{ operacaoDTO.IdVeiculo }).FirstOrDefault();
            var cliente = _serviceUsuario.GetById(new string[] { operacaoDTO.IdCliente }).FirstOrDefault();
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
            var veiculoDTO = _serviceOperacao.GetAll(
                operacao => operacao.Veiculo,
                operacao => operacao.ProdutoOperacoes.Select(x => x.Produto),
                operacao => operacao.Cliente,
                operacao => operacao.Vendedor
            ).ToList().Select((operacao, index) => OperacaoGetDTO.MapEntityAsDTO(operacao, index + 1));
            return veiculoDTO;
        }

        public OperacaoGetDTO GetById(string id)
        {
            var operacao = _serviceOperacao.GetById(new string[] { id }, 
                operacao => operacao.Veiculo,
                operacao => operacao.ProdutoOperacoes.Select(x => x.Produto),
                operacao => operacao.Cliente,
                operacao => operacao.Vendedor).FirstOrDefault();
            var operacaoDTO = OperacaoGetDTO.MapEntityAsDTO(operacao, 1);
            return operacaoDTO;
        }

        public ApplicationResult Update(string id, OperacaoUpdateDTO operacaoDTO)
        {
            operacaoDTO.Validate();
            var operacaoAtual = _serviceOperacao.GetById(new string[] { id }).FirstOrDefault();
            if (operacaoAtual == null)
            {
                operacaoDTO.AddNotification("Operacao", "Não existe operacao com o id informado!");
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(operacaoDTO);
            }
            if (!operacaoDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(operacaoDTO);
            }
            operacaoAtual?.FillUpdate((OperacaoSituacaoEnum)operacaoDTO.Situacao);
            var result = _serviceOperacao.Update(operacaoAtual);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }
    }
}

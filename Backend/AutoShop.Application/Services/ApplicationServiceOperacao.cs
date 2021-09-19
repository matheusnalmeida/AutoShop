using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ApplicationResult Add(OperacaoCreateDTO operacaoDTO)
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
                return MountApplicationResultFromNotifiable(operacaoDTO);
            }
            //Entidade
            var operacao = new Operacao(veiculo.Preco, operacaoDTO.QuantidadeDeParcelas, veiculo, cliente);
            foreach (var idProduto in operacaoDTO.IdsProdutos)
            {
                operacao.AdicionarProdutoOperacao(new ProdutoOperacao(operacao.Id, idProduto));
            }
            var result = _serviceOperacao.Add(operacao);
            if (result.IsValid)
            {
                result.AddNotification("Veiculo", "Veiculo cadastrado com sucesso!");
            }
            return MountApplicationResultFromNotifiable(result);
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

        private static ApplicationResult MountApplicationResultFromNotifiable(Notifiable<Notification> notifiable)
        {
            return new ApplicationResult(notifiable.IsValid, notifiable.Notifications.Select(x => x.Message));
        }
    }
}

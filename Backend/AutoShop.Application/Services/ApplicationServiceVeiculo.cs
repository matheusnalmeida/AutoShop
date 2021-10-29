using AutoShop.Application.DTO.Veiculo;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Application.Result.Mapper;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceVeiculo : IApplicationServiceVeiculo
    {
        private readonly IServiceVeiculo _serviceVeiculo;

        public ApplicationServiceVeiculo(IServiceVeiculo serviceVeiculo)
        {
            _serviceVeiculo = serviceVeiculo;
        }

        public ApplicationCreateResult Add(VeiculoCreateDTO veiculoDTO)
        {
            veiculoDTO.Validate();
            if (!veiculoDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(null, veiculoDTO);
            }
            // VO
            var nome = new Nome(veiculoDTO.Nome);
            var preco = new Preco(veiculoDTO.Valor);
            //Entidade
            var veiculo = new Veiculo(nome, veiculoDTO.Ano,veiculoDTO.Modelo, preco, veiculoDTO.ImageURL, (VeiculoTipoEnum)veiculoDTO.Tipo);
            var result = _serviceVeiculo.Add(veiculo);
            return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(veiculo.Id, result);
        }

        public IEnumerable<VeiculoGetDTO> GetAll()
        {
            var veiculoDTO = _serviceVeiculo.GetAll().Select(veiculo => VeiculoGetDTO.MapEntityAsDTO(veiculo));
            return veiculoDTO;
        }

        public VeiculoGetDTO GetById(string id)
        {
            var veiculo = _serviceVeiculo.GetById(new string[] { id }).FirstOrDefault();
            var veiculoDTO = VeiculoGetDTO.MapEntityAsDTO(veiculo);
            return veiculoDTO;
        }

        public ApplicationResult Remove(string id)
        {
            var result = _serviceVeiculo.Remove(id);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }

        public ApplicationResult Update(string id, VeiculoUpdateDTO veiculoDTO)
        {
            veiculoDTO.Validate();
            var veiculoAtual = _serviceVeiculo.GetById(new string[] { id }).FirstOrDefault();
            if (veiculoAtual == null)
            {
                veiculoDTO.AddNotification("Veiculo", "Não existe veiculo com o id informado!");
            }
            if (!veiculoDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(veiculoDTO);
            }
            var preco = new Preco(veiculoDTO.Valor);
            veiculoAtual?.FillUpdate(preco);
            var result = _serviceVeiculo.Update(veiculoAtual);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }
    }
}

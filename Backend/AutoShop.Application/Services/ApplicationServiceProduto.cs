using AutoShop.Application.DTO.Produto;
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
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto _serviceProduto;

        public ApplicationServiceProduto(IServiceProduto serviceProduto)
        {
            _serviceProduto = serviceProduto;
        }

        public ApplicationCreateResult Add(ProdutoCreateDTO produtoDTO)
        {
            produtoDTO.Validate();
            if (!produtoDTO.IsValid) 
            {
                return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(null, produtoDTO);
            }
            //VO
            var nome = new Nome(produtoDTO.Nome);
            var preco = new Preco(produtoDTO.Preco);
            //Entidade
            var produto = new Produto(nome, preco, (ProdutoTipoEnum)produtoDTO.Tipo);
            var result = _serviceProduto.Add(produto);
            return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(produto.Id, result);
        }

        public IEnumerable<ProdutoGetDTO> GetAll()
        {
            var produtosDTO = _serviceProduto.GetAll().Select(produto => ProdutoGetDTO.MapEntityAsDTO(produto));
            return produtosDTO;
        }

        public ProdutoGetDTO GetById(string id)
        {
            var produto = _serviceProduto.GetById(id);
            var produtoDTO = ProdutoGetDTO.MapEntityAsDTO(produto);
            return produtoDTO;
        }

        public ApplicationResult Remove(string id)
        {
            var result = _serviceProduto.Remove(id);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }

        public ApplicationResult Update(string id, ProdutoUpdateDTO produtoDTO)
        {
            produtoDTO.Validate();
            var produtoAtual = _serviceProduto.GetById(id);
            if (produtoAtual == null)
            {
                produtoDTO.AddNotification("Produto", "Não existe produto com o id informado!");
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(produtoDTO);
            }
            if (!produtoDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(produtoDTO);
            }
            var novoPreco = new Preco(produtoDTO.Preco);
            produtoAtual?.FillUpdate(novoPreco);
            var result = _serviceProduto.Update(produtoAtual);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }
    }
}

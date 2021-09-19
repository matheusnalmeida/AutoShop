using AutoShop.Application.DTO.Produto;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto _serviceProduto;

        public ApplicationServiceProduto(IServiceProduto serviceProduto)
        {
            _serviceProduto = serviceProduto;
        }

        public ApplicationResult Add(ProdutoCreateDTO produtoDTO)
        {
            produtoDTO.Validate();
            if (!produtoDTO.IsValid) 
            {
                return MountApplicationResultFromNotifiable(produtoDTO);
            }
            //VO
            var nome = new Nome(produtoDTO.Nome);
            var preco = new Preco(produtoDTO.Preco);
            //Entidade
            var produto = new Produto(nome, preco, (ProdutoTipoEnum)produtoDTO.Tipo);
            var result = _serviceProduto.Add(produto);
            if (result.IsValid)
            {
                result.AddNotification("Produto", "Produto cadastrado com sucesso!");
            }
            return MountApplicationResultFromNotifiable(result);
        }

        public IEnumerable<ProdutoGetDTO> GetAll()
        {
            var produtosDTO = _serviceProduto.GetAll().Select(produto => 
                                                        new ProdutoGetDTO() {
                                                            Nome = produto.Nome.Valor,
                                                            Preco = produto.Preco.Valor,
                                                            Tipo = produto.ToString()
                                                        });
            return produtosDTO;
        }

        public ProdutoGetDTO GetById(string id)
        {
            var produto = _serviceProduto.GetById(id);
            var produtoDTO = new ProdutoGetDTO()
            {
                Nome = produto.Nome.Valor,
                Preco = produto.Preco.Valor,
                Tipo = produto.ToString()
            };
            return produtoDTO;
        }

        public ApplicationResult Remove(string id)
        {
            var result = _serviceProduto.Remove(id);
            return MountApplicationResultFromNotifiable(result);
        }

        public ApplicationResult Update(string id, ProdutoUpdateDTO produtoDTO)
        {
            produtoDTO.Validate();
            if (!produtoDTO.IsValid)
            {
                return MountApplicationResultFromNotifiable(produtoDTO);
            }
            var produtoAtual = _serviceProduto.GetById(id);
            if (produtoAtual == null) {
                produtoDTO.AddNotification("Produto", "Não existe produto com o id informado!");
                return MountApplicationResultFromNotifiable(produtoDTO);
            }
            var novoPreco = new Preco(produtoDTO.Preco);
            produtoAtual.FillUpdate(novoPreco);
            var result = _serviceProduto.Update(produtoAtual);
            if (result.IsValid)
            {
                result.AddNotification("Produto", "Produto cadastrado com sucesso!");
            }
            return MountApplicationResultFromNotifiable(result);
        }

        private ApplicationResult MountApplicationResultFromNotifiable(Notifiable<Notification> notifiable) {
            return new ApplicationResult(notifiable.IsValid, notifiable.Notifications.Select(x => x.Message));
        }
    }
}

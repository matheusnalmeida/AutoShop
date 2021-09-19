using AutoShop.Domain.Entities;
using AutoShop.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Interfaces.Services
{
    public interface IServiceOperacao : IServiceBase<Operacao>
    {
        Preco CalcularValorTotal(decimal valorVeiculo, decimal valorProdutos);
        Preco CalcularValorFinanciado(decimal valorTotal, int quantidadeDeParcelas);
    }
}

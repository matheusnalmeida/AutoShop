using AutoShop.Domain.Entities;
using AutoShop.Domain.ValueObjects;

namespace AutoShop.Domain.Interfaces.Services
{
    public interface IServiceOperacao : IServiceBase<Operacao>
    {
        Preco CalcularValorTotal(decimal valorVeiculo, decimal valorProdutos);
        Preco CalcularValorFinanciado(decimal valorTotal, int quantidadeDeParcelas);
    }
}

using AutoShop.Application.DTO.Produto;
using AutoShop.Application.Result;
using System.Collections.Generic;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        ApplicationCreateResult Add(ProdutoCreateDTO obj);

        ProdutoGetDTO GetById(string Id);

        IEnumerable<ProdutoGetDTO> GetAll();

        ApplicationResult Update(string id, ProdutoUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

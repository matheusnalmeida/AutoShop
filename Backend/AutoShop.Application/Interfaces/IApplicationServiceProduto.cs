using AutoShop.Application.DTO.Produto;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        ApplicationResult Add(ProdutoCreateDTO obj);

        ProdutoGetDTO GetById(string Id);

        IEnumerable<ProdutoGetDTO> GetAll();

        ApplicationResult Update(ProdutoUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

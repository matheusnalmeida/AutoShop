using AutoShop.Application.DTO.Produto;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        public ApplicationResult Add(ProdutoCreateDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoGetDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProdutoGetDTO GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Update(ProdutoUpdateDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}

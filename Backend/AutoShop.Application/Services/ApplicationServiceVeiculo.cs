using AutoShop.Application.DTO.Veiculo;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceVeiculo : IApplicationServiceVeiculo
    {
        public ApplicationResult Add(VeiculoCreateDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VeiculoGetDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public VeiculoGetDTO GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Update(VeiculoUpdateDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}

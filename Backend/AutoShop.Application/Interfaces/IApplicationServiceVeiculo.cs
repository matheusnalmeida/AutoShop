using AutoShop.Application.DTO.Veiculo;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceVeiculo
    {
        ApplicationResult Add(Veoculo obj);

        VeiculoGetDTO GetById(string Id);

        IEnumerable<VeiculoGetDTO> GetAll();

        ApplicationResult Update(string id, VeiculoUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

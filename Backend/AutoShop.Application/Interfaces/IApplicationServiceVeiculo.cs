using AutoShop.Application.DTO.Veiculo;
using AutoShop.Application.Result;
using System.Collections.Generic;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceVeiculo
    {
        ApplicationResult Add(VeiculoCreateDTO obj);

        VeiculoGetDTO GetById(string Id);

        IEnumerable<VeiculoGetDTO> GetAll();

        ApplicationResult Update(string id, VeiculoUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

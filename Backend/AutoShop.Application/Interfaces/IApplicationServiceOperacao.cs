using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Result;
using System.Collections.Generic;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceOperacao
    {
        ApplicationCreateResult Add(OperacaoCreateDTO obj);

        OperacaoGetDTO GetById(string Id);

        IEnumerable<OperacaoGetDTO> GetAll();

        ApplicationResult Update(string id, OperacaoUpdateDTO obj);
    }
}

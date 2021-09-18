using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceOperacao
    {
        ApplicationResult Add(OperacaoCreateDTO obj);

        OperacaoGetDTO GetById(string Id);

        IEnumerable<OperacaoGetDTO> GetAll();
    }
}

using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceOperacao : IApplicationServiceOperacao
    {
        public ApplicationResult Add(OperacaoCreateDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperacaoGetDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public OperacaoGetDTO GetById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceUsuario : IApplicationServiceUsuario
    {
        public ApplicationResult Add(UsuarioCreateDTO obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioGetDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UsuarioGetDTO GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult Update(UsuarioUpdateDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}

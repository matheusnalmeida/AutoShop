using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceUsuario
    {
        ApplicationResult Add(UsuarioCreateDTO obj);

        UsuarioGetDTO GetById(string Id);

        IEnumerable<UsuarioGetDTO> GetAll();

        ApplicationResult Update(string id, UsuarioUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

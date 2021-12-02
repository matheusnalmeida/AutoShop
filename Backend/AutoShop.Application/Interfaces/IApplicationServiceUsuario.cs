using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Result;
using System.Collections.Generic;

namespace AutoShop.Application.Interfaces
{
    public interface IApplicationServiceUsuario
    {
        ApplicationCreateResult Add(UsuarioCreateDTO obj);

        UsuarioGetDTO GetById(string Id);

        IEnumerable<UsuarioGetDTO> GetAll();

        IEnumerable<UsuarioGetDTO> GetAllLogin();

        ApplicationResult Update(string id, UsuarioUpdateDTO obj);

        ApplicationResult Remove(string Id);
    }
}

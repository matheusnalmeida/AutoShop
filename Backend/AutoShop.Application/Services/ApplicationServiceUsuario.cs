using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Application.Result.Mapper;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceUsuario : IApplicationServiceUsuario
    {
        private readonly IServiceUsuario _serviceUsuario;

        public ApplicationServiceUsuario(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        public ApplicationCreateResult Add(UsuarioCreateDTO usuarioDTO)
        {
            usuarioDTO.Validate();
            if (!usuarioDTO.IsValid) 
            {
                return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(null, usuarioDTO);
            }
            // VO
            var cpf = new CPF(usuarioDTO.Cpf);
            var telefone = new Telefone(usuarioDTO.Telefone);
            var email = new Email(usuarioDTO.Email);
            var senha = new Senha(usuarioDTO.Senha);
            //Entidade
            var usuario = new Usuario(cpf, usuarioDTO.Idade, telefone, email, senha, (UsuarioTipoEnum)usuarioDTO.Tipo);
            var result = _serviceUsuario.Add(usuario);
            return ApplicationResultMapper.MountApplicationCreateResultFromNotifiable(usuario.Id, result);
        }

        public IEnumerable<UsuarioGetDTO> GetAll()
        {
            var produtosDTO = _serviceUsuario.GetAll().Select(usuario => UsuarioGetDTO.MapEntityAsDTO(usuario));
            return produtosDTO;
        }

        public IEnumerable<UsuarioGetDTO> GetAllLogin()
        {
            var produtosDTO = _serviceUsuario.GetAllLogin().Select(usuario => UsuarioGetDTO.MapEntityAsDTO(usuario));
            return produtosDTO;
        }

        public UsuarioGetDTO GetById(string id)
        {
            var usuario = _serviceUsuario.GetById(new string[] { id }).FirstOrDefault();
            var usuarioDTO = UsuarioGetDTO.MapEntityAsDTO(usuario);
            return usuarioDTO;
        }

        public ApplicationResult Remove(string id)
        {
            var result = _serviceUsuario.Remove(id);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }

        public ApplicationResult Update(string id, UsuarioUpdateDTO usuarioDTO)
        {
            usuarioDTO.Validate();
            var usuarioAtual = _serviceUsuario.GetById(new string[] { id }).FirstOrDefault();
            if (usuarioAtual == null)
            {
                usuarioDTO.AddNotification("Usuario", "Não existe usuario com o id informado!");
            }
            if (!usuarioDTO.IsValid)
            {
                return ApplicationResultMapper.MountApplicationResultFromNotifiable(usuarioDTO);
            }
            var telefone = new Telefone(usuarioDTO.Telefone);
            var email = new Email(usuarioDTO.Email);
            var senha = new Senha(usuarioDTO.Senha);
            usuarioAtual?.FillUpdate(telefone, email, senha);
            var result = _serviceUsuario.Update(usuarioAtual);
            return ApplicationResultMapper.MountApplicationResultFromNotifiable(result);
        }
    }
}

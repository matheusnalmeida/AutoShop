using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Enums;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Services
{
    public class ApplicationServiceUsuario : IApplicationServiceUsuario
    {
        private readonly IServiceUsuario _serviceUsuario;

        public ApplicationServiceUsuario(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        public ApplicationResult Add(UsuarioCreateDTO usuarioDTO)
        {
            usuarioDTO.Validate();
            if (!usuarioDTO.IsValid) 
            {
                return MountApplicationResultFromNotifiable(usuarioDTO);
            }
            // VO
            var cpf = new CPF(usuarioDTO.Cpf);
            var telefone = new Telefone(usuarioDTO.Telefone);
            var email = new Email(usuarioDTO.Email);
            var senha = new Senha(usuarioDTO.Senha);
            //Entidade
            var usuario = new Usuario(cpf, usuarioDTO.Idade, telefone, email, senha, (UsuarioTipoEnum)usuarioDTO.Tipo);
            var result = _serviceUsuario.Add(usuario);
            return MountApplicationResultFromNotifiable(result);
        }

        public IEnumerable<UsuarioGetDTO> GetAll()
        {
            var produtosDTO = _serviceUsuario.GetAll().Select(usuario => UsuarioGetDTO.MapEntityAsDTO(usuario));
            return produtosDTO;
        }

        public UsuarioGetDTO GetById(string id)
        {
            var usuario = _serviceUsuario.GetById(id);
            var usuarioDTO = UsuarioGetDTO.MapEntityAsDTO(usuario);
            return usuarioDTO;
        }

        public ApplicationResult Remove(string id)
        {
            var result = _serviceUsuario.Remove(id);
            return MountApplicationResultFromNotifiable(result);
        }

        public ApplicationResult Update(string id, UsuarioUpdateDTO usuarioDTO)
        {
            usuarioDTO.Validate();
            var usuarioAtual = _serviceUsuario.GetById(id);
            if (usuarioAtual == null)
            {
                usuarioDTO.AddNotification("Usuario", "Não existe usuario com o id informado!");
            }
            if (!usuarioDTO.IsValid)
            {
                return MountApplicationResultFromNotifiable(usuarioDTO);
            }
            var telefone = new Telefone(usuarioDTO.Telefone);
            var email = new Email(usuarioDTO.Email);
            var senha = new Senha(usuarioDTO.Senha);
            usuarioAtual?.FillUpdate(telefone, email, senha);
            var result = _serviceUsuario.Update(usuarioAtual);
            return MountApplicationResultFromNotifiable(result);
        }

        private static ApplicationResult MountApplicationResultFromNotifiable(Notifiable<Notification> notifiable)
        {
            return new ApplicationResult(notifiable.IsValid, notifiable.Notifications.Select(x => x.Message));
        }
    }
}

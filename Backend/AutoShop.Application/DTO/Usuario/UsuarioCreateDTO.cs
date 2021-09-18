using AutoShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.DTO.Usuario
{
    public class UsuarioCreateDTO
    {
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
    }
}

using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using AutoShop.Shared.Enums;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Entities
{
    public class Usuario : Entity, IEntityValidate<Usuario>
    {
        public CPF Cpf { get; set; }
        public int Idade { get; set; }
        public Telefone Telefone { get; set; }
        public Email Email { get; set; }
        public Senha Senha { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
        public List<Operacao> Operacoes { get; set; }
        public bool Ativo { get; set; }

        private Usuario(){}
        
        public Usuario(CPF cpf, int idade, Telefone telefone, Email email, Senha senha, UsuarioTipoEnum tipo)
        {
            Cpf = cpf;
            Idade = idade;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Operacoes = new List<Operacao>();
            Ativo = true;

            AddNotifications(cpf, telefone, email, senha);
            AddEntityValidation();
        }

        public void AddEntityValidation()
        {
            var idadeContract = new Contract<Usuario>()
                    .Requires()
                    .IsGreaterThan(Idade, 17, "Usuario.Idade", "A idade minima para o usuário é de 18 anos");

            AddNotifications(idadeContract);
        }

        public void FillUpdate(Telefone telefone, Email email, Senha senha) {
            Telefone = telefone ?? Telefone;
            Email = email ?? Email;
            Senha = senha ?? Senha;
            AddNotifications(Telefone, Email, Senha);
        }
        public void ValidateUpdate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                AddNotification("Usuario.Id", "Para atualizar o usuario é necessário informar o seu id");
            }
        }
    }
}

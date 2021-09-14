using AutoShop.Domain.Enums;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
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
        public decimal RendaMedia { get; set; }
        public int Idade { get; set; }
        public Telefone Telefone { get; set; }
        public Email Email { get; set; }
        public string Senha { get; set; }
        public ClienteTipoEnum Tipo { get; set; }
        public List<Operacao> Operacoes { get; set; }
        public bool Ativo { get; set; }

        public Usuario(CPF cpf, decimal rendaMedia, int idade, Telefone telefone, Email email, string senha, ClienteTipoEnum tipo)
        {
            Cpf = cpf;
            RendaMedia = rendaMedia;
            Idade = idade;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Operacoes = new List<Operacao>();
            Ativo = true;

            AddNotifications(cpf, telefone, email);
            AddEntityValidation();
        }

        public void AddEntityValidation()
        {
            var idadeContract = new Contract<Usuario>()
                    .Requires()
                    .IsGreaterThan(Idade, 17, "Usuario.Idade", "A idade minima para o usuário é de 18 anos");

            var senhaContract = new Contract<Usuario>()
                    .Requires()
                    .IsNotNullOrEmpty(Senha,"Usuario.Senha", "A senha não pode ser vazia");

            AddNotifications(idadeContract, senhaContract);
        }
    }
}

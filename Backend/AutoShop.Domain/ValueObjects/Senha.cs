using AutoShop.Shared.ValueObjects;
using Flunt.Validations;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoShop.Domain.ValueObjects
{
    public class Senha : ValueObject
    {
        public string Valor { get; set; }

        private Senha() { }

        public Senha(string senha)
        {
            AddNotifications(
                new Contract<Senha>()
                .Requires()
                .IsNotNullOrEmpty(senha, "Senha.Valor", "Senha inválida")
                .IsNotNullOrWhiteSpace(senha, "Senha.Valor", "Senha inválida"));

            Valor = HashPassword(senha);
        }

        private string HashPassword(string senha) {
            if (!IsValid) {
                return senha;
            }

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
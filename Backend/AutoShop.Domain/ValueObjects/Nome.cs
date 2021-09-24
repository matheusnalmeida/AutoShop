using AutoShop.Shared.ValueObjects;
using Flunt.Validations;

namespace AutoShop.Domain.ValueObjects
{
    public class Nome : ValueObject
    {
        public string Valor { get; set; }

        private Nome() { }

        public Nome(string nome)
        {
            Valor = nome;

            AddNotifications(new Contract<Nome>()
                    .Requires()
                    .IsNotNullOrEmpty(Valor, "Nome", "Nome não informado")
                    .IsLowerOrEqualsThan(Valor, 50, "Nome", "O nome deve ter até 50 caracteres"));
        }
    }
}

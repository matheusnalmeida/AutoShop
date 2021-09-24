using AutoShop.Shared.ValueObjects;
using Flunt.Validations;

namespace AutoShop.Domain.ValueObjects
{
    public class Preco : ValueObject
    {
        public decimal Valor { get; set; }

        private Preco() { }

        public Preco(decimal valor)
        {
            Valor = valor;

            AddNotifications(new Contract<Preco>()
                    .Requires()
                    .IsGreaterThan(Valor, 0, "Preco.Valor", "O preço não pode ser nulo ou negativo"));
        }
    }
}

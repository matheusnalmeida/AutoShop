using AutoShop.Shared.ValueObjects;
using Flunt.Validations;

namespace AutoShop.Domain.ValueObjects
{
    public class Telefone : ValueObject
    {
        public string Numero { get; set; }

        private Telefone() { }

        public Telefone(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract<Telefone>()
                    .Requires()
                    .Matches(Numero, @"^[1-9]{2}(?:[2-8]|9[1-9])[0-9]{3}[0-9]{4}$", "Telefone.Numero", "Número de telefone inválido"));
        }
    }
}

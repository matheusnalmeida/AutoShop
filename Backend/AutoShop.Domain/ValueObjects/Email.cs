using AutoShop.Shared.ValueObjects;
using Flunt.Validations;

namespace AutoShop.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Endereco { get; private set; }

        private Email() { }

        public Email(string address)
        {
            Endereco = address;

            AddNotifications(new Contract<Email>()
                .Requires()
                .IsEmail(Endereco, "Email.Address", "E-mail inválido")
            );
        }
    }
}

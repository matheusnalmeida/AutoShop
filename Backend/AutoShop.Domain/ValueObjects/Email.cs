using AutoShop.Shared.ValueObjects;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        [Column("Email")]
        public string Address { get; private set; }

        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Email>()
                .Requires()
                .IsEmail(Address, "Email.Address", "E-mail inválido")
            );
        }
    }
}

using AutoShop.Shared.Util;
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
    public class CPF : ValueObject
    {
        public string Numero { get;  set; }

        private CPF() { }

        public CPF(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract<CPF>()
                .Requires()
                .IsTrue(GenericValidators.IsCpfValid(Numero), "CPF.Numero", "CPF inválido")
            );
        }
    }
}

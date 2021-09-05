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
    public class Nome : ValueObject
    {
        [Column("Nome")]
        public string Valor { get; set; }
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

using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoShop.Domain.Entities
{
    public class InstituicaoFinanceira : Entity
    {
        public CNPJ Cnpj { get; set; }
        public Nome Nome { get; set; }
        public List<Produto> Produtos { get; set; }

        public InstituicaoFinanceira(CNPJ cnpj, Nome nome)
        {
            Cnpj = cnpj;
            Nome = nome;
            Produtos = new List<Produto>();

            AddNotifications(Cnpj, Nome);            
        }
    }
}

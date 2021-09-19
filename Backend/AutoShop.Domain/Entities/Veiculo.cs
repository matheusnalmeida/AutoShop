using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Entities;
using AutoShop.Shared.Enums;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Entities
{
    public class Veiculo : Entity, IEntityValidate<Veiculo>
    {
        public Nome Nome { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public Preco Preco { get; set; }
        public string ImagemURL { get; set; }
        public VeiculoTipoEnum Tipo { get; set; }
        public List<Operacao> Operacoes { get; set; }
        public bool Ativo { get; set; }

        private Veiculo(){}

        public Veiculo(Nome nome, int ano, string modelo, Preco preco, string imagemURL, VeiculoTipoEnum tipo)
        {
            Nome = nome; 
            Ano = ano;
            Modelo = modelo;
            Preco = preco;
            ImagemURL = imagemURL;
            Tipo = tipo;
            Operacoes = new List<Operacao>();
            Ativo = true;

            AddNotifications(Nome, Preco);
            AddEntityValidation();
        }

        public void AddEntityValidation()
        {
            var anoContract = new Contract<Veiculo>()
                .Requires()
                .IsGreaterThan(Ano, 1884, "Veiculo.Ano", "O Ano do veiculo não pode ser menor que 1884")
                .IsLowerThan(Ano, DateTime.Now.Year, "Veiculo.Ano", "O Ano do veiculo não pode ser maior que o ano atual");

            var modeloContract = new Contract<Veiculo>()
                .Requires()
                .IsNotNullOrEmpty(Modelo, "Veiculo.Modelo", "Modelo não informado")
                .IsLowerOrEqualsThan(Modelo, 50, "Veiculo.Modelo", "O Modelo deve ter até 50 caracteres");

            var imagemUrContract = new Contract<Veiculo>()
                .Requires()
                .IsUrl(ImagemURL, "Veiculo.ImagemURL", "Url para imagem do veiculo é inválida");

            AddNotifications(anoContract, modeloContract, imagemUrContract);
        }

        public void FillUpdate(Preco preco) {
            Preco = preco ?? Preco;
            AddNotifications(Preco);
        }

        public void ValidateUpdate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                AddNotification("Veiculo.Id", "Para atualizar o veiculo é necessário informar o seu id");
            }
        }
    }
}

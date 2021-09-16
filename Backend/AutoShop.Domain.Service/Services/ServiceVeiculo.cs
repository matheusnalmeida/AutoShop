using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceVeiculo : IServiceVeiculo
    {
        private readonly IRepositoryVeiculo _repository;

        public ServiceVeiculo(IRepositoryVeiculo repository)
        {
            _repository = repository;
        }

        public Notifiable<Notification> Add(Veiculo veiculo)
        {
            if (veiculo.IsValid)
            {
                _repository.Add(veiculo);
            }
            return veiculo;
        }

        public IEnumerable<Veiculo> GetAll()
        {
            return _repository.GetAll();
        }

        public Veiculo GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(Veiculo veiculo)
        {
            var veiculoAtual = GetById(veiculo?.Id);
            ValidaVeiculoExiste(veiculo, veiculoAtual);
            if (veiculo.IsValid)
            {
                return veiculo;
            }
            _repository.Remove(veiculoAtual);
            return veiculoAtual;
        }

        public Notifiable<Notification> Update(Veiculo veiculo)
        {
            var veiculoAtual = GetById(veiculo?.Id);
            ValidaVeiculoExiste(veiculo, veiculoAtual);
            if (veiculo.IsValid)
            {
                return veiculo;
            }
            veiculoAtual.FillUpdate(veiculo);
            _repository.Update(veiculoAtual);
            return veiculoAtual;
        }

        private void ValidaVeiculoExiste(Veiculo veiculo, Veiculo veiculoAtual)
        {
            if (veiculoAtual == null)
            {
                veiculo.AddNotification("Instituição Financeira", "Não existe instituição financeira com o id informado");
            }
        }
    }
}

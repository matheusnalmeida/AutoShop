﻿using AutoShop.Domain.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public ServiceVeiculo(IRepositoryVeiculo repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Notifiable<Notification> Add(Veiculo veiculo)
        {
            if (veiculo.IsValid)
            {
                _repository.Add(veiculo);
                _unitOfWork.PersistChanges();
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

        public Notifiable<Notification> Remove(string id)
        {
            var veiculoAtual = GetById(id);
            if (veiculoAtual == null)
            {
                veiculoAtual.AddNotification("Veiculo", "Não existe veiculo com o id informado");
                return veiculoAtual;
            }
            _repository.Remove(veiculoAtual);
            _unitOfWork.PersistChanges();
            return veiculoAtual;
        }

        public Notifiable<Notification> Update(Veiculo veiculo)
        {
            veiculo.ValidateUpdate();
            ValidaVeiculoExiste(veiculo);
            if (!veiculo.IsValid)
            {
                return veiculo;
            }
            _repository.Update(veiculo);
            _unitOfWork.PersistChanges();
            return veiculo;
        }

        private void ValidaVeiculoExiste(Veiculo veiculo)
        {
            var veiculoAtual = GetById(veiculo?.Id);
            if (veiculoAtual == null)
            {
                veiculo.AddNotification("Veiculo", "Não existe veiculo com o id informado");
            }
        }
    }
}

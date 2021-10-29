using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.Notifications;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IQueryable<Veiculo> GetAll(params Expression<Func<Veiculo, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties).Where(x => x.Ativo);
        }

        public IQueryable<Veiculo> GetById(string[] ids, params Expression<Func<Veiculo, object>>[] includeProperties)
        {
            return _repository.GetById(ids, includeProperties);
        }

        public Notifiable<Notification> Remove(string id)
        {
            var veiculoAtual = GetById(new string[] { id }).FirstOrDefault();
            if (veiculoAtual == null)
            {
                var veiculoNaoExistenteResult = new ServiceNotification(new Notification("Veiculo", "Não existe veiculo com o id informado"));
                return veiculoNaoExistenteResult;
            }
            _repository.Remove(veiculoAtual);
            _unitOfWork.PersistChanges();
            return veiculoAtual;
        }

        public Notifiable<Notification> Update(Veiculo veiculo)
        {
            veiculo.ValidateUpdate();
            var veiculoAtual = GetById(new string[] { veiculo?.Id }).FirstOrDefault();
            if (veiculoAtual == null)
            {
                veiculo.AddNotification("Veiculo", "Não existe veiculo com o id informado");
            }
            if (!veiculo.IsValid)
            {
                return veiculo;
            }
            _repository.Update(veiculo);
            _unitOfWork.PersistChanges();
            return veiculo;
        }
    }
}

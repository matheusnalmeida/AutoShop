using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceInstituicaoFinanceira : IServiceInstituicaoFinanceira
    {
        private readonly IRepositoryInstituicaoFinanceira _repository;
        private readonly IRepositoryProduto _repositoryProduto;

        public ServiceInstituicaoFinanceira(IRepositoryInstituicaoFinanceira repository, IRepositoryProduto repositoryProduto)
        {
            _repository = repository;
            _repositoryProduto = repositoryProduto;
        }

        public Notifiable<Notification> Add(InstituicaoFinanceira instituicaoFinanceira)
        {
            if (instituicaoFinanceira.IsValid){
                _repository.Add(instituicaoFinanceira);
            }
            return instituicaoFinanceira;
        }

        public IEnumerable<InstituicaoFinanceira> GetAll()
        {
            return _repository.GetAll();
        }

        public InstituicaoFinanceira GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(InstituicaoFinanceira instituicaoFinanceira)
        {
            var instituicaoFinanceiraAtual = GetById(instituicaoFinanceira?.Id);
            ValidaInstituicaoFinanceiraExiste(instituicaoFinanceira, instituicaoFinanceiraAtual);
            if (!instituicaoFinanceira.IsValid) 
            {
                return instituicaoFinanceira;             
            }

            var produtosIntituicaoFinanceira = _repositoryProduto.GetAll().Where(produto => produto.IdInstituicaoFinanceira.Equals(instituicaoFinanceira.Id));
            using (TransactionScope scope = new TransactionScope()) {
                foreach (var produto in produtosIntituicaoFinanceira)
                {
                    _repositoryProduto.Remove(produto);
                }
                _repository.Remove(instituicaoFinanceiraAtual);
                scope.Complete();
            }

            return instituicaoFinanceiraAtual;
        }

        public Notifiable<Notification> Update(InstituicaoFinanceira instituicaoFinanceira)
        {
            var instituicaoFinanceiraAtual = GetById(instituicaoFinanceira?.Id);
            ValidaInstituicaoFinanceiraExiste(instituicaoFinanceira, instituicaoFinanceiraAtual);
            if (!instituicaoFinanceira.IsValid)
            {
                return instituicaoFinanceira;
            }
            instituicaoFinanceiraAtual.FillUpdate(instituicaoFinanceira);
            _repository.Update(instituicaoFinanceiraAtual);
            return instituicaoFinanceiraAtual;
        }

        private void ValidaInstituicaoFinanceiraExiste(InstituicaoFinanceira instituicaoFinanceira, InstituicaoFinanceira instituicaoFinanceiraAtual)
        {
            if (instituicaoFinanceiraAtual == null)
            {
                instituicaoFinanceira.AddNotification("Instituição Financeira", "Não existe instituição financeira com o id informado");
            }
        }
    }
}

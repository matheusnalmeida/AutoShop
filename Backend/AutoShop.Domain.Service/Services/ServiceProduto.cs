using AutoShop.Domain.Entities;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.Notifications;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace AutoShop.Domain.Service.Services
{
    public class ServiceProduto : IServiceProduto 
    {
        private readonly IRepositoryProduto _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceProduto(IRepositoryProduto repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Notifiable<Notification> Add(Produto produto)
        {
            if (produto.IsValid) {
                _repository.Add(produto);
                _unitOfWork.PersistChanges();
            }
            return produto;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _repository.GetAll().Where(x => x.Ativo);
        }

        public Produto GetById(string id)
        {
            return _repository.GetById(id);
        }

        public Notifiable<Notification> Remove(string id)
        {
            var produtoAtual = GetById(id);
            if (produtoAtual == null)
            {
                var produtoNaoExistenteResult = new ServiceNotification(new Notification("Produto", "Não existe produto com o id informado"));
                return produtoNaoExistenteResult;
            }
            _repository.Remove(produtoAtual);
            _unitOfWork.PersistChanges();
            return new ServiceNotification(produtoAtual);
        }

        public Notifiable<Notification> Update(Produto produto)
        {
            produto.ValidateUpdate();
            ValidaProdutoExiste(produto);
            if (!produto.IsValid)
            {
                return produto;
            }
            _repository.Update(produto);
            _unitOfWork.PersistChanges();
            return produto;
        }

        private void ValidaProdutoExiste(Produto produto)
        {
            var produtoAtual = _repository.GetById(produto.Id);
            if (produtoAtual == null)
            {
                produto.AddNotification("Produto", "Não existe produto com o id informado");
            }
        }
    }
}

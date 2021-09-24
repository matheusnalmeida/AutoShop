using AutoShop.Shared.Entities;
using Flunt.Notifications;
using System.Collections.Generic;

namespace AutoShop.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : Entity
    {
        Notifiable<Notification> Add(TEntity obj);

        Notifiable<Notification> Update(TEntity obj);

        Notifiable<Notification> Remove(string id);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(string id);
    }
}

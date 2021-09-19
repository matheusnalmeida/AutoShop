using AutoShop.Shared.Entities;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

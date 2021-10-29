using AutoShop.Shared.Entities;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoShop.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : Entity
    {
        Notifiable<Notification> Add(TEntity obj);

        Notifiable<Notification> Update(TEntity obj);

        Notifiable<Notification> Remove(string id);

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetById(string[] ids, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}

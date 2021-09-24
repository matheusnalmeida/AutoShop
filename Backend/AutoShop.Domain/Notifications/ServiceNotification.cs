using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Notifications
{
    // Usado para retornar Notification nos services quando não conseguir retornar a entidade
    public class ServiceNotification : Notifiable<Notification>
    {
        public ServiceNotification(Notification notification) {
            AddNotification(notification);
        }

        public ServiceNotification(Notifiable<Notification> notifiable)
        {
            Notifications.Concat(notifiable.Notifications);
        }
    }
}

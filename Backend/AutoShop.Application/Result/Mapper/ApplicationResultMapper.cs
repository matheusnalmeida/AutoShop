using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Application.Result.Mapper
{
    public static class ApplicationResultMapper
    {
        public static ApplicationResult MountApplicationResultFromNotifiable(Notifiable<Notification> notifiable)
        {
            return new ApplicationResult(notifiable.IsValid, notifiable.Notifications.Select(x => x.Message));
        }

        public static ApplicationCreateResult MountApplicationCreateResultFromNotifiable(string id, Notifiable<Notification> notifiable)
        {
            return new ApplicationCreateResult(id, notifiable.IsValid, notifiable.Notifications.Select(x => x.Message));
        }
    }
}

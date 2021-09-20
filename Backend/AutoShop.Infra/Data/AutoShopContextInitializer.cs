using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Infra.Data
{
    public class AutoShopContextInitializer
    {
        public static void EnsureCreate(AutoShopContext context)
        {
            context.Database.Migrate();
        }
    }
}

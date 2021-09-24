using Microsoft.EntityFrameworkCore;

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

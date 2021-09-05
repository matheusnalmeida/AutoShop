using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Shared.Entities
{
    public interface IEntityValidate<TEntity> where TEntity : Entity
    {
        // Método para adicionar os validators em propriedades especificas a uma entidade
        public void AddEntityValidation();
    }
}

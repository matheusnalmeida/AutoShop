namespace AutoShop.Shared.Entities
{
    public interface IEntityValidate<TEntity> where TEntity : Entity
    {
        // Método para adicionar os validators em propriedades especificas a uma entidade
        public void AddEntityValidation();
    }
}

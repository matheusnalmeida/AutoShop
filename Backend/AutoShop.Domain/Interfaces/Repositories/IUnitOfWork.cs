namespace AutoShop.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public void PersistChanges();
    }
}

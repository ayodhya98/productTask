namespace DAL.Repositories.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product  { get; }
        ICustomerRepository Customer { get; }
        

        void SaveAsync();
    }
}

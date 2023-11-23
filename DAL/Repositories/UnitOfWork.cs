using DAL.Repositories.IRepository;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);

        }
        public IProductRepository Product { get; private set; }
        public ICustomerRepository Customer { get; private set; }

        public void Dispose()
        {
           _db.Dispose();
        }

        public void SaveAsync()
        {
            _db.SaveChangesAsync().GetAwaiter().GetResult();
        }

        
    }
}

using DAL.Model;
using DAL.Repositories.IRepository;

namespace DAL.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db): base(db)
        {
           _db = db;
        }

    }
}

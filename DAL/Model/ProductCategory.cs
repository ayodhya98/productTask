namespace DAL.Model
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Product product { get; set; }
        public Category category { get; set; }
    }
}

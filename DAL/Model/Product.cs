namespace DAL.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public ICollection<ProductCategory> productCategories { get; set; }

    }
}

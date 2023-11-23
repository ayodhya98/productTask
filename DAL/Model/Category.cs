namespace DAL.Model
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set;}
        public ICollection<ProductCategory> productCategories { get; set; }
    }
}

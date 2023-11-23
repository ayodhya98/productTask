namespace BLL.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}

using BLL.Dto;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetAllProducts();

        Task<ResponseDto<ProductDto>> GetProductById(int id);

        public Task<ProductDto> CreateProduct(CreateProductDto createProductDto);

        public Task<bool>  DeleteProduct(int id);

    }
}

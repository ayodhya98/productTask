using AutoMapper;
using DAL.Model;
using BLL.Dto;
using BLL.Services.Interfaces;
using DAL.Repositories.IRepository;

namespace BLL.Services
{
    public class ProductService : IProductService   
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
           _unitOfWork = unitOfWork;
        }
        public async Task<ProductDto> CreateProduct(CreateProductDto createProductDto)
        {
            ProductDto productDto = new ProductDto();

            if (createProductDto == null)
            {
                return  productDto;
            }
             Product product = new Product();
            product = _mapper.Map<Product>(createProductDto);

           await _unitOfWork.Product.AddAsync(product);
            _unitOfWork.SaveAsync();

            productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if (id == 0) return false;

            await _unitOfWork.Product.Remove(id);
            _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {

            var products = await _unitOfWork.Product.GetAllAsync();

            var ProductsToReturn = _mapper.Map<List<ProductDto>>(products);
            
            return ProductsToReturn;
        }
        public async Task<ResponseDto<ProductDto>> GetProductById(int id)
        {
            try
            {
                ResponseDto<ProductDto> responseDto = new();
                var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.httpCode = 404;
                    responseDto.error = "Product Not Found";
                }
                else {
                    var productdto = _mapper.Map<ProductDto>(product);
                    responseDto.IsSuccess = true;
                    responseDto.httpCode = 200;
                    responseDto.data = productdto;                  
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                ResponseDto<ProductDto> responseDto = new();
                responseDto.IsSuccess = false;
                responseDto.httpCode = 400;
                responseDto.error = "bad request";
                //logger 
                return responseDto;
            }
        }
    }
}

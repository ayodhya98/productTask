using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<ProductDto>> GetAllOrders();

        Task<ResponseDto<ProductDto>> GetOrdertById(int id);

        public Task<ProductDto> CreateOrdert(CreateProductDto createProductDto);

        public Task<bool> DeleteOrder(int id);

    }
}

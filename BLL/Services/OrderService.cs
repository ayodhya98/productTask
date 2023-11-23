using BLL.Dto;
using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        public Task<ProductDto> CreateOrdert(CreateProductDto createProductDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ProductDto>> GetOrdertById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

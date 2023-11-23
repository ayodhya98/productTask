using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerDto>> GetAllCustomers();

        Task<ResponseDto<CustomerDto>> GetCustomerById(int id);
        public Task<CustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto);

        public Task<bool> DeleteCustomer(int id);
    }
}


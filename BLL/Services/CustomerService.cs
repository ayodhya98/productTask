using AutoMapper;
using BLL.Dto;
using BLL.Services.Interfaces;
using DAL.Model;
using DAL.Repositories;
using DAL.Repositories.IRepository;

namespace BLL.Services
{
    public class CustomerService : ICustomerService

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CustomerDto>> GetAllCustomers()
        {
            var Customer = await _unitOfWork.Customer.GetAllAsync();

            var CustomerToReturn = _mapper.Map<List<CustomerDto>>(Customer);

            return CustomerToReturn;
        }

        public async Task<ResponseDto<CustomerDto>> GetCustomerById(int id)

        {
            try
            {
                ResponseDto<CustomerDto> responseDto = new();
                var customer = await _unitOfWork.Customer.GetFirstOrDefaultAsync(x => x.Id == id);

                if (customer == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.httpCode = 404;
                    responseDto.error = "Customer Not Found";
                }
                else
                {
                    var customerDto = _mapper.Map<CustomerDto>(customer);
                    responseDto.IsSuccess = true;
                    responseDto.httpCode= 200;
                    responseDto.data = customerDto;

                }
                return responseDto;
            }
            catch (Exception ex)
         
            {
                ResponseDto<CustomerDto> responseDto = new();
                responseDto.IsSuccess = false;
                responseDto.httpCode = 400;
                responseDto.error = "bad request";
                return responseDto;
            }
        }
            
        

        public async Task<CustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            CustomerDto customerDto = new CustomerDto();

            if (createCustomerDto == null)
            {
                return customerDto;
            }
            Customer customer = new Customer();
            customer = _mapper.Map<Customer>(createCustomerDto);

            await _unitOfWork.Customer.AddAsync(customer);
            _unitOfWork.SaveAsync();

            customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;


        }

        public async Task<bool> DeleteCustomer(int id)
        {
            if (id == 0) return false;
            await _unitOfWork.Customer.Remove(id);
            _unitOfWork.SaveAsync();
            return true;
        }


    }
}

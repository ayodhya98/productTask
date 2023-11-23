using Asp.Versioning;
using BLL.Dto;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> createCustomer(CreateCustomerDto createCustomerDto)
        {
            return Ok(await _customerService.CreateCustomer(createCustomerDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int id)
        {
            return Ok(await _customerService.DeleteCustomer(id));
        }
    } 
}

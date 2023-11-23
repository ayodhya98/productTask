using Asp.Versioning;
using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;


        public ProductController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
           
            return Ok(await _productService.GetProductById(id));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("createProduct")]

        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
        {

            return Ok(await _productService.CreateProduct(createProductDto));
        }

        [HttpDelete("{id:int}")]
         public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }


        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login(LoginRequestDto user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await _authService.Login(user);

        //        if (result != null)
        //        {
        //            LoginResponseDto responseDto = new LoginResponseDto()
        //            {
        //                Id = result.Id,
        //                Email = result.Email,
        //                Jwt = _authService.GenerateTokenString(result),
        //                FirstName = result.FirstName,
        //                LastName = result.LastName,


        //            };

        //            return Ok(responseDto);
        //        }
        //        else
        //            return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        
        //[HttpPost]
        //[Route("register")]
        //[Authorize]
        //public async Task<IActionResult> CreateUser(UserCreationDto user)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var result = await _authService.RegisterUser(user);

        //        if (result)
        //        {

        //            return Ok("success");
        //        }
        //        else
        //            return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
    }
}

//AuthorizeAttribute done



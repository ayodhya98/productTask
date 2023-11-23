using Asp.Versioning;
using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            
            _authService = authService;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _authService.Login(user);

                if (result != null)
                {
                    LoginResponseDto responseDto = new LoginResponseDto()
                    {
                        Id = result.Id,
                        Email = result.Email,
                        Jwt = _authService.GenerateTokenString(result),
                        FirstName = result.FirstName,
                        LastName = result.LastName,


                    };

                    return Ok(responseDto);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("register")]
        [Authorize]
        public async Task<IActionResult> CreateUser(UserCreationDto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await _authService.RegisterUser(user);

                if (result)
                {

                    return Ok("success");
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

    }


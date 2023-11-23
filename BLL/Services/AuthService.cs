using DAL.Model;
using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._config = config;

        }

        public async Task<bool> RegisterUser(UserCreationDto user, string roleName)
        {


            var identityUserExists = await _userManager.FindByEmailAsync(user.Email);

            if (identityUserExists != null)
            {
                throw new Exception("A user with this email address already exists");
            }


            var identityUser = new ApplicationUser()
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if( result.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(identityUser, roleName);
                if (!role.Succeeded)
                {
                    throw new Exception("Error in assigning role to user");

                }
            }
            return result.Succeeded;
        }


        public async Task<ApplicationUser> Login(LoginRequestDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            if (identityUser == null)
            {
                throw new Exception("Email address does not exist");
            }

            var response = await _userManager.CheckPasswordAsync(identityUser, user.Password);

            if (response)
            {
                return identityUser;
            }
            else
            {
                throw new Exception("Wrong password for this user");
            }


        }

        public string GenerateTokenString(ApplicationUser user)
        {
            IEnumerable<Claim> claims = new List<Claim>()
            {

                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            SigningCredentials signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredential);


            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public Task<bool> RegisterUser(UserCreationDto user)
        {
            throw new NotImplementedException();
        }
    }
}

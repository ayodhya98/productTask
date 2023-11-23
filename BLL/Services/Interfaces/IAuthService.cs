using DAL.Model;
using BLL.Dto;

namespace BLL.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateTokenString(ApplicationUser user);
        Task<ApplicationUser> Login(LoginRequestDto user);
        Task<bool> RegisterUser(UserCreationDto user);

    }
}

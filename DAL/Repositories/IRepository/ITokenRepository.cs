using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface ITokenRepository
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
        string CreateNewRefreshToken(string userId, string tokenId);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

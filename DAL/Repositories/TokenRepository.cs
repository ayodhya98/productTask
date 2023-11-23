using DAL.Model;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TokenRepository : ITokenRepository


    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TokenRepository(IConfiguration config,ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            _context = context;
            _userManager = userManager;

        }

        public string CreateToken(ApplicationUser user, IList<string> roles)
        {
            try
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = creds,
                    Issuer = _config["Jwt:Issuer"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenResult = tokenHandler.WriteToken(token);

                if (tokenResult is null)
                {
                    return null;
                }

                return tokenResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating JWT token", ex);
            }
        }


        //public async Task<TokenDto> RefreshAccessToken(TokenDTO tokenDTO)
        //{
        //    try
        //    {
        //        var existingRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(u => u.Refresh_Token == tokenDTO.RefreshToken);

        //        if (existingRefreshToken != null && IsRefreshTokenValid(tokenDTO.RefreshToken, existingRefreshToken.UserId, existingRefreshToken.JwtTokenId))
        //        {
        //            var accessTokenData = GetAccessTokenData(tokenDTO.Token);

        //            if (accessTokenData.isSuccessful)
        //            {
        //                existingRefreshToken.IsValid = false;
        //                _context.SaveChanges();

        //                var user = await _userManager.FindByIdAsync(accessTokenData.userId);
        //                var roles = await _userManager.GetRolesAsync(user);
        //                var jwtTokenId = $"JTI{Guid.NewGuid()}";
        //                var newAccessToken = CreateToken(user, roles);
        //                var newRefreshToken = CreateNewRefreshToken(user.Id, jwtTokenId);

        //                return new TokenDTO
        //                {
        //                    Token = newAccessToken,
        //                    RefreshToken = newRefreshToken
        //                };
        //            }
        //        }

        //        throw new Exception("Invalid refresh token or access token");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error refreshing access token", ex);
        //    }
        //}

        private bool IsRefreshTokenValid(string refreshToken, string userId, string jwtTokenId)
        {
            RefreshToken storedRefreshToken = GetRefreshTokenFromDatabase(userId, jwtTokenId);

            return storedRefreshToken != null &&
                   storedRefreshToken.Refresh_Token == refreshToken &&
                   storedRefreshToken.IsValid &&
                   DateTime.UtcNow < storedRefreshToken.ExpiresAt;
        }

        private RefreshToken GetRefreshTokenFromDatabase(string userId, string jwtTokenId)
        {
            return _context.refreshTokens.FirstOrDefault(rt => rt.UserId == userId && rt.JwtTokenId == jwtTokenId);
        }


        public string CreateNewRefreshToken(string userId, string tokenId)
        {
            RefreshToken refreshToken = new()
            {
                IsValid = true,
                UserId = userId,
                JwtTokenId = tokenId,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                Refresh_Token = Guid.NewGuid() + "-" + Guid.NewGuid(),
            };
            _context.refreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return refreshToken.Refresh_Token;
        }

        private (bool isSuccessful, string userId, string tokenId) GetAccessTokenData(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);

                if (jwt == null || jwt.Claims == null)
                {

                    Console.WriteLine("JWT or claims are null.");
                    return (false, null, null);
                }

                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti)?.Value;
                var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

                return (true, userId, jwtTokenId);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in GetAccessTokenData: {ex.Message}");
                return (false, null, null);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}

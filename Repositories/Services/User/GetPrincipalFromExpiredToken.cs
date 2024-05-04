using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using MyNoteApi.Repositories.Interfaces.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    private Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return Result.Failure<ClaimsPrincipal>("Token Not Valid !");
            }
            return principal;
        }
        catch (Exception)
        {
            return Result.Failure<ClaimsPrincipal>("Token Not Valid !");
        }
    }
}
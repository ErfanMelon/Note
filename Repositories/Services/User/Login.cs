using CSharpFunctionalExtensions;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyNoteApi.Repositories.Services.User;

public partial class UserService : IUserService
{
    public async Task<Result<LoginResponseViewModel>> Login(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null) return Result.Failure<LoginResponseViewModel>("User not found !");
        if (!await _userManager.CheckPasswordAsync(user, model.Password)) return Result.Failure<LoginResponseViewModel>("Wrong password !");
        
        var claims = await _userManager.GetClaimsAsync(user);
        var refreshTokenClaim = claims.SingleOrDefault(e => e.Type == "RefreshToken");
        var refreshTokenExpirationDate = claims.SingleOrDefault(e => e.Type == "RefreshTokenExpirationDate");
        string refreshToken;
        DateTime refreshTokenExpiryDate;
        if (refreshTokenClaim != null && DateTime.Parse(refreshTokenExpirationDate?.Value ?? DateTime.MinValue.ToString()).CompareTo(DateTime.Now) >= 0)
        {
            refreshToken = refreshTokenClaim.Value;
            refreshTokenExpiryDate = DateTime.Parse(refreshTokenExpirationDate?.Value ?? DateTime.MinValue.ToString());
        }
        else
        {
            refreshToken = GenerateRefreshToken();
            var defaultRefreshTokenExTime = !int.TryParse(_configuration["JWT:RefreshTokenExpirationDays"], out var validRefreshTokenInDays);
            refreshTokenExpiryDate = DateTime.Now.AddDays(validRefreshTokenInDays);
        }
        await _userManager.ReplaceClaimAsync(user, new Claim("RefreshToken", string.Empty), new Claim("RefreshToken", refreshToken));
        
        await _userManager.ReplaceClaimAsync(user, new Claim("RefreshTokenExpirationDate", DateTime.MinValue.ToString()), new Claim("RefreshTokenExpirationDate", refreshTokenExpiryDate.ToString()));
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var token = GetToken(authClaims);
        var result = new LoginResponseViewModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            TokenExpirationDate = token.ValidTo,
            RefreshTokenExpirationDate = refreshTokenExpiryDate
        };
        return Result.Success(result);
    }
}
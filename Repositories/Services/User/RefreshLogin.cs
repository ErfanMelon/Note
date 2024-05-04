using CSharpFunctionalExtensions;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    public async Task<Result<LoginResponseViewModel>> RefreshLogin(RefreshTokenViewModel model)
    {
        var tokenPrincipalResult = GetPrincipalFromExpiredToken(model.Token);
        if (tokenPrincipalResult.IsFailure)
            return Result.Failure<LoginResponseViewModel>(tokenPrincipalResult.Error);
        var id = tokenPrincipalResult.Value.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString();
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Failure<LoginResponseViewModel>("User not found !");
        }
        var claims = await _userManager.GetClaimsAsync(user);
        var refreshToken = claims.SingleOrDefault(e => e.Type == "RefreshToken");
        if (refreshToken is null || model.Refresh != refreshToken.Value)
        {
            return Result.Failure<LoginResponseViewModel>("Token Not Valid !");
        }
        var refreshTokenExpirationDate = claims.SingleOrDefault(e => e.Type == "RefreshTokenExpirationDate");
        if (refreshTokenExpirationDate is null || !DateTime.TryParse(refreshTokenExpirationDate.Value, out var exdate))
        {
            return Result.Failure<LoginResponseViewModel>("Token Not Valid !");
        }
        if (exdate.CompareTo(DateTime.Now) < 0)
        {
            return Result.Failure<LoginResponseViewModel>("Token Not Valid !");
        }
        var newRefreshToken = GenerateRefreshToken();
        await _userManager.ReplaceClaimAsync(user, refreshToken, new Claim("RefreshToken", newRefreshToken));
        var defaultRefreshTokenExTime = !int.TryParse(_configuration["JWT:RefreshTokenExpirationDays"], out var validRefreshTokenInDays);
        DateTime refreshTokenExpiryDate = DateTime.Now.AddDays(validRefreshTokenInDays);
        await _userManager.ReplaceClaimAsync(user, refreshTokenExpirationDate, new Claim("RefreshTokenExpirationDate", refreshTokenExpiryDate.ToString()));
        var token = GetToken(tokenPrincipalResult.Value.Claims.ToList());
        return new LoginResponseViewModel
        {
            RefreshToken = newRefreshToken,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpirationDate = token.ValidTo,
            RefreshTokenExpirationDate = refreshTokenExpiryDate
        };
    }
}
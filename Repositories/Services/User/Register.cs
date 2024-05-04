using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.Entities.User;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;
using System.Security.Claims;

namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    public async Task<Result> Register(RegisterViewModel model)
    {
        var isEmailTaken = await _userManager.Users.AnyAsync(e => e.NormalizedEmail == model.Email.ToUpper());
        if (isEmailTaken)
            return Result.Failure($"Email {model.Email} Taken Use Another !");
        var user = new AppUser
        {
            Email = model.Email,
            UserName = model.Email,
            Name = model.Name
        };
        var registrationResult = await _userManager.CreateAsync(user, model.Password);
        if (!registrationResult.Succeeded)
        {
            return Result.Failure(string.Join('\n', registrationResult.Errors.Select(err => err.Description).ToList()));
        }
        await _userManager.AddToRoleAsync(user, AppRoles.USER);
        var userClaims = new Claim[]
        {
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Role,AppRoles.USER),
            new Claim("RefreshToken", string.Empty),
            new Claim("RefreshTokenExpirationDate", DateTime.MinValue.ToString())
        };
        await _userManager.AddClaimsAsync(user, userClaims);

        return Result.Success();
    }
}
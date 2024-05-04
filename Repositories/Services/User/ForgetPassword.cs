using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.Entities.User;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;

namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    public async Task<Result> ForgetPassword(ForgetPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
            return Result.Failure("User Not Found !");

        var otpRequest = await _context.RequestOTPs
            .FirstOrDefaultAsync(e => e.IsValid && e.RequestType == OTPType.ForgetPassword && e.User.Id == user.Id && e.Code == model.Code && e.ExpirationDate > DateTime.Now);
        if (otpRequest is null)
            return Result.Failure("Code Expired Or Not Exist !");
        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
        if (removePasswordResult.Succeeded)
        {
            var passwordChangedResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!passwordChangedResult.Succeeded)
            {
                return Result.Failure(string.Join('\n', passwordChangedResult.Errors.Select(err => err.Description).ToList()));
            }
            otpRequest.IsValid = false;
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        return Result.Failure("Error Ocurr !");
    }
}
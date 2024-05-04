using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;

namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    public async Task<Result> ConfirmEmail(VerifyEmailViewModel model)
    {
        var user = await _context.Users.SingleOrDefaultAsync(e => e.NormalizedEmail == model.Email.ToUpper());
        if (user is null)
            return Result.Failure("User Not Found !");

        var otpRequest = await _context.RequestOTPs
            .FirstOrDefaultAsync(e =>
            e.IsValid &&
            e.User.Id == user.Id && e.RequestType == Models.Entities.User.OTPType.ConfirmEmail &&
            e.Code == model.Code && e.ExpirationDate > DateTime.Now);
        if (otpRequest is null)
            return Result.Failure("Code Expired Or Not Exist !");
        otpRequest.IsValid = false;
        user.EmailConfirmed = true;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}
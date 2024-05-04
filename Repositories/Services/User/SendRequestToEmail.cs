using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.Entities.User;
using MyNoteApi.Models.ViewModels.Email;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;
namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    public async Task<Result> SendRequestToEmail(RequestEmailViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
            return Result.Failure("User not found !");
        var otpRequest = await _context.RequestOTPs
            .FirstOrDefaultAsync(e => e.IsValid &&
            e.User.Id == user.Id &&
            e.ExpirationDate > DateTime.Now &&
            (int)e.RequestType == (int)model.TypeOfRequest);
        if (otpRequest is null)
        {
            var random = new Random();
            otpRequest = new RequestOTP
            {
                Code = random.Next(10000, 99999).ToString(),
                ExpirationDate = DateTime.Now.AddHours(2),
                User = user,
                IsValid = true,
                RequestType = (OTPType)(int)model.TypeOfRequest
            };
            await _context.RequestOTPs.AddAsync(otpRequest);
            await _context.SaveChangesAsync();
        }
        SendEmailViewModel emailModel;
        switch (model.TypeOfRequest)
        {
            case RequestEmailType.ForgetPassword:
                emailModel = new SendEmailViewModel(model.Email, "Password Reset", $"Your Code is : {otpRequest.Code}");
                break;
            case RequestEmailType.EmailConfirmation:
                emailModel = new SendEmailViewModel(model.Email, "Email Confirmation", $"Your Code is : {otpRequest.Code}");
                break;
            default:
                return Result.Failure("Cannot Find Request Type !");
        }
        _emailService.Send(emailModel);
        return Result.Success();
    }
}
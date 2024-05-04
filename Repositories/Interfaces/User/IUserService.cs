using CSharpFunctionalExtensions;
using MyNoteApi.Models.ViewModels.User;

namespace MyNoteApi.Repositories.Interfaces.User;

public interface IUserService
{
    Task<Result> Register(RegisterViewModel model);
    Task<Result<LoginResponseViewModel>> Login(LoginViewModel model);
    Task<Result<LoginResponseViewModel>> RefreshLogin(RefreshTokenViewModel model);
    Task<Result> ConfirmEmail(VerifyEmailViewModel model);
    Task<Result> ForgetPassword(ForgetPasswordViewModel model);
    Task<Result> SendRequestToEmail(RequestEmailViewModel model);
    Task CsvReport();
}
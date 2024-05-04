using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using MyNoteApi.Models.ViewModels.User;
using MyNoteApi.Repositories.Interfaces.User;
using MyNoteApi.Repositories.Services;

namespace MyNoteApi.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="201">Indicate that user created successfully</response>
    /// <response code="400">If model is not valid</response>
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _userService.Register(model);
        if (result.IsSuccess)
            return Created(string.Empty, result.ToResult());
        return BadRequest(result.ToResult());
    }
    /// <summary>
    /// Login an user with credential
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">Successfull login</response>
    /// <response code="401">invalid credential</response>
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _userService.Login(model);
        if (result.IsSuccess)
            return Ok(result.ToResult());
        return Unauthorized(result.ToResult());
    }
    /// <summary>
    /// Regenerate token to validate user with old token
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="200">New refreshed token</response>
    /// <response code="401">Invalid token</response>
    [HttpPatch("RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
    {
        var result = await _userService.RefreshLogin(model);
        if (result.IsSuccess)
            return Ok(result.ToResult());
        return Unauthorized(result.ToResult());
    }
    /// <summary>
    /// Verify user's email
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="202">Email confirm successfull</response>
    /// <response code="400">Invalid code</response>
    [HttpPost("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        var result = await _userService.ConfirmEmail(model);
        if (result.IsSuccess)
            return Accepted(result.ToResult());
        return BadRequest(result.ToResult());
    }
    /// <summary>
    /// Reset user's password
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="202">Password changed successfully</response>
    /// <response code="400">Invalid code or password weak</response>
    [HttpPost("ForogetPassword")]
    public async Task<IActionResult> ForogetPassword(ForgetPasswordViewModel model)
    {
        var result = await _userService.ForgetPassword(model);
        if (result.IsSuccess)
            return Accepted(result.ToResult());
        return BadRequest(result.ToResult());
    }
    /// <summary>
    /// Send 5 digit otp code to user's email
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="202">Otp code sent</response>
    /// <response code="400">User not found</response>
    /// <response code="500">Internal error</response>
    [HttpPost("SendRequestEmail")]
    public async Task<IActionResult> SendEmailForgetPassword(RequestEmailViewModel model)
    {
        var result = await _userService.SendRequestToEmail(model);
        if (result.IsSuccess)
            return Accepted(result.ToResult());
        return BadRequest(result.ToResult());
    }
    [HttpGet("Report")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Report()
    {
        await _userService.CsvReport();
        return Ok();
    }
}

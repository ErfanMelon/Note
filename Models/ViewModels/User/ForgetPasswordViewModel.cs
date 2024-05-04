using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class ForgetPasswordViewModel
{
    /// <summary>
    /// User's Email
    /// </summary>
    /// <example>user@example.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    /// <summary>
    /// OTP Code
    /// </summary>
    /// <example>12345</example>
    [Required]
    public string Code { get; set; }
    /// <summary>
    /// User's New Password
    /// </summary>
    /// <example>S3cur3P@ssword!</example>
    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }
}

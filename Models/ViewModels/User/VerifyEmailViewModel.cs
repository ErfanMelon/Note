using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class VerifyEmailViewModel
{
    /// <summary>
    /// OTP Code
    /// </summary>
    /// <example>12345</example>
    [Required]
    public string Code { get; set; }
    /// <summary>
    /// User's Email
    /// </summary>
    /// <example>user@example.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

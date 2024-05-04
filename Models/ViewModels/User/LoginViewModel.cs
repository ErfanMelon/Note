using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class LoginViewModel
{
    /// <summary>
    /// User's Email
    /// </summary>
    /// <example>user@example.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    /// <summary>
    /// User's Password
    /// </summary>
    /// <example>A123456789a*</example>
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
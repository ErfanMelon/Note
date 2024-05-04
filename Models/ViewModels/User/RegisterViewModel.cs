using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class RegisterViewModel
{
    /// <summary>
    /// User's Name
    /// </summary>
    /// <example>John Doe</example>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
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
    /// <example>S3cur3P@ssword!</example>
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}

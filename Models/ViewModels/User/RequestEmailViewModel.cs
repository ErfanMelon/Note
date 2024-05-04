using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class RequestEmailViewModel
{
    /// <summary>
    /// User's Email
    /// </summary>
    /// <example>user@example.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public RequestEmailType TypeOfRequest { get; set; } = RequestEmailType.EmailConfirmation;
}
/// <summary>
/// ForgetPassword = 1,
/// EmailConfirmation = 2
/// </summary>
public enum RequestEmailType : int
{
    ForgetPassword = 1,
    EmailConfirmation = 2
}
using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.User;

public class ConfirmationViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

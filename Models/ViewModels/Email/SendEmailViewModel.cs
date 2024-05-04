using System.Net.Mail;

namespace MyNoteApi.Models.ViewModels.Email;

public record SendEmailViewModel(string to, string title, string message, Attachment? attachment = null);

using MyNoteApi.Models.ViewModels.Email;

namespace MyNoteApi.Repositories.Interfaces.Email;

public interface IEmailService
{
    void Send(SendEmailViewModel model);
}

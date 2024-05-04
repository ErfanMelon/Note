using MyNoteApi.Repositories.Interfaces.Email;

namespace MyNoteApi.Repositories.Services.Email;

public partial class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}

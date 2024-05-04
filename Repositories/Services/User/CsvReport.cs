using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.ViewModels.Email;
using MyNoteApi.Repositories.Interfaces.User;
using System.Globalization;
using System.Net.Mail;

namespace MyNoteApi.Repositories.Services.User;

public partial class UserService : IUserService
{
    public async Task CsvReport()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();
        using var stream = new MemoryStream();
        using var streamWriter = new StreamWriter(stream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(users);
        streamWriter.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        var attachment = new Attachment(stream, "Users List.csv");
        var email = new SendEmailViewModel("<Email>", "Report User", $"This is MyNote Users ! Generated On {DateTime.Now}", attachment);
        _emailService.Send(email);
        return;
    }
}

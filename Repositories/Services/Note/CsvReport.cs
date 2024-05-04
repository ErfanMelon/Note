using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.ViewModels.Email;
using MyNoteApi.Repositories.Interfaces.Note;
using System.Globalization;
using System.IO;
using System.Net.Mail;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    public async Task CsvReport()
    {
        var memos = await _context.Memos.Include(e=>e.User).AsNoTracking().ToListAsync();
        using var stream = new MemoryStream();
        using var streamWriter = new StreamWriter(stream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(memos);
        streamWriter.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        var attachment = new Attachment(stream, "Memo List.csv");
        var email = new SendEmailViewModel("<Email>", "Report Memo", $"This is MyNote Memos ! Generated On {DateTime.Now}", attachment);
        _emailService.Send(email);
        return;
    }
}

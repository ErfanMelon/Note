using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Data;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Email;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;
    public MemoService(AppDbContext context, IEmailService emailService)
        => (_context, _emailService) = (context, emailService);
    public async Task<Result> AddToCategory(string userId, string memoId, string? categoryId = null)
    {
        var memo = await _context.Memos
            .Where(e => e.User.Id == userId)
            .Where(e => e.IsDeleted == false)
            .SingleOrDefaultAsync(e => e.Id == memoId.ToGuid());
        if (memo is null) return Result.Failure("memo not found ");

        if (string.IsNullOrEmpty(categoryId))
        {
            memo.CategoryId = null;
        }
        else
        {
            var category = await _context.Categories
                .Where(e => e.User.Id == userId)
                .Where(e => e.IsDeleted == false)
                .SingleOrDefaultAsync(e => e.Id == categoryId.ToGuid());
            if (category is null) return Result.Failure("category not found ");

            memo.Category = category;
        }
        await _context.SaveChangesAsync();
        return Result.Success();
    }
    public async Task<Result<IList<MemoDto>>> GetMemoByCategory(string userId, string categoryId)
    {
        var memos = await _context.Memos
            .Where(e => e.IsDeleted == false)
            .Where(e => e.User.Id == userId)
            .Where(e => e.CategoryId == categoryId.ToGuid())
            .Select(e => new MemoDto
            {
                Content = e.Content,
                CreatedOn = e.CreatedOn,
                Id = e.Id.ToString(),
                ModifiedOn = e.ModifiedOn,
                Title = e.Title
            })
            .ToListAsync();
        return memos;

    }
}
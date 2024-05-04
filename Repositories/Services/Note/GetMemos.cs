using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;
public partial class MemoService : IMemoService
{
    public async Task<Result<IList<MemoDto>>> GetMemos(GetMemosDto model)
    {
        var user = await GetUserById(model.userId);
        if (user.IsFailure) return Result.Failure<IList<MemoDto>>(user.Error);

        var memos = _context.Memos
            .Include(e => e.User)
            .AsNoTracking()
            .Where(e => e.User.Id == model.userId && e.IsDeleted == false)
            .Skip((model.page - 1) * model.size)
            .Take(model.size)
            .AsQueryable();
        var result = memos.Select(e => new MemoDto
        {
            Content = e.Content,
            Title = e.Title,
            CreatedOn = e.CreatedOn,
            ModifiedOn = e.ModifiedOn,
            Id = e.Id.ToString()
        }).ToList();
        return result;
    }
}
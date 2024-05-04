using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;
public partial class MemoService : IMemoService
{
    public async Task<Result<MemoDto>> GetMemoById(GetMemoDto model)
    {
        var user = await GetUserById(model.userId);
        if (user.IsFailure) return Result.Failure<MemoDto>(user.Error);

        var memo = await _context.Memos
            .SingleOrDefaultAsync(e => e.IsDeleted == false && e.Id == model.memoId.ToGuid() && e.User.Id == user.Value.Id);
        if (memo is null)
            return Result.Failure<MemoDto>("could not find note !");
        return new MemoDto
        {
            Content = memo.Content,
            Title = memo.Title,
            CreatedOn = memo.CreatedOn,
            ModifiedOn = memo.ModifiedOn,
            Id = memo.Id.ToString()
        };
    }
}

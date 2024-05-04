using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    public async Task<Result> ModifyMemo(UpdateMemoDto model)
    {
        var user = await GetUserById(model.userId);
        if (user.IsFailure) return Result.Failure(user.Error);

        var memo = await _context.Memos
            .SingleOrDefaultAsync(e => e.IsDeleted == false && e.Id == model.memoId.ToGuid() && e.User.Id == user.Value.Id);
        if (memo is null)
            return Result.Failure<MemoDto>("could not find note !");
        memo.Content = model.content;
        memo.Title = model.title;
        memo.ModifiedOn = DateTime.Now;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}
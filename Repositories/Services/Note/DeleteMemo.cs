using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    public async Task<Result> DeleteMemo(string userId, string memoId)
    {
        var memo = await _context.Memos.SingleOrDefaultAsync(e => e.IsDeleted == false && e.User.Id == userId && e.Id == memoId.ToGuid());
        if (memo is null)
            return Result.Failure("not found");
        memo.IsDeleted = true;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}
using CSharpFunctionalExtensions;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;
public partial class MemoService : IMemoService
{
    public async Task<Result<string>> CreateMemo(NewMemoDto model)
    {
        var user = await GetUserById(model.userId);
        if (user.IsFailure) return Result.Failure<string>(user.Error);
        var memo = new Models.Entities.Note.Memo
        {
            Content = model.content,
            Title = model.title,
            CreatedOn = DateTime.Now,
            IsDeleted = false,
            User = user.Value
        };
        await _context.Memos.AddAsync(memo);
        await _context.SaveChangesAsync();
        return memo.Id.ToString();
    }
}
using CSharpFunctionalExtensions;
using MyNoteApi.Models.Entities.User;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    private async Task<Result<AppUser>> GetUserById(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return Result.Failure<AppUser>("user not found !");
        return user;
    }
}
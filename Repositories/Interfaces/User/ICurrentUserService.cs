namespace MyNoteApi.Repositories.Interfaces.User;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? Email { get; }
}

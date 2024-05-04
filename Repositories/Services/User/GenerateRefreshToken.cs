using MyNoteApi.Repositories.Interfaces.User;
using System.Security.Cryptography;

namespace MyNoteApi.Repositories.Services.User;
public partial class UserService : IUserService
{
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

}
using Microsoft.AspNetCore.Identity;

namespace MyNoteApi.Models.Entities.User;

public class AppRole : IdentityRole { }
public static class AppRoles
{
    public const string USER = "User";
}

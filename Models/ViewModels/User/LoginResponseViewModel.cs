namespace MyNoteApi.Models.ViewModels.User;

public class LoginResponseViewModel
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpirationDate { get; set; }
    public DateTime RefreshTokenExpirationDate { get; set; }
}
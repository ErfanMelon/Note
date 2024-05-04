namespace MyNoteApi.Models.Entities.User;

public class RequestOTP
{
    public Guid Id { get; set; }
    public virtual AppUser User { get; set; }
    public string Code { get; set; }
    public OTPType RequestType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsValid { get; set; } = true;
}
public enum OTPType : int
{
    ForgetPassword = 1,
    ConfirmEmail = 2
}
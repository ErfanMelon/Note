namespace MyNoteApi.Models.ViewModels.User;

public class RefreshTokenViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
    public string Token { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <example>7C0VnbfUnlj7CY6zaGUxj74iPfji7fsA4Rtm0EZrxbRR2OUqayT03GLzBke2pqrNBx+cDwNXj1LH4KUXByKSEg==</example>
    public string Refresh { get; set; }
}

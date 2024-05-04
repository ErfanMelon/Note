using Microsoft.AspNetCore.Identity;
using MyNoteApi.Models.Entities.Note;
using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.Entities.User;

public class AppUser:IdentityUser
{
    [MaxLength(150)]
    public string? Name { get; set; }
    public virtual ICollection<Memo> Memos { get; set; }
}

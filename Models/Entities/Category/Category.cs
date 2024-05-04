using MyNoteApi.Models.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.Entities.Note;

public class Category
{
    public Guid Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; } = false;
    public virtual AppUser User { get; set; }
    public string UserId { get; set; }
    public virtual ICollection<Memo> Memos { get; set; }
}

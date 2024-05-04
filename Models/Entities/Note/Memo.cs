using MyNoteApi.Models.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.Entities.Note;

public class Memo
{
    public Guid Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; }
    public string? Content { get; set; }
    public virtual AppUser User { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; } = false;
    public virtual Category? Category { get; set; }
    public Guid? CategoryId { get; set; }
}

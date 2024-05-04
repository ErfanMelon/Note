namespace MyNoteApi.Models.DataTransfareObject.Note;

public class MemoDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}

using MyNoteApi.Models.ViewModels.Note;

namespace MyNoteApi.Models.DataTransfareObject.Note;

public class SearchMemoDto : SearchMemoViewModel
{
    public string UserId { get; set; }
}

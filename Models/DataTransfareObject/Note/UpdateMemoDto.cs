using MyNoteApi.Models.ViewModels.Note;

namespace MyNoteApi.Models.DataTransfareObject.Note;

public record UpdateMemoDto(string userId,string memoId,string title,string content):UpdateMemoViewModel(memoId
    ,title,content);
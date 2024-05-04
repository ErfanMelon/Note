using MyNoteApi.Models.ViewModels.Note;

namespace MyNoteApi.Models.DataTransfareObject.Note;

public record NewMemoDto(string userId,string title, string content) :NewMemoViewModel(title,content);

using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.Note;

public record UpdateMemoViewModel(string memoId, [Required][MaxLength(200)] string title, string content);
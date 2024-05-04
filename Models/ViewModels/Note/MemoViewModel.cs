using System.ComponentModel.DataAnnotations;

namespace MyNoteApi.Models.ViewModels.Note;
/// <summary>
/// 
/// </summary>
/// <param name="title"></param>
/// <param name="content"></param>
public record NewMemoViewModel([Required][MaxLength(200)] string title, string content);

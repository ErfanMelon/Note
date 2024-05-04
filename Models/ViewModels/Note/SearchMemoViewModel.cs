namespace MyNoteApi.Models.ViewModels.Note;

public class SearchMemoViewModel
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Parameter { get; set; }
    public SortType SortType { get; set; } = SortType.CreationDate;
}
/// <summary>
/// CreationDate = 1,
/// Title = 2,
/// ModifyDate = 3,
/// Category = 4
/// </summary>
public enum SortType : int
{
    CreationDate = 1,
    Title = 2,
    ModifyDate = 3,
    Category = 4
}

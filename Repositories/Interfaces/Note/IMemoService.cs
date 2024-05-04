using CSharpFunctionalExtensions;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Models.ViewModels.Note;

namespace MyNoteApi.Repositories.Interfaces.Note;

public interface IMemoService
{
    Task<Result<string>> CreateMemo(NewMemoDto model);
    Task<Result<MemoDto>> GetMemoById(GetMemoDto model);
    Task<Result<IList<MemoDto>>> GetMemos(GetMemosDto model);
    Task<Result> ModifyMemo(UpdateMemoDto model);
    Task<Result> DeleteMemo(string userId, string memoId);
    Task CsvReport();
    Task<Result> AddToCategory(string userId, string memoId, string? categoryId = null);
    Task<Result<IList<MemoDto>>> GetMemoByCategory(string userId, string categoryId);
    Task<Result<IList<MemoDto>>> SearchMemo(SearchMemoDto model);
}

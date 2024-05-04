using CSharpFunctionalExtensions;
using MyNoteApi.Models.ViewModels.Category;

namespace MyNoteApi.Repositories.Interfaces.Category;

public interface ICategoryService
{
    Task<Result> Create(string userId,string name);
    Task<Result> Update(string userId, string categoryId,string name);
    Task<Result> Delete(string userId, string categoryId);
    Task<Result<List<CategoryViewModel>>> GetById(string userId);
}

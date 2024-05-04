using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Models.DataTransfareObject.Note;
using MyNoteApi.Repositories.Interfaces.Note;

namespace MyNoteApi.Repositories.Services.Note;

public partial class MemoService : IMemoService
{
    public async Task<Result<IList<MemoDto>>> SearchMemo(SearchMemoDto model)
    {
        var result = _context.Memos
            .AsNoTracking()
            .Where(e => e.User.Id == model.UserId)
            .AsQueryable();
        if (!string.IsNullOrEmpty(model.Parameter))
        {
            result = result.Where(e => e.Title.Contains(model.Parameter)).AsQueryable();
            result = result.Where(e => e.Content.Contains(model.Parameter)).AsQueryable();
            var memos = _context.Categories
                .AsNoTracking()
                .Include(e => e.Memos)
                .Where(e => e.UserId == model.UserId && e.IsDeleted == false && e.Name.Contains(model.Parameter))
                .SelectMany(e => e.Memos).AsQueryable();
            result = result.Union(memos).AsQueryable();
        }
        switch (model.SortType)
        {
            case Models.ViewModels.Note.SortType.CreationDate:
                result = result.OrderBy(e => e.CreatedOn).AsQueryable();
                break;
            case Models.ViewModels.Note.SortType.Title:
                result = result.OrderBy(e => e.Title).AsQueryable();
                break;
            case Models.ViewModels.Note.SortType.ModifyDate:
                result = result.OrderBy(e => e.ModifiedOn).AsQueryable();
                break;
            case Models.ViewModels.Note.SortType.Category:
                result = result.OrderBy(e => e.CategoryId).AsQueryable();
                break;
            default:
                break;
        }
        result = result.Skip((model.Page - 1) * model.PageSize)
        .Take(model.PageSize).AsQueryable();
        var output = result.Select(e => new MemoDto
        {
            Content = e.Content,
            Title = e.Title,
            CreatedOn = e.CreatedOn,
            ModifiedOn = e.ModifiedOn,
            Id = e.Id.ToString()

        }).ToList();
        return output;
    }
}

using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyNoteApi.Data;
using MyNoteApi.Models.Entities.Note;
using MyNoteApi.Models.ViewModels.Category;
using MyNoteApi.Repositories.Interfaces.Category;

namespace MyNoteApi.Repositories.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Create(string userId, string name)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(e => e.Id == userId);
        if (user is null) return Result.Failure("user not found ");

        var category = new Category
        {
            User = user,
            Name = name,
            CreatedOn = DateTime.Now,
            IsDeleted = false
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> Delete(string userId, string categoryId)
    {
        var category = await _context.Categories
            .Where(e => e.IsDeleted == false)
            .Where(e => e.UserId == userId)
            .SingleOrDefaultAsync(e => e.Id == categoryId.ToGuid());
        if (category is null) return Result.Failure("category not found ");
        category.IsDeleted = true;
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<List<CategoryViewModel>>> GetById(string userId)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .Where(e => e.IsDeleted == false)
            .Select(e => new CategoryViewModel
            {
                Id = e.Id,
                Name = e.Name
            })
            .ToListAsync();
        return categories;
    }

    public async Task<Result> Update(string userId, string categoryId, string name)
    {
        var category = await _context.Categories
                    .Where(e => e.IsDeleted == false)
                    .Where(e => e.UserId == userId)
                    .SingleOrDefaultAsync(e => e.Id == categoryId.ToGuid());
        if (category is null) return Result.Failure("category not found ");
        category.Name = name;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}

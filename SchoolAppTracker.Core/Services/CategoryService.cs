using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly SchoolAppTrackerContext _context;
    private readonly ILogger _logger;

    public CategoryService(SchoolAppTrackerContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger(nameof(CategoryService));
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created category {Name} with ID {ID}", category.Name, category.ID);
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated category {Name} with ID {ID}", category.Name, category.ID);
        return category;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted category with ID {ID}", id);
        }
    }
}

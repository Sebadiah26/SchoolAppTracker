using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public List<Category> Categories { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditId { get; set; }

    public async Task OnGetAsync()
    {
        Categories = await _categoryService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostCreateAsync(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            await _categoryService.CreateAsync(new Category { Name = name });
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id, string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                category.Name = name;
                await _categoryService.UpdateAsync(category);
            }
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToPage();
    }
}

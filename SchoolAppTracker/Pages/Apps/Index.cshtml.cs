using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Apps;

public class IndexModel : PageModel
{
    private readonly IApplicationService _applicationService;
    private readonly ICategoryService _categoryService;
    private readonly IDepartmentService _departmentService;

    public IndexModel(IApplicationService applicationService, ICategoryService categoryService, IDepartmentService departmentService)
    {
        _applicationService = applicationService;
        _categoryService = categoryService;
        _departmentService = departmentService;
    }

    public List<Application> Applications { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }

    [BindProperty(SupportsGet = true)]
    public AppStatus? StatusFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? CategoryFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? DepartmentFilter { get; set; }

    public SelectList StatusOptions { get; set; } = null!;
    public SelectList CategoryOptions { get; set; } = null!;
    public SelectList DepartmentOptions { get; set; } = null!;

    public async Task OnGetAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        var departments = await _departmentService.GetAllAsync();

        StatusOptions = new SelectList(Enum.GetValues<AppStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text", StatusFilter.HasValue ? (int)StatusFilter : null);
        CategoryOptions = new SelectList(categories, "ID", "Name", CategoryFilter);
        DepartmentOptions = new SelectList(departments, "ID", "Name", DepartmentFilter);

        if (SearchName != null || StatusFilter.HasValue || CategoryFilter.HasValue || DepartmentFilter.HasValue)
        {
            Applications = await _applicationService.SearchAsync(SearchName, StatusFilter, CategoryFilter, DepartmentFilter);
        }
        else
        {
            Applications = await _applicationService.GetAllAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Apps;

public class CreateModel : PageModel
{
    private readonly IApplicationService _applicationService;
    private readonly ICategoryService _categoryService;
    private readonly IDepartmentService _departmentService;
    private readonly SchoolAppTrackerContext _context;

    public CreateModel(IApplicationService applicationService, ICategoryService categoryService, IDepartmentService departmentService, SchoolAppTrackerContext context)
    {
        _applicationService = applicationService;
        _categoryService = categoryService;
        _departmentService = departmentService;
        _context = context;
    }

    [BindProperty]
    public Application Application { get; set; } = new();

    [BindProperty]
    public List<int> SelectedDepartmentIds { get; set; } = new();

    [BindProperty]
    public List<int> SelectedGradeLevelIds { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulateViewBag();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateViewBag();
            return Page();
        }

        Application.ApplicationDepartments = SelectedDepartmentIds
            .Select(id => new ApplicationDepartment { DepartmentId = id })
            .ToList();

        Application.ApplicationGradeLevels = SelectedGradeLevelIds
            .Select(id => new ApplicationGradeLevel { GradeLevelId = id })
            .ToList();

        await _applicationService.CreateAsync(Application);
        return RedirectToPage("/Apps/Index");
    }

    private async Task PopulateViewBag()
    {
        var categories = await _categoryService.GetAllAsync();
        var departments = await _departmentService.GetAllAsync();
        var gradeLevels = await _context.GradeLevels.OrderBy(g => g.ID).ToListAsync();

        ViewData["CategoryOptions"] = new SelectList(categories, "ID", "Name");
        ViewData["StatusOptions"] = new SelectList(Enum.GetValues<AppStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text");
        ViewData["ApprovalStatusOptions"] = new SelectList(Enum.GetValues<ApprovalStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text");
        ViewData["Departments"] = departments;
        ViewData["GradeLevels"] = gradeLevels;
        ViewData["SelectedDepartmentIds"] = SelectedDepartmentIds;
        ViewData["SelectedGradeLevelIds"] = SelectedGradeLevelIds;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Apps;

public class EditModel : PageModel
{
    private readonly IApplicationService _applicationService;
    private readonly ICategoryService _categoryService;
    private readonly IDepartmentService _departmentService;
    private readonly SchoolAppTrackerContext _context;

    public EditModel(IApplicationService applicationService, ICategoryService categoryService, IDepartmentService departmentService, SchoolAppTrackerContext context)
    {
        _applicationService = applicationService;
        _categoryService = categoryService;
        _departmentService = departmentService;
        _context = context;
    }

    [BindProperty]
    public Application Application { get; set; } = null!;

    [BindProperty]
    public List<int> SelectedDepartmentIds { get; set; } = new();

    [BindProperty]
    public List<int> SelectedGradeLevelIds { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null)
            return NotFound();

        Application = app;
        SelectedDepartmentIds = app.ApplicationDepartments.Select(ad => ad.DepartmentId).ToList();
        SelectedGradeLevelIds = app.ApplicationGradeLevels.Select(ag => ag.GradeLevelId).ToList();

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

        // Remove existing join table entries
        var existingDepts = await _context.ApplicationDepartments.Where(ad => ad.ApplicationId == Application.ID).ToListAsync();
        var existingGrades = await _context.ApplicationGradeLevels.Where(ag => ag.ApplicationId == Application.ID).ToListAsync();
        _context.ApplicationDepartments.RemoveRange(existingDepts);
        _context.ApplicationGradeLevels.RemoveRange(existingGrades);
        await _context.SaveChangesAsync();

        Application.ApplicationDepartments = SelectedDepartmentIds
            .Select(id => new ApplicationDepartment { ApplicationId = Application.ID, DepartmentId = id })
            .ToList();

        Application.ApplicationGradeLevels = SelectedGradeLevelIds
            .Select(id => new ApplicationGradeLevel { ApplicationId = Application.ID, GradeLevelId = id })
            .ToList();

        await _applicationService.UpdateAsync(Application);
        return RedirectToPage("/Apps/Details", new { id = Application.ID });
    }

    private async Task PopulateViewBag()
    {
        var categories = await _categoryService.GetAllAsync();
        var departments = await _departmentService.GetAllAsync();
        var gradeLevels = await _context.GradeLevels.OrderBy(g => g.ID).ToListAsync();

        ViewData["CategoryOptions"] = new SelectList(categories, "ID", "Name");
        ViewData["StatusOptions"] = new SelectList(Enum.GetValues<AppStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text");
        ViewData["ApprovalStatusOptions"] = new SelectList(Enum.GetValues<ApprovalStatus>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text");
        ViewData["RosteringMethodOptions"] = new SelectList(Enum.GetValues<RosteringMethod>().Select(s => new { Value = (int)s, Text = s.ToString() }), "Value", "Text");
        ViewData["Departments"] = departments;
        ViewData["GradeLevels"] = gradeLevels;
        ViewData["SelectedDepartmentIds"] = SelectedDepartmentIds;
        ViewData["SelectedGradeLevelIds"] = SelectedGradeLevelIds;
    }
}

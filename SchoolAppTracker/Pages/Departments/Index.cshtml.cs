using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Departments;

public class IndexModel : PageModel
{
    private readonly IDepartmentService _departmentService;

    public IndexModel(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public List<Department> Departments { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? EditId { get; set; }

    public async Task OnGetAsync()
    {
        Departments = await _departmentService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostCreateAsync(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            await _departmentService.CreateAsync(new Department { Name = name });
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id, string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department != null)
            {
                department.Name = name;
                await _departmentService.UpdateAsync(department);
            }
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _departmentService.DeleteAsync(id);
        return RedirectToPage();
    }
}

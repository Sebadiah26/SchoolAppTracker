using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Apps;

public class DeleteModel : PageModel
{
    private readonly IApplicationService _applicationService;

    public DeleteModel(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [BindProperty]
    public Application Application { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null)
            return NotFound();

        Application = app;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _applicationService.DeleteAsync(Application.ID);
        return RedirectToPage("/Apps/Index");
    }
}

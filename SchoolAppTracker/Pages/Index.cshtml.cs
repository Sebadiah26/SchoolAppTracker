using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppTracker.Core.Models;
using SchoolAppTracker.Core.Services.Interfaces;

namespace SchoolAppTracker.Pages;

public class IndexModel : PageModel
{
    private readonly IApplicationService _applicationService;

    public IndexModel(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public DashboardStats Stats { get; set; } = new();

    public async Task OnGetAsync()
    {
        Stats = await _applicationService.GetDashboardStatsAsync();
    }
}

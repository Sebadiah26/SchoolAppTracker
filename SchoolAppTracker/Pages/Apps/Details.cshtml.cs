using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAppTracker.Core.Services;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Pages.Apps;

public class DetailsModel : PageModel
{
    private readonly IApplicationService _applicationService;
    private readonly IScreenshotService _screenshotService;
    private readonly IWebHostEnvironment _env;

    public DetailsModel(IApplicationService applicationService, IScreenshotService screenshotService, IWebHostEnvironment env)
    {
        _applicationService = applicationService;
        _screenshotService = screenshotService;
        _env = env;
    }

    public Application Application { get; set; } = null!;

    [BindProperty]
    public IFormFile? ScreenshotUpload { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var app = await _applicationService.GetByIdAsync(id);
        if (app == null)
            return NotFound();

        Application = app;
        return Page();
    }

    public async Task<IActionResult> OnPostUploadAsync(int id)
    {
        if (ScreenshotUpload == null || ScreenshotUpload.Length == 0)
            return RedirectToPage(new { id });

        using var stream = new MemoryStream();
        await ScreenshotUpload.CopyToAsync(stream);
        stream.Position = 0;

        var screenshot = await _screenshotService.UploadAsync(id, stream, ScreenshotUpload.FileName, ScreenshotUpload.ContentType);
        ScreenshotService.SaveFile(_env.WebRootPath, screenshot.FilePath, stream);

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostDeleteScreenshotAsync(int id, int screenshotId)
    {
        var screenshots = await _screenshotService.GetByApplicationIdAsync(id);
        var screenshot = screenshots.FirstOrDefault(s => s.ID == screenshotId);
        if (screenshot != null)
        {
            ScreenshotService.DeleteFile(_env.WebRootPath, screenshot.FilePath);
            await _screenshotService.DeleteAsync(screenshotId);
        }

        return RedirectToPage(new { id });
    }
}

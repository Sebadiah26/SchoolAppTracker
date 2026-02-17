using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services;

public class ScreenshotService : IScreenshotService
{
    private readonly SchoolAppTrackerContext _context;
    private readonly ILogger _logger;

    private static readonly HashSet<string> AllowedMimeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/gif", "image/webp"
    };

    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    public ScreenshotService(SchoolAppTrackerContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger(nameof(ScreenshotService));
    }

    public async Task<List<Screenshot>> GetByApplicationIdAsync(int applicationId)
    {
        return await _context.Screenshots
            .Where(s => s.ApplicationId == applicationId)
            .OrderByDescending(s => s.UploadedDate)
            .ToListAsync();
    }

    public async Task<Screenshot> UploadAsync(int applicationId, Stream fileStream, string fileName, string mimeType)
    {
        if (!AllowedMimeTypes.Contains(mimeType))
            throw new InvalidOperationException($"File type '{mimeType}' is not allowed. Only JPEG, PNG, GIF, and WebP images are supported.");

        if (fileStream.Length > MaxFileSize)
            throw new InvalidOperationException($"File size exceeds the maximum allowed size of 5MB.");

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var storedFileName = $"{Guid.NewGuid()}{ext}";
        var relativePath = $"/uploads/screenshots/{applicationId}/{storedFileName}";

        var screenshot = new Screenshot
        {
            ApplicationId = applicationId,
            FileName = fileName,
            FilePath = relativePath,
            FileSize = fileStream.Length,
            MimeType = mimeType,
            UploadedDate = DateTime.UtcNow
        };

        _context.Screenshots.Add(screenshot);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Uploaded screenshot {FileName} for application {ApplicationId}", fileName, applicationId);
        return screenshot;
    }

    public async Task DeleteAsync(int id)
    {
        var screenshot = await _context.Screenshots.FindAsync(id);
        if (screenshot != null)
        {
            _context.Screenshots.Remove(screenshot);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted screenshot {ID} for application {ApplicationId}", id, screenshot.ApplicationId);
        }
    }

    public static void SaveFile(string webRootPath, string relativePath, Stream fileStream)
    {
        var fullPath = Path.Combine(webRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        var directory = Path.GetDirectoryName(fullPath)!;
        Directory.CreateDirectory(directory);

        using var output = File.Create(fullPath);
        fileStream.Position = 0;
        fileStream.CopyTo(output);
    }

    public static void DeleteFile(string webRootPath, string relativePath)
    {
        var fullPath = Path.Combine(webRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}

using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services.Interfaces;

public interface IScreenshotService
{
    Task<List<Screenshot>> GetByApplicationIdAsync(int applicationId);
    Task<Screenshot> UploadAsync(int applicationId, Stream fileStream, string fileName, string mimeType);
    Task DeleteAsync(int id);
}

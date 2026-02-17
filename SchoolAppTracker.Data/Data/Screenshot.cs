namespace SchoolAppTracker.Data.Data;

public class Screenshot
{
    public int ID { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; }
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
}

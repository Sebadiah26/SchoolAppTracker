namespace SchoolAppTracker.Core.Models;

public class DashboardStats
{
    public int TotalApps { get; set; }
    public int ActiveApps { get; set; }
    public int InactiveApps { get; set; }
    public int UnderReviewApps { get; set; }
    public int DeprecatedApps { get; set; }
    public int PendingApproval { get; set; }
    public List<ExpiringContract> ExpiringContracts { get; set; } = new List<ExpiringContract>();
}

public class ExpiringContract
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public DateTime? ContractEndDate { get; set; }
}

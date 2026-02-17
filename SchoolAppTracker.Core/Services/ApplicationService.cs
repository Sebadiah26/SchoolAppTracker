using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAppTracker.Core.Models;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services;

public class ApplicationService : IApplicationService
{
    private readonly SchoolAppTrackerContext _context;
    private readonly ILogger _logger;

    public ApplicationService(SchoolAppTrackerContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger(nameof(ApplicationService));
    }

    public async Task<List<Application>> GetAllAsync()
    {
        return await _context.Applications
            .Include(a => a.Category)
            .Include(a => a.ApplicationDepartments).ThenInclude(ad => ad.Department)
            .Include(a => a.ApplicationGradeLevels).ThenInclude(ag => ag.GradeLevel)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<Application?> GetByIdAsync(int id)
    {
        return await _context.Applications
            .Include(a => a.Category)
            .Include(a => a.ApplicationDepartments).ThenInclude(ad => ad.Department)
            .Include(a => a.ApplicationGradeLevels).ThenInclude(ag => ag.GradeLevel)
            .Include(a => a.Screenshots)
            .FirstOrDefaultAsync(a => a.ID == id);
    }

    public async Task<List<Application>> SearchAsync(string? name, AppStatus? status, int? categoryId, int? departmentId)
    {
        var query = _context.Applications
            .Include(a => a.Category)
            .Include(a => a.ApplicationDepartments).ThenInclude(ad => ad.Department)
            .Include(a => a.ApplicationGradeLevels).ThenInclude(ag => ag.GradeLevel)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(a => a.Name.Contains(name));

        if (status.HasValue)
            query = query.Where(a => a.Status == status.Value);

        if (categoryId.HasValue)
            query = query.Where(a => a.CategoryId == categoryId.Value);

        if (departmentId.HasValue)
            query = query.Where(a => a.ApplicationDepartments.Any(ad => ad.DepartmentId == departmentId.Value));

        return await query.OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<Application> CreateAsync(Application application)
    {
        application.CreatedDate = DateTime.UtcNow;
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created application {Name} with ID {ID}", application.Name, application.ID);
        return application;
    }

    public async Task<Application> UpdateAsync(Application application)
    {
        application.ModifiedDate = DateTime.UtcNow;
        _context.Applications.Update(application);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated application {Name} with ID {ID}", application.Name, application.ID);
        return application;
    }

    public async Task DeleteAsync(int id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted application with ID {ID}", id);
        }
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var apps = await _context.Applications.ToListAsync();
        var thirtyDaysFromNow = DateTime.UtcNow.AddDays(30);

        return new DashboardStats
        {
            TotalApps = apps.Count,
            ActiveApps = apps.Count(a => a.Status == AppStatus.Active),
            InactiveApps = apps.Count(a => a.Status == AppStatus.Inactive),
            UnderReviewApps = apps.Count(a => a.Status == AppStatus.UnderReview),
            DeprecatedApps = apps.Count(a => a.Status == AppStatus.Deprecated),
            PendingApproval = apps.Count(a => a.ApprovalStatus == ApprovalStatus.Pending),
            ExpiringContracts = apps
                .Where(a => a.ContractEndDate.HasValue && a.ContractEndDate.Value <= thirtyDaysFromNow && a.ContractEndDate.Value >= DateTime.UtcNow)
                .Select(a => new ExpiringContract
                {
                    ApplicationId = a.ID,
                    ApplicationName = a.Name,
                    ContractEndDate = a.ContractEndDate
                })
                .OrderBy(e => e.ContractEndDate)
                .ToList()
        };
    }
}

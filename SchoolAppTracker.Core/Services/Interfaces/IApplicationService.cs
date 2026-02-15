using SchoolAppTracker.Core.Models;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services.Interfaces;

public interface IApplicationService
{
    Task<List<Application>> GetAllAsync();
    Task<Application?> GetByIdAsync(int id);
    Task<List<Application>> SearchAsync(string? name, AppStatus? status, int? categoryId, int? departmentId);
    Task<Application> CreateAsync(Application application);
    Task<Application> UpdateAsync(Application application);
    Task DeleteAsync(int id);
    Task<DashboardStats> GetDashboardStatsAsync();
}

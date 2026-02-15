using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services.Interfaces;

public interface IDepartmentService
{
    Task<List<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<Department> CreateAsync(Department department);
    Task<Department> UpdateAsync(Department department);
    Task DeleteAsync(int id);
}

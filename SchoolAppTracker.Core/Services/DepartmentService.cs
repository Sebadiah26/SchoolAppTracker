using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly SchoolAppTrackerContext _context;
    private readonly ILogger _logger;

    public DepartmentService(SchoolAppTrackerContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger(nameof(DepartmentService));
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _context.Departments.OrderBy(d => d.Name).ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<Department> CreateAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created department {Name} with ID {ID}", department.Name, department.ID);
        return department;
    }

    public async Task<Department> UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated department {Name} with ID {ID}", department.Name, department.ID);
        return department;
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted department with ID {ID}", id);
        }
    }
}

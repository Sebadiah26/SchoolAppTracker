using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolAppTracker.Core.Services;
using SchoolAppTracker.Core.Services.Interfaces;
using SchoolAppTracker.Data;

namespace SchoolAppTracker.Core.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddSchoolAppTrackerCoreClasses(this IServiceCollection services, IConfiguration configuration)
    {
        var activeDb = configuration.GetConnectionString("ActiveDB") ?? "DefaultConnection";
        var connectionString = configuration.GetConnectionString(activeDb);

        services.AddDbContext<SchoolAppTrackerContext>(options =>
            options.UseSqlServer(connectionString, sql =>
                sql.MigrationsAssembly("SchoolAppTracker")));

        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        return services;
    }
}

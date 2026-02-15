using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAppTracker.Data.Data;

namespace SchoolAppTracker.Data;

public class SchoolAppTrackerContext : DbContext
{
    private readonly ILogger<SchoolAppTrackerContext> _logger;

    public SchoolAppTrackerContext(DbContextOptions<SchoolAppTrackerContext> options, ILogger<SchoolAppTrackerContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
    public DbSet<ApplicationGradeLevel> ApplicationGradeLevels => Set<ApplicationGradeLevel>();
    public DbSet<ApplicationDepartment> ApplicationDepartments => Set<ApplicationDepartment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("school");

        // Composite keys for join tables
        modelBuilder.Entity<ApplicationGradeLevel>()
            .HasKey(ag => new { ag.ApplicationId, ag.GradeLevelId });

        modelBuilder.Entity<ApplicationDepartment>()
            .HasKey(ad => new { ad.ApplicationId, ad.DepartmentId });

        // Relationships
        modelBuilder.Entity<ApplicationGradeLevel>()
            .HasOne(ag => ag.Application)
            .WithMany(a => a.ApplicationGradeLevels)
            .HasForeignKey(ag => ag.ApplicationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ApplicationGradeLevel>()
            .HasOne(ag => ag.GradeLevel)
            .WithMany(g => g.ApplicationGradeLevels)
            .HasForeignKey(ag => ag.GradeLevelId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ApplicationDepartment>()
            .HasOne(ad => ad.Application)
            .WithMany(a => a.ApplicationDepartments)
            .HasForeignKey(ad => ad.ApplicationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ApplicationDepartment>()
            .HasOne(ad => ad.Department)
            .WithMany(d => d.ApplicationDepartments)
            .HasForeignKey(ad => ad.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

        // Decimal precision
        modelBuilder.Entity<Application>()
            .Property(a => a.AnnualCost)
            .HasColumnType("decimal(18,2)");

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { ID = 1, Name = "Learning Management" },
            new Category { ID = 2, Name = "Assessment" },
            new Category { ID = 3, Name = "Communication" },
            new Category { ID = 4, Name = "Productivity" },
            new Category { ID = 5, Name = "Administrative" },
            new Category { ID = 6, Name = "Special Education" }
        );

        // Seed Departments
        modelBuilder.Entity<Department>().HasData(
            new Department { ID = 1, Name = "Math" },
            new Department { ID = 2, Name = "Science" },
            new Department { ID = 3, Name = "English" },
            new Department { ID = 4, Name = "Social Studies" },
            new Department { ID = 5, Name = "IT" },
            new Department { ID = 6, Name = "Administration" },
            new Department { ID = 7, Name = "Counseling" },
            new Department { ID = 8, Name = "Special Education" }
        );

        // Seed Grade Levels
        var gradeLevels = new List<GradeLevel>
        {
            new GradeLevel { ID = 1, Name = "K" }
        };
        for (int i = 1; i <= 12; i++)
        {
            gradeLevels.Add(new GradeLevel { ID = i + 1, Name = i.ToString() });
        }
        modelBuilder.Entity<GradeLevel>().HasData(gradeLevels);

        _logger.LogInformation("SchoolAppTrackerContext OnModelCreating completed");
    }
}

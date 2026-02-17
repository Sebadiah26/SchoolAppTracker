using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppTracker.Data.Data;

public class Application
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? IconPath { get; set; }
    public string? Description { get; set; }
    public string? VendorName { get; set; }
    public string? VendorContact { get; set; }
    public string? WebsiteUrl { get; set; }

    public AppStatus Status { get; set; } = AppStatus.UnderReview;
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public RosteringMethod RosteringMethod { get; set; } = RosteringMethod.None;
    public bool RosterStudents { get; set; }
    public bool RosterTeachers { get; set; }
    public bool RosterGuardians { get; set; }

    public bool SsoEnabled { get; set; }
    public string? SsoProvider { get; set; }
    public bool FerpaCompliant { get; set; }
    public bool CoppaCompliant { get; set; }
    public string? DataPrivacyAgreementUrl { get; set; }

    public DateTime? ContractStartDate { get; set; }
    public DateTime? ContractEndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? AnnualCost { get; set; }

    public string? LicenseType { get; set; }
    public int? NumberOfLicenses { get; set; }

    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    public List<ApplicationGradeLevel> ApplicationGradeLevels { get; set; } = new List<ApplicationGradeLevel>();
    public List<ApplicationDepartment> ApplicationDepartments { get; set; } = new List<ApplicationDepartment>();
    public List<Screenshot> Screenshots { get; set; } = new List<Screenshot>();
}

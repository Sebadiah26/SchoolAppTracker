namespace SchoolAppTracker.Data.Data;

public class ApplicationDepartment
{
    public int ApplicationId { get; set; }
    public Application Application { get; set; } = null!;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}

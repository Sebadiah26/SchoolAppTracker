namespace SchoolAppTracker.Data.Data;

public class Department
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ApplicationDepartment> ApplicationDepartments { get; set; } = new List<ApplicationDepartment>();
}

namespace SchoolAppTracker.Data.Data;

public class GradeLevel
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ApplicationGradeLevel> ApplicationGradeLevels { get; set; } = new List<ApplicationGradeLevel>();
}

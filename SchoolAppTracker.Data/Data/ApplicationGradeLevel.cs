namespace SchoolAppTracker.Data.Data;

public class ApplicationGradeLevel
{
    public int ApplicationId { get; set; }
    public Application Application { get; set; } = null!;
    public int GradeLevelId { get; set; }
    public GradeLevel GradeLevel { get; set; } = null!;
}

namespace SchoolAppTracker.Data.Data;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Application> Applications { get; set; } = new List<Application>();
}

namespace Domain;

public class Projects
{
    public int Id { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public int ProjectManager { get; set; }
    public Users Users { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    // 0001-01-01 is the default value for DateOnly if an object is not set
    // We are setting the default value to 0001-01-01
    public DateOnly? EndDate { get; set; } = new DateOnly(0001, 01, 01);
}
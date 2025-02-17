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
    public DateOnly EndDate { get; set; }
}
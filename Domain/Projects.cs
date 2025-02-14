namespace Domain;

public class Projects
{
    public int Id { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public int ProjectManager { get; init; }
    public User Users { get; init; } = null!;

    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
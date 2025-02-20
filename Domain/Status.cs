namespace Domain;

/// <summary>
/// This class is used to store the status of the project
/// </summary>
public class Status
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    // This creates a bidirectional (navigation) relationship between the Project and Status entities
    public ICollection<Projects> Projects { get; set; } = new List<Projects>();
}
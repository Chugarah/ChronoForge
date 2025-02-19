namespace Domain;

public class Customers
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int ContactPersonId { get; init; }
    public Users Users { get; init; } = null!;
}
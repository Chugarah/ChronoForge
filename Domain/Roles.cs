namespace Domain;

public class Roles
{
    public int Id { get; init; }
    public ICollection<User> Users { get; init; } = null!;

    public string Name { get; init; } = null!;
}
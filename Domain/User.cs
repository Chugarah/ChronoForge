namespace Domain;

public class User
{
    public int Id { get; init; }
    public ICollection<Projects> Projects { get; set; } = [];
    public ICollection<Roles> Roles { get; set; } = [];

    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
}
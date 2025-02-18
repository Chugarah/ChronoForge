namespace Domain;

public class Users
{
    public int Id { get; init; }
    public ICollection<Projects> Projects { get; set; } = [];
    public ICollection<Roles> Roles { get; set; } = [];

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
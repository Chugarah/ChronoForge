namespace Domain;

public class Users
{
    public int Id { get; init; }
    public ICollection<Roles> Roles { get; set; } = new List<Roles>();

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
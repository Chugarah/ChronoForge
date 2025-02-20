namespace Domain;

public class ServiceContracts
{
    public int Id { get; init; }
    // This creates a bidirectional (navigation) relationship between the Project and Services entities
    public ICollection<Projects> Projects { get; init; } = new List<Projects>();

    public int CustomerId { get; set; }
    public Customers Customers { get; init; } = null!;

    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; init; } = null!;

    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
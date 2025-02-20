namespace Domain;

public class Services
{
    public int Id { get; init; }

    public int CustomerId { get; init; }
    public Customers Customers { get; init; } = null!;

    public int PaymentTypeId { get; init; }
    public PaymentType PaymentType { get; init; } = null!;

    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
}
namespace Domain;

public class PaymentType
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Currency { get; init; } = null!;
}
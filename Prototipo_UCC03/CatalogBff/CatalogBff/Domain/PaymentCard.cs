namespace CatalogBff.Domain;

public record PaymentCard
{
    public string? Id  { get; init; }
    public string Number { get; init; }
    public string Cvv { get; init; }
    public int Year { get; init; }
    public int Month { get; init; }
};
namespace ProductService.Domain.Model;

public class Stock(Product product)
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
    public Guid ProductId => product.Id;
}
namespace OrderService.Core;

public class Product
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
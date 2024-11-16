using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Controllers.Resources;

public class CartResource
{
    public Guid? Id { get; set; }
    public IReadOnlyList<Product> Products { get; init; } = new List<Product>();
}
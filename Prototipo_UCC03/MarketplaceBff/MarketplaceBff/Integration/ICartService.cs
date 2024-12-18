using MarketplaceBff.Domain;

namespace MarketplaceBff.Integration;

public interface ICartService
{
    public Task<Cart> AddProductAsync(Guid userId, CartProduct product);

    public Task<Cart> GetCartAsync(string userId);
}
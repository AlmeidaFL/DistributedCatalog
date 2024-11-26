using OrderService.Core;
namespace OrderService.Persistence.Repositories.Implementation;
using MongoDB.Driver;

public class CartRepository(IMongoDatabase database) : ICartRepository
{
    private readonly IMongoCollection<Cart> _cartCollection = database.GetCollection<Cart>("Carts");

    public async Task AddProduct(Guid userId, Product product)
    {
        var cart = await GetCartOrNull(userId) 
                   ?? new Cart(userId, Enumerable.Empty<Product>().ToList());

        var existingProduct = cart.Products.FirstOrDefault(p => p.Id == product.Id);

        if (existingProduct != null)
        {
            existingProduct.Quantity += product.Quantity;
        }
        else
        {
            var newList = cart.Products.ToList();
            newList.Add(product);
            cart.Products = newList;
        }

        var update = Builders<Cart>.Update.Set(c => c.Products, cart.Products);
        await _cartCollection.UpdateOneAsync(c => c.CustomerId == userId, update, new UpdateOptions { IsUpsert = true });
    }


    public async Task AddToCart(Cart cart)
    {
        var options = new ReplaceOptions { IsUpsert = true };
        await _cartCollection.ReplaceOneAsync(c => c.CustomerId == cart.CustomerId, cart, options);
    }

    public async Task CreateCart(Cart cart)
    {
        await _cartCollection.InsertOneAsync(cart);
    }

    public async Task<Cart> GetCart(Guid cartId)
    {
        var cart = await _cartCollection.Find(c => c.CustomerId == cartId).FirstOrDefaultAsync();

        return cart ?? throw new Exception($"There is no cart with id {cart}");
    }
    
    public async Task<Cart?> GetCartOrNull(Guid cartId)
    {
        return await _cartCollection.Find(c => c.CustomerId == cartId).FirstOrDefaultAsync();
    }
}

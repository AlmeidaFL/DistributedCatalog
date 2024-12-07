using MarketplaceBff.Domain;

namespace MarketplaceBff.Integration;

public interface IRegisterService
{
    public Task<User> Login(string email, string password);
    public Task<User> Register(User user);
    public Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address);
    public Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard paymentCard);
    public Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId);
    public Task<IList<PaymentCard>> GetCardOptions(Guid userId);
}
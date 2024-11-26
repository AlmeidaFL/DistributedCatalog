using System.Collections;
using Microsoft.EntityFrameworkCore;
using RegisterService.Domain;

namespace RegisterService.Persistence;

public class UserRepository(UserDbContext dbContext) : IUserRepository
{
    private readonly UserDbContext context = dbContext;
    
    public async Task AddUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public Task<User?> GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public Task<User?> GetUserById(Guid id)
    {
        return context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<PaymentCard>> GetPaymentCards(Guid userId)
    {
        return (await context.Users
                   .Include(u => u.PaymentCards)
                   .FirstOrDefaultAsync(x => x.Id == userId))?.PaymentCards 
               ?? new List<PaymentCard>();
    }

    public async Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId)
    {
        return (await context.Users
                   .Include(u => u.DeliveryAddresses)
                   .FirstOrDefaultAsync(x => x.Id == userId))?.DeliveryAddresses 
               ?? new List<DeliveryAddress>();
    }

    public async Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address)
    {
        var user = await context.Users
            .Include(u => u.DeliveryAddresses)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            throw new Exception($"User with id {userId} doesn't exists.");
        }
        
        user.DeliveryAddresses.Add(address);
        await context.SaveChangesAsync();
        return user.DeliveryAddresses;
    }

    public async Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard paymentCard)
    {
        var user = await context.Users
            .Include(u => u.PaymentCards)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            throw new Exception($"User with id {userId} doesn't exists.");
        }
        
        user.PaymentCards.Add(paymentCard);
        await context.SaveChangesAsync();
        return user.PaymentCards;
    }
}

public interface IUserRepository
{
    public Task AddUser(User user);
    public Task UpdateUser(User user);
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserById(Guid id);
    public Task<IList<PaymentCard>> GetPaymentCards(Guid userId);
    public Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId);
    public Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address);
    public Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard paymentCard);
}
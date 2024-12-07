using CatalogBff.Domain;
using GrpcContracts;
using GrpcContracts.Register;
using DeliveryAddress = CatalogBff.Domain.DeliveryAddress;
using PaymentCard = CatalogBff.Domain.PaymentCard;

namespace CatalogBff.Integration.Implementation;
using Grpc = GrpcContracts.Register.RegisterService;

public class RegisterService(Grpc.RegisterServiceClient client) : IRegisterService
{
    public async Task<User> Login(string email, string password)
    {
        var result = await client.LoginAsync(new GrpcContracts.Register.UserLogin()
        {
            Email = email,
            Password = password
        });

        if (result.IsError)
        {
            throw new Exception($"Login error {result.Message}");
        }
        
        return new User()
        {
            Name = result.User.Name,
            Cnpj = result.User.Cnpj,
            Email = result.User.Email,
            Id = Guid.Parse(result.User.Id),
            Phone = result.User.Telephone,
            Role = result.User.Role,
        };
    }

    public async Task<User> Register(User user)
    {
        var result = await client.RegisterAsync(new GrpcContracts.Register.UserRegister()
        {
            Name = user.Name,
            Password = user.Password,
            Cnpj = user.Cnpj,
            Email = user.Email,
            Telephone= user.Phone,
            Role = user.Role,
        });

        if (result.IsError)
        {
            throw new Exception($"Register error {result.Message}");
        }
        
        return new User()
        {
            Name = result.User.Name,
            Cnpj = result.User.Cnpj,
            Email = result.User.Email,
            Id = Guid.Parse(result.User.Id),
            Phone = result.User.Telephone,
            Role = result.User.Role,
        };
    }
    
    public async Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId)
    {
        var addresses = await client.GetDeliveryAddressesAsync(new UserId(){UserId_ = userId.ToString()});
        
        return addresses.Addresses.Select(ToModel).ToList();
    }
    
    public async Task<IList<PaymentCard>> GetCardOptions(Guid userId)
    {
        var cards = await client.GetPaymentOptionsAsync(new UserId(){UserId_ = userId.ToString()});
        
        return cards.Cards.Select(ToModel).ToList();
    }

    private static DeliveryAddress ToModel(GrpcContracts.Register.DeliveryAddress address)
    {
        return new DeliveryAddress()
        {
            Id = address.Id,
            City = address.City,
            Street = address.Street,
            ZipCode = address.ZipCode,
            State = address.State,
            Number = address.Number
        };
    }

    private static PaymentCard ToModel(GrpcContracts.Register.PaymentCard paymentCard)
    {
        return new PaymentCard()
        {
            Id = paymentCard.Id,
            Cvv = paymentCard.Cvv.ToString(),
            Number = paymentCard.Number,
            Month = paymentCard.ValidUntilMonth,
            Year = paymentCard.ValidUntilYear
        };
    }

    public async Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address)
    {
        var addresses = await client.AddDeliveryAddressAsync(new GrpcContracts.Register.DeliveryAddress()
        {
            UserId = userId.ToString(),
            City = address.City,
            Street = address.Street,
            ZipCode = address.ZipCode,
            State = address.State,
            Number = address.Number,
        });
        
        return addresses.Addresses.Select(ToModel).ToList();
    }

    public async Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard paymentCard)
    {
        var cards = await client.AddPaymentCardsAsync(new GrpcContracts.Register.PaymentCard()
        {
            UserId = userId.ToString(),
            Cvv = paymentCard.Cvv.ToString(),
            Number = paymentCard.Number,
            ValidUntilMonth = paymentCard.Month,
            ValidUntilYear = paymentCard.Year,
        });
        
        return cards.Cards.Select(ToModel).ToList();
    }
}
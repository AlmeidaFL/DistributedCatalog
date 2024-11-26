using Grpc.Core;
using GrpcContracts.Register;
using RegisterService.Application.Services;
using RegisterService.Domain;
using DeliveryAddress = GrpcContracts.Register.DeliveryAddress;
using PaymentCard = GrpcContracts.Register.PaymentCard;
using User = GrpcContracts.Register.UserRegister;

namespace RegisterService.Web;

public class RegisterController(IUserService userService) : GrpcContracts.Register.RegisterService.RegisterServiceBase
{
    private readonly IUserService userService = userService;

    public override async Task<PaymentRepeated> AddPaymentCards(
        PaymentCard request,
        ServerCallContext context)
    {
        var cards = await userService.AddPaymentCard(Guid.Parse(request.UserId), 
            new Domain.PaymentCard()
            {
                Cvv = request.Cvv,
                Number = request.Number,
                ValidUntilMonth = request.ValidUntilMonth,
                ValidUntilYear = request.ValidUntilYear,
            });
        
        return new PaymentRepeated()
        {
            Cards =
            {
                cards.Select(ToResource)
            }
        };
    }

    private static PaymentCard ToResource(Domain.PaymentCard paymentCard)
    {
        return new PaymentCard()
        {
            Cvv = paymentCard.Cvv,
            Id = paymentCard.Id.ToString(),
            Number = paymentCard.Number,
            UserId = paymentCard.UserId.ToString(),
            ValidUntilMonth = paymentCard.ValidUntilMonth,
            ValidUntilYear = paymentCard.ValidUntilYear,
        };
    }

    public override async Task<DeliveryRepeated> AddDeliveryAddress(
        DeliveryAddress request,
        ServerCallContext context)
    {
        var addresses = await userService.AddDeliveryAddress(Guid.Parse(request.UserId), 
            new Domain.DeliveryAddress()
            {
                City = request.City,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode,
                Number = request.Number,
            });
        
        return new DeliveryRepeated()
        {
            Addresses = { addresses.Select(ToResource) }
        };
    }

    public override async Task<DeliveryRepeated> GetDeliveryAddresses(UserId request, ServerCallContext context)
    {
        var addresses = await userService.GetDeliveryAddresses(Guid.Parse(request.UserId_));
        return new DeliveryRepeated()
        {
            Addresses = { addresses.Select(ToResource) }
        };
    }

    private static DeliveryAddress ToResource(Domain.DeliveryAddress address)
    {
        return new DeliveryAddress()
        {
            Id = address.Id.ToString(),
            Street = address.Street,
            State = address.State,
            UserId = address.UserId.ToString(),
            ZipCode = address.ZipCode,
            City = address.City,
            Number = address.Number
        };
    }

    public override async Task<PaymentRepeated> GetPaymentOptions(UserId request, ServerCallContext context)
    {
        var cards = await userService.GetPaymentCards(Guid.Parse(request.UserId_));
        return new PaymentRepeated()
        {
            Cards =
            {
                cards.Select(ToResource)
            }
        };
    }

    public override async Task<Result> Login(UserLogin request, ServerCallContext context)
    {
        try
        {
            var (jwtToken, user) = await userService.Login(request.Email, request.Password);
            return new Result
            {
                Message = jwtToken,
                User = new UserRegister()
                {
                    Name = user.Name,
                    Id = user.Id.ToString(),
                    Cnpj = user.Cnpj,
                    Email = user.Email,
                    Role = user.Role.Value,
                    Telephone = user.MobilePhone
                }
            };
        }
        catch (Exception ex)
        {
            return new Result()
            {
                Message = $"It was not possible to log in. {ex.Message}",
                IsError = true
            };
        }
    }

    public override async Task<Result> Register(User request, ServerCallContext context)
    {
        try
        {
            var user = await userService.Register(new Domain.User()
            {
                Name = request.Name,
                Cnpj = request.Cnpj,
                Email = request.Email,
                MobilePhone = request.Telephone,
                Password = request.Password,
                Role = Role.Parse(request.Role),
            });
            return new Result
            {
                Message = $"Register Success",
                User = new UserRegister()
                {
                    Cnpj = user.Cnpj,
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    Telephone = user.MobilePhone,
                    Role = user.Role.Value,
                    Name = user.Name,
                }
            };
        }
        catch (Exception ex)
        {
            return new Result
            {
                Message = $"It was not possible to log in. {ex.Message}",
                IsError = true
            };
        }
    }
}
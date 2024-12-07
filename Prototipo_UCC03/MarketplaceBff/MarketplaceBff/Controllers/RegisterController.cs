using MarketplaceBff.Controllers.Resources;
using MarketplaceBff.Domain;
using MarketplaceBff.Integration;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBff.Controllers;

[ApiController]
[Route("api")]
public class RegisterController(IRegisterService registerService) : ControllerBase
{
    [HttpPost("auth/login")]
    public async Task<ActionResult<User>> Login([FromBody] UserLoginRequestResource user)
    {
        try
        {
            var loggedUser = await registerService.Login(user.Email, user.Password);
            return loggedUser;
        }
        catch (Exception ex)
        {
            return new ObjectResult(ex.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
    
    [HttpPost("auth/register")]
    public async Task<ActionResult<User>> Register([FromBody] User user)
    {
        try
        {
            var registeredUser = await registerService.Register(user);
            return registeredUser;
        }
        catch (Exception ex)
        {
            return new ObjectResult(ex.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [HttpPut("{userId}/addresses")]
    public async Task<IList<DeliveryAddress>> AddAddress(string userId, [FromBody] DeliveryAddress address)
    {
        try
        {
            var addresses = await registerService.AddDeliveryAddress(Guid.Parse(userId), address);
            return addresses;
        }
        catch (Exception ex)
        {
            return [];
        }
    }
    
    [HttpGet("{userId}/addresses")]
    public async Task<IList<DeliveryAddress>> GetAddresses(string userId)
    {
        try
        {
            var addresses = await registerService.GetDeliveryAddresses(Guid.Parse(userId));
            return addresses;
        }
        catch (Exception ex)
        {
            return [];
        }
    }
    
    [HttpPut("{userId}/cards")]
    public async Task<IList<PaymentCard>> AddCard(string userId, [FromBody] PaymentCard card)
    {
        try
        {
            var cards = await registerService.AddPaymentCard(Guid.Parse(userId), card);
            return cards;
        }
        catch (Exception ex)
        {
            return [];
        }
    }
    
    [HttpGet("{userId}/cards")]
    public async Task<IList<PaymentCard>> GetCards(string userId)
    {
        try
        {
            var cards = await registerService.GetCardOptions(Guid.Parse(userId));
            return cards;
        }
        catch (Exception ex)
        {
            return [];
        }
    }
}
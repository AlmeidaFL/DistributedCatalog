using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RegisterService.Domain;
using RegisterService.Persistence;

namespace RegisterService.Application.Services;

public class UserService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings) : IUserService
{
    public async Task<(string jwtToken, User user)> Login(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user == null)
        {
            throw new Exception("This user is not registered");
        }
        
        // var incomingPassword = EncryptionUtil.Decrypt(password);
        // var persistedPassword = EncryptionUtil.Decrypt(user.Password);
        if (password != user.Password)
        {
            throw new Exception("Invalid password");
        }

        return (GenerateJwtToken(email, user.Role.Value), user);
    }

    private async Task<User> GetUserByEmail(string email)
    {
        return await userRepository.GetUserByEmail(email) ?? throw new Exception("This user is not registered");
    }

    public async Task<User> Register(User user)
    {
        var persistedUser = await userRepository.GetUserByEmail(user.Email);

        if (persistedUser?.Email == user.Email)
        {
            throw new Exception("This user is already registered");
        }
        
        await userRepository.AddUser(user);
        return await userRepository.GetUserByEmail(user.Email);
    }

    public async Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId)
    {
        return await userRepository.GetDeliveryAddresses(userId);
    }

    public async Task<IList<PaymentCard>> GetPaymentCards(Guid userId)
    {
        return await userRepository.GetPaymentCards(userId);
    }

    public async Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address)
    {
        try
        {
            return await userRepository.AddDeliveryAddress(userId, address);
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard card)
    {
        try
        {
            return await userRepository.AddPaymentCard(userId, card);
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    private string GenerateJwtToken(string userEmail, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userEmail),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSettings.Value.Issuer,
            audience: jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public interface IUserService
{
    Task<(string jwtToken, User user)>  Login(string email, string password);
    Task<User> Register(User user);
    public Task<IList<DeliveryAddress>> GetDeliveryAddresses(Guid userId);
    public Task<IList<PaymentCard>> GetPaymentCards(Guid userId);
    
    public Task<IList<DeliveryAddress>> AddDeliveryAddress(Guid userId, DeliveryAddress address);
    public Task<IList<PaymentCard>> AddPaymentCard(Guid userId, PaymentCard card);
}
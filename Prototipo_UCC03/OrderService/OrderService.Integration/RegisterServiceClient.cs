using GrpcContracts.Register;
using OrderService.Core;

namespace OrderService.Integration;

public class RegisterServiceClient(GrpcContracts.Register.RegisterService.RegisterServiceClient client)
{
    private readonly RegisterService.RegisterServiceClient client = client;
    
    public async Task<Address?> GetAddressById(Guid customerId, string id)
    {
        var addresses = await client.GetDeliveryAddressesAsync(new UserId()
        {
            UserId_ = customerId.ToString()
        });
        
        var foundAddress = addresses.Addresses.FirstOrDefault(x => x.Id == id);
        if (foundAddress == null)
        {
            return null;
        }

        return new Address()
        {
            City = foundAddress.City,
            Number = foundAddress.Number,
            Street = foundAddress.Street,
            ZipCode = foundAddress.ZipCode,
            State = foundAddress.State,
        };
    }
}
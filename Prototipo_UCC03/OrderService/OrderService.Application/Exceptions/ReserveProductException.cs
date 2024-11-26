namespace OrderService.Application.Exceptions;

public class ReserveProductException : Exception
{
    public Guid CustomerId { get; internal set; }
    
    public ReserveProductException(Guid customerId, string message) : base(message)
    {
        CustomerId = customerId;
    }
}
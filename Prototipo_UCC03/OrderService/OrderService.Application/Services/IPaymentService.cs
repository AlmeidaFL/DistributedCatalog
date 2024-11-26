using OrderService.Persistence;

namespace OrderService.Application.Services;

public interface IPaymentService
{
    public Task ProcessPayment(Order order);
    public Task CancelPayment(Guid paymentId);
}
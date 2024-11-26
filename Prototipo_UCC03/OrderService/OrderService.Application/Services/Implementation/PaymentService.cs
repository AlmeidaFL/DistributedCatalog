using MassTransit;
using OrderService.Application.Saga.Events;
using OrderService.Persistence;

namespace OrderService.Application.Services.Implementation;

public class PaymentService() : IConsumer<OrderPersisted>
{
    public async Task Consume(ConsumeContext<OrderPersisted> context)
    {
        // context.Send(new PaymentRejected()
        // {
        //     Reason = "Payment cancelled",
        // });
        //
        // await Task.Delay(TimeSpan.FromSeconds(16));
        
        await context.Send(new PaymentProcessed()
        {
            PaymentId = Guid.NewGuid(),
            OrderId = context.Message.CorrelationId,
            CorrelationId = context.Message.CorrelationId,
        });
    }
}
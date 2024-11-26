using MassTransit;
using OrderService.Application.Saga.Events;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Services.Implementation;

public class ShipmentService : IConsumer<PaymentProcessedExternal>
{
    public static double GetShipmentCost() => 15;
    public Task<ShippingInformation> GetShippingInformation(Order order)
    {
        return Task.FromResult(new ShippingInformation()
        {
            Cost = new Random().Next(10, 15) * 5
        });
    }
    

    public async Task Consume(ConsumeContext<PaymentProcessedExternal> context)
    {
        // if (new Random().Next() % 3 == 0)
        // {
        //     eventPublisher.Publish(new ShipmentNotCreated()
        //     {
        //         OrderId = Guid.NewGuid(),
        //     });
        // }
        
        // context.Send(new ShipmentCancelled()
        // {
        //     OrderId = orderId
        // });
        
        // Simulate service process
        // await Task.Delay(TimeSpan.FromSeconds(16));
        
        await context.Send(new ShipmentCreated()
        {
            ShipmentId = Guid.NewGuid(),
            OrderId = context.Message.OrderId,
            CorrelationId = context.Message.CorrelationId,
        });
    }
}
using MassTransit;
using OrderService.Application.Exceptions;
using OrderService.Application.Saga.Events;
using OrderService.Application.Services;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Saga;

public class OrderSaga : MassTransitStateMachine<OrderState>
{
    private readonly IOrderService orderService;
    
    public State WaitingForOrderConfirmation { get; private set; }
    public State PaymentPending { get; private set; }
    public State PaymentApproved { get; private set; }
    public State Approved { get; private set; }
    public State Rejected { get; private set; }
    public State ProductReservationFailed { get; private set; }
    
    public Event<PreOrderReceived> PreOrderReceived { get; private set; }
    public Event<OrderCreated> ReceivedCreateOrder { get; private set; }
    public Event<PaymentProcessed> PaymentProcessed { get; private set; }
    public Event<PaymentRejected> PaymentRejected { get; private set; }
    public Event<PaymentCancelled> PaymentCancelled { get; private set; }
    public Event<ShipmentCreated> ShipmentCreated { get; private set; }
    public Event<ShipmentCancelled> ShipmentCancelled { get; private set; }
    public Event<OrderCancelled> OrderCancelled { get; private set; }
    public Event<OrderPersisted> OrderPersisted { get; private set; }
    public Event<PaymentProcessedExternal> PaymentProcessedExternal { get; private set; }


    public OrderSaga(IOrderService orderService)
    {
        this.orderService = orderService;
        
        InstanceState(x => x.CurrentState);

        Event(() => PreOrderReceived, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => ReceivedCreateOrder, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PaymentProcessed, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PaymentRejected, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PaymentCancelled, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => ShipmentCreated, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => ShipmentCancelled, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => OrderCancelled, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => OrderPersisted, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PaymentProcessedExternal, x => x.CorrelateById(m => m.Message.CorrelationId));
        
        Initially(
            When(PreOrderReceived)
                .ThenAsync(async context =>
                {
                    context.Saga.CorrelationId = context.Message.CorrelationId;
                    context.Saga.CustomerId = Guid.Parse(context.Message.CustomerId);
                    context.Saga.CreatedAt = DateTime.UtcNow;
                    context.Saga.UpdatedAt = DateTime.UtcNow;
                    
                    await this.orderService.ReserveProducts(context.Saga.CustomerId);
                })
                .TransitionTo(WaitingForOrderConfirmation)
                .Catch<ReserveProductException>(binder => binder.ThenAsync(context =>
                {
                    Console.WriteLine("Error reserving products");
                    return Task.CompletedTask;
                }))
                .Catch<Exception>(binder => binder.ThenAsync(context =>
                {
                    Console.WriteLine("Unexpected error");
                    return Task.CompletedTask;
                }))
        );

        During(WaitingForOrderConfirmation,
            When(ReceivedCreateOrder)
                .ThenAsync(async context =>
                {
                    context.Saga.DeliveryAddressId = context.Message.DeliveryAddressId;
                    context.Saga.PaymentOptionId = context.Message.PaymentOptionId;
                    context.Saga.OrderId = await this.orderService.CreateOrder(
                        new Customer()
                        {
                            Id = context.Saga.CustomerId,
                            PaymentOptionId = context.Saga.PaymentOptionId,
                            AddressId = context.Saga.DeliveryAddressId,
                        });
                    context.Saga.UpdatedAt = DateTime.UtcNow;
                })
                .TransitionTo(PaymentPending)
                // Will be used by PaymentService to publish event
                .Publish(context => new OrderPersisted()
                {
                    OrderId = context.Saga.OrderId,
                    CorrelationId = context.Saga.CorrelationId,
                })
                .Catch<AddressNotFoundException>(binder => binder.ThenAsync(async context =>
                    {
                        await this.orderService.ReserveProducts(context.Saga.CustomerId);
                        Console.WriteLine("Customer Address not found");
                    })
                    .TransitionTo(Rejected))
                .Catch<LackOfProductOnCartException>(binder => binder.ThenAsync(async context =>
                    {
                        await this.orderService.UnreserveProducts(context.Saga.CustomerId);
                        Console.WriteLine("Error on lack of products");
                    })
                    .TransitionTo(Rejected)));

        
        During(PaymentPending,
            When(PaymentProcessed) // Sent By PaymentService
                .ThenAsync(async p =>
                {
                    await this.orderService.UpdateOrderStatus(
                        p.Message.OrderId, 
                        OrderStatus.ProcessingShipping.ToString());
                    p.Saga.PaymentId = p.Message.PaymentId;
                    p.Saga.UpdatedAt = DateTime.UtcNow;
                })
                .TransitionTo(PaymentApproved)
                // Will be used to notify ShipmentService to publish event ShipmentCreated or ShipmentCancelled
                .Publish(context => new PaymentProcessedExternal()
                {
                    OrderId = context.Saga.OrderId,
                    CorrelationId = context.Saga.CorrelationId,
                }),
            When(PaymentRejected) // Sent By PaymentService
                .ThenAsync(async p =>
                {
                    await this.orderService.CancelOrder(p.Message.OrderId);
                    await this.orderService.UnreserveProducts(p.Saga.CustomerId);
                    p.Saga.UpdatedAt = DateTime.UtcNow;
                })
                .TransitionTo(Rejected));
        
        During(PaymentApproved,
            When(ShipmentCreated)
                .ThenAsync(async p =>
                {
                    await this.orderService.UpdateOrderStatus(
                        p.Message.OrderId,
                        OrderStatus.ShipmentWillSendProduct.ToString());
                    p.Saga.ShipmentId = p.Message.ShipmentId;
                    p.Saga.UpdatedAt = DateTime.UtcNow;
                })
                .TransitionTo(Approved)
                .Finalize(),
            When(ShipmentCancelled)
                .ThenAsync(async p =>
                {
                    await this.orderService.CancelOrder(p.Saga.OrderId);
                    await this.orderService.UnreserveProducts(p.Saga.CustomerId);
                    p.Saga.UpdatedAt = DateTime.UtcNow;
                    Console.WriteLine("Cancelling Payment");
                })
                .TransitionTo(Rejected)
                .Finalize());
        
        
        SetCompletedWhenFinalized();
    }
}


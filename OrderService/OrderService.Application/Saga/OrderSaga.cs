using MassTransit;
using OrderService.Application.Saga.Events;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Saga;

public class OrderSaga : MassTransitStateMachine<OrderState>
{
    public State Pending { get; private set; }
    public State Approved { get; private set; }
    public State Rejected { get; private set; }

    public Event<IOrderCreated> OrderCreated { get; private set; }
    public Event<IPaymentProcessed> PaymentProcessed { get; private set; }

    public OrderSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreated, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => PaymentProcessed, x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(OrderCreated)
                .Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.Products = new List<Product>(context.Data.Products);
                })
                .TransitionTo(Pending)
        );

        During(Pending,
            When(PaymentProcessed)
                .Then(context => Console.WriteLine($"Pedido {context.Instance.OrderId} pago com sucesso"))
                .TransitionTo(Approved));
    }
}


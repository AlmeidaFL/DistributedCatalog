using CatalogBff.Domain;
using CatalogBff.Integration;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Saga.Events;

namespace CatalogBff.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(
    IOrderService orderService,
    IPublishEndpoint publishEndpoint) : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpPost("pre-order/{customerId}")]
    public async Task<IActionResult> StartPreOrder(string customerId)
    {
        var correlationId = Guid.NewGuid();

        var preOrderEvent = new PreOrderReceived()
        {
            CorrelationId = correlationId,
            CustomerId = customerId
        };

        await _publishEndpoint.Publish(preOrderEvent);

        return Accepted(new { CorrelationId = correlationId });
    }
    
    [HttpPost("{correlationId}")]
    public async Task<IActionResult> Order(string correlationId)
    {
        var preOrderEvent = new OrderCreated()
        {
            CorrelationId = Guid.Parse(correlationId),
        };

        await _publishEndpoint.Publish(preOrderEvent);

        return Accepted();
    }

    [HttpGet("{customerId}")]
    public async Task<IList<Order>> GetOrder(string customerId)
    {
        try
        {
            return await orderService.GetOrdersByCustomerId(Guid.Parse(customerId));
        }
        catch (Exception ex)
        {
            return [];
        }
    }
}
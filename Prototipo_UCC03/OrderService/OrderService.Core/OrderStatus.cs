namespace OrderService.Core;

public class OrderStatus
{
    public static readonly OrderStatus Pending = new("Pending");
    public static readonly OrderStatus Cancelled = new("Cancelled");
    public static readonly OrderStatus ProcessingShipping = new("Processing Shipping");
    public static readonly OrderStatus ShipmentWillSendProduct = new("Shipment service will send product");
    private string Name { get; set; }

    internal OrderStatus(string name)
    {
        this.Name = name;
    }
    
    public override string ToString() => Name;
}
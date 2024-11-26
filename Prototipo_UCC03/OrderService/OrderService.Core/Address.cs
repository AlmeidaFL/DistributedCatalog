using Microsoft.EntityFrameworkCore;

namespace OrderService.Core;

public record Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Number { get; set; }
};
namespace MarketplaceBff.Domain;

public class User
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? Cnpj { get; set; }
    public string? Role { get; set; }
}
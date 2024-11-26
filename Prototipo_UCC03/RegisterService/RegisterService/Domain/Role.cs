namespace RegisterService.Domain;

public record Role
{
    public static Role Vendor = new("Vendor");
    public static Role Customer = new("Customer");
    public static Role Admin = new("Admin");
    public string Value { get; set; }

    public static Role[] allRoles = { Vendor, Customer, Admin };
    
    public Role(string value)
    {
        this.Value = value;
    }

    public static Role Parse(string value)
    {
        return allRoles.First(r => string.Equals(value, r.Value, StringComparison.InvariantCultureIgnoreCase));
    }
    
    public override string ToString() => Value;
}
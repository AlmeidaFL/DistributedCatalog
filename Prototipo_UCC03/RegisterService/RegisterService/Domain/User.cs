using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterService.Domain;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string MobilePhone { get; set; }
    [Required]
    public string Cnpj { get; set; }
    [Required]
    public Role Role { get; set; }
    public IList<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();
    public IList<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();
}
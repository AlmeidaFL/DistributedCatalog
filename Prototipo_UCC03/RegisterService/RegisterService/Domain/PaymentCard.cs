using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterService.Domain;

public record PaymentCard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    public string Number { get; init; }
    public string Cvv { get; init; }
    public int ValidUntilYear { get; init; }
    public int ValidUntilMonth { get; init; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
};
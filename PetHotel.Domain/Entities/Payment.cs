namespace PetHotel.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; } = default!;
    public DateTime PaymentDate { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? OwnerId { get; set; }
    public Owner? Owner { get; set; }
}

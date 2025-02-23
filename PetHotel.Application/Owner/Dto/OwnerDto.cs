using PetHotel.Application.Animal.Dto;
using PetHotel.Application.Payment.Dto;
using PetHotel.Domain.Entities;

namespace PetHotel.Application.Owner.Dto;

public class OwnerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<AnimalDto> Animals { get; set; } = [];
    public List<PaymentDto> Payments { get; set; } = [];
}

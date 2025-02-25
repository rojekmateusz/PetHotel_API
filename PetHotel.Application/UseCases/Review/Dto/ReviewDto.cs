using PetHotel.Domain.Entities;

namespace PetHotel.Application.UseCases.Review.Dto;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}

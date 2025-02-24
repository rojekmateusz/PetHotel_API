using MediatR;

namespace PetHotel.Application.Animal.Command.CreateAnimal;

public class CreateAnimalCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Spiecies { get; set; } = default!;
    public string Breed { get; set; } = default!;
    public int? Age { get; set; }
    public decimal? Weight { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int OwnerID { get; set; }

}
